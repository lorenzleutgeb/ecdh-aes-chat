namespace LeutgebAes
{
    partial class ToolForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolForm));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonSelectMessage = new System.Windows.Forms.Button();
            this.buttonSelectCipher = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonDecrypt = new System.Windows.Forms.RadioButton();
            this.radioButtonEncrypt = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.textBoxIv = new System.Windows.Forms.TextBox();
            this.labelMessage = new System.Windows.Forms.Label();
            this.labelCipher = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButtonCBC = new System.Windows.Forms.RadioButton();
            this.radioButtonECB = new System.Windows.Forms.RadioButton();
            this.radioButtonCFB = new System.Windows.Forms.RadioButton();
            this.radioButtonCTR = new System.Windows.Forms.RadioButton();
            this.buttonRandomizeKey = new System.Windows.Forms.Button();
            this.buttonRandomizeIv = new System.Windows.Forms.Button();
            this.buttonBlockEncrypt = new System.Windows.Forms.Button();
            this.textBoxBlockKey = new System.Windows.Forms.TextBox();
            this.textBoxBlock = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonBlockDecrypt = new System.Windows.Forms.Button();
            this.textBoxBlockOutput = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.FileName = "message.txt";
            this.openFileDialog.Filter = "Alle Dateien|*.*|Textdateien|*.txt";
            this.openFileDialog.Title = "Klartext öffnen ...";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "txt";
            this.saveFileDialog.FileName = "cipher.txt";
            this.saveFileDialog.Filter = "Alle Dateien|*.*|Textdateien|*.txt";
            this.saveFileDialog.Title = "Geheimtext speichern unter ...";
            // 
            // buttonSelectMessage
            // 
            this.buttonSelectMessage.Location = new System.Drawing.Point(120, 29);
            this.buttonSelectMessage.Name = "buttonSelectMessage";
            this.buttonSelectMessage.Size = new System.Drawing.Size(130, 23);
            this.buttonSelectMessage.TabIndex = 0;
            this.buttonSelectMessage.Text = "Klartext auswählen";
            this.buttonSelectMessage.UseVisualStyleBackColor = true;
            this.buttonSelectMessage.Click += new System.EventHandler(this.buttonSelectFileClick);
            // 
            // buttonSelectCipher
            // 
            this.buttonSelectCipher.Location = new System.Drawing.Point(120, 58);
            this.buttonSelectCipher.Name = "buttonSelectCipher";
            this.buttonSelectCipher.Size = new System.Drawing.Size(130, 23);
            this.buttonSelectCipher.TabIndex = 1;
            this.buttonSelectCipher.Text = "Geheimtext auswählen";
            this.buttonSelectCipher.UseVisualStyleBackColor = true;
            this.buttonSelectCipher.Click += new System.EventHandler(this.buttonSelectFileClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonDecrypt);
            this.groupBox1.Controls.Add(this.radioButtonEncrypt);
            this.groupBox1.Location = new System.Drawing.Point(6, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(108, 64);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Operation";
            // 
            // radioButtonDecrypt
            // 
            this.radioButtonDecrypt.AutoSize = true;
            this.radioButtonDecrypt.Location = new System.Drawing.Point(6, 42);
            this.radioButtonDecrypt.Name = "radioButtonDecrypt";
            this.radioButtonDecrypt.Size = new System.Drawing.Size(90, 17);
            this.radioButtonDecrypt.TabIndex = 1;
            this.radioButtonDecrypt.TabStop = true;
            this.radioButtonDecrypt.Text = "Entschlüsseln";
            this.radioButtonDecrypt.UseVisualStyleBackColor = true;
            this.radioButtonDecrypt.CheckedChanged += new System.EventHandler(this.operationChange);
            // 
            // radioButtonEncrypt
            // 
            this.radioButtonEncrypt.AutoSize = true;
            this.radioButtonEncrypt.Checked = true;
            this.radioButtonEncrypt.Location = new System.Drawing.Point(6, 19);
            this.radioButtonEncrypt.Name = "radioButtonEncrypt";
            this.radioButtonEncrypt.Size = new System.Drawing.Size(90, 17);
            this.radioButtonEncrypt.TabIndex = 0;
            this.radioButtonEncrypt.TabStop = true;
            this.radioButtonEncrypt.Text = "Verschlüsseln";
            this.radioButtonEncrypt.UseVisualStyleBackColor = true;
            this.radioButtonEncrypt.CheckedChanged += new System.EventHandler(this.operationChange);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Schlüssel:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Initialisierungsvektor:";
            // 
            // textBoxKey
            // 
            this.textBoxKey.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxKey.ForeColor = System.Drawing.Color.Green;
            this.textBoxKey.Location = new System.Drawing.Point(190, 97);
            this.textBoxKey.MaxLength = 32;
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(216, 20);
            this.textBoxKey.TabIndex = 5;
            this.textBoxKey.Text = "12345678901234567890123456789012";
            this.textBoxKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxKey.TextChanged += new System.EventHandler(this.byteInputTextChanged);
            // 
            // textBoxIv
            // 
            this.textBoxIv.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIv.ForeColor = System.Drawing.Color.Green;
            this.textBoxIv.Location = new System.Drawing.Point(190, 122);
            this.textBoxIv.MaxLength = 32;
            this.textBoxIv.Name = "textBoxIv";
            this.textBoxIv.Size = new System.Drawing.Size(216, 20);
            this.textBoxIv.TabIndex = 6;
            this.textBoxIv.Text = "12345678901234567890123456789012";
            this.textBoxIv.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelMessage
            // 
            this.labelMessage.AutoSize = true;
            this.labelMessage.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMessage.Location = new System.Drawing.Point(256, 34);
            this.labelMessage.Name = "labelMessage";
            this.labelMessage.Size = new System.Drawing.Size(13, 13);
            this.labelMessage.TabIndex = 7;
            this.labelMessage.Text = "?";
            // 
            // labelCipher
            // 
            this.labelCipher.AutoSize = true;
            this.labelCipher.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCipher.Location = new System.Drawing.Point(256, 63);
            this.labelCipher.Name = "labelCipher";
            this.labelCipher.Size = new System.Drawing.Size(13, 13);
            this.labelCipher.TabIndex = 8;
            this.labelCipher.Text = "?";
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(229, 148);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStartClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButtonCBC);
            this.groupBox2.Controls.Add(this.radioButtonECB);
            this.groupBox2.Controls.Add(this.radioButtonCFB);
            this.groupBox2.Controls.Add(this.radioButtonCTR);
            this.groupBox2.Location = new System.Drawing.Point(6, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(58, 114);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Modus";
            // 
            // radioButtonCBC
            // 
            this.radioButtonCBC.AutoSize = true;
            this.radioButtonCBC.Enabled = false;
            this.radioButtonCBC.Location = new System.Drawing.Point(6, 88);
            this.radioButtonCBC.Name = "radioButtonCBC";
            this.radioButtonCBC.Size = new System.Drawing.Size(46, 17);
            this.radioButtonCBC.TabIndex = 3;
            this.radioButtonCBC.Text = "CBC";
            this.radioButtonCBC.UseVisualStyleBackColor = true;
            // 
            // radioButtonECB
            // 
            this.radioButtonECB.AutoSize = true;
            this.radioButtonECB.Location = new System.Drawing.Point(6, 65);
            this.radioButtonECB.Name = "radioButtonECB";
            this.radioButtonECB.Size = new System.Drawing.Size(46, 17);
            this.radioButtonECB.TabIndex = 2;
            this.radioButtonECB.Text = "ECB";
            this.radioButtonECB.UseVisualStyleBackColor = true;
            // 
            // radioButtonCFB
            // 
            this.radioButtonCFB.AutoSize = true;
            this.radioButtonCFB.Enabled = false;
            this.radioButtonCFB.Location = new System.Drawing.Point(6, 42);
            this.radioButtonCFB.Name = "radioButtonCFB";
            this.radioButtonCFB.Size = new System.Drawing.Size(45, 17);
            this.radioButtonCFB.TabIndex = 1;
            this.radioButtonCFB.Text = "CFB";
            this.radioButtonCFB.UseVisualStyleBackColor = true;
            // 
            // radioButtonCTR
            // 
            this.radioButtonCTR.AutoSize = true;
            this.radioButtonCTR.Checked = true;
            this.radioButtonCTR.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCTR.Name = "radioButtonCTR";
            this.radioButtonCTR.Size = new System.Drawing.Size(47, 17);
            this.radioButtonCTR.TabIndex = 0;
            this.radioButtonCTR.TabStop = true;
            this.radioButtonCTR.Text = "CTR";
            this.radioButtonCTR.UseVisualStyleBackColor = true;
            // 
            // buttonRandomizeKey
            // 
            this.buttonRandomizeKey.Location = new System.Drawing.Point(412, 94);
            this.buttonRandomizeKey.Name = "buttonRandomizeKey";
            this.buttonRandomizeKey.Size = new System.Drawing.Size(112, 23);
            this.buttonRandomizeKey.TabIndex = 11;
            this.buttonRandomizeKey.Text = "Zufällig generieren";
            this.buttonRandomizeKey.UseVisualStyleBackColor = true;
            this.buttonRandomizeKey.Click += new System.EventHandler(this.buttonRandomizeClick);
            // 
            // buttonRandomizeIv
            // 
            this.buttonRandomizeIv.Location = new System.Drawing.Point(412, 120);
            this.buttonRandomizeIv.Name = "buttonRandomizeIv";
            this.buttonRandomizeIv.Size = new System.Drawing.Size(112, 23);
            this.buttonRandomizeIv.TabIndex = 12;
            this.buttonRandomizeIv.Text = "Zufällig generieren";
            this.buttonRandomizeIv.UseVisualStyleBackColor = true;
            this.buttonRandomizeIv.Click += new System.EventHandler(this.buttonRandomizeClick);
            // 
            // buttonBlockEncrypt
            // 
            this.buttonBlockEncrypt.Location = new System.Drawing.Point(74, 70);
            this.buttonBlockEncrypt.Name = "buttonBlockEncrypt";
            this.buttonBlockEncrypt.Size = new System.Drawing.Size(104, 23);
            this.buttonBlockEncrypt.TabIndex = 17;
            this.buttonBlockEncrypt.Text = "Verschlüsseln";
            this.buttonBlockEncrypt.UseVisualStyleBackColor = true;
            this.buttonBlockEncrypt.Click += new System.EventHandler(this.buttonBlockEncryptClick);
            // 
            // textBoxBlockKey
            // 
            this.textBoxBlockKey.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBlockKey.ForeColor = System.Drawing.Color.Green;
            this.textBoxBlockKey.Location = new System.Drawing.Point(74, 44);
            this.textBoxBlockKey.MaxLength = 32;
            this.textBoxBlockKey.Name = "textBoxBlockKey";
            this.textBoxBlockKey.Size = new System.Drawing.Size(216, 20);
            this.textBoxBlockKey.TabIndex = 16;
            this.textBoxBlockKey.Text = "0f1571c947d9e8590cb7add6af7f6798";
            this.textBoxBlockKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxBlock
            // 
            this.textBoxBlock.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBlock.ForeColor = System.Drawing.Color.Green;
            this.textBoxBlock.Location = new System.Drawing.Point(74, 19);
            this.textBoxBlock.MaxLength = 32;
            this.textBoxBlock.Name = "textBoxBlock";
            this.textBoxBlock.Size = new System.Drawing.Size(216, 20);
            this.textBoxBlock.TabIndex = 15;
            this.textBoxBlock.Text = "12345678901234567890123456789012";
            this.textBoxBlock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Schlüssel:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Block:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonBlockDecrypt);
            this.groupBox3.Controls.Add(this.textBoxBlockOutput);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonBlockEncrypt);
            this.groupBox3.Controls.Add(this.textBoxBlock);
            this.groupBox3.Controls.Add(this.textBoxBlockKey);
            this.groupBox3.Location = new System.Drawing.Point(550, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(298, 125);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Single Block";
            // 
            // buttonBlockDecrypt
            // 
            this.buttonBlockDecrypt.Location = new System.Drawing.Point(184, 70);
            this.buttonBlockDecrypt.Name = "buttonBlockDecrypt";
            this.buttonBlockDecrypt.Size = new System.Drawing.Size(106, 23);
            this.buttonBlockDecrypt.TabIndex = 21;
            this.buttonBlockDecrypt.Text = "Entschlüsseln";
            this.buttonBlockDecrypt.UseVisualStyleBackColor = true;
            this.buttonBlockDecrypt.Click += new System.EventHandler(this.buttonBlockDecryptClick);
            // 
            // textBoxBlockOutput
            // 
            this.textBoxBlockOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBlockOutput.ForeColor = System.Drawing.Color.Green;
            this.textBoxBlockOutput.Location = new System.Drawing.Point(74, 99);
            this.textBoxBlockOutput.MaxLength = 32;
            this.textBoxBlockOutput.Name = "textBoxBlockOutput";
            this.textBoxBlockOutput.ReadOnly = true;
            this.textBoxBlockOutput.Size = new System.Drawing.Size(216, 20);
            this.textBoxBlockOutput.TabIndex = 20;
            this.textBoxBlockOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.buttonSelectMessage);
            this.groupBox4.Controls.Add(this.buttonRandomizeIv);
            this.groupBox4.Controls.Add(this.buttonSelectCipher);
            this.groupBox4.Controls.Add(this.buttonRandomizeKey);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.buttonStart);
            this.groupBox4.Controls.Add(this.textBoxKey);
            this.groupBox4.Controls.Add(this.labelCipher);
            this.groupBox4.Controls.Add(this.textBoxIv);
            this.groupBox4.Controls.Add(this.labelMessage);
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(532, 209);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Datei";
            // 
            // LeutgebAesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 231);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LeutgebAesForm";
            this.Text = "AES GUI";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonSelectMessage;
        private System.Windows.Forms.Button buttonSelectCipher;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonDecrypt;
        private System.Windows.Forms.RadioButton radioButtonEncrypt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.TextBox textBoxIv;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.Label labelCipher;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButtonECB;
        private System.Windows.Forms.RadioButton radioButtonCFB;
        private System.Windows.Forms.RadioButton radioButtonCTR;
        private System.Windows.Forms.Button buttonRandomizeKey;
        private System.Windows.Forms.Button buttonRandomizeIv;
        private System.Windows.Forms.RadioButton radioButtonCBC;
        private System.Windows.Forms.Button buttonBlockEncrypt;
        private System.Windows.Forms.TextBox textBoxBlockKey;
        private System.Windows.Forms.TextBox textBoxBlock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button buttonBlockDecrypt;
        private System.Windows.Forms.TextBox textBoxBlockOutput;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}