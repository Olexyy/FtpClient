using System;
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
        }
        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.GetCwd(item);
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                this.Ftp.Upload(localItem);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                FtpItem item = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.Delete(item);
            } 
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {
            if (this.listViewFtp.SelectedItems.Count == 1)
            {
                FtpItem ftpItem = this.listViewFtp.SelectedItems[0].Tag as FtpItem;
                this.Ftp.Download(ftpItem, this.Local.Cwd, null);
            }
        }
        private void LocalEventHandler(object sender, LocalEventArgs args)
        {
            if (args.Type == LocalEventType.ListDirectory || args.Type == LocalEventType.DeleteFileOk ||
                args.Type == LocalEventType.DeleteFolderOk || args.Type == LocalEventType.MakeDirectoryOk)
            {
                this.Invoke(new Action(() => {
                    this.listViewLocal.Items.Clear();
                    this.textBoxCwdLocal.Text = args.Cwd.FullPath;
                    args.Cwd.Items.ForEach(i => { this.AddItemLocal(i); });
                }));
            }
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
        private void AddItemLocal(LocalItem item)
        {
            ListViewItem listViewItem = new ListViewItem(item.Name);
            listViewItem.Tag = item;
            this.listViewLocal.Items.Add(listViewItem);
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
        private void buttonCooseCwd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBoxCwdLocal.Text = dialog.SelectedPath;
                this.Local.Cwd.GetCwd(dialog.SelectedPath);
            }
        }
        private void listViewLocal_DoubleClick(object sender, EventArgs e)
        {
            if (this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                if (localItem.Type == LocalItemType.Folder)
                    this.Local.Cwd.GetCwd(localItem.FullPath);
            }
        }

        private void FtpForm_Load(object sender, EventArgs e)
        {
            this.Local = new Local(this.textBoxCwdLocal.Text, this.LocalEventHandler);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(this.Local.Cwd.Root))
                this.Local.Cwd.GetCwd(this.Local.Cwd.Root);
        }

        private void buttonDeleteLocal_Click(object sender, EventArgs e)
        {
            if(this.listViewLocal.SelectedItems.Count == 1)
            {
                LocalItem localItem = this.listViewLocal.SelectedItems[0].Tag as LocalItem;
                this.Local.Cwd.DeleteItem(localItem);
            }
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            this.Local.Cwd.GetCwd(this.Local.Cwd.FullPath);
        }

        private void buttonNewFolderLocal_Click(object sender, EventArgs e)
        {
            this.Local.Cwd.AddFolder();
        }
    }
}
