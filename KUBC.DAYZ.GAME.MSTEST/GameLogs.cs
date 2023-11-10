namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// Тесты разбора игровых событий
    /// </summary>
    [TestClass]
    public class GameLogs
    {
        /// <summary>
        /// Загрузить лог ADM игры
        /// </summary>
        [TestMethod]
        public void AdminLog()
        {
            var fLog = new FileInfo("TestFiles\\GameLogs\\DayZServer_x64_2023_08_30_215815690.ADM");
            var tLog = new LogFiles.ADM.Log(fLog);
            Console.WriteLine($"Из файла {fLog.Name} прочитано {tLog.Read()} строк");
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
        }
    }
}