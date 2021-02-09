using System;

namespace nu3ProductUpdate.Data.Interfaces
{
    public interface IFileInfo
    {
        string Filename { get; set; }

        string MimeType { get; set; }

        long Length { get; set; }
        int Chunks { get; set; }
        DateTime UploadDate { get; set; }
    }
}