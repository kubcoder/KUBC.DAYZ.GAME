using KUBC.DAYZ.GAME.LogFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// Тестируем логи
    /// </summary>
    [TestClass]
    public class TestRPT
    {
        /// <summary>
        /// Проверка чтения RPT лога
        /// </summary>
        [TestMethod]
        public void ReadRPT()
        {
            var testFile = new FileInfo("TestFiles\\GameLogs\\LOG.RPT");
            int averageFps = 0;
            int usedMemory = 0;
            int connectEvent = 0;
            using (var fileReader = new StreamReader(testFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                var line = fileReader.ReadLine();
                while(line != null) 
                {
                    if (line.Contains("Average server FPS"))
                        averageFps++;
                    if (line.Contains("Used memory"))
                        usedMemory++;
                    if (line.Contains("steamID", StringComparison.OrdinalIgnoreCase))
                        connectEvent++;
                    line = fileReader.ReadLine();
                }
            }
            var rpt = new GAME.LogFiles.RPT.Log();
            rpt.OpenFile(testFile);
            var Events = rpt.ReadToEnd();
            Assert.IsNotNull(Events);
            int lAverageFps = 0;
            int lUsedMemory = 0;
            int lConnectEvent = 0;
            foreach(var entity in Events)
            {
                if (entity is GAME.LogFiles.RPT.AverageFPS)
                    lAverageFps++;
                if (entity is GAME.LogFiles.RPT.UsedMemory)
                    lUsedMemory++;
                if (entity is GAME.LogFiles.RPT.ConnectEvent)
                    lConnectEvent++;
            }
            Console.WriteLine($"Данных о ФПС в логе {averageFps} загружено как данных {lAverageFps}");
            Assert.AreEqual(averageFps, lAverageFps);
            Console.WriteLine($"Данных о памяти в логе {usedMemory} загружено как данных {lUsedMemory}");
            Assert.AreEqual(usedMemory, lUsedMemory);
            Console.WriteLine($"Данных о подключениях игроков в логе {connectEvent} загружено как данных {lConnectEvent}");
            Assert.AreEqual(connectEvent, lConnectEvent);

        }


        /// <summary>
        /// Проверяем как работает парсер FPS
        /// </summary>
        [TestMethod]
        public void TestAverageFPS()
        {
            string TestLine = "4:43:44.752 Average server FPS: 289.73 (measured interval: 60 s)";
            var parser = new GAME.LogFiles.RPT.AverageFPSParser();
            var fps = parser.CreateEntity(TestLine);
            Assert.IsNotNull(fps);
            Console.WriteLine(fps.GetXML());
            DateTime time = DateTime.Now;
            var rnd = new Random();
            var Culture = System.Globalization.CultureInfo.InvariantCulture;
            long startMemory = GC.GetTotalMemory(true);
            for (int i=0;i<1000000;i++)
            {
                time = time.AddSeconds(1);
                TestLine = $"{time.ToLongTimeString()}.{time.Millisecond} Average server FPS: {(rnd.NextDouble()*10000).ToString("F2",Culture)} (measured interval: 60 s)";
                //Console.WriteLine(TestLine); На больших значениях консолька падает
                fps = parser.CreateEntity(TestLine);
                Assert.IsNotNull(fps);
                fps = null;
            }
            long endMemory = GC.GetTotalMemory(true);
            Console.WriteLine($"Размер кучи перед запуском теста {startMemory}, размер кучи после завершения теста {endMemory}, разница составляет {endMemory-startMemory}");
            endMemory = GC.GetTotalMemory(true);
            Console.WriteLine($"размер кучи после сжатия {endMemory}, разница составляет {endMemory - startMemory}");
        }
        /// <summary>
        /// Проверяем как работает парсер памяти
        /// </summary>
        [TestMethod]
        public void TestUsedMemory()
        {
            string TestLine = "4:43:44.752 Used memory: 2544602 KB";
            var parser = new GAME.LogFiles.RPT.UsedMemoryPaser();
            var memory = parser.CreateEntity(TestLine);
            Assert.IsNotNull(memory);
            Console.WriteLine(memory.GetXML());
        }
        
        /// <summary>
        /// Проверяем чтение события игрок подключен
        /// </summary>
        [TestMethod]
        public void TestConnectPlayer()
        {
            string TestLine = "7:20:41.676 Player \"Survivor\" is connected (steamID=76561198083818124)";
            var parser = new GAME.LogFiles.RPT.ConnectEventParser();
            var connect = parser.CreateEntity(TestLine);
            Assert.IsNotNull(connect);
            Console.WriteLine(connect.GetXML());
        }
    }
}
