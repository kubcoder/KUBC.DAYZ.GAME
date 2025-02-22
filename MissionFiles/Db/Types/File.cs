using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Файл types.xml
/// </summary>
public class File : List<Item>, IXmlSerializable
{
    public const string ROOT_NODE_NAME = "types";
    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }
    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        this.Clear();
        while(reader.Read())
        {
            if (reader.IsStartElement())
            {
                if (reader.Name == ROOT_NODE_NAME)
                {
                    ReadTypes(reader.ReadSubtree());
                }
            }
        }
    }

    private void ReadTypes(XmlReader reader)
    {
        while(reader.Read())
        {
            if (reader.IsStartElement())
            {
                if (reader.Name == Item.ROOT_NODE_NAME)
                {
                    var item = new Item();
                    item.ReadXml(reader.ReadSubtree());
                    this.Add(item);
                }
            }
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        throw new NotImplementedException();
    }
}
