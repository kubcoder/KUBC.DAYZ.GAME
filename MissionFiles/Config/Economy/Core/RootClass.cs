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
/// Описание корневого класса
/// </summary>
public class RootClass : IXmlSerializable
{
    /// <summary>
    /// Имя узла XML
    /// </summary>
    public const string ROOT_NODE_NAME = "rootclass";

    private const string ATTR_NAME = "name";
    private const string ATTR_ACT = "act";
    private const string ATTR_REPORT_MEM_LOD = "reportMemoryLOD";
    private const string REPORT_MEM_LOD_FALSE = "no";
    private const string REPORT_MEM_LOD_TRUE = "yes";

    /// <summary>
    /// Имя базового класса
    /// </summary>
    public string Name = string.Empty;

    /// <summary>
    /// Зоны действия
    /// </summary>
    public string? Act;

    /// <summary>
    /// Включение/отключение какого то лога
    /// </summary>
    public bool? ReportMemLod;

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        if (reader.Name == ROOT_NODE_NAME)
        {
            var name = reader.GetAttribute(ATTR_NAME);
            if (name != null)
            {
                Name = name;
            }
            Act = reader.GetAttribute(ATTR_ACT);
            var report = reader.GetAttribute(ATTR_REPORT_MEM_LOD);
            switch (report)
            {
                case REPORT_MEM_LOD_FALSE:
                    ReportMemLod = false;
                    break;
                case REPORT_MEM_LOD_TRUE:
                    ReportMemLod = true;
                    break;
                default:
                    ReportMemLod = null;
                    break;
            }
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        writer.WriteAttributeString(ATTR_NAME, Name);
        if (!string.IsNullOrEmpty(Act))
        {
            writer.WriteAttributeString(ATTR_ACT, Act);
        }
        if (ReportMemLod.HasValue)
        {
            if (ReportMemLod.Value)
                writer.WriteAttributeString(ATTR_REPORT_MEM_LOD, REPORT_MEM_LOD_TRUE);
            else
                writer.WriteAttributeString(ATTR_REPORT_MEM_LOD, REPORT_MEM_LOD_FALSE);
        }
        writer.WriteEndElement();
    }
}
