using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.Limits.Definition;

/// <summary>
/// Инструменты для работы с файлом
/// cfglimitsdefinition.xml
/// </summary>
public class CfgLimitsDefinition : IXmlSerializable
{
    private const string ROOT_NODE = "lists";

    private const string CATEGORIES_ROT = "categories";
    private const string CATEGORIES_NODE = "category";

    /// <summary>
    /// Имена категорий
    /// </summary>
    public ListNames Categories = new()
    {
        RootNodeName = CATEGORIES_ROT,
        NodeName = CATEGORIES_NODE
    };


    private const string TAGS_ROT = "tags";
    private const string TAGS_NODE = "tag";

    /// <summary>
    /// Имена категорий
    /// </summary>
    public ListNames Tags = new()
    {
        RootNodeName = TAGS_ROT,
        NodeName = TAGS_NODE
    };

    private const string USAGE_ROT = "usageflags";
    private const string USAGE_NODE = "usage";

    /// <summary>
    /// Имена тематических признаков
    /// </summary>
    public ListNames UsageFlags = new()
    {
        RootNodeName = USAGE_ROT,
        NodeName = USAGE_NODE
    };

    private const string VALUE_ROT = "valueflags";
    private const string VALUE_NODE = "value";

    /// <summary>
    /// Имена зон спавна игровых
    /// предметов
    /// </summary>
    public ListNames ValueFlags = new()
    {
        RootNodeName = VALUE_ROT,
        NodeName = VALUE_NODE
    };

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        while (reader.Read())
        {
            if (reader.IsStartElement())
            {
                switch (reader.Name)
                {
                    case CATEGORIES_ROT:
                        Categories.ReadXml(reader.ReadSubtree());
                        break;
                    case TAGS_ROT:
                        Tags.ReadXml(reader.ReadSubtree());
                        break;
                    case USAGE_ROT:
                        UsageFlags.ReadXml(reader.ReadSubtree());
                        break;
                    case VALUE_ROT:
                        ValueFlags.ReadXml(reader.ReadSubtree());
                        break;
                }
            }
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE);
        Categories.WriteXml(writer);
        Tags.WriteXml(writer);
        UsageFlags.WriteXml(writer);
        ValueFlags.WriteXml(writer);
        writer.WriteEndElement();
    }
}
