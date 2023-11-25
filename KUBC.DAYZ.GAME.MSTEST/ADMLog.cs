using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class ADMLog
    {
        [TestMethod]
        public void BledOut()
        {
            string Line = "  Player \"kot23rus\" (DEAD) (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<12745.6, 9671.5, 6.0>) bled out";
            GAME.LogFiles.ADM.BledOut e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.BledOut.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Built()
        {
            string Line = "  Player \"Zorro\" (id=OxjFoUFrQmU2hecaqJd6RRxgtqhaTMg_jZY_lHiGh8s= pos=<3442.7, 12324.2, 239.4>)Built base on Забор with Кирка";
            GAME.LogFiles.ADM.Built e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Built.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Chat()
        {
            string Line = "Chat(\"GayZ\"(id=ID34fdoIpGJdd236w9Oh_fu2dP1hDPVnX6NWxhN_gYE=)): для чего шкура волка нужна";
            GAME.LogFiles.ADM.Chat e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Chat.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
    }
}
