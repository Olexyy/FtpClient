namespace FtpClient
{
    partial class FtpForm
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
            this.listViewFtp = new System.Windows.Forms.ListView();
            this.columnFtpFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonUpload = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.listViewLocal = new System.Windows.Forms.ListView();
            this.columnFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxCredentials = new System.Windows.Forms.GroupBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUserName = new System.Windows.Forms.Label();
            this.labelHost = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxCwdRemote = new System.Windows.Forms.TextBox();
            this.textBoxCwdLocal = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonNewFolder = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.uttonGo = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.groupBoxCredentials.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewFtp
            // 
            this.listViewFtp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFtpFileName});
            this.listViewFtp.Location = new System.Drawing.Point(12, 12);
            this.listViewFtp.Name = "listViewFtp";
            this.listViewFtp.Size = new System.Drawing.Size(373, 223);
            this.listViewFtp.TabIndex = 0;
            this.listViewFtp.UseCompatibleStateImageBehavior = false;
            this.listViewFtp.View = System.Windows.Forms.View.Details;
            this.listViewFtp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseDoubleClick);
            // 
            // columnFtpFileName
            // 
            this.columnFtpFileName.Text = "Remote:";
            this.columnFtpFileName.Width = 346;
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(258, 51);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(97, 23);
            this.buttonDownload.TabIndex = 1;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonUpload
            // 
            this.buttonUpload.Location = new System.Drawing.Point(258, 22);
            this.buttonUpload.Name = "buttonUpload";
            this.buttonUpload.Size = new System.Drawing.Size(97, 23);
            this.buttonUpload.TabIndex = 2;
            this.buttonUpload.Text = "Upload";
            this.buttonUpload.UseVisualStyleBackColor = true;
            this.buttonUpload.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(138, 51);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(97, 23);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(19, 22);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(97, 23);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // listViewLocal
            // 
            this.listViewLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnFileName});
            this.listViewLocal.Location = new System.Drawing.Point(392, 12);
            this.listViewLocal.Name = "listViewLocal";
            this.listViewLocal.Size = new System.Drawing.Size(373, 223);
            this.listViewLocal.TabIndex = 5;
            this.listViewLocal.UseCompatibleStateImageBehavior = false;
            this.listViewLocal.View = System.Windows.Forms.View.Details;
            this.listViewLocal.DoubleClick += new System.EventHandler(this.listViewLocal_DoubleClick);
            // 
            // columnFileName
            // 
            this.columnFileName.Text = "Local:";
            this.columnFileName.Width = 345;
            // 
            // groupBoxCredentials
            // 
            this.groupBoxCredentials.Controls.Add(this.labelPassword);
            this.groupBoxCredentials.Controls.Add(this.labelUserName);
            this.groupBoxCredentials.Controls.Add(this.labelHost);
            this.groupBoxCredentials.Controls.Add(this.textBoxPassword);
            this.groupBoxCredentials.Controls.Add(this.textBoxUserName);
            this.groupBoxCredentials.Controls.Add(this.textBoxHost);
            this.groupBoxCredentials.Location = new System.Drawing.Point(12, 261);
            this.groupBoxCredentials.Name = "groupBoxCredentials";
            this.groupBoxCredentials.Size = new System.Drawing.Size(373, 100);
            this.groupBoxCredentials.TabIndex = 6;
            this.groupBoxCredentials.TabStop = false;
            this.groupBoxCredentials.Text = "Credentials";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Enabled = false;
            this.labelPassword.Location = new System.Drawing.Point(23, 73);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(56, 13);
            this.labelPassword.TabIndex = 5;
            this.labelPassword.Text = "Password:";
            // 
            // labelUserName
            // 
            this.labelUserName.AutoSize = true;
            this.labelUserName.Enabled = false;
            this.labelUserName.Location = new System.Drawing.Point(23, 47);
            this.labelUserName.Name = "labelUserName";
            this.labelUserName.Size = new System.Drawing.Size(61, 13);
            this.labelUserName.TabIndex = 4;
            this.labelUserName.Text = "User name:";
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Enabled = false;
            this.labelHost.Location = new System.Drawing.Point(23, 22);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(90, 13);
            this.labelHost.TabIndex = 3;
            this.labelHost.Text = "Host:           ftp://";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(111, 70);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(236, 20);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.Text = "1qaz!QAZ";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(111, 44);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(236, 20);
            this.textBoxUserName.TabIndex = 1;
            this.textBoxUserName.Text = "inua";
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(111, 19);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(236, 20);
            this.textBoxHost.TabIndex = 0;
            this.textBoxHost.Text = "testforfree.somee.com";
            // 
            // textBoxCwdRemote
            // 
            this.textBoxCwdRemote.Location = new System.Drawing.Point(12, 241);
            this.textBoxCwdRemote.Name = "textBoxCwdRemote";
            this.textBoxCwdRemote.Size = new System.Drawing.Size(373, 20);
            this.textBoxCwdRemote.TabIndex = 7;
            // 
            // textBoxCwdLocal
            // 
            this.textBoxCwdLocal.Location = new System.Drawing.Point(437, 241);
            this.textBoxCwdLocal.Name = "textBoxCwdLocal";
            this.textBoxCwdLocal.Size = new System.Drawing.Size(244, 20);
            this.textBoxCwdLocal.TabIndex = 8;
            this.textBoxCwdLocal.Text = "C:\\";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRefresh);
            this.groupBox1.Controls.Add(this.buttonNewFolder);
            this.groupBox1.Controls.Add(this.buttonDownload);
            this.groupBox1.Controls.Add(this.buttonUpload);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.buttonDelete);
            this.groupBox1.Location = new System.Drawing.Point(392, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 100);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Actions";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(19, 51);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(97, 23);
            this.buttonRefresh.TabIndex = 6;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonNewFolder
            // 
            this.buttonNewFolder.Location = new System.Drawing.Point(138, 22);
            this.buttonNewFolder.Name = "buttonNewFolder";
            this.buttonNewFolder.Size = new System.Drawing.Size(97, 23);
            this.buttonNewFolder.TabIndex = 5;
            this.buttonNewFolder.Text = "New folder";
            this.buttonNewFolder.UseVisualStyleBackColor = true;
            this.buttonNewFolder.Click += new System.EventHandler(this.buttonNewFolder_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 368);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(777, 22);
            this.statusStrip1.TabIndex = 10;
            this.statusStrip1.Text = "statusStrip";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(722, 239);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(43, 23);
            this.buttonOpen.TabIndex = 11;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonCooseCwd_Click);
            // 
            // uttonGo
            // 
            this.uttonGo.Location = new System.Drawing.Point(687, 239);
            this.uttonGo.Name = "uttonGo";
            this.uttonGo.Size = new System.Drawing.Size(29, 23);
            this.uttonGo.TabIndex = 12;
            this.uttonGo.Text = "Go";
            this.uttonGo.UseVisualStyleBackColor = true;
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(391, 239);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(40, 23);
            this.buttonBack.TabIndex = 13;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // FtpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 390);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.uttonGo);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.textBoxCwdLocal);
            this.Controls.Add(this.textBoxCwdRemote);
            this.Controls.Add(this.groupBoxCredentials);
            this.Controls.Add(this.listViewLocal);
            this.Controls.Add(this.listViewFtp);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "FtpForm";
            this.ShowIcon = false;
            this.Text = "Ftp Client";
            this.Load += new System.EventHandler(this.FtpForm_Load);
            this.groupBoxCredentials.ResumeLayout(false);
            this.groupBoxCredentials.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewFtp;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonUpload;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.ColumnHeader columnFtpFileName;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ListView listViewLocal;
        private System.Windows.Forms.ColumnHeader columnFileName;
        private System.Windows.Forms.GroupBox groupBoxCredentials;
        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelUserName;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox textBoxCwdRemote;
        private System.Windows.Forms.TextBox textBoxCwdLocal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Button buttonNewFolder;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button uttonGo;
        private System.Windows.Forms.Button buttonBack;
    }
}

