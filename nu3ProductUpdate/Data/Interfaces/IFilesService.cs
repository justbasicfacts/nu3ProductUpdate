using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Classes.Events;
using System;
using System.Collections.Generic;
using System.IO;

namespace nu3ProductUpdate.Data.Interfaces
{
    public interface IFilesService
    {
        public event EventHandler<FileUploadedEventArgs> OnFileUploaded;

        CustomFileInfo Add(string id, string fileName, Stream stream);

        bool Delete(string fileName);

        IEnumerable<CustomFileInfo> FindAll();

        CustomFileStream GetFileStreamById(string id);

        CustomFileInfo GetFileInfoById(string id);
    }
}