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
/// Файлы конфигурации центральной
/// экономики
/// </summary>
public class CEConfig : List<CEFile>, IXmlSerializable
{
    /// <summary>
    /// имя тэга XML элемента
    /// </summary>
    public const string ROOT_NODE_NAME = "ce";

    private const string ATTR_FOLDER = "folder";

    private const string DEFAIULT_FOLDER = "db";
    
    /// <summary>
    /// Имя директории 
    /// с файлами конфигурации
    /// </summary>
    public string Folder = DEFAIULT_FOLDER;

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
                switch (reader.Name)
                {
                    case ROOT_NODE_NAME:
                        ReadRoot(reader);
                        break;
                    case CEFile.ROOT_NODE_NAME:
                        ReadFile(reader);
                        break;
                }
            }
        }
    }

    private void ReadFile(XmlReader reader)
    {
        var file = new CEFile();
        file.ReadXml(reader);
        if (!string.IsNullOrEmpty(file.FileName))
            Add(file);
    }

    private void ReadRoot(XmlReader reader)
    {
        var folder = reader.GetAttribute(ATTR_FOLDER);
        if (!string.IsNullOrEmpty(folder))
            Folder = folder;
        else
            Folder = DEFAIULT_FOLDER;
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        writer.WriteAttributeString(ATTR_FOLDER, Folder);
        foreach (var file in this)
            file.WriteXml(writer);
        writer.WriteEndElement();
    }
}
