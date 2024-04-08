using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Класс сереализирует все данные в виде аттрибутов
    /// </summary>
    public abstract class AXMLConfig : Config, IXmlSerializable
    {
        
        /// <inheritdoc/>
        public XmlSchema? GetSchema()
        {
            return null;
        }
        /// <inheritdoc/>
        public void ReadXml(XmlReader reader)
        {
            Params.Clear();
            if (reader.MoveToFirstAttribute())
            {
                AddParametr(reader.Name, reader.Value);
                while (reader.MoveToNextAttribute()) 
                {
                    AddParametr(reader.Name, reader.Value);
                }
            }
        }

        private void AddParametr(string key, string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Params.Add(key, null);
                return;
            }
            if (int.TryParse(value, out var intValue))
            {
                Params.Add(key, intValue);
                return;
            }
            if (decimal.TryParse(value, out var decimalValue))
            {
                Params.Add(key, decimalValue);
                return;
            }
            Params.Add(key, value);
        }

        /// <inheritdoc/>
        public void WriteXml(XmlWriter writer)
        {
            
            foreach (var e in this.Params)
            {
                writer.WriteAttributeString(e.Key, e.Value?.ToString());
            }
        }

        


    }
}
