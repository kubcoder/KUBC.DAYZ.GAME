using KUBC.DAYZ.GAME.LogFiles.Crash;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class TestCrash
    {
        static FileInfo GetTestFile() => new("TestFiles\\GameLogs\\Crash.log");

        [TestMethod]
        public void TestReadFile()
        {
            using GAME.LogFiles.Crash.Log log = new LogFiles.Crash.Log();
            log.OpenFile(GetTestFile());
            var crashes = log.ReadToEnd();
            Assert.IsNotNull(crashes);
            foreach(CrashEntity crash in crashes) 
            {
                if (crash.ChekNotCompile())
                {
                    Console.WriteLine("============== фатальный краш на старте ===============================");
                }
                Console.WriteLine(crash.GetXML());
            }
        }
    }
}
