using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Класс сереализует все данные как отдельные элементы
    /// </summary>
    public abstract class XMLConfig : Config, IXmlSerializable
    {
        /// <inheritdoc/>
        public XmlSchema? GetSchema()
        {
            return null;
        }
        /// <inheritdoc/>
        public abstract void ReadXml(XmlReader reader);
        /// <inheritdoc/>
        public void WriteXml(XmlWriter writer)
        {
            foreach (var e in this.Params)
            {
                if (e.Value != null)
                {
                    writer.WriteStartElement(e.Key);
                    var xmlValue = e.Value as IXmlSerializable;
                    if (xmlValue != null)
                    {
                        xmlValue.WriteXml(writer);
                    }
                    else
                    {
                        XmlSerializer s = new XmlSerializer(e.Value.GetType());
                        s.Serialize(writer, e.Value);
                    }
                    writer.WriteEndElement();
                }
                
            }
        }
    }
}
