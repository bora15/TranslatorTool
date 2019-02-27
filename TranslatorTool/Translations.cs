using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TranslatorTool
{
    [XmlRoot(ElementName = "translations")]
    public class Translations
    {
        [XmlElement(ElementName = "translation")]
        public List<Translation> Translation { get; set; }
    }
}
