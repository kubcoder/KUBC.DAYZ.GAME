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
            var rpt = new GAME.LogFiles.RPT.Log();
            rpt.OpenFile(new FileInfo("TestFiles\\GameLogs\\LOG.RPT"));
            var entity = rpt.ReadToEnd();
        }


        /// <summary>
        /// Проверяем как работает сереализатор
        /// </summary>
        [TestMethod]
        public void TestAverageFPS()
        {
            string TestLine = "4:43:44.752 Average server FPS: 289.73 (measured interval: 60 s)";
            var parser = new GAME.LogFiles.RPT.AverageFPSParser();
            var fps = parser.CreateEntity(TestLine);
            Assert.IsNotNull(fps);
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
    }
}
