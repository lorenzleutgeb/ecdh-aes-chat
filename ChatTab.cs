using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace LeutgebAes
{
    class ChatTab : TabPage
    {
        private IBlockCipher cipher;
        private PKCSPaddedStream stream;
        private TextBox textBox = new TextBox();

        public Stream Stream
        {
            get { return stream; }
        }

        public IBlockCipher Cipher
        {
            get { return this.cipher; }
        }

        public TextBox TextBox
        {
            get { return this.textBox; }
        }

        public ChatTab(IBlockCipher cipher, PKCSPaddedStream stream, Socket socket) : base()
        {
            this.cipher = cipher;
            this.stream = stream;

            Text = socket.RemoteEndPoint.ToString();

            textBox.Multiline = true;
            textBox.ReadOnly = true;
            textBox.Size = new Size(479, 399);
            textBox.Location = new Point(6, 6);
            textBox.Font = new Font("Consolas", 10);

            Controls.Add(textBox);
        }

        public void Close()
        {
            this.stream.Close();
            Controls.Clear();
        }
    }
}
