using LiteDB;
using nu3ProductUpdate.Classes.Events;
using System;
using System.Collections.Generic;
using System.IO;

namespace nu3ProductUpdate.Data.Interfaces
{
    public interface IFilesService
    {
        public event EventHandler<FileUploadedEventArgs> OnFileUploaded;

        LiteFileInfo<string> Add(string id, string fileName, Stream stream);

        bool Delete(string fileName);

        IEnumerable<LiteFileInfo<string>> FindAll();

        LiteFileStream<string> GetFileStreamById(string id);

        LiteFileInfo<string> GetFileInfoById(string id);
    }
}