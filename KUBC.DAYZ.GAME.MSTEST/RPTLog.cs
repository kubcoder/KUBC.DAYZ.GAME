using KUBC.DAYZ.GAME.LogFiles;
using KUBC.DAYZ.GAME.LogFiles.RPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// Класс тестов RPT
    /// </summary>
    [TestClass]
    public class RPTLog
    {
        [TestMethod]
        public void ConectEvent()
        {
            var Line = "          1:32:27.205 Player \"CheK\" is connected (steamID=76561198849145553)";
            GAME.LogFiles.RPT.ConectEvent e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.RPT.ConectEvent.FromXML(xml);
            Assert.IsNotNull(rE,"Не смогли прочитать данные из xml");
            Assert.AreEqual(rE.NickName, e.NickName, "Не загрузилось имя юзера");
            Assert.AreEqual(rE.SteamID, e.SteamID, "Не загрузился идентификатор юзера");
            Assert.AreEqual(rE.ConnectTime, e.ConnectTime, "Не загрузилась дата подключения юзера");
        }
        [TestMethod]
        public void AverageFPS() 
        {
            var Line = " 1:21:30.266 Average server FPS: 239.30 (measured interval: 60 s)";
            GAME.LogFiles.RPT.AverageFPS e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.RPT.AverageFPS.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
            Assert.AreEqual(rE.FPS, e.FPS, "Не загрузилось FPS");
            Assert.AreEqual(rE.MeasuredTime, e.MeasuredTime, "Не загрузилось время съема данных");
        }
        [TestMethod]
        public void UsedMemory()
        {
            var Line = "1:21:30.266 Used memory: 2189521 KB";
            GAME.LogFiles.RPT.UsedMemory e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.RPT.UsedMemory.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
            Assert.AreEqual(rE.MemoryKB, e.MemoryKB, "Не загрузилось использование памяти");
            Assert.AreEqual(rE.MeasuredTime, e.MeasuredTime, "Не загрузилось время съема данных");
        }

    }
}
