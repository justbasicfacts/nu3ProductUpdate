using LiteDB;
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

        public LiteFileInfo<string> Add(string id, string fileName, Stream stream)
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

            return retVal;
        }

        public bool Delete(string id)
        {
            return _productFilesStorage.Delete(id);
        }

        public IEnumerable<LiteFileInfo<string>> FindAll()
        {
            return _productFilesStorage.FindAll().OrderByDescending(item => item.UploadDate);
        }

        public LiteFileInfo<string> GetFileInfoById(string id)
        {
            return _productFilesStorage.FindById(id);
        }

        public LiteFileStream<string> GetFileStreamById(string id)
        {
            return _productFilesStorage.OpenRead(id);
        }

        private ILiteStorage<string> GetFileStorage(LiteDatabase productDb)
        {
            return productDb.GetStorage<string>("productFiles");
        }
    }
}