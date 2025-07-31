using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

/// <summary>
/// Элемент набора игровых предметов
/// </summary>
public class PresetItem : IXmlSerializable
{
    /// <summary>
    /// Имя узла игрового предмета
    /// </summary>
    public const string NODENAME = "item";

    /// <summary>
    /// Атрибут в котором сохраняется имя
    /// класса игрового предмета
    /// </summary>
    public const string ATTR_NAME = "name";

    /// <summary>
    /// Атрибут в котором сохраняется шанс выпадения
    /// </summary>
    public const string ATTR_CHANCE = "chance";

    /// <summary>
    /// Имя класса игрового предмета
    /// </summary>
    public string? Name;

    /// <summary>
    /// Шанс выпадения
    /// </summary>
    public double Chance = 0.5;

    private System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        if (reader.IsStartElement())
        {
            if (reader.Name == NODENAME)
            {
                Name = reader.GetAttribute(ATTR_NAME);
                var chance = reader.GetAttribute(ATTR_CHANCE);
                double.TryParse(chance, culture, out Chance);
            }
        }
    }



    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        if (Name != null)
        {
            writer.WriteStartElement(NODENAME);
            writer.WriteAttributeString(ATTR_NAME, Name);
            writer.WriteAttributeString(ATTR_CHANCE, Chance.ToString(culture));
            writer.WriteEndElement();
        }
    }
}
