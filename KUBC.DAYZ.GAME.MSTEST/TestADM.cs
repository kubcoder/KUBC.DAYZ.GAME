using KUBC.DAYZ.GAME.LogFiles;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// Класс колличественного показателя событий в журнале
    /// </summary>
    internal class ADMCounters
    {
        private readonly int BledOut = 0;
        private readonly int Built = 0;
        private readonly int Chat = 0;
        private readonly int Dismantled = 0;
        private readonly int DugIn = 0;
        private readonly int DugOut = 0;
        private readonly int Folded = 0;
        private readonly int Lowered = 0;
        private readonly int Raised = 0;
        private readonly int Unmounted = 0;
        private readonly int Mounted = 0;
        private readonly int Packed = 0;
        private readonly int Placed = 0;
        private readonly int PlayerConnect = 0;
        private readonly int PlayerDisconnect = 0;
        private readonly int PlayerDamage = 0;
        private readonly int PlayerDied = 0;
        private readonly int PlayerKilled = 0;
        private readonly int PlayerList = 0;
        private readonly int Regained = 0;
        private readonly int Report = 0;
        private readonly int Suicide = 0;
        private readonly int Unconscious = 0;

        public int LinesInFile = 0;

        public ADMCounters(FileInfo testFile)
        {
            using StreamReader fileReader = new (testFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                LinesInFile++;
                if (line.Contains("bled out", StringComparison.OrdinalIgnoreCase))
                    BledOut++;
                if (line.Contains("Built", StringComparison.OrdinalIgnoreCase))
                    Built++;
                if (line.Contains("Chat", StringComparison.OrdinalIgnoreCase))
                    Chat++;
                if (line.Contains("Dismantled", StringComparison.OrdinalIgnoreCase))
                    Dismantled++;
                if (line.Contains("Dug in", StringComparison.OrdinalIgnoreCase))
                    DugIn++;
                if (line.Contains("Dug out", StringComparison.OrdinalIgnoreCase))
                    DugOut++;
                if (line.Contains("folded", StringComparison.OrdinalIgnoreCase))
                    Folded++;
                if (line.Contains("has lowered", StringComparison.OrdinalIgnoreCase))
                    Lowered++;
                if (line.Contains("has raised", StringComparison.OrdinalIgnoreCase))
                    Raised++;
                if (line.Contains("Mounted"))
                    Mounted++;
                if (line.Contains("Unmounted", StringComparison.OrdinalIgnoreCase))
                    Unmounted++;
                if (line.Contains("packed", StringComparison.OrdinalIgnoreCase))
                    Packed++;
                if (line.Contains("placed", StringComparison.OrdinalIgnoreCase))
                    Placed++;
                if (line.Contains("is connected", StringComparison.OrdinalIgnoreCase))
                    PlayerConnect++;
                if (line.Contains("has been disconnected", StringComparison.OrdinalIgnoreCase))
                    PlayerDisconnect++;
                if (line.Contains("hit by", StringComparison.OrdinalIgnoreCase))
                    PlayerDamage++;
                if (line.Contains("died. Stats>", StringComparison.OrdinalIgnoreCase))
                    PlayerDied++;
                if (line.Contains("killed by", StringComparison.OrdinalIgnoreCase))
                    PlayerKilled++;
                if (line.Contains("PlayerList log", StringComparison.OrdinalIgnoreCase))
                    PlayerList++;
                if (line.Contains("regained consciousness", StringComparison.OrdinalIgnoreCase))
                    Regained++;
                if (line.Contains("PLAYER REPORT:", StringComparison.OrdinalIgnoreCase))
                    Report++;
                if (line.Contains("committed suicide", StringComparison.OrdinalIgnoreCase))
                    Suicide++;
                if (line.Contains("is unconscious", StringComparison.OrdinalIgnoreCase))
                    Unconscious++;
                line = fileReader.ReadLine();
            }
        }

        public ADMCounters(IEnumerable<ILogEntity> entitites)
        {
            foreach (var entity in entitites)
            {


                if (entity.GetType() == typeof(GAME.LogFiles.ADM.BledOut))
                {
                    BledOut++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Dismantled))
                {
                    Dismantled++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Built))
                {
                    Built++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Chat))
                {
                    Chat++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.DugIn))
                {
                    DugIn++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.DugOut))
                {
                    DugOut++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Folded))
                {
                    Folded++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Raised))
                {
                    Raised++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Lowered))
                {
                    Lowered++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Unmounted))
                {
                    Unmounted++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Mounted))
                {
                    Mounted++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Packed))
                {
                    Packed++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Placed))
                {
                    Placed++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.PlayerConnect))
                {
                    PlayerConnect++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.PlayerDisconnect))
                {
                    PlayerDisconnect++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.PlayerDamage))
                {
                    PlayerDamage++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.PlayerDied))
                {
                    PlayerDied++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.PlayerKilled))
                {
                    PlayerKilled++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.PlayerList))
                {
                    PlayerList++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Regained))
                {
                    Regained++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Report))
                {
                    Report++;
                    continue;
                }
                
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Unconscious))
                {
                    Unconscious++;
                    continue;
                }
                if (entity.GetType() == typeof(GAME.LogFiles.ADM.Suicide))
                {
                    Suicide++;
                    continue;
                }
            }
        }
        /// <summary>
        /// Проверить правильность.
        /// </summary>
        /// <param name="readed"></param>
        public void Chek(ADMCounters readed)
        {
            Console.WriteLine($"Данных о BledOut в логе {BledOut} загружено как данных {readed.BledOut}");
            Assert.AreEqual(BledOut, readed.BledOut);
            Console.WriteLine($"Данных о Built в логе {Built} загружено как данных {readed.Built}");
            Assert.AreEqual(Built, readed.Built);
            Console.WriteLine($"Данных о Chat в логе {Chat} загружено как данных {readed.Chat}");
            Assert.AreEqual(Chat, readed.Chat);
            Console.WriteLine($"Данных о Dismantled в логе {Dismantled} загружено как данных {readed.Dismantled}");
            Assert.AreEqual(Dismantled, readed.Dismantled);
            Console.WriteLine($"Данных о DugIn в логе {DugIn} загружено как данных {readed.DugIn}");
            Assert.AreEqual(DugIn, readed.DugIn);
            Console.WriteLine($"Данных о DugOut в логе {DugOut} загружено как данных {readed.DugOut}");
            Assert.AreEqual(DugOut, readed.DugOut);
            Console.WriteLine($"Данных о Folded в логе {Folded} загружено как данных {readed.Folded}");
            Assert.AreEqual(Folded, readed.Folded);
            Console.WriteLine($"Данных о Lowered в логе {Lowered} загружено как данных {readed.Lowered}");
            Assert.AreEqual(Lowered, readed.Lowered);
            Console.WriteLine($"Данных о Raised в логе {Raised} загружено как данных {readed.Raised}");
            Assert.AreEqual(Raised, readed.Raised);
            Console.WriteLine($"Данных о Mounted в логе {Mounted} загружено как данных {readed.Mounted}");
            Assert.AreEqual(Mounted, readed.Mounted);
            Console.WriteLine($"Данных о Unmounted в логе {Unmounted} загружено как данных {readed.Unmounted}");
            Assert.AreEqual(Unmounted, readed.Unmounted);
            Console.WriteLine($"Данных о Packed в логе {Packed} загружено как данных {readed.Packed}");
            Assert.AreEqual(Packed, readed.Packed);
            Console.WriteLine($"Данных о Placed в логе {Placed} загружено как данных {readed.Placed}");
            Assert.AreEqual(Placed, readed.Placed);
            Console.WriteLine($"Данных о PlayerConnect в логе {PlayerConnect} загружено как данных {readed.PlayerConnect}");
            Assert.AreEqual(PlayerConnect, readed.PlayerConnect);
            Console.WriteLine($"Данных о PlayerDisconnect в логе {PlayerDisconnect} загружено как данных {readed.PlayerDisconnect}");
            Assert.AreEqual(PlayerDisconnect, readed.PlayerDisconnect);
            Console.WriteLine($"Данных о PlayerDamage в логе {PlayerDamage} загружено как данных {readed.PlayerDamage}");
            Assert.AreEqual(PlayerDamage, readed.PlayerDamage);
            Console.WriteLine($"Данных о PlayerDied в логе {PlayerDied} загружено как данных {readed.PlayerDied}");
            Assert.AreEqual(PlayerDied, readed.PlayerDied);
            Console.WriteLine($"Данных о PlayerKilled в логе {PlayerKilled} загружено как данных {readed.PlayerKilled}");
            Assert.AreEqual(PlayerKilled, readed.PlayerKilled);
            Console.WriteLine($"Данных о PlayerList в логе {PlayerList} загружено как данных {readed.PlayerList}");
            Assert.AreEqual(PlayerList, readed.PlayerList);
            Console.WriteLine($"Данных о Regained в логе {Regained} загружено как данных {readed.Regained}");
            Assert.AreEqual(Regained, readed.Regained);
            Console.WriteLine($"Данных о Report в логе {Report} загружено как данных {readed.Report}");
            Assert.AreEqual(Report, readed.Report);
            Console.WriteLine($"Данных о Suicide в логе {Suicide} загружено как данных {readed.Suicide}");
            Assert.AreEqual(Suicide, readed.Suicide);
            Console.WriteLine($"Данных о Unconscious в логе {Unconscious} загружено как данных {readed.Unconscious}");
            Assert.AreEqual(Unconscious, readed.Unconscious);
        }
    }


    [TestClass]
    public class TestADM
    {
        static FileInfo GetTestFile() => new("TestFiles\\GameLogs\\LOG.ADM");

        readonly List<string> UnknowLines = [];

        /// <summary>
        /// Тест чтения законченого лога ADM
        /// </summary>
        [TestMethod]
        public void ReadFullLog()
        {
            var file = GetTestFile();
            var fileStat = new ADMCounters(file);
            var adm = new GAME.LogFiles.ADM.Log();
            UnknowLines.Clear();
            adm.UnknowString += Adm_UnknowString;
            adm.OpenFile(file);
            var sTime = DateTime.Now;
            var logEntities = adm.ReadToEnd();
            var eTime = DateTime.Now;
            Console.WriteLine($"Время чтения лога{eTime.Subtract(sTime)}");
            Assert.IsNotNull(logEntities);
            var loadStat = new ADMCounters(logEntities);
            fileStat.Chek(loadStat);

            Console.WriteLine($"В файле {fileStat.LinesInFile} строк, не смогли прочитать {UnknowLines.Count}");
        }

        private void Adm_UnknowString(object? sender, string e)
        {
            Console.WriteLine(e);
            UnknowLines.Add(e);
        }

        /// <summary>
        /// Тестируем событие чтения BledOut
        /// </summary>
        [TestMethod]
        public void BledOut()
        {
            string Line = "20:11:08 | Player \"(Admin) kot23rus\" (DEAD) (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<12745.6, 9671.5, 6.0>) bled out";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.BledOutParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения Built
        /// </summary>
        [TestMethod]
        public void Built()
        {
            var parser = new GAME.LogFiles.ADM.BuiltParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("Built"))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();

                }
                line = fileReader.ReadLine();
            }
        }
        /// <summary>
        /// Тестируем событие чтения Chat
        /// </summary>
        [TestMethod]
        public void Chat()
        {
            string Line = "18:54:27 | Chat(\"KIRYAESHKA\"(id=rC10G8v_XtecP_lavlTpkJRjRM3Gz1M9MAjTeMy66_c=)): кожу делать";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.ChatParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }

        /// <summary>
        /// Тестируем событие чтения Dismantled
        /// </summary>
        [TestMethod]
        public void Dismantled()
        {
            string Line = "19:55:57 | Player \"Zorro\" (id=OxjFoUFrQmU2hecaqJd6RRxgtqhaTMg_jZY_lHiGh8s= pos=<3442.7, 12323.7, 239.4>)Dismantled Нижняя деревянная стена from Ворота with Топорик";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.DismantledParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения DugIn
        /// </summary>
        [TestMethod]
        public void DugIn()
        {
            string Line = "19:55:57 | Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<271.3, 798.5, 560.0>)Player SurvivorBase<5f3d7020> Dug in SeaChest<fd1db930> at position 0x000000006f8c82e0 {<270.87,559.957,797.327>}";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.DugInParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения DugOut
        /// </summary>
        [TestMethod]
        public void DugOut()
        {
            string Line = "19:55:57 | Player \"(Admin) Berserk\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<271.3, 798.5, 560.0>)Player SurvivorBase<5f3d7020> Dug out UndergroundStash<ff33bfe0> at position 0x000000006f8c82e0 {<270.87,560.177,797.327>}";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.DugOutParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения Folded
        /// </summary>
        [TestMethod]
        public void Folded()
        {
            string Line = "19:55:57 | Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<274.5, 800.3, 560.2>) folded Сторожевая башня";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.FoldedParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения Folded
        /// </summary>
        [TestMethod]
        public void Lowered()
        {
            string Line = "19:55:57 | Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<6965.6, 3506.5, 43.6>) has lowered Flag_LivoniaPolice on TerritoryFlag at <6965.393555, 42.649849, 3507.230225>";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.LoweredParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения Folded
        /// </summary>
        [TestMethod]
        public void Raised()
        {
            string Line = "19:55:57 | Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<6966.1, 3506.0, 42.9>) has raised Flag_LivoniaPolice on TerritoryFlag at<6965.393555, 42.649849, 3507.230225>";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.RaisedParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }

        /// <summary>
        /// Тестируем событие чтения Unmounted
        /// </summary>
        [TestMethod]
        public void Unmounted()
        {
            string Line = "19:55:57 | Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<276.1, 803.3, 560.4>)Player SurvivorBase<5f3d7020> Unmounted BarbedWire from Fence";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.UnmountedParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения Mounted
        /// </summary>
        [TestMethod]
        public void Mounted()
        {
            var parser = new GAME.LogFiles.ADM.MountedParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("Mounted"))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();

                }
                line = fileReader.ReadLine();
            }
        }

        /// <summary>
        /// Тестируем событие чтения Packed
        /// </summary>
        [TestMethod]
        public void Packed()
        {
            string Line = "19:55:57 | Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<279.7, 805.7, 560.7>) packed Автомобильная палатка with Hands ";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.PackedParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }
        /// <summary>
        /// Тестируем событие чтения Placed
        /// </summary>
        [TestMethod]
        public void Placed()
        {
            var parser = new GAME.LogFiles.ADM.PlacedParser();
            using StreamReader fileReader = new (GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("Placed", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();
                }
                line = fileReader.ReadLine();
            }

        }
        /// <summary>
        /// Тестируем событие чтения Packed
        /// </summary>
        [TestMethod]
        public void PlayerConnect()
        {
            string Line = "19:55:57 | Player \"snutik\" is connected (id=Lnb_j2h9D7rznkixOOc3H59XRXlFQM5uu-F_ge9_cms=)";
            Console.WriteLine(Line);
            var parser = new GAME.LogFiles.ADM.PlayerConnectParser();
            var entity = parser.CreateEntity(Line);
            Assert.IsNotNull(entity);
            Console.WriteLine(entity.GetXML());
        }

        /// <summary>
        /// Тестируем событие чтения Disconnect
        /// </summary>
        [TestMethod]
        public void Disconect()
        {
            var parser = new GAME.LogFiles.ADM.PlayerDisconnectParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("has been disconnected", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();
                }
                line = fileReader.ReadLine();
            }
        }
        /// <summary>
        /// Тестируем событие чтения Damage
        /// </summary>
        [TestMethod]
        public void Damage()
        {
            var parser = new GAME.LogFiles.ADM.PlayerDamageParser();
            using StreamReader fileReader = new (GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("hit by", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();
                }
                line = fileReader.ReadLine();
            }
        }
        /// <summary>
        /// Тестируем событие чтения PlayerDied
        /// </summary>
        [TestMethod]
        public void PlayerDied()
        {
            var parser = new GAME.LogFiles.ADM.PlayerDiedParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("died. Stats>", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();
                }
                line = fileReader.ReadLine();
            }
        }
        /// <summary>
        /// Тестируем событие чтения PlayerKilled
        /// </summary>
        [TestMethod]
        public void PlayerKilled()
        {
            var parser = new GAME.LogFiles.ADM.PlayerKilledParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("killed by>", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();
                }
                line = fileReader.ReadLine();
            }
        }
        /// <summary>
        /// Тестируем событие чтения PlayerKilled
        /// </summary>
        [TestMethod]
        public void PlayerKList()
        {
            var parser = new GAME.LogFiles.ADM.PlayerListParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            var Report = new List<string>();
            bool ReadReport = false;
            while (line != null)
            {
                if (ReadReport)
                {
                    Report.Add(line);
                    if (line.Contains("#####"))
                    {
                        ILogEntity? entity = null;
                        foreach (var l in Report)
                        {
                            Console.WriteLine(l);
                            if (entity == null)
                            {
                                entity = parser.CreateEntity(l);
                            }
                            else
                            {
                                if (!entity.IsEndRead())
                                {
                                    entity.AppendLine(l);
                                }
                            }
                        }
                        Console.WriteLine("----------------");
                        Assert.IsNotNull(entity);
                        Assert.IsTrue(entity.IsEndRead());
                        Console.WriteLine(entity.GetXML());
                        Console.WriteLine();
                        Console.WriteLine("_______________________________");
                    }
                }
                else
                {
                    if (line.Contains("PlayerList log", StringComparison.OrdinalIgnoreCase))
                    {
                        Report.Clear();
                        ReadReport = true;
                        Report.Add(line);
                    }
                }
                line = fileReader.ReadLine();
            }
        }

        /// <summary>
        /// Тестируем событие чтения PlayerKilled
        /// </summary>
        [TestMethod]
        public void Suicide()
        {
            var parser = new GAME.LogFiles.ADM.SuicideParser();
            using StreamReader fileReader = new(GetTestFile().Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            var line = fileReader.ReadLine();
            while (line != null)
            {
                if (line.Contains("committed suicide", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(line);
                    var entity = parser.CreateEntity(line);
                    Assert.IsNotNull(entity);
                    Console.WriteLine(entity.GetXML());
                    Console.WriteLine();
                }
                line = fileReader.ReadLine();
            }
        }

    }
}
