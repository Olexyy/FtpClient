using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;

namespace FtpClient
{
    public enum FtpItemType { Folder, File, Cwd }
    public enum FtpEventType { ListDirectory, UploadOk, DeleteOk }
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
            this.Cwd = new FtpCwd("", domain);
            this.Lock = new object();
            this.FtpEvent += eventHandler;
        }
        public Ftp(NetworkCredential credentials, FtpEventHandler eventHandler)
        {
            this.Credentials = credentials;
            this.Cwd = new FtpCwd(String.Empty, credentials.Domain);
            this.Lock = new object();
            this.FtpEvent += eventHandler;
        }
        private void DefineCwd(string folderName)
        {
            switch (folderName)
            {
                // TODO: logic on backward
                case ".":
                    this.Cwd.Path = this.Credentials.Domain;
                    break;
                case "..":
                    break;
                default:
                    this.Cwd.Path = this.Cwd.Path + "/" + folderName;
                    break;
            }
        }
        public void GetCwd(FtpItem item = null)
        {
            if (item == null)
                new Thread(this.GetCwdAsync).Start(this.Cwd.Path);
            else if (item.Type == FtpItemType.Folder)
            {
                this.Cwd.Path = item.Path + "/" + item.Name;
                new Thread(this.GetCwdAsync).Start(this.Cwd.Path);
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
            if(type == DIR)
                return new FtpFolder(FtpItemType.Folder, name, this.Cwd.Path, datetime);
            else
                return new FtpFile(FtpItemType.File, name, this.Cwd.Path, datetime);
        }
        public void Upload(string path, string localFile)
        {
            List<string> param = new List<string>() { this.Cwd.Path + @"/text.txt", @"C:\text.txt" };
            new Thread(this.UploadAsync).Start(param);
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
                }
                this.GetCwdAsync(this.Cwd.Path);
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
        public void Delete(FtpItem item = null)
        {
            if (item == null) ;
            else if (item.Type == FtpItemType.Folder) ;
            else if (item.Type == FtpItemType.File)
            {
                string filePath = item.Path + "/" + item.Name;
                new Thread(this.DeleteAsync).Start(filePath);
            }
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
                this.GetCwdAsync(this.Cwd.Path);
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
    public abstract class FtpItem
    {
        public FtpItemType Type { get; private set; }
        public string Name { get; private set; }
        public string Path { get; set; }
        public Nullable<DateTime> Timestamp { get; set; }
        public FtpItem(FtpItemType type, string name, string path, Nullable<DateTime> timestamp)
        {
            this.Type = type;
            this.Name = name;
            this.Path = path;
            this.Timestamp = timestamp;
        }
    }
    public class FtpFolder : FtpItem
    {
        public FtpFolder(FtpItemType type, string name, string path, DateTime timestamp) : base(FtpItemType.Folder, name, path,timestamp) { }
    }
    public class FtpFile : FtpItem
    {
        public string Extension { get; private set; }
        public FtpFile(FtpItemType type, string name, string path, DateTime timestamp) : base(FtpItemType.File, name, path,timestamp) { }
    }
    public class FtpCwd : FtpItem
    {
        public List<FtpItem> Items { get; private set; }
        public FtpCwd(string name, string path) : base(FtpItemType.Cwd, name, path, null)
        {
            this.Items = new List<FtpItem>();
        }
    }

    public abstract class LocalItem
    {
        public FtpItemType Type { get; private set; }
        public string Name { get; private set; }
        public string Path { get; set; }
        public Nullable<DateTime> Timestamp { get; set; }
        public LocalItem(FtpItemType type, string name, string path, Nullable<DateTime> timestamp)
        {
            this.Type = type;
            this.Name = name;
            this.Path = path;
            this.Timestamp = timestamp;
        }
    }
    public class LocalFolder : LocalItem
    {
        public LocalFolder(FtpItemType type, string name, string path, DateTime timestamp) : base(FtpItemType.Folder, name, path, timestamp) { }
    }
    public class LocalFile : LocalItem
    {
        public string Extension { get; private set; }
        public LocalFile(FtpItemType type, string name, string path, DateTime timestamp) : base(FtpItemType.File, name, path, timestamp) { }
    }
    public class LocalCwd : LocalItem
    {
        public List<LocalItem> Items { get; private set; }
        public LocalCwd(string name, string path) : base(FtpItemType.Cwd, name, path, null)
        {
            this.Items = new List<LocalItem>();
            foreach (string itemPathName in Directory.GetDirectories(this.Path))
            {
                DateTime timestamp = Directory.GetLastWriteTime(itemPathName);
                string folderName = System.IO.Path.GetFileName(itemPathName);
                this.Items.Add(new LocalFolder(FtpItemType.Folder, folderName, this.Path, timestamp));
            }
            foreach (string itemPathName in Directory.GetFiles(this.Path))
            {
                DateTime timestamp = Directory.GetLastWriteTime(itemPathName);
                string fileName = System.IO.Path.GetFileName(itemPathName);
                this.Items.Add(new LocalFile(FtpItemType.File, fileName, this.Path, timestamp));
            }
        }
    }
    public class Local
    {
        public LocalCwd Cwd { get; private set; }
        public Local()
        {
            this.Cwd = new LocalCwd("", @"C:\");
        }
    }
}
