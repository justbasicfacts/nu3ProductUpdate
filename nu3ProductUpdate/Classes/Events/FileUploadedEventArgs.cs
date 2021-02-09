using System;
using System.IO;

namespace nu3ProductUpdate.Classes.Events
{
    public class FileUploadedEventArgs : EventArgs
    {
        public Stream File { get; set; }
        public string MimeType { get; set; }

        public FileUploadedEventArgs(Stream file, string mimeType)
        {
            File = file;
            MimeType = mimeType;
        }
    }
}