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
/// Значения по умолчанию центральной экономики
/// </summary>
public class Defaults : Dictionary<string, string?>, IXmlSerializable
{

    /// <summary>
    /// имя тэга XML элемента
    /// </summary>
    public const string ROOT_NODE_NAME = "defaults";

    private const string NODE_NAME = "default";

    private const string ATTR_NAME = "name";

    private const string ATTR_VALUE = "value";

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
            if ((reader.IsStartElement())&&(reader.Name==NODE_NAME))
            {
                AddElement(reader);
            }
        }
    }

    private void AddElement(XmlReader reader)
    {
        var name = reader.GetAttribute(ATTR_NAME);
        if (!string.IsNullOrEmpty(name))
        {
            Add(name, reader.GetAttribute(ATTR_VALUE));
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        foreach(var item in this)
        {
            writer.WriteStartElement(NODE_NAME);
            writer.WriteAttributeString(ATTR_NAME,item.Key);
            writer.WriteAttributeString(ATTR_VALUE, item.Value);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
    }
}
