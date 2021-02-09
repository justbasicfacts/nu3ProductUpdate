using LiteDB;
using System.Xml.Serialization;

namespace nu3ProductUpdate.Models
{
    [XmlRoot("inventory", IsNullable = false)]
    public class Inventory
    {
        [BsonId(true)]
        [XmlElement(ElementName = "handle")]
        public string Handle { get; set; }

        [XmlElement(ElementName = "location")]
        public string Location { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }
    }

    [XmlRoot(ElementName = "inventory-list", IsNullable = false)]
    public class InventoryList
    {
        [XmlElement("inventory", IsNullable = false)]
        public Inventory[] Items { get; set; }
    }
}