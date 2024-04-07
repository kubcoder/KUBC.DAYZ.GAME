using KUBC.DAYZ.GAME.LogFiles.RPT;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class TestLogFiles
    {
        /// <summary>
        /// Папочка где лежит куча тестовых файликов
        /// </summary>
        /// <returns></returns>
        protected DirectoryInfo GetTestPath() => new DirectoryInfo("F:\\DAYZ.80\\300\\Profiles");


        /// <summary>
        /// Проверяем что файлик найден реально новым
        /// </summary>
        [TestMethod]
        public void TestFindNewFile()
        {
            List<DateTime> ModifiedTimes = [];
            var rptLogs = new GAME.LogFiles.RPT.RPTLogs(GetTestPath());
            var fileLogs = rptLogs.GetAll();
            Console.WriteLine("Список файлов + время создания + время последней записи");
            foreach (var log in fileLogs) 
            {
                Console.WriteLine($"{log.Name}\t{log.CreationTime}\t{log.LastWriteTime}");
                ModifiedTimes.Add(log.LastWriteTime);
            }
            Console.WriteLine("=============================================================");
            var newsetTime = ModifiedTimes.OrderDescending().FirstOrDefault();
            Console.WriteLine($"Самая новая дата записи:{newsetTime}");
            Console.WriteLine("=============================================================");
            var newsetFile = rptLogs.GetNewest();
            Console.WriteLine($"Новый файл:{newsetFile.Name}\t{newsetFile.CreationTime}\t{newsetFile.LastWriteTime}");
            Assert.AreEqual(newsetTime, newsetFile.LastWriteTime, "Чет не то нашли");
        }

        private GAME.LogFiles.RPT.Log RPT = new();

        [TestMethod]
        public void TestLoadRPT()
        {
            var rptLogs = new GAME.LogFiles.RPT.RPTLogs(GetTestPath());
            var fileLogs = rptLogs.GetAll();
            foreach (var log in fileLogs)
            {
                ParseRPTLog(log);
            }
        }

        private void ParseRPTLog(FileInfo log)
        {
            Console.WriteLine($"Открываем файл {log.Name}");
            RPT.OpenFile(log);
            var fileInfo = new RPTCounters(log);
            var sTime = DateTime.Now;
            var Events = RPT.ReadToEnd();
            var eTime = DateTime.Now;
            Console.WriteLine($"Время чтения лога{eTime.Subtract(sTime)}");
            Assert.IsNotNull(Events);
            var loadedInfo = new RPTCounters(Events);
            fileInfo.CheckResult(loadedInfo);
            Console.WriteLine("==============================================");
        }

        private GAME.LogFiles.ADM.Log ADM = new();

        private void ParseADMLog(FileInfo log)
        {
            Console.WriteLine($"Открываем файл {log.Name}");
            ADM.OpenFile(log);
            var fileInfo = new ADMCounters(log);
            var sTime = DateTime.Now;
            var Events = ADM.ReadToEnd();
            var eTime = DateTime.Now;
            Console.WriteLine($"Время чтения лога{eTime.Subtract(sTime)}");
            Assert.IsNotNull(Events);
            var loadedInfo = new ADMCounters(Events);
            fileInfo.Chek(loadedInfo);
            Console.WriteLine("==============================================");
        }

        [TestMethod]
        public void TestLoadADM()
        {
            var admLogs = new GAME.LogFiles.ADM.ADMLogs(GetTestPath());
            var fileLogs = admLogs.GetAll();
            foreach (var log in fileLogs)
            {
                ParseADMLog(log);
            }
        }

    }
}
