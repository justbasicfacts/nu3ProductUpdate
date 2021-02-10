using LiteDB;
using nu3ProductUpdate.Data.Enums;
using nu3ProductUpdate.Data.Interfaces;
using System;

namespace nu3ProductUpdate.Classes
{
    public class CustomFileInfo : IFileInfo
    {
        public CustomFileInfo()
        {
        }

        public CustomFileInfo(LiteFileInfo<string> fileInfo)
        {
            if (fileInfo != null)
            {
                Id = fileInfo.Id;
                Filename = fileInfo.Filename;
                MimeType = fileInfo.MimeType;
                Length = fileInfo.Length;
                Chunks = fileInfo.Chunks;
                UploadDate = fileInfo.UploadDate;
            }
        }

        public CustomFileInfo(string id, string filename, string mimeType, long length, int chunks, DateTime uploadDate)
        {
            Id = id;
            Filename = filename;
            MimeType = mimeType;
            Length = length;
            Chunks = chunks;
            UploadDate = uploadDate;
        }

        public string Id { get; set; }
        public string Filename { get; set; }
        public string MimeType { get; set; }
        public long Length { get; set; }
        public int Chunks { get; set; }
        public DateTime UploadDate { get; set; }
        public FileType FileType
        {
            get
            {
                return MimeType == AllowedMimeTypes.Text.Xml ? FileType.Product : FileType.Inventory;
            }
        }
    }
}