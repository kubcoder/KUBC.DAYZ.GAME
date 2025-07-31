using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

/// <summary>
/// Инструменты для работы с файлом cfgrandompresets.xml
/// </summary>
public class CfgRandomPresets : List<Preset>, IXmlSerializable
{
    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <summary>
    /// Имя узла
    /// </summary>
    public const string NODENAME = "randompresets";

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        this.Clear();
        var canRead = reader.Read();
        while (canRead)
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case Cargo.NODENAME:
                            var cargo = new Cargo();
                            cargo.ReadXml(reader.ReadSubtree());
                            if (cargo.Name != null)
                                this.Add(cargo);
                            break;
                        case Attachments.NODENAME:
                            var attachments = new Attachments();
                            attachments.ReadXml(reader.ReadSubtree());
                            if (attachments.Name != null)
                                this.Add(attachments);
                            break;
                        default:
                            canRead = reader.Read();
                            break;
                    }
                }
                else
                    canRead = reader.Read();
            }
            else
                canRead = reader.Read();
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartDocument(true);
        writer.WriteStartElement(NODENAME);
        foreach (var preset in this)
        {
            preset.WriteXml(writer);
        }
        writer.WriteEndElement();
        writer.WriteEndDocument();
    }
}
