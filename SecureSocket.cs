using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using LeutgebAes.EllipticCurve;

namespace LeutgebAes
{
    class SecureSocket : Socket
    {
        private X9ECParameters parameters;
        private BigInteger key;
        private Point secret;
        private Rijndael cipher;
        private byte[] initializationVector = new byte[16];
        
        public SecureSocket()
            : this(Tools.RandomBigInteger(64))
        {
        }

        public SecureSocket(BigInteger key)
            : this(key, X9ECParameters.Dummy)
        {
        }

        public SecureSocket(BigInteger key, AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
            : this(key, X9ECParameters.Dummy, addressFamily, socketType, protocolType)
        {
        }

        public SecureSocket(BigInteger key, X9ECParameters parameters)
            : this(key, parameters, AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {
        }

        public SecureSocket(BigInteger key, X9ECParameters parameters, AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
            : base(addressFamily, socketType, protocolType)
        {
            this.key = key;
            this.parameters = parameters;
        }

        public Rijndael Cipher
        {
            get
            {
                return cipher;
            }
        }

        public byte[] InitializationVector
        {
            get { return this.initializationVector; }
        }

        public new Socket Accept()
        {
            Socket client = base.Accept();
            SendString(client, parameters.Name);
            SendPoint(client, parameters.G * key);
            secret = ReceivePoint(client) * key;
            byte[] tmp = secret.X.ToBigInteger().ToByteArray();
            Array.Resize<byte>(ref tmp, 16);
            cipher = new Rijndael(tmp);
            initializationVector = secret.Y.ToBigInteger().ToByteArray();
            Console.WriteLine(secret);
            return client;
        }
        
        public new void Connect(EndPoint remoteEP)
        {
            base.Connect(remoteEP);
            parameters = X9ECParameters.GetByName(ReceiveString(this));

            if (parameters == null)
            {
                base.Disconnect(false);
                return;
            }

            SendPoint(this, parameters.G * key);
            secret = ReceivePoint(this) * key;
            byte[] tmp = secret.X.ToBigInteger().ToByteArray();
            Array.Resize<byte>(ref tmp, 16);
            cipher = new Rijndael(tmp);
            initializationVector = secret.Y.ToBigInteger().ToByteArray();
            Console.WriteLine(secret);
        }

        public new void Connect(IPAddress address, int port)
        {
            this.Connect(new IPEndPoint(address, port));
        }

        public new void Connect(string host, int port)
        {
            this.Connect(new DnsEndPoint(host, port));
        }

        public new void Connect(IPAddress[] addresses, int port)
        {
            throw new NotImplementedException();
        }

        private void SendString(Socket socket, string s)
        {
            byte[] buffer = new byte[4 + s.Length];
            Array.Copy(BitConverter.GetBytes(s.Length), buffer, 4);
            Array.Copy(UTF8Encoding.UTF8.GetBytes(s), 0, buffer, 4, s.Length);
            Send(socket, buffer);
        }

        private string ReceiveString(Socket socket) {
            return UTF8Encoding.UTF8.GetString(Receive(socket, BitConverter.ToInt32(Receive(socket, 4), 0)));
        }

        private void SendPoint(Socket socket, Point p)
        {
            byte[] x = p.X.Value.ToByteArray(), y = p.Y.Value.ToByteArray();
            byte[] buffer = new byte[8 + x.Length + y.Length];
            Array.Copy(BitConverter.GetBytes(x.Length), 0, buffer, 0, 4);
            Array.Copy(x, 0, buffer, 4, x.Length);
            Array.Copy(BitConverter.GetBytes(y.Length), 0, buffer, 4 + x.Length, 4);
            Array.Copy(y, 0, buffer, 8 + x.Length, y.Length);
            Send(socket, buffer);
        }

        private Point ReceivePoint(Socket socket)
        {
            return new Point(parameters.Curve,
                parameters.Curve.GenerateFieldElement(new BigInteger(Receive(socket, BitConverter.ToInt32(Receive(socket, 4), 0)))),
                parameters.Curve.GenerateFieldElement(new BigInteger(Receive(socket, BitConverter.ToInt32(Receive(socket, 4), 0))))
            );
        }

        private void Send(Socket socket, byte[] buffer, SocketFlags flags = SocketFlags.None)
        {
            if (socket == this)
                base.Send(buffer, flags);
            else
                socket.Send(buffer, flags);
        }

        private byte[] Receive(Socket socket, int length)
        {
            byte[] buffer = new byte[length];
            if (socket == this)
                base.Receive(buffer);
            else
                socket.Receive(buffer);
            return buffer;
        }

        /*public void Send(byte[] buffer, int offset, int size, SocketFlags flags = SocketFlags.None)
        {
            byte[] tmp = new byte[16];
            if (size == 16)
            {
                Array.Copy(buffer, offset, tmp, 0, size);
                EncryptAndSend(tmp, flags);
            }
            else if (size > 16)
            {
                for (int i = 0; i < size / 16; i++)
                    Send(buffer, offset + i * 16, 16, flags);

                Send(buffer, (buffer.Length / 16) * 16, buffer.Length % 16, flags);
            }
            else
            {
                byte padding = (byte)(16 - size % 16);
                Array.Copy(buffer, offset, tmp, 0, size);

                for (int i = size; i < 16; i++)
                    tmp[i] = padding;

                EncryptAndSend(tmp, flags);
            }
        }

        public void Send(byte[] buffer, int size, SocketFlags flags = SocketFlags.None)
        {
            Send(buffer, 0, size, flags);
        }

        public void Send(byte[] buffer, SocketFlags flags = SocketFlags.None)
        {
            Send(buffer, 0, buffer.Length, flags);
        }

        private void EncryptAndSend(byte[] buffer, SocketFlags flags = SocketFlags.None)
        {
            if (buffer.Length != 16)
                throw new ArgumentException("At this stage, the buffer must be padded to 16 bytes!");

            for (int i = 0; i < 16; i++)
                buffer[i] ^= initializationVector[0][i];

            buffer = cipher.Encrypt(buffer);
            Array.Copy(buffer, initializationVector, 16);

            Send(this, buffer, flags);
        }

        public void Receive(byte[] buffer, int offset, int size, SocketFlags flags = SocketFlags.None)
        {
            byte[] chunk = new byte[16], tmp = new byte[16];
            for (int i = 0; i < size; i += 16)
            {
                base.Receive(chunk);
                Array.Copy(chunk, tmp, 16);
                chunk = cipher.Decrypt(chunk);

                for (int j = 0; j < 16; j++)
                    chunk[j] ^= initializationVector[1][j];

                Array.Copy(tmp, initializationVector[1], 16);
                Array.Copy(chunk, 0, buffer, offset + i, Math.Min(offset + i + 16, offset + size) - offset); 
            }
        }

        public void Receive(byte[] buffer, SocketFlags flags = SocketFlags.None)
        {
            Receive(buffer, 0, buffer.Length, flags);
        }

        public void Receive(byte[] buffer, int size, SocketFlags flags = SocketFlags.None)
        {
            Receive(buffer, 0, size, flags);
        }*/
    }
}
