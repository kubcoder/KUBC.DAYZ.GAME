using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// Класс начального тестирования работы 
    /// унифицированных сереализаторов.
    /// TODO: после откатки абстракций
    /// удалить данный класс нахуй
    /// </summary>
    [TestClass]
    public class TestConfigs
    {
        [TestMethod]
        public void TestWrite()
        {
            var tEntity = new GAME.MissionFiles.DB.Economy.EconomyConfig();
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlSerializer serializer = new(tEntity.GetType());
            serializer.Serialize(sw, tEntity);
            sw.Flush();
            sw.Close();
            var xml = sb.ToString();
            Console.WriteLine(xml);
            var reader = new StringReader(xml);
            var lEntity = serializer.Deserialize(reader);
            reader.Dispose();
            Assert.AreEqual(tEntity, lEntity);
        }

        [TestMethod]
        public void TestWriteTypes()
        {
            var tEntity = new GAME.MissionFiles.DB.Types.ItemTypes();
            var tItem = new GAME.MissionFiles.DB.Types.Item("ACOGOptic");
            tEntity.SetValue(tItem.SectionName, tItem);
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            XmlSerializer serializer = new(tEntity.GetType());
            serializer.Serialize(sw, tEntity);
            sw.Flush();
            sw.Close();
            var xml = sb.ToString();
            Console.WriteLine(xml);
        }
    }
}
