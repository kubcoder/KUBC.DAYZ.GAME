using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.Economy.Core;

/// <summary>
/// Описание файла конфигурации 
/// центральной экономики
/// </summary>
public class CEFile : IXmlSerializable
{
    /// <summary>
    /// имя тэга XML элемента
    /// </summary>
    public const string ROOT_NODE_NAME = "file";

    private const string ATTR_NAME = "name";

    private const string ATTR_TYPE = "type";

    private const string VALUE_TYPE_TYPES = "types";

    /// <summary>
    /// Имя файла
    /// </summary>
    public string FileName = "";

    /// <summary>
    /// Тип файла конфигурации
    /// </summary>
    public string FileType = VALUE_TYPE_TYPES;

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        var name = reader.GetAttribute(ATTR_NAME);
        if (name != null)
            FileName = name;
        var type = reader.GetAttribute(ATTR_TYPE);
        if (!string.IsNullOrEmpty(type))
            FileType = type;
        else
            FileType = VALUE_TYPE_TYPES;
    }
    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        writer.WriteAttributeString(ATTR_NAME, FileName);
        writer.WriteAttributeString(ATTR_TYPE, FileType);
        writer.WriteEndElement();
    }
}
