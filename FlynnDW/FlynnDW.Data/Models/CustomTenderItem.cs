using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "CustomTenderItem", Namespace = "YUMArtsExtensions")]
    public class CustomTenderItem
    {
        [XmlElement(ElementName = "VendorEncryptedPrimaryAccount", Namespace = "YUMArtsExtensions")]
        public string VendorEncryptedPrimaryAccount { get; set; }
    }
}
