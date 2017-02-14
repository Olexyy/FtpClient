using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using FtpClient;

namespace net4
{
    public partial class FtpForm : Form
    {
        private string baseAddress = @"ftp://testforfree.somee.com";
        private string pass = "1qaz!QAZ";
        private string user = "inua";
        private string cwd;
        private Ftp Ftp { get; set; }
        private Local Local { get; set; }

        public FtpForm()
        {
            InitializeComponent();
            this.cwd = this.baseAddress;
            this.Ftp = new Ftp(this.baseAddress, this.user, this.pass, this.FtpEventHandler);
            this.Local = new Local();
            foreach (LocalItem item in this.Local.Cwd.Items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name);
                listViewItem.Tag = item;
                this.listViewLocal.Items.Add(listViewItem);
            }
        }

        public void ReadDirectory(string path)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(path);
            request.Credentials = new NetworkCredential(this.user, this.pass);
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = request.GetResponse() as FtpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
            this.listView.Items.Clear();
            while (!reader.EndOfStream)
            {
                ListViewItem line = new ListViewItem(reader.ReadLine());
                this.listView.Items.Add(line);
            }
        }

        public void DeleteFile(string path)
        {
            path = this.cwd + @"/text.txt";
 
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(path);
            request.Credentials = new NetworkCredential(this.user, this.pass);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            var response = (FtpWebResponse)request.GetResponse();
            response.Close();
        }

        public void AddFile(string path, string localFile)
        {
            path = this.cwd + @"/text.txt";
            localFile = @"D:\text.txt";
            
            int bufferSize = 2048;
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(path);
            request.Credentials = new NetworkCredential(this.user, this.pass);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            Stream push = request.GetRequestStream();
            /* Open a File Stream to Read the File for Upload */
            FileStream localFileStream = File.Open(localFile, FileMode.Open);
            /* Buffer for the Downloaded Data */
            byte[] byteBuffer = new byte[bufferSize];
            int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
            /* Upload the File by Sending the Buffered Data Until the Transfer is Complete
            */
            try
            {
                while (bytesSent != 0)
                {
                    push.Write(byteBuffer, 0, bytesSent);
                    bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                }
            }
            finally { push.Close(); localFileStream.Close(); }
        }

        private void listView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            FtpItem item = this.listView.SelectedItems[0].Tag as FtpItem;
            //this.cwd = this.cwd + "/" + itemName;
            //this.ReadDirectory(this.cwd);
            this.Ftp.GetCwd(item);
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            this.Ftp.Upload(null, null);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            FtpItem item = this.listView.SelectedItems[0].Tag as FtpItem;
            this.Ftp.Delete(item);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            this.Ftp.GetCwd();
        }

        private void FtpEventHandler(object sender, FtpEventArgs args)
        {
            if(args.Type == FtpEventType.ListDirectory)
            {
                this.Invoke(new Action(() => {
                    this.listView.Items.Clear();
                    args.Cwd.Items.ForEach(i => { this.AddItem(i); });
                }));
            }
        }
        private void AddItem(FtpItem item)
        {
            ListViewItem listViewItem = new ListViewItem(item.Name);
            listViewItem.Tag = item;
            this.listView.Items.Add(listViewItem);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            this.Ftp.GetCwd();
        }
    }
}
