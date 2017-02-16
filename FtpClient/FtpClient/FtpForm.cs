﻿using System;
using System.Windows.Forms;

namespace FtpClient
{
    public partial class FtpForm : Form
    {
        private Ftp Ftp { get; set; }
        private Local Local { get; set; }

        public FtpForm()
        {
            InitializeComponent();
            this.Ftp = new Ftp(@"ftp://"+this.textBoxHost.Text.Trim(), this.textBoxUserName.Text.Trim(), this.textBoxPassword.Text.Trim(), this.FtpEventHandler);
            this.Local = new Local();
            foreach (LocalItem item in this.Local.Cwd.Items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name);
                listViewItem.Tag = item;
                this.listViewLocal.Items.Add(listViewItem);
            }
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
            this.Ftp.GetCwd(item);
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
            this.Ftp.Upload(localItem);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
            this.Ftp.Delete(item);
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            FtpItem ftpItem = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
            this.Ftp.Download(ftpItem, this.Local.Cwd, null);
        }

        private void FtpEventHandler(object sender, FtpEventArgs args)
        {
            if(args.Type == FtpEventType.ListDirectory)
            {
                this.Invoke(new Action(() => {
                    this.listViewFtp.Items.Clear();
                    this.textBoxCwdRemote.Text = args.Cwd.FullPath;
                    args.Cwd.Items.ForEach(i => { this.AddItem(i); });
                }));
            }
            if( args.Type == FtpEventType.DownloadOk || args.Type == FtpEventType.UploadOk ||
                args.Type == FtpEventType.DeleteFileOk || args.Type == FtpEventType.MakeDirectoryOk ||
                args.Type == FtpEventType.DeleteFolderOk )
            {
                this.Invoke(new Action(() => {
                    this.Ftp.GetCwd();
                }));
            }
        }
        private void AddItem(FtpItem item)
        {
            ListViewItem listViewItem = new ListViewItem(item.Name);
            listViewItem.Tag = item;
            this.listViewFtp.Items.Add(listViewItem);
        }
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            this.Ftp.GetCwd();
        }

        private void buttonNewFolder_Click(object sender, EventArgs e)
        {
            this.Ftp.NewFolder("New_folder");
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.Ftp.GetCwd();
        }
    }
}
