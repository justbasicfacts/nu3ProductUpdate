using LiteDB;
using Microsoft.Extensions.Options;
using nu3ProductUpdate.Data.Classes;
using nu3ProductUpdate.Data.Interfaces;

namespace nu3ProductUpdate.Data
{
    public class DBContext : IDbContext
    {
        public DBContext(IOptions<LiteDbOptions> options)
        {
            Database = new LiteDatabase(options.Value.DatabaseLocation);
        }

        public LiteDatabase Database { get; }
    }
}