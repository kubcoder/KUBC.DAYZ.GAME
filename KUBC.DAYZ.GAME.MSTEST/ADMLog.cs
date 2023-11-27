using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    /*Закопал откопал морской сундук
        23:53:03 | Player "kot23rus" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<271.3, 798.5, 560.0>)Player SurvivorBase<5f3d7020> Dug in SeaChest<fd1db930> at position 0x000000006f8c82e0 {<270.87,559.957,797.327>}
        23:53:33 | Player "kot23rus" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<271.3, 798.5, 560.0>)Player SurvivorBase<5f3d7020> Dug out UndergroundStash<ff33bfe0> at position 0x000000006f8c82e0 {<270.87,560.177,797.327>}
     */
    [TestClass]
    public class ADMLog
    {
        [TestMethod]
        public void Unmounted()
        {
            string Line = "Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<276.1, 803.3, 560.4>)Player SurvivorBase<5f3d7020> Unmounted BarbedWire from Fence";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Unmounted e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Unmounted.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void Mounted()
        {
            string Line = "Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<272.6, 801.8, 560.1>)Player SurvivorBase<5f3d7020> Mounted BarbedWire on Fence";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Mounted e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Mounted.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void Packed()
        {
            string Line = "Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<279.7, 805.7, 560.7>) packed Автомобильная палатка with Hands ";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Packed e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Packed.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void Folded()
        {
            string Line = "Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<274.5, 800.3, 560.2>) folded Сторожевая башня";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Folded e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Folded.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void Raised()
        {
            string Line = "Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<6966.1, 3506.0, 42.9>) has raised Flag_LivoniaPolice on TerritoryFlag at<6965.393555, 42.649849, 3507.230225>";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Raised e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Raised.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void Lowered()
        {
            string Line = "Player \"kot23rus\" (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<6965.6, 3506.5, 43.6>) has lowered Flag_LivoniaPolice on TerritoryFlag at <6965.393555, 42.649849, 3507.230225>";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Lowered e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Lowered.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void BledOut()
        {
            string Line = "  Player \"kot23rus\" (DEAD) (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<12745.6, 9671.5, 6.0>) bled out";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.BledOut e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.BledOut.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Built()
        {
            string Line = " Player \"Zorro\" (id=OxjFoUFrQmU2hecaqJd6RRxgtqhaTMg_jZY_lHiGh8s= pos=<3442.6, 12323.8, 239.4>)Built wall_base_down on Забор with Топорик";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Built e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Built.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Chat()
        {
            //string Line = "Chat(\"GayZ\"(id=ID34fdoIpGJdd236w9Oh_fu2dP1hDPVnX6NWxhN_gYE=)): для чего шкура волка нужна";
            string Line = "Chat(\"(Admin) Berserk\"(id=JAknhO3dSKs7gQQLVfOwxu9dEubV4IciZcbP8VIFgD4=)): /goa on";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Chat e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Chat.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Dismantled()
        {
            string Line = " Player \"Zorro\" (id=OxjFoUFrQmU2hecaqJd6RRxgtqhaTMg_jZY_lHiGh8s= pos=<3442.7, 12323.7, 239.4>)Dismantled Нижняя деревянная стена from Ворота with Топорик";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Dismantled e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Dismantled.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Placed()
        {
            string Line = "Player \"GayZ\" (id=ID34fdoIpGJdd236w9Oh_fu2dP1hDPVnX6NWxhN_gYE= pos=<1993.0, 7386.6, 258.1>) placed Деревянный ящик";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Placed e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Placed.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void PlayerConect()
        {
            string Line = "Player \"snutik\" is connected (id=Lnb_j2h9D7rznkixOOc3H59XRXlFQM5uu-F_ge9_cms=)";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.PlayerConect e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.PlayerConect.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        [TestMethod]
        public void PlayerDamage()
        {
            ///hit by
            string[] Lines = [
                "Player Survivor of server (DEAD) (id=zJ3Zckx3cwQbg5MI-Pc-a1jrDQFTqRZzLGIjyZN6RGs= pos=<4537.2, 9689.8, 339.1>)[HP: 0] hit by explosion (LandFuelFeed_Ammo)",
                "Player \"Survivor\" (id=qOkKOr39TMj0MtUK0ECMa8Taro9GhUJ2NVPh2QP00Bo= pos=<12064.7, 9063.0, 54.1>)[HP: 99.2775] hit by Зараженный into LeftArm(18) for 7.225 damage (MeleeInfectedLong)",
                "Player \"cepgo\" (id=B7Baj5I93qjqUMHxknYNad8oVW_CCrQRn5k9nXTeCgs= pos=<11131, 12204.7, 199.5>)[HP: 96.2819] hit by Player \"kokz23\" (id=fmloKxXlBrQcLZ4WtVzqZGSC-tVRHc-yaa7NQ4r3h7o= pos=<11129.8, 12205.3, 199.7>) into LeftArm(18) for 18.7 damage (MeleeSharpLight_4) with Штык для M4-A1",
                "Player \"Survivor\" (id=ewe0-V2hPYd5unczHZLaHKp9dnu6_DQf8QjkHxLj_vI= pos=<10224.2, 1617.6, 6.0>)[HP: 1.5526] hit by FallDamageHealth",
                "Player \"JAMBO\" (id=RmmUayXydhGz8xepLSd5o2WMcpJQMjkTI-iRpwRJ1sE= pos=<4174.0, 10988.9, 338.7>)[HP: 98.1048] hit by Hatchback_02 with TransportHit",
                "Player \"SKIF\" (id=iWRBuw-Q8VLckgarFtIxzOz0S-q7FstY9pokxnFfevE= pos=<1073.4, 4657.8, 140.0>)[HP: 80] hit by Волк into Head(0) for 10 damage (MeleeWolf)",
                "Player \"kot23rus\" (DEAD) (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<477.8, 7394.2, 249.9>)[HP: 0] hit by Player \"pumpkin\" (id=IwIRKD1pQW4WkZZYg0rVoNd73Otl7Bt0TJkXsvXG15U= pos=<474.4, 7393.2, 250.1>) into Head(0) for 96.3698 damage (Bullet_556x45) with M4-A1 from 3.53212 meters "
                ];
            foreach(var Line in Lines)
            {
                Console.WriteLine(Line);
                GAME.LogFiles.ADM.PlayerDamage e = new();
                Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
                var xml = e.GetXML();
                Console.WriteLine(xml);
                var rE = GAME.LogFiles.ADM.PlayerDamage.FromXML(xml);
                Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        [TestMethod]
        public void PlayerDied()
        {
            string Line = "Player \"kot23rus\" (DEAD) (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<12745.6, 9671.5, 6.0>) died. Stats> Water: 542.722 Energy: 542.722 Bleed sources: 3";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.PlayerDied e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.PlayerDied.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void PlayerDisconect()
        {
            string Line = "Player \"pumpkin\"(id=IwIRKD1pQW4WkZZYg0rVoNd73Otl7Bt0TJkXsvXG15U=) has been disconnected";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.PlayerDisconect e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.PlayerDisconect.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void PlayerKilled()
        {
            ///hit by
            string[] Lines = [
                "Player \"[OD] Cicada\" (DEAD) (id=OPg1Eo-hQrQvNxiXQxg1weQGe-intKE26NpYmSkwZYI= pos=<12023.1, 9181.6, 54.2>) killed by ZmbF_ShortSkirt_stripes",
                "Player \"Your mom is an elephant\" (DEAD) (id=lU_rJdHx4uAbT6WytcFZZB0zPzZLYskELJ2dVtCNQO4= pos=<11788.1, 4611.9, 189.5>) killed by Player \"vladislav zmiyuk\" (id=y9VFAR4GM_QG9_m43m3u2jWD2A-k-hlXaGIWYc7k32w= pos=<11786.5, 4610.8, 189.5>) with Sporter 22 from 2.01984 meters ",
                "Player \"Your mom is an elephant\" (DEAD) (id=lU_rJdHx4uAbT6WytcFZZB0zPzZLYskELJ2dVtCNQO4= pos=<5999.0, 3277.9, 75.2>) killed by ZmbM_CitizenASkinny_Brown",
                "Player \"Survivor\" (DEAD) (id=zJ3Zckx3cwQbg5MI-Pc-a1jrDQFTqRZzLGIjyZN6RGs= pos=<4537.9, 9688.9, 339.1>) killed by Player \"kepka\" (id=jSqka-VsnPLspSAWBo3cAHePW6NqaeBWHPSyu-_jzaQ= pos=<4537.4, 9616.5, 347.5>) with AUR AX from 72.8276 meters ",
                "\"SuperMan2023\" (DEAD) (id=aUDoPHq_GdkWNVXQaKXsaMK5MV6nGT2m5SsCLkKVUgQ= pos=<4560.7, 9698.2, 339.1>) killed by Player \"kepka\" (id=jSqka-VsnPLspSAWBo3cAHePW6NqaeBWHPSyu-_jzaQ= pos=<4557.1, 9593.8, 353>) with AUR AX from 105.377 meters "
                ];
            foreach (var Line in Lines)
            {
                Console.WriteLine(Line);
                GAME.LogFiles.ADM.PlayerKilled e = new();
                Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
                var xml = e.GetXML();
                Console.WriteLine(xml);
                var rE = GAME.LogFiles.ADM.PlayerKilled.FromXML(xml);
                Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
        [TestMethod]
        public void PlayerList()
        {
            ///hit by
            string[] Lines = [
                "##### PlayerList log: 4 players",
                "Player \"KIRYAESHKA\" (id=rC10G8v_XtecP_lavlTpkJRjRM3Gz1M9MAjTeMy66_c= pos=<4335.7, 13105.3, 178.4>)",
                "Player \"Survivor\" (id=qOkKOr39TMj0MtUK0ECMa8Taro9GhUJ2NVPh2QP00Bo= pos=<12977.5, 7759.3, 17.1>)",
                "Player \"cepgo\" (id=B7Baj5I93qjqUMHxknYNad8oVW_CCrQRn5k9nXTeCgs= pos=<13747.4, 14111, 28.7>)",
                "Player \"pumpkin\" (id=IwIRKD1pQW4WkZZYg0rVoNd73Otl7Bt0TJkXsvXG15U= pos=<9907.0, 6708.2, 178.3>)",
                "Player \"kot23rus\" (DEAD) (id=B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA= pos=<12745.6, 9671.5, 6.0>)",
                "#####"
                ];
            GAME.LogFiles.ADM.PlayerList? pl = null;
            foreach (var Line in Lines)
            {
                Console.WriteLine(Line);
                if (pl!=null)
                {
                    if (pl.AppendLine(Line))
                        Console.WriteLine("Закончили читать список игроков");
                }
                else
                {
                    pl = new();
                    if (pl.Init(Line))
                    {
                        Console.WriteLine("Начали читать список игроков");
                    }
                    else
                    {
                        Console.WriteLine("Не поняли что нужно читать");
                        pl.Dispose();
                        pl = null;
                    }
                }
            }
            if (pl!=null)
            {
                var xml = pl.GetXML();
                Console.WriteLine(xml);
                var rE = GAME.LogFiles.ADM.PlayerList.FromXML(xml);
                Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
            }
            else
            {
                Assert.Fail("Список игроков не прочитан");
            }
            
        }
        [TestMethod]
        public void Regained()
        {
            string Line = "Player \"Survivor\" (id=qOkKOr39TMj0MtUK0ECMa8Taro9GhUJ2NVPh2QP00Bo= pos=<10560.6, 9133.3, 170.9>) regained consciousness";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Regained e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Regained.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Report()
        {
            string Line = "PLAYER REPORT: <2023-11-26_18-14-16> <B1idL_7H1auUS5DPBOEDcTFQ3EBBrzFLa8r1GGmv7GA=>: тестовое сообщение";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Report e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Report.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Suicide()
        {
            //string Line = "Player \"CrazyGrizzly\" (id=yhqXUrWQ7LlBEubvKY9KI9dpGSwU_sg8bb-2nTBTEdo= pos=<13150, 7142.3, 6.0>) committed suicide";
            string Line = "Player 'vladislav zmiyuk' (id=y9VFAR4GM_QG9_m43m3u2jWD2A-k-hlXaGIWYc7k32w=) committed suicide.";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Suicide e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Suicide.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }
        [TestMethod]
        public void Unconscious()
        {
            string Line = "Player \"Survivor\" (id=qOkKOr39TMj0MtUK0ECMa8Taro9GhUJ2NVPh2QP00Bo= pos=<10559.7, 9133.0, 171.0>) is unconscious";
            Console.WriteLine(Line);
            GAME.LogFiles.ADM.Unconscious e = new();
            Assert.IsTrue(e.Init(Line), "Не смогли прочитать правильную строчку");
            var xml = e.GetXML();
            Console.WriteLine(xml);
            var rE = GAME.LogFiles.ADM.Unconscious.FromXML(xml);
            Assert.IsNotNull(rE, "Не смогли прочитать данные из xml");
        }

        /// <summary>
        /// Загрузить лог ADM игры
        /// </summary>
        [TestMethod]
        public void AdminLog()
        {
            var fLog = new FileInfo("TestFiles\\GameLogs\\LOG.ADM");
            var tLog = new LogFiles.ADM.Log(fLog);
            tLog.ReadLine += TLog_ReadLine;
            Console.WriteLine();
            Console.WriteLine($"Из файла {fLog.Name} прочитано {tLog.Read()} строк");
            if (tLog.Errors.Count > 0)
            {
                Console.WriteLine($"При чтении лога было {tLog.Errors.Count} ошибок");
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
            Console.WriteLine("Было найдено событий:");
            Console.WriteLine($"Событий смерти от потери крови {tLog.BledOuts.Count} записей");
            Console.WriteLine($"Событий строительства объектов {tLog.Builts.Count} записей");
            Console.WriteLine($"Событий демонтажа объектов {tLog.Dismantleds.Count} записей");
            Console.WriteLine($"Ошибок на стороне сервера {tLog.Errors.Count} записей");
            Console.WriteLine($"Журнал позиций игроков {tLog.LogPlayers.Count} записей");
            Console.WriteLine($"Событий размещения предметов игроками {tLog.Placeds.Count} записей");
            Console.WriteLine($"Сообщений в игровой чат {tLog.PlayerChat.Count} записей");
            Console.WriteLine($"Событий подключения игроков {tLog.PlayerConnects.Count} записей");
            Console.WriteLine($"Событий урона здоровью игроков {tLog.PlayerDamages.Count} записей");
            Console.WriteLine($"Смертей игроков {tLog.PlayerDieds.Count} записей");
            Console.WriteLine($"Событий отключения игроков {tLog.PlayerDisconnects.Count} записей");
            Console.WriteLine($"Событий убийств игрока другим игроком {tLog.PlayerKilleds.Count} записей");
            Console.WriteLine($"Событий когда игрок очнулся после потери сознания {tLog.PlayerRegained.Count} записей");
            Console.WriteLine($"Сообщений администрации сервера от игроков {tLog.PlayersReport.Count} записей");
            Console.WriteLine($"Событий когда игрок потерял сознание {tLog.PlayerUnconscious.Count} записей");
            Console.WriteLine($"Самоубийства игроков {tLog.Suicides.Count} записей");
            tLog.CloseFile();
        }

        /// <summary>
        /// Тотальный разбор файлов ADM с целью найти строчки которые
        /// парсер не понимает.
        /// </summary>
        /// <remarks>
        /// В папочке теста должна быть папочка ADM  с файлами, вот тест просто берет 
        /// все файлы и читает их. Все строчки которые не распознаны выкидываются в 
        /// стандартный вывод.
        /// </remarks>
        [TestMethod]
        public void StressTest()
        {
            var path = new DirectoryInfo("ADM");
            if (path.Exists )
            {
                var files = path.EnumerateFiles();
                UnknowCount = 0;
                foreach(var file in files)
                {
                    var tLog = new LogFiles.ADM.Log(file);
                    //TODO:     Вот тут изменить событие что бы не писалось сразу в кносоль, а тупо накапливалось.
                    //          Т.е. набираем стату, и в консоль пишем чет вроде файл такойто, прочитали столько-то строк
                    //          нераспознали столько то строк.
                    tLog.ReadLine += TLog_ReadLine;
                    tLog.Read();
                    tLog.CloseFile();
                }
                //TODO:         А вот тут пишем, что прочитали столько то файлов, в которых не поняли столько-то строк
                //              и дальше полный список строк которые парсер не понял от слова совсем... 
            }
        }

        protected static int UnknowCount = 0;

        private static void TLog_ReadLine(object? sender, LogFiles.ExtendReadEventArgs e)
        {
            
            Console.WriteLine(e.Line);
            UnknowCount++;
        }
    }
}
