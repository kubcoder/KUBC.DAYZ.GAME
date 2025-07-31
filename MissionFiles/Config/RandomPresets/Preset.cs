using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

/// <summary>
/// Элемент настройки набора
/// </summary>
public abstract class Preset : IXmlSerializable
{
    /// <summary>
    /// Атрибут в котором сохраняется имя
    /// набора
    /// </summary>
    public const string ATTR_NAME = "name";

    /// <summary>
    /// Атрибут в котором сохраняется шанс выпадения
    /// </summary>
    public const string ATTR_CHANCE = "chance";

    /// <summary>
    /// Имя набора
    /// </summary>
    public string? Name;

    /// <summary>
    /// Шанс выпадения
    /// </summary>
    public double Chance = 0.0;

    /// <summary>
    /// Игровые предметы набора
    /// </summary>
    public List<PresetItem> Items = [];

    private System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        Items.Clear();
        while (reader.Read())
        {
            if (reader.IsStartElement())
            {
                if (reader.Name == NodeName)
                {
                    Name = reader.GetAttribute(ATTR_NAME);
                    var chance = reader.GetAttribute(ATTR_CHANCE);
                    double.TryParse(chance, culture, out Chance);
                }
                else
                {
                    var item = new PresetItem();
                    item.ReadXml(reader);
                    if (item.Name != null)
                        Items.Add(item);
                }
            }
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        if (Name == null)
            return;
        writer.WriteStartElement(NodeName);
        writer.WriteAttributeString(ATTR_CHANCE, Chance.ToString(culture));
        writer.WriteAttributeString(ATTR_NAME, Name);
        foreach (var item in Items)
        {
            item.WriteXml(writer);
        }
        writer.WriteEndElement();
    }

    /// <summary>
    /// Имя узла набора
    /// </summary>
    protected abstract string NodeName { get; }
}
