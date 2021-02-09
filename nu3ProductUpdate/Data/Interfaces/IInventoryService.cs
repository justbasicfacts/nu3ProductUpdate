using nu3ProductUpdate.Models;
using System.Collections.Generic;

namespace nu3ProductUpdate.Data.Interfaces
{
    public interface IInventoryService
    {
        Inventory GetByHandle(string handle);

        IEnumerable<Inventory> FindAll();

        void Subscribe(IFilesService filesService);
    }
}