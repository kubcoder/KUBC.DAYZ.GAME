using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Настройка игрового предмета 
/// в центральной экономике сервера
/// </summary>
public class Item : IXmlSerializable
{
    /// <summary>
    /// имя тэга XML элемента
    /// </summary>
    public const string ROOT_NODE_NAME = "type";

    private const string ATTR_NAME = "name";

    private const string NODE_NOMINAL = "nominal";

    private const string NODE_LIFETIME = "lifetime";

    private const string NODE_RESTOK = "restock";

    private const string NODE_MIN = "min";

    private const string NODE_QUANT_MIN = "quantmin";

    private const string NODE_QUANT_MAX = "quantmax";

    private const string NODE_COST = "cost";

    private const string NODE_CATEGORY = "category";

    private const string NODE_USAGE = "usage";

    private const string NODE_VALUE = "value";

    private const string NODE_TAG = "tag";

    /// <summary>
    /// Имя класса игрового предмета
    /// </summary>
    public string Name = string.Empty;

    /// <summary>
    /// Номинальное количество предмета 
    /// в игровом мире
    /// </summary>
    public int Nominal = 0;

    /// <summary>
    /// Время жизни игрового предмета
    /// в мире в секундах
    /// </summary>
    public int LifeTime = 0;

    /// <summary>
    /// Через сколько секунд разрешено
    /// удаление предмета из игрового мира
    /// для пересоздания
    /// </summary>
    public int Restock = 0;

    /// <summary>
    /// Минимальное количество экземпляров
    /// по достижению которого начинается
    /// создание новых предметов в мире
    /// </summary>
    public int Min = 0;

    /// <summary>
    /// Минимальное наполнение предмета
    /// при создании предмета в игровом мире
    /// </summary>
    public int QuantMin = 0;

    /// <summary>
    /// Минимальное наполнение предмета
    /// при создании предмета в игровом мире
    /// </summary>
    public int QuantMax = 0;

    /// <summary>
    /// Важность... тут вопросы
    /// </summary>
    public int Cost = 0;

    /// <summary>
    /// Флаги настройки учета количества
    /// игровых предметов в мире
    /// </summary>
    public CEFlags Flags = new();

    /// <summary>
    /// Категории игровых предметов
    /// </summary>
    public List<string> Categories = [];

    /// <summary>
    /// Функциональное назначение предмета.
    /// Используется для тематического размещения
    /// игровых предметов
    /// </summary>
    public List<string> Usages = [];

    /// <summary>
    /// Имена зон ограничивающие создание
    /// предметов
    /// </summary>
    public List<string> Values = [];


    /// <summary>
    /// ТЭГи размещения и использования предмета
    /// </summary>
    public List<string> Tags = [];



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
            if (reader.Name == ROOT_NODE_NAME)
            {
                var itemName = reader.GetAttribute(ATTR_NAME);
                if (itemName != null)
                {
                    Name = itemName;
                    Categories.Clear();
                    Usages.Clear();
                    Values.Clear();
                    Tags.Clear();
                    Read(reader.ReadSubtree());
                }
            }
        }
    }

    /// <summary>
    /// Читаем настройки игрового предмета
    /// </summary>
    /// <param name="reader"></param>
    private void Read(XmlReader reader)
    {
        while (reader.Read())
        {
            if (reader.IsStartElement())
            {
                switch (reader.Name)
                {
                    case NODE_NOMINAL:
                        Nominal = reader.ReadElementContentAsInt();
                        break;
                    case NODE_LIFETIME:
                        LifeTime = reader.ReadElementContentAsInt();
                        break;
                    case NODE_RESTOK:
                        Restock = reader.ReadElementContentAsInt();
                        break;
                    case NODE_MIN:
                        Min = reader.ReadElementContentAsInt();
                        break;
                    case NODE_QUANT_MIN:
                        QuantMin = reader.ReadElementContentAsInt();
                        break;
                    case NODE_QUANT_MAX:
                        QuantMax = reader.ReadElementContentAsInt();
                        break;
                    case NODE_COST:
                        Cost = reader.ReadElementContentAsInt();
                        break;
                    case CEFlags.ROOT_NODE_NAME:
                        Flags.ReadXml(reader);
                        break;
                    case NODE_CATEGORY:
                        ReadAttrName(reader, Categories);
                        break;
                    case NODE_USAGE:
                        ReadAttrName(reader, Usages);
                        break;
                    case NODE_VALUE:
                        ReadAttrName(reader, Values);
                        break;
                    case NODE_TAG:
                        ReadAttrName(reader, Tags);
                        break;

                }
            }
        }
    }

    /// <summary>
    /// Прочитать атрибут с именем
    /// </summary>
    /// <param name="reader">Контекст чтения данных</param>
    /// <param name="target">В какую коллекцию добавить имя</param>
    private static void ReadAttrName(XmlReader reader, List<string> target)
    {
        var name = reader.GetAttribute(ATTR_NAME);
        if (!string.IsNullOrEmpty(name))
        {
            target.Add(name);
        }
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        writer.WriteAttributeString(ATTR_NAME, Name);
        WriteElement(writer, NODE_NOMINAL, Nominal);
        WriteElement(writer, NODE_LIFETIME, LifeTime);
        WriteElement(writer, NODE_RESTOK, Restock);
        WriteElement(writer, NODE_MIN, Min);
        WriteElement(writer, NODE_QUANT_MIN, QuantMin);
        WriteElement(writer, NODE_QUANT_MAX, QuantMax);
        WriteElement(writer, NODE_COST, Cost);
        Flags.WriteXml(writer);
        foreach (var cat in Categories)
        {
            WriteElementAttrName(writer, NODE_CATEGORY, cat);
        }
        foreach (var tag in Tags)
        {
            WriteElementAttrName(writer, NODE_TAG, tag);
        }
        foreach (var usage in Usages)
        {
            WriteElementAttrName(writer, NODE_USAGE, usage);
        }
        foreach (var value in Values)
        {
            WriteElementAttrName(writer, NODE_VALUE, value);
        }
        writer.WriteEndElement();
    }

    private static void WriteElementAttrName(XmlWriter writer, string nodeName, string name)
    {
        writer.WriteStartElement(nodeName);
        writer.WriteAttributeString(ATTR_NAME, name);
        writer.WriteEndElement();
    }

    /// <summary>
    /// Запись в файл элемент с целочисленным значением
    /// </summary>
    /// <param name="writer">контекст записи данных</param>
    /// <param name="nodeName">Имя узла</param>
    /// <param name="value">Значение</param>
    private static void WriteElement(XmlWriter writer, string nodeName, int value)
    {
        writer.WriteStartElement(nodeName);
        writer.WriteValue(value);
        writer.WriteEndElement();
    }
}
