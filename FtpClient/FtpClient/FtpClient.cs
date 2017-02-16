using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;

namespace FtpClient
{
    public enum FtpEventType { ListDirectory, UploadOk, DeleteOk, DownloadOk }
    public class FtpEventArgs : EventArgs
    {
        public FtpEventType Type { get; set; }
        public FtpCwd Cwd { get; set; }
        public FtpEventArgs(FtpEventType type, FtpCwd cwd)
        {
            this.Type = type;
            this.Cwd = cwd;
        }
    }
    public delegate void FtpEventHandler (object sender, FtpEventArgs args);
    public class Ftp
    {
        public NetworkCredential Credentials { get; private set; }
        public FtpCwd Cwd { get; private set; }
        public event FtpEventHandler FtpEvent;
        private object Lock { get; set; }
        private const string DIR = "<DIR>";
        private const int BufferSize = 2048;
        public Ftp(string domain, string userName, string password, FtpEventHandler eventHandler)
        {
            NetworkCredential credentials = new NetworkCredential(userName, password, domain);
            this.Credentials = credentials;
            this.Cwd = new FtpCwd("", domain, null);
            this.Lock = new object();
            this.FtpEvent += eventHandler;
        }
        public Ftp(NetworkCredential credentials, FtpEventHandler eventHandler)
        {
            this.Credentials = credentials;
            this.Cwd = new FtpCwd(String.Empty, credentials.Domain, null);
            this.Lock = new object();
            this.FtpEvent += eventHandler;
        }
        public void GetCwd(FtpItem item = null)
        {
            if (item == null)
                new Thread(this.GetCwdAsync).Start(this.Cwd.FullPath);
            else if (item.Type == FtpItemType.Folder)
            {
                this.Cwd.FullPath = item.FullPath;
                new Thread(this.GetCwdAsync).Start(this.Cwd.FullPath);
            }
        }
        private void GetCwdAsync(object param)
        {
            try
            {
                string cwdPath = param as String;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(cwdPath);
                request.Credentials = new NetworkCredential(this.Credentials.UserName, this.Credentials.Password);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = request.GetResponse() as FtpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                lock (this.Lock)
                {
                    this.Cwd.Items.Clear();
                    while (!reader.EndOfStream)
                        this.Cwd.Items.Add(this.CreateFtpItem(reader.ReadLine()));
                }
                FtpEventArgs args = new FtpEventArgs(FtpEventType.ListDirectory, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch (Exception e)
            {
                throw new Exception("List directory fail.", e);
            }
        }
        private FtpItem CreateFtpItem(string raw)
        {
            List<string> parsed = raw.Split(' ').ToList();
            parsed.RemoveAll(i => i == String.Empty);
            string date = parsed[0];
            string time = parsed[1];
            string type = parsed[2];
            string name = parsed[3];
            DateTime datetime = DateTime.Parse(date + " " + time);
            string fullPath = this.Cwd.FullPath + "/" + name;
            if (type == DIR)
                return new FtpFolder(name, fullPath, this.Cwd.FullPath, datetime);
            else
                return new FtpFile(name, fullPath, this.Cwd.FullPath, datetime);
        }
        public void Upload(LocalItem localItem)
        {
            if(localItem.Type == LocalItemType.File)
            {
                List<string> param = new List<string>() { this.Cwd.FullPath + "/" + localItem.Name, localItem.FullPath };
                new Thread(this.UploadAsync).Start(param);
            }

        }
        private void UploadAsync(object param)
        {
            Stream upload = null;
            FileStream local = null;
            try
            {
                List<string> list = param as List<string>;
                string path = list.First();
                string localFile = list.Last();
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(path);
                request.Credentials = new NetworkCredential(this.Credentials.UserName, this.Credentials.Password);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                upload = request.GetRequestStream();
                local = File.Open(localFile, FileMode.Open);
                byte[] byteBuffer = new byte[BufferSize];
                int bytesSent = local.Read(byteBuffer, 0, BufferSize);
                while (bytesSent != 0)
                {
                    upload.Write(byteBuffer, 0, bytesSent);
                    bytesSent = local.Read(byteBuffer, 0, BufferSize);
                }// change it get cwd should be result of request
                this.GetCwdAsync(this.Cwd.FullPath);
                FtpEventArgs args = new FtpEventArgs(FtpEventType.UploadOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch(Exception e)
            {
                throw new Exception("Upload fail.", e);
            }
            finally
            {
                if (upload != null)
                    upload.Close();
                if(local != null)
                    local.Close();
            }
        }
        public void Download(FtpItem ftpItem, LocalCwd localCwd, string newName = null)
        {
            if (ftpItem.Type == FtpItemType.File)
            {
                newName = (newName == null) ? ftpItem.Name : newName;
                List<string> param = new List<string>() { ftpItem.FullPath, localCwd.FullPath, newName };
                new Thread(this.UploadAsync).Start(param);
            }

        }
        private void DownloadAsync(object param)
        {
            Stream download = null;
            FileStream local = null;
            try
            {
                List<string> list = param as List<string>;
                string ftpPath = list.First();
                string localPath = list[1];
                string newName = list.Last();
                string localNewPath = Path.Combine(localPath, newName);
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpPath);
                request.Credentials = new NetworkCredential(this.Credentials.UserName, this.Credentials.Password);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                download = request.GetRequestStream();
                local = File.Open(localNewPath, FileMode.OpenOrCreate);
                byte[] byteBuffer = new byte[BufferSize];
                int bytesRecieve = download.Read(byteBuffer, 0, BufferSize);
                while (bytesRecieve != 0)
                {
                    local.Write(byteBuffer, 0, bytesRecieve);
                    bytesRecieve = download.Read(byteBuffer, 0, BufferSize);
                }
                FtpEventArgs args = new FtpEventArgs(FtpEventType.DownloadOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch (Exception e)
            {
                throw new Exception("Upload fail.", e);
            }
            finally
            {
                if (download != null)
                    download.Close();
                if (local != null)
                    local.Close();
            }
        }
        public void Delete(FtpItem item = null)
        {
            if (item == null) ;
            else if (item.Type == FtpItemType.Folder) ;
            else if (item.Type == FtpItemType.File)
                new Thread(this.DeleteAsync).Start(item.FullPath);
        }
        private void DeleteAsync(object param)
        {
            FtpWebResponse response = null;
            try
            {
                string path = param as string;
                FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(path);
                request.Credentials = new NetworkCredential(this.Credentials.UserName, this.Credentials.Password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                response = (FtpWebResponse)request.GetResponse();
                this.GetCwdAsync(this.Cwd.FullPath);
                FtpEventArgs args = new FtpEventArgs(FtpEventType.DeleteOk, this.Cwd);
                if (this.FtpEvent != null)
                    this.FtpEvent(this, args);
            }
            catch(Exception e)
            {
                throw new Exception("Delete fail.", e);
            }
            finally
            {
                if(response != null)
                    response.Close();
            }
        }
    }
    public class Local
    {
        public LocalCwd Cwd { get; private set; }
        public Local()
        {
            this.Cwd = new LocalCwd("", @"C:\", null);
        }
    }
}
