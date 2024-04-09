using KUBC.DAYZ.GAME.MissionFiles.DB.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Коллекция настройки игровых предметов
    /// </summary>
    [XmlRoot("types")]
    public class ItemTypes: XMLConfig
    {
        /// <inheritdoc/>
        public override void ReadXml(XmlReader reader)
        {
            Params.Clear();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == Item.NODENAME)
                    {
                        Item item = new(reader.Name);
                        item.ReadXml(reader.ReadSubtree());
                        this.Params.Add(item.SectionName, item);
                    }
                }
            }
        }
        /// <inheritdoc/>
        public override void WriteXml(XmlWriter writer)
        {
            foreach (var e in this.Params)
            {
                var wItem = e.Value as Item;
                if (wItem != null) 
                {
                    writer.WriteStartElement(Item.NODENAME);
                    wItem.WriteXml(writer);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
    }
}
