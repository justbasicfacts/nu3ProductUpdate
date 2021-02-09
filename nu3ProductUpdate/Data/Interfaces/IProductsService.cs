using LiteDB;
using nu3ProductUpdate.Models;
using System.Collections.Generic;

namespace nu3ProductUpdate.Data.Interfaces
{
    public interface IProductsService
    {
        long Insert(Product product);

        bool Update(Product product);

        Product GetByHandle(string handle);

        IEnumerable<Product> FindAll();

        void Subscribe(IFilesService filesService);

        bool Exists(System.Linq.Expressions.Expression<System.Func<Product, bool>> predicate);
    }
}