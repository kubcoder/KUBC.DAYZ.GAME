using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class CrashLog
    {
        [TestMethod]
        public void ParseCrash()
        {
            var log = "------------------------------------\r\nXSERVER, 24.11 2023 21:31:49\r\n\r\n[wpnfsm] SurvivorM_Cyril:011 warning - pending event already posted, curr_event={ WpnEv id=SWAP_MAGAZINE pl=SurvivorM_Cyril:011 mag=Mag_AK74_30Rnd:026851 } new_event={ WpnEv id=DETACH_MAGAZINE pl=SurvivorM_Cyril:011 mag=Mag_AK74_30Rnd:07618 }\r\nClass:      'DayZPlayerInventory'\r\nFunction: 'Error'\r\nStack trace:\r\nscripts/1_Core/proto\\endebug.c:92\r\nscripts/4_World/systems\\inventory\\dayzplayerinventory.c:292\r\nscripts/4_World/classes\\weapons\\weaponmanager.c:774\r\nscripts/4_World/classes\\weapons\\weaponmanager.c:889\r\nscripts/4_World/entities\\manbase\\playerbase.c:2835\r\n\r\nRuntime mode\r\nCLI params: adminLog  doLogs  netLog  port 2302 profiles mpmissions\\kub.chernarusplus\\profiles freezeCheck  config serverDZ.cfg serverMod @EraAlcohol;@EraChat;@EraCmd;@EraItems;@EraSpawn; \r\n\r\n";
            var e = new GAME.LogFiles.Crash.Entity();
            var reader = new StringReader(log);
            var line = reader.ReadLine();
            if (line!=null)
            {
                Assert.IsTrue(e.Init(line));
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (!e.IsReadSucces())
                    {
                        if (line != null)
                        {

                            if (e.AppendLine(line))
                            {

                            }
                        }
                    }
                }
            }
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = LogFiles.Crash.Entity.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void ParseLog()
        {
            var fLog = new FileInfo("TestFiles\\GameLogs\\crash.log");
            var tLog = new LogFiles.Crash.Log(fLog);
            Console.WriteLine($"Из файла {fLog.Name} прочитано {tLog.Read()} строк");
            if (tLog.Errors.Count > 0)
            {
                Console.WriteLine($"При чтении лога было {tLog.Errors} ошибок");
                foreach (var e in tLog.Errors)
                {
                    Console.WriteLine("==========================================================");
                    Console.WriteLine($"Исходная строчка [{e.SourceLine}]");
                    Console.WriteLine($"Ошибка:{e.Exception?.Message}");
                }
            }
            else
            {
                Console.WriteLine("При чтении не было ошибочек");
            }
            tLog.CloseFile();
            Console.WriteLine($"Было найдено крашей:{tLog.Crashes.Count}");
            Assert.AreNotEqual(tLog.Crashes.Count, 0, "Чет не прочитали краши, а должны были...");
        }
    }
}
