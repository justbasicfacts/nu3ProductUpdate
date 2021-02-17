using LiteDB;
using System.Text.Json.Serialization;
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

        [JsonIgnore]
        public string UniqueId
        {
            get
            {
                return Handle + Location;
            }
        }
    }

    [XmlRoot(ElementName = "inventory-list", IsNullable = false)]
    public class InventoryList
    {
        [XmlElement("inventory", IsNullable = false)]
        public Inventory[] Items { get; set; }
    }
}