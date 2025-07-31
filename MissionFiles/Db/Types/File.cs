using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Файл с описанием настроек
/// игровых предметов для центральной
/// экономики сервера
/// </summary>
public class File : List<Item>, IXmlSerializable
{
    /// <summary>
    /// корневой XML тэг коллекции
    /// </summary>
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
        while (reader.Read())
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
    /// <summary>
    /// Читаем коллекцию настроек
    /// </summary>
    /// <param name="reader">Ветки дерева файла types</param>
    private void ReadTypes(XmlReader reader)
    {
        while (reader.Read())
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
        writer.WriteStartElement(ROOT_NODE_NAME);
        foreach (var item in this)
        {
            item.WriteXml(writer);
        }
        writer.WriteEndElement();
    }
}
