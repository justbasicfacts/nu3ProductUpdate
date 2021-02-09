using nu3ProductUpdate.Classes.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace nu3ProductUpdate.Models
{
    [XmlRoot(ElementName = "product", IsNullable = false)]
    public class Product : IEquatable<Product>
    {
        [XmlElement(ElementName = "id", IsNullable = false)]
        public long Id { get; set; }

        [XmlElement(ElementName = "title", IsNullable = false)]
        public string Title { get; set; }

        [XmlElement(ElementName = "body-html", IsNullable = false)]
        public string Bodyhtml { get; set; }

        [XmlElement(ElementName = "vendor", IsNullable = false)]
        public string Vendor { get; set; }

        [XmlElement(ElementName = "product-type", IsNullable = false)]
        public string Producttype { get; set; }

        [XmlElement(ElementName = "created-at", IsNullable = false)]
        public DateTime CreatedAt { get; set; }

        [XmlElement(ElementName = "handle", IsNullable = false)]
        public string Handle { get; set; }

        [XmlElement(ElementName = "published-scope", IsNullable = false)]
        public string PublishedScope { get; set; }

        [XmlElement(ElementName = "tags", IsNullable = false)]
        public string Tags { get; set; }

        [XmlElement(ElementName = "image", IsNullable = false)]
        public Image Image { get; set; }

        [XmlElement(ElementName = "location", IsNullable = false)]
        public string Location { get; set; }

        [XmlElement(ElementName = "amount", IsNullable = false)]
        public decimal Amount { get; set; }

        public bool Equals([AllowNull] Product other)
        {
            if (other != null)
            {
                if (ReferenceEquals(this, other)) return true;
                if (ReferenceEquals(this, null)) return false;
                if (ReferenceEquals(other, null)) return false;
                if (GetType() != other.GetType()) return false;

                if (Id == other.Id &&
                    Bodyhtml == other.Bodyhtml &&
                    CreatedAt == other.CreatedAt &&
                    Handle.IsEquals(other.Handle) &&
                    Producttype.IsEquals(other.Producttype) &&
                    PublishedScope.IsEquals(other.PublishedScope) &&
                    Tags.IsEquals(other.Tags) &&
                    Title.IsEquals(other.Title) &&
                    Image.Equals(other.Image)

                    )
                {
                    return true;
                }
            }

            return false;
        }
    }

    [XmlRoot(ElementName = "products", IsNullable = false)]
    public class Products
    {
        [XmlElement("product", IsNullable = false)]
        public Product[] Items { get; set; }
    }
}