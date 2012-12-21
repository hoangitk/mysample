using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AsyncTcpMessages.Simulator
{
    [Serializable]
    public class FileMessage
    {
        public string FileName { get; set; }
        public byte[] FileInBytes { get; set; }

        public FileMessage(string filePath)
        {
            this.FileName = Path.GetFileName(filePath);
            this.FileInBytes = File.ReadAllBytes(filePath);
        }
    }
}
