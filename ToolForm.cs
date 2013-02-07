using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading;

namespace LeutgebAes
{
    public partial class ToolForm : Form
    {
        public ToolForm()
        {
            InitializeComponent();
            (new ChatForm()).Show();
        }

        private void buttonStartClick(object sender, EventArgs e)
        {
            if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            {
                Thread thread = new Thread(new ThreadStart(delegate { buttonStartClick(sender, e); }));
                thread.IsBackground = true;
                thread.Start();
                return;
            }

            Rijndael aes = new Rijndael(Tools.StringToBytes(textBoxKey.Text));
            Stream messageFile = new PKCSPaddedStream(aes, new FileStream(labelMessage.Text, FileMode.OpenOrCreate)),
                cipherFile = new FileStream(labelCipher.Text, FileMode.OpenOrCreate);

            IBlockCipherMode blockMode = null;

            if (radioButtonCFB.Checked)
                blockMode = new CipherFeedback(aes, Tools.StringToBytes(textBoxIv.Text));
            else if (radioButtonCTR.Checked)
                blockMode = new SegmentedIntegerCounter(aes, Tools.StringToBytes(textBoxIv.Text));
            else if (radioButtonECB.Checked)
                blockMode = new ElectronicCodeBook(aes);
            else if (radioButtonCBC.Checked)
                blockMode = new CipherBlockChaining(aes, Tools.StringToBytes(textBoxIv.Text));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long bytes;

            if (radioButtonEncrypt.Checked)
            {
                bytes = messageFile.Length;
                blockMode.Encrypt(messageFile, cipherFile);
            }
            else
            {
                bytes = cipherFile.Length;
                blockMode.Decrypt(cipherFile, messageFile);
            }

            stopwatch.Stop();

            Console.WriteLine(stopwatch.Elapsed + " (" + (stopwatch.Elapsed.Ticks / bytes) + " ticks/byte)");
            MessageBox.Show("Cryption done!", stopwatch.Elapsed + " (" + (stopwatch.Elapsed.Ticks / bytes) + " ticks/byte)");

            messageFile.Close();
            cipherFile.Close();
        }

        private void buttonSelectFileClick(object sender, EventArgs e)
        {
            FileDialog dialog = radioButtonEncrypt.Checked == (sender == buttonSelectMessage) ? openFileDialog as FileDialog : saveFileDialog as FileDialog;
            Label label = null;

            if (sender == buttonSelectMessage)
            {
                dialog.Title = "Klartext auswählen";
                dialog.FileName = "message.txt";
                label = labelMessage;
            }
            else if (sender == buttonSelectCipher)
            {
                dialog.Title = "Geheimtext auswählen";
                dialog.FileName = "cipher.txt";
                label = labelCipher;
            }
            dialog.ShowDialog();
            label.Text = dialog.FileName;
        }


        private void byteInputTextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.ForeColor = textBox.Text.Length == 32 ? Color.Green : Color.Red;
        }

        private void operationChange(object sender, EventArgs e)
        {
            labelMessage.Text = labelCipher.Text = "?";
        }

        private void buttonRandomizeClick(object sender, EventArgs e)
        {
            (sender == buttonRandomizeKey ? textBoxKey : textBoxIv).Text = Tools.ToString(Tools.RandomBytes(16));
        }

        private void buttonBlockEncryptClick(object sender, EventArgs e)
        {
            textBoxBlockOutput.Text = Tools.ToString(new Rijndael(Tools.StringToBytes(textBoxBlockKey.Text)).Encrypt(Tools.StringToBytes(textBoxBlock.Text)));
        }

        private void buttonBlockDecryptClick(object sender, EventArgs e)
        {
            textBoxBlockOutput.Text = Tools.ToString(new Rijndael(Tools.StringToBytes(textBoxBlockKey.Text)).Decrypt(Tools.StringToBytes(textBoxBlock.Text)));
        }
    }
}