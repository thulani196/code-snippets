using System.Xml.Serialization;

namespace FlynnDW.DataLayer.Models
{
    [XmlRoot(ElementName = "EventID", Namespace = "YUMArtsExtensions")]
    public class EventID
    {
        [XmlAttribute(AttributeName = "EventName")]
        public string EventName { get; set; }

        [XmlText]
        public string Text { get; set; }
    }
}
