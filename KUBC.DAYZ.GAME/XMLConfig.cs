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
        public virtual void WriteXml(XmlWriter writer)
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
                        writer.WriteValue(e.Value);
                    }
                    writer.WriteEndElement();
                }
            }
        }
        /// <summary>
        /// Добавить параметр в коллекцию
        /// </summary>
        /// <param name="key">Ключ параметра</param>
        /// <param name="value">Значение параметра</param>
        protected void AddParametr(string key, string? value)
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
        
        /// <summary>
        /// Сохранить данные в файл
        /// </summary>
        /// <param name="file"></param>
        public void Save(FileInfo file)
        {
            XmlSerializer s = new XmlSerializer(this.GetType());
            using(StreamWriter stream = new StreamWriter(file.FullName))
            {
                using(XmlWriter writer = XmlWriter.Create(stream, GetSettings()))
                {
                    s.Serialize(writer, this);
                }
            }
        }
        /// <summary>
        /// Получить настройки записи в XML
        /// </summary>
        /// <returns></returns>
        protected virtual XmlWriterSettings GetSettings()
        {
            return new()
            {
                OmitXmlDeclaration = false,
                Indent = true
            };
        }
    }
}
