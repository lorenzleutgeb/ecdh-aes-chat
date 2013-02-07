using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using LeutgebAes.EllipticCurve;
using System.Threading;
using System.IO;

namespace LeutgebAes
{
    public partial class ChatForm : Form
    {
        private SecureSocket socket = new SecureSocket(Tools.RandomBigInteger(16), X9ECParameters.GetByName("secp192r1"));

        public ChatForm()
        {
            InitializeComponent();

            while (true)
            {
                try
                {
                    socket.Bind(new IPEndPoint(IPAddress.Any, Tools.GetUnsafeRandomInt(1024, 65535)));
                    Text = socket.LocalEndPoint.ToString();
                    socket.Listen(16);
                    Thread acceptor = new Thread(Accept);
                    acceptor.IsBackground = true;
                    acceptor.Start(socket);
                    Thread listener = new Thread(Listen);
                    listener.IsBackground = true;
                    listener.Start();
                    break;
                }
                catch (SocketException)
                {
                }
            }
        }

        private void Accept(object context)
        {
            SecureSocket socket = (SecureSocket)context;
            while (true)
            {
                Socket client = socket.Accept();

                Invoke((Action)delegate
                {
                    if (textBox1.Visible)
                        textBox1.Visible = false;

                    this.tabControl.Controls.Add(new ChatTab(socket.Cipher, new PKCSPaddedStream(socket.Cipher, new NetworkStream(client)), client));
                });
            }
        }

        private void Listen()
        {
            while (true) foreach (TabPage page in tabControl.TabPages)
            {
                var tab = page as ChatTab;
                if (tab != null) {
                    if (tab.Stream.CanRead)
                    {
                        byte[] buffer = new byte[16];

                        try
                        {
                            if (tab.Stream.Read(buffer, 0, 16) < 1)
                                continue;
                        }
                        catch (IOException)
                        {
                            tab.Close();
                        }

                        tab.Cipher.Decrypt(buffer);

                        Invoke((Action)delegate
                        {
                            tab.TextBox.Text += "Remote: " + UTF8Encoding.UTF8.GetString(buffer);
                            tab.TextBox.Text += "\r\n";
                        });
                    }
                }
            }
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                return;

            if (textBox.Text[0] == '/')
            {
                var command = textBox.Text.Split(' ');

                switch (command[0])
                {
                    case "/open":
                        SecureSocket client = new SecureSocket();
                        var ep = command[1].Split(':');
                        client.Connect(new IPEndPoint(IPAddress.Parse(ep[0]), Convert.ToInt32(ep[1])));
                        Invoke((Action)delegate
                        {
                            if (textBox1.Visible)
                                textBox1.Visible = false;

                            this.tabControl.Controls.Add(new ChatTab(client.Cipher, new PKCSPaddedStream(client.Cipher, new NetworkStream(client)), client));
                        });
                        break;
                    case "/close":
                        CloseCurrent();
                        break;
                    case "/exit":
                        while (tabControl.TabPages.Count != 0)
                            CloseCurrent();
                        Close();
                        break;
                }
            }
            else
            {
                var buffer = UTF8Encoding.UTF8.GetBytes(textBox.Text);
                var tab = tabControl.SelectedTab as ChatTab;

                if (tab == null)
                    return;

                try
                {
                    tab.Stream.Write(buffer, 0, buffer.Length);
                }
                catch
                {
                    tab.Close();
                }
                Invoke((Action)delegate
                {
                    tab.TextBox.Text += "Local: " + UTF8Encoding.UTF8.GetString(buffer) + "\r\n";
                });
            }
            textBox.Text = "";
        }

        private void CloseCurrent()
        {
            var tab = tabControl.SelectedTab as ChatTab;

            if (tab == null)
                return;

            tab.Close();
            tabControl.TabPages.Remove(tabControl.SelectedTab);
        }
    }
}
