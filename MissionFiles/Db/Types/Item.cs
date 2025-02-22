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
    public const string ROOT_NODE_NAME = "type";

    public const string ATTR_NAME = "name";

    public const string NODE_NOMINAL = "nominal";

    public const string NODE_LIFETIME = "lifetime";

    public const string NODE_RESTOK = "restock";

    public const string NODE_MIN = "min";

    public const string NODE_QUANT_MIN = "quantmin";

    public const string NODE_QUANT_MAX = "quantmax";

    public const string NODE_COST = "cost";

    public const string NODE_CATEGORY = "category";

    public const string NODE_USAGE = "usage";

    public const string NOTE_VALUE = "value";

    public const string NODE_TAG = "tag";

    public string Name = string.Empty;

    public int Nominal = 0;

    public int LifeTime = 0;

    public int Restock = 0;

    public int Min = 0;

    public int QuantMin = 0;

    public int QuantMax = 0;

    public int Cost = 0;

    public CEFlags Flags = new();

    public List<string> Categories = [];

    public List<string> Usages = [];

    public List<string> Values = [];

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

    private void Read(XmlReader reader)
    {
        while(reader.Read())
        {
            if (reader.IsStartElement())
            {
                switch(reader.Name)
                {
                    case NODE_NOMINAL:
                        Nominal = reader.ReadElementContentAsInt();
                        break;
                    case NODE_LIFETIME:
                        LifeTime = reader.ReadElementContentAsInt();
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
                    case NOTE_VALUE:
                        ReadAttrName(reader, Values);
                        break;
                    case NODE_TAG:
                        ReadAttrName(reader, Tags);
                        break;

                }
            }
        }
    }

    private void ReadAttrName(XmlReader reader, List<string> target)
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
        throw new NotImplementedException();
    }
}
