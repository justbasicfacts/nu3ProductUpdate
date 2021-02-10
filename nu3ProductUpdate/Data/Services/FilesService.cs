using LiteDB;
using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Classes.Events;
using nu3ProductUpdate.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nu3ProductUpdate.Data.Services
{
    public class FilesService : IFilesService
    {
        private ILiteStorage<string> _productFilesStorage;

        public FilesService(IDbContext context)
        {
            _productFilesStorage = GetFileStorage(context.Database);
        }

        public event EventHandler<FileUploadedEventArgs> OnFileUploaded;

        public CustomFileInfo Add(string id, string fileName, Stream stream)
        {
            LiteFileInfo<string> retVal;
            using (stream)
            {
                retVal = _productFilesStorage.Upload(id, fileName, stream);
                if (retVal != null)
                {
                    if (OnFileUploaded != null)
                    {
                        OnFileUploaded(this, new FileUploadedEventArgs(stream, retVal.MimeType));
                    }
                }
            }

            return new CustomFileInfo(retVal);
        }

        public bool Delete(string id)
        {
            return _productFilesStorage.Delete(id);
        }

        public IEnumerable<CustomFileInfo> FindAll()
        {
            return _productFilesStorage.FindAll().OrderByDescending(item => item.UploadDate).Select(item => new CustomFileInfo(item));
        }

        public CustomFileInfo GetFileInfoById(string id)
        {
            var foundFile = _productFilesStorage.FindById(id);
            if (foundFile != null)
            {
                return new CustomFileInfo(foundFile);
            }

            return null;
        }

        public CustomFileStream GetFileStreamById(string id)
        {
            return new CustomFileStream(_productFilesStorage.OpenRead(id));
        }

        private ILiteStorage<string> GetFileStorage(LiteDatabase productDb)
        {
            return productDb.GetStorage<string>("productFiles");
        }
    }
}