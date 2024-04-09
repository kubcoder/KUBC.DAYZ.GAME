using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class TestMissionFiles
    {
        /// <summary>
        /// Получить папочку с тестовыми файлами миссии
        /// </summary>
        /// <returns></returns>
        public static GAME.MissionFiles.Mission GetMission()
        {
            return new MissionFiles.Mission(new("TestFiles\\Mission"));
        }

        /// <summary>
        /// Проверить файл экономики
        /// </summary>
        [TestMethod]
        public void TestEconomy()
        {
            var mission = GetMission();
            var economy = mission.DB.Economy.Load() as GAME.MissionFiles.DB.Economy.EconomyConfig;
            Assert.IsNotNull(economy);
            Assert.AreNotEqual(economy.Count(), 0);
            foreach(var e in economy)
            {
                var sect = e.Value as GAME.MissionFiles.DB.Economy.EconomyEntity;
                Assert.IsNotNull(sect);
                Console.WriteLine($"========== {e.Key} ===============");
                foreach(var ed in sect)
                {
                    Console.WriteLine($"{ed.Key}:{ed.Value}");
                }
            }
            
        }
    }
}
