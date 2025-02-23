using Microsoft.VisualBasic.FileIO;
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
/// Класс работы с файлом
/// cfgeconomycore.xml
/// </summary>
public class CfgEconomyCore : IXmlSerializable
{
    /// <summary>
    /// имя тэга XML элемента
    /// </summary>
    public const string ROOT_NODE_NAME = "economycore";

    /// <summary>
    /// Базовые классы
    /// </summary>
    public RootClassCollection Classes = [];

    /// <summary>
    /// Настройки по умолчанию
    /// центральной экономики
    /// </summary>
    public Defaults? Defaults;

    /// <summary>
    /// Файлы конфигурации
    /// </summary>
    public CEConfig? CE;

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        while(reader.Read())
        {
            if (reader.IsStartElement())
            {
                switch(reader.Name)
                {
                    case RootClassCollection.ROOT_NODE_NAME:
                        Classes.ReadXml(reader.ReadSubtree());
                        break;
                    case Defaults.ROOT_NODE_NAME:
                        Defaults = [];
                        Defaults.ReadXml(reader.ReadSubtree());
                        break;
                    case CEConfig.ROOT_NODE_NAME:
                        CE = [];
                        CE.ReadXml(reader.ReadSubtree());
                        break;
                }
            }
        }
    }
    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        Classes.WriteXml(writer);
        Defaults?.WriteXml(writer);
        CE?.WriteXml(writer);
        writer.WriteEndElement();
    }

}
