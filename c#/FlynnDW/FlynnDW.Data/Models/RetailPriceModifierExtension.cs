using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "RetailPriceModifierExtension", Namespace = "YUMArtsExtensions")]
    public class RetailPriceModifierExtension
    {
        [XmlElement(ElementName = "PromotionCount", Namespace = "YUMArtsExtensions")]
        public string PromotionCount { get; set; }
    }
}
