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
        private int averageFps = 0;
        private int usedMemory = 0;
        private int connectEvent = 0;

        private static FileInfo GetTestFile() => new FileInfo("TestFiles\\GameLogs\\LOG.RPT");

        private const string TAG_AVERAGEFPS = "Average server FPS";

        private const string TAG_USEDMEMORY = "Used memory";

        private const string TAG_STEAMID = "steamID";

        private void CalculateEvents(FileInfo testFile)
        {
            averageFps = 0;
            usedMemory = 0;
            connectEvent = 0;
            using (StreamReader fileReader = new StreamReader(testFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                var line = fileReader.ReadLine();
                while (line != null)
                {
                    if (line.Contains(TAG_AVERAGEFPS))
                        averageFps++;
                    if (line.Contains(TAG_USEDMEMORY))
                        usedMemory++;
                    if (line.Contains(TAG_STEAMID, StringComparison.OrdinalIgnoreCase))
                        connectEvent++;
                    line = fileReader.ReadLine();
                }
            }
        }

        int lAverageFps = 0;
        int lUsedMemory = 0;
        int lConnectEvent = 0;

        private void CalculateLoadEvents(IEnumerable<ILogEntity> Events)
        {
            lAverageFps = 0;
            lUsedMemory = 0;
            lConnectEvent = 0;
            foreach (var entity in Events)
            {
                if (entity is GAME.LogFiles.RPT.AverageFPS)
                    lAverageFps++;
                if (entity is GAME.LogFiles.RPT.UsedMemory)
                    lUsedMemory++;
                if (entity is GAME.LogFiles.RPT.ConnectEvent)
                    lConnectEvent++;
            }
        }

        /// <summary>
        /// Проверка чтения RPT лога
        /// </summary>
        [TestMethod]
        public void ReadRPT()
        {
            var testFile = GetTestFile();
            CalculateEvents(testFile);
            var rpt = new GAME.LogFiles.RPT.Log();
            rpt.OpenFile(testFile);
            var sTime = DateTime.Now;
            var Events = rpt.ReadToEnd();
            var eTime = DateTime.Now;
            Console.WriteLine($"Время чтения лога{eTime.Subtract(sTime)}");
            Assert.IsNotNull(Events);
            CalculateLoadEvents(Events);
            CheckResult();
        }
        /// <summary>
        /// Тупо сравниваем что в файле и что мы смогли прочитать
        /// </summary>
        private void CheckResult()
        {
            Console.WriteLine($"Данных о ФПС в логе {averageFps} загружено как данных {lAverageFps}");
            Assert.AreEqual(averageFps, lAverageFps);
            Console.WriteLine($"Данных о памяти в логе {usedMemory} загружено как данных {lUsedMemory}");
            Assert.AreEqual(usedMemory, lUsedMemory);
            Console.WriteLine($"Данных о подключениях игроков в логе {connectEvent} загружено как данных {lConnectEvent}");
            Assert.AreEqual(connectEvent, lConnectEvent);
        }

        /// <summary>
        /// Проверка чтения RPT лога который еще пишется
        /// </summary>
        /// <remarks>
        /// Косяк в том что файл читаем находу с дочитыванием.
        /// И бывает что строчка порвана, т.е. не дописана до конца и нужно понять работает ли дочитывание или 
        /// где то косяк.
        /// </remarks>
        [TestMethod]
        public void ReadActiveRPT()
        {
            var sourceFile = GetTestFile();
            var SourceLines = new List<string>();
            using (var fileReader = new StreamReader(sourceFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
            {
                var line = fileReader.ReadLine();
                while (line != null)
                {
                    SourceLines.Add(line);
                    line = fileReader.ReadLine();
                }
            }
            var testFile = new FileInfo("test.rpt");
            var rpt = new GAME.LogFiles.RPT.Log();
            rpt.OpenFile(testFile);
            var Events = new List<ILogEntity>();
            var rnd = new Random();
            using (var writer = testFile.CreateText())
            {
                for (int i = 0; i < SourceLines.Count; i++) 
                {
                    if (i<100)
                    {
                        writer.WriteLine(SourceLines[i]);
                    }
                    else
                    {
                        if (HasTestTag(SourceLines[i]))
                        {
                            var t = rnd.Next(0,SourceLines[i].Length);
                            writer.Write(SourceLines[i].Substring(0, t));
                            writer.Flush();
                            var re = rpt.ReadToEnd();
                            if (re != null)
                                Events.AddRange(re);
                            writer.WriteLine(SourceLines[i].Substring(t));
                        }
                        else
                        {
                            writer.WriteLine(SourceLines[i]);
                        }
                    }
                }
                writer.Flush();
            }
            var endRead = rpt.ReadToEnd();
            if (endRead != null)
                Events.AddRange(endRead);
            CalculateEvents(testFile);
            CalculateLoadEvents(Events);
            CheckResult();
        }
        /// <summary>
        /// Проверяем есть ли в строчке интересующие нас данные
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool HasTestTag(string line)
        {
            if (line.Contains(TAG_STEAMID))
                return true;
            if (line.Contains(TAG_USEDMEMORY))
                return true;
            if (line.Contains(TAG_AVERAGEFPS))
                return true;
            return false;
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
                //Console.WriteLine(TestLine); //На больших значениях консолька падает
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
