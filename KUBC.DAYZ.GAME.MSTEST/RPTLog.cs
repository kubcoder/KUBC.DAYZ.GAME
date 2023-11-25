﻿using KUBC.DAYZ.GAME.LogFiles.RPT;
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
            GAME.LogFiles.RPT.ConectEvent e = new(Line);
            Assert.IsTrue(e.IsReadOk, "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
        }
        [TestMethod]
        public void AverageFPS() 
        {
            var Line = " 1:21:30.266 Average server FPS: 239.30 (measured interval: 60 s)";
            GAME.LogFiles.RPT.AverageFPS e = new(Line);
            Assert.IsTrue(e.IsReadOk, "Не смогли прочитать правильную строчку");
        }
        
    }
}