using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.Economy.Core;

/// <summary>
/// Коллекция настроек
/// корневых классов
/// </summary>
public class RootClassCollection : List<RootClass>, IXmlSerializable
{
    /// <summary>
    /// имя тэга XML элемента
    /// </summary>
    public const string ROOT_NODE_NAME = "classes";

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        this.Clear();
        while (reader.Read())
        {
            if (reader.IsStartElement())
            {
                if (reader.Name == RootClass.ROOT_NODE_NAME)
                {
                    var item = new RootClass();
                    item.ReadXml(reader);
                    this.Add(item);
                }
            }
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        foreach (var file in this)
            file.WriteXml(writer);
        writer.WriteEndElement();
    }
}
