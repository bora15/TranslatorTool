using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TranslatorTool
{
    [XmlRoot(ElementName = "translation")]
    public class Translation
    {
        [XmlElement(ElementName = "from")]
        public string From { get; set; }
        [XmlElement(ElementName = "to")]
        public string To { get; set; }
        [XmlAttribute(AttributeName = "timestamp")]
        public string Timestamp { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public int Id { get; set; }
    }
}
