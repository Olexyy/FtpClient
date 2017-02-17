using System;

namespace FtpClient
{
    public class Local
    {
        public LocalCwd Cwd { get; private set; }
        public Local()
        {
            this.Cwd = new LocalCwd("", @"C:\local", null);
        }
    }
}
