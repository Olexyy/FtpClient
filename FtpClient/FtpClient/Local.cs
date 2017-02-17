using System;

namespace FtpClient
{
    public enum LocalEventType
    {
        Exception, ListDirectory, UploadOk, DeleteFileOk,
        DeleteFolderOk, DownloadOk, MakeDirectoryOk,
    }
    public class LocalEventArgs : EventArgs
    {
        public LocalEventType Type { get; set; }
        public LocalCwd Cwd { get; set; }
        public Exception Exception { get; set; }
        public LocalEventArgs(LocalEventType type, LocalCwd cwd, Exception exception = null)
        {
            this.Type = type;
            this.Cwd = cwd;
            this.Exception = exception;
        }
    }
    public delegate void LocalEventHandler(object sender, LocalEventArgs args);
    public class Local
    {
        public LocalCwd Cwd { get; private set; }
        public Local(LocalEventHandler eventHandler)
        {
            this.Cwd = new LocalCwd(@"C:\");
            this.Cwd.LocalEvent += eventHandler;
            this.Cwd.GetCwd();
        }
    }
}
