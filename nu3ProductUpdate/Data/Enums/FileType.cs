using System.Runtime.Serialization;

namespace nu3ProductUpdate.Data.Enums
{
    public enum FileType
    {
        [EnumMember(Value = "Product")]
        Product,

        [EnumMember(Value = "Inventory")]
        Inventory
    }
}