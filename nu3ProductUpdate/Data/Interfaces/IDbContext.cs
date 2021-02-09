using LiteDB;

namespace nu3ProductUpdate.Data.Interfaces
{
    public interface IDbContext
    {
        LiteDatabase Database { get; }
    }
}