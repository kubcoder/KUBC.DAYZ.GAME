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

    public const string ATTR_COUNT_IN_CARGO = "count_in_cargo";
    public const string ATTR_COUNT_IN_HOARDER = "count_in_hoarder";
    public const string ATTR_COUNT_IN_MAP = "count_in_map";
    public const string ATTR_COUNT_IN_PLAYER = "count_in_player";
    public const string ATTR_COUNT_CRAFTED = "crafted";
    public const string ATTR_COUNT_DELOOT = "deloot";
    public const string ATTR_VALUE_TRUE = "1";
    public const string ATTR_VALUE_FALSE = "0";
    public const string ROOT_NODE_NAME = "flags";

    public bool InCargo = true;
    public bool InHoarder = true;
    public bool InMap = true;
    public bool InPlayer = false;
    public bool Crafted = false;
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

    private bool ReadAttribute(XmlReader reader, string attrName, bool defValue = false)
    {
        var aStr = reader.GetAttribute(attrName);
        switch(aStr)
        {
            case ATTR_VALUE_FALSE:
                return false;
            case ATTR_VALUE_TRUE:
                return true;
            default:
                return defValue;
        }
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

    private void WriteAttribute(XmlWriter writer, string attrName, bool attrValue)
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
