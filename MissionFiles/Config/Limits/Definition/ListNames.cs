using Microsoft.VisualBasic.FileIO;
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
/// Абстрактный список имен
/// </summary>
public class ListNames : List<string>, IXmlSerializable
{
    private const string ATTR_NAME = "name";

    /// <summary>
    /// Имя корневого узла списка
    /// </summary>
    public required string RootNodeName;

    /// <summary>
    /// Имя узла элемента 
    /// списка
    /// </summary>
    public required string NodeName;

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        Clear();
        while(reader.Read())
        {
            if ((reader.IsStartElement())&&(reader.Name == NodeName))
            {
                var name = reader.GetAttribute(ATTR_NAME);
                if (name != null)
                    Add(name);
            }
        }
    }
    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(RootNodeName);
        foreach(var name in this)
        {
            writer.WriteStartElement(NodeName);
            writer.WriteAttributeString(ATTR_NAME, name);
            writer.WriteEndElement();
        }
        writer.WriteEndElement();
    }
}
