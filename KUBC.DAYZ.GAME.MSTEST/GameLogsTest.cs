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
    public class GameLogsTest
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
    }
}
