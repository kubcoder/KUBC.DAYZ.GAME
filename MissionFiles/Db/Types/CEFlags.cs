using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Флаги центральной экономики
/// </summary>
/// <remarks>
/// Т.е. каким образом центральная экономика учитывает игровой предмет,
/// что центральная экономика может с ним делать.
/// </remarks>
public class CEFlags : IXmlSerializable
{
    /// <summary>
    /// Имя элемента XML
    /// </summary>
    public const string ROOT_NODE_NAME = "flags";

    private const string ATTR_COUNT_IN_CARGO = "count_in_cargo";
    private const string ATTR_COUNT_IN_HOARDER = "count_in_hoarder";
    private const string ATTR_COUNT_IN_MAP = "count_in_map";
    private const string ATTR_COUNT_IN_PLAYER = "count_in_player";
    private const string ATTR_COUNT_CRAFTED = "crafted";
    private const string ATTR_COUNT_DELOOT = "deloot";
    private const string ATTR_VALUE_TRUE = "1";
    private const string ATTR_VALUE_FALSE = "0";

    /// <summary>
    /// Учитывать игровые предметы в контейнерах (ящиках, рюкзаках, багажниках)
    /// </summary>
    public bool InCargo = true;

    /// <summary>
    /// Учитывать игровые предметы в накопителях (бочки, палатки, схроны)
    /// </summary>
    public bool InHoarder = true;
    
    /// <summary>
    /// Учитывать игровые предметы в мире, в общем все что просто 
    /// валяется в домах, на земле и т.д.
    /// </summary>
    public bool InMap = true;

    /// <summary>
    /// Учитывать игровые предметы у игроков.
    /// </summary>
    public bool InPlayer = false;

    /// <summary>
    /// Учитывать созданные предметы
    /// </summary>
    public bool Crafted = false;

    /// <summary>
    /// Разрешить удаление предметов для
    /// пересоздания в другом месте
    /// </summary>
    public bool Deloot = false;

    /// <inheritdoc/>
    public XmlSchema? GetSchema()
    {
        return null;
    }

    /// <inheritdoc/>
    public void ReadXml(XmlReader reader)
    {
        InCargo = ReadAttribute(reader, ATTR_COUNT_IN_CARGO);
        InHoarder = ReadAttribute(reader, ATTR_COUNT_IN_HOARDER);
        InMap = ReadAttribute(reader, ATTR_COUNT_IN_MAP);
        InPlayer = ReadAttribute(reader, ATTR_COUNT_IN_PLAYER);
        Crafted = ReadAttribute(reader, ATTR_COUNT_CRAFTED);
        Deloot = ReadAttribute(reader, ATTR_COUNT_DELOOT);
    }

    private static bool ReadAttribute(XmlReader reader, string attrName, bool defValue = false)
    {
        var aStr = reader.GetAttribute(attrName);
        return aStr switch
        {
            ATTR_VALUE_FALSE => false,
            ATTR_VALUE_TRUE => true,
            _ => defValue,
        };
    }

    /// <inheritdoc/>
    public void WriteXml(XmlWriter writer)
    {
        writer.WriteStartElement(ROOT_NODE_NAME);
        WriteAttribute(writer,ATTR_COUNT_IN_CARGO, InCargo);
        WriteAttribute(writer, ATTR_COUNT_IN_HOARDER, InHoarder);
        WriteAttribute(writer, ATTR_COUNT_IN_MAP, InMap);
        WriteAttribute(writer, ATTR_COUNT_IN_PLAYER, InPlayer);
        WriteAttribute(writer, ATTR_COUNT_CRAFTED, Crafted);
        WriteAttribute(writer, ATTR_COUNT_DELOOT, Deloot);
        writer.WriteEndElement();
    }

    private static void WriteAttribute(XmlWriter writer, string attrName, bool attrValue)
    {
        if (attrValue)
        {
            writer.WriteAttributeString(attrName, ATTR_VALUE_TRUE);
        }
        else
        {
            writer.WriteAttributeString(attrName, ATTR_VALUE_FALSE);
        }
        
    }

    
}
