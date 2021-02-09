using nu3ProductUpdate.Classes.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace nu3ProductUpdate.Models
{
    [XmlRoot(ElementName = "image", IsNullable = false)]
    public class Image : IEquatable<Image>
    {
        [XmlElement(ElementName = "id", IsNullable = false)]
        public long Id { get; set; }

        [XmlElement(ElementName = "product-id", IsNullable = false)]
        public long ProductId { get; set; }

        [XmlElement(ElementName = "created-at", IsNullable = false)]
        public DateTime CreateDate { get; set; }

        [XmlElement(ElementName = "updated-at", IsNullable = false)]
        public DateTime UpdatedAt { get; set; }

        [XmlElement(ElementName = "width", IsNullable = false)]
        public long Width { get; set; }

        [XmlElement(ElementName = "height", IsNullable = false)]
        public long Height { get; set; }

        [XmlElement(ElementName = "src", IsNullable = false)]
        public string Src { get; set; }

        [XmlElement(ElementName = "position", IsNullable = false)]
        public long Position { get; set; }

        [XmlElement(ElementName = "alt", IsNullable = false)]
        public string Alt { get; set; }

        public bool Equals([AllowNull] Image other)
        {
            if (other != null)
            {
                if (ReferenceEquals(this, other)) return true;
                if (ReferenceEquals(this, null)) return false;
                if (ReferenceEquals(other, null)) return false;
                if (GetType() != other.GetType()) return false;

                if (Id == other.Id &&
                    ProductId == other.ProductId &&
                    CreateDate == other.CreateDate &&
                    UpdatedAt == other.UpdatedAt &&
                    Width == other.Width &&
                    Height == other.Height &&
                    Position == other.Position &&
                    Src.IsEquals(other.Src) &&
                    Alt.IsEquals(other.Alt))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}