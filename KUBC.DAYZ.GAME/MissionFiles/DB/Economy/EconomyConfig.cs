using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Economy
{
    /// <summary>
    /// Конфигурация экономики
    /// </summary>
    [XmlRoot("economy")]
    public class EconomyConfig:XMLConfig
    {
        /// <summary>
        /// Создание базовых настроек экономики
        /// </summary>
        public EconomyConfig()
        {
            
            SetValue("dynamic", new EconomyEntity("dynamic"));
            SetValue("animals", new EconomyEntity("animals"));
            SetValue("zombies", new EconomyEntity("zombies"));
            SetValue("vehicles", new EconomyEntity("vehicles"));
            SetValue("randoms", new EconomyEntity("randoms"));
            SetValue("custom", new EconomyEntity("custom"));
            SetValue("building", new EconomyEntity("building"));
            SetValue("player", new EconomyEntity("player"));
        }
        /// <inheritdoc/>
        public override void ReadXml(XmlReader reader)
        {
            Params.Clear();
            while(reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    var ee = new EconomyEntity(reader.Name);
                    ee.ReadXml(reader);
                    Params.Add(ee.SectionName, ee);
                }
            }
        }
    }
}
