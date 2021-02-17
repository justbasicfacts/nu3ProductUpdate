using LiteDB;
using nu3ProductUpdate.Classes;
using nu3ProductUpdate.Data.Interfaces;
using nu3ProductUpdate.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace nu3ProductUpdate.Data.Services
{
    public class InventoryService : IInventoryService
    {
        private ILiteCollection<Inventory> _inventoryCollection;
        private IProductsService _productsService;

        public InventoryService(IDbContext context, IProductsService productsService)
        {
            _productsService = productsService;
            _inventoryCollection = GetCollection(context.Database);
        }

        public IEnumerable<Inventory> FindAll()
        {
            return _inventoryCollection.FindAll();
        }

        public Inventory GetByHandle(string handle)
        {
            return _inventoryCollection.FindById(handle);
        }

        public ILiteCollection<Inventory> GetCollection(ILiteDatabase database)
        {
            return database.GetCollection<Inventory>("Inventory");
        }

        public void Subscribe(IFilesService filesService)
        {
            filesService.OnFileUploaded += FilesService_OnFileUploaded;
        }

        private void FilesService_OnFileUploaded(object sender, nu3ProductUpdate.Classes.Events.FileUploadedEventArgs e)
        {
            if (e.MimeType == AllowedMimeTypes.Text.Csv)
            {
                var inventoryList = GetInventoryFromCsv(e.File);

                foreach (var inventoryItem in inventoryList)
                {
                    var foundInventoryItem = _inventoryCollection.FindOne(item => item.UniqueId == inventoryItem.UniqueId);
                    if (foundInventoryItem != null)
                    {
                        _inventoryCollection.Update(inventoryItem);
                    }
                    else
                    {
                        if (_productsService.Exists(item => item.Handle == inventoryItem.Handle))
                        {
                            _inventoryCollection.Insert(inventoryItem);
                        }
                    }
                }
            }
        }

        private Inventory[] GetInventoryFromCsv(Stream stream)
        {
            Inventory[] retVal = null;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                stream.Position = 0;
                string[] headers = streamReader.ReadLine().Replace("\"", "").Split(';');

                using (var memoryStream = new MemoryStream())
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    ConvertInventoryCsvToXml(streamReader, streamWriter, headers);
                    memoryStream.Position = 0;
                    XmlSerializer serializer = new XmlSerializer(typeof(InventoryList));
                    var inventoryList = (InventoryList)serializer.Deserialize(memoryStream);
                    if (inventoryList != null)
                    {
                        retVal = inventoryList.Items;
                    }
                }
            }

            return retVal;
        }

        private void ConvertInventoryCsvToXml(StreamReader streamReader, StreamWriter streamWriter, string[] headers)
        {
            {
                streamWriter.WriteLine("<inventory-list>");

                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    line = line.Replace("\"", "");
                    streamWriter.WriteLine("<inventory>");
                    var lines = line.Split(';');
                    for (int i = 0; i < lines.Length; i++)
                    {
                        streamWriter.WriteLine(string.Format("<{0}>{1}</{0}>", headers[i], lines[i]));
                    }

                    streamWriter.WriteLine("</inventory>");
                }

                streamWriter.WriteLine("</inventory-list>");
            }

            streamWriter.Flush();
        }
    }
}