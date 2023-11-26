﻿namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Журнал администраторов игры
    /// </summary>
    public class Log : File
    {
        /// <summary>
        /// Паттерн поиска файла лога
        /// </summary>
        public const string FileSearch = "*.ADM";

        /// <summary>
        /// Открываем файл лога админов
        /// </summary>
        /// <param name="openFile">файл который нужно открыть</param>
        public Log(FileInfo openFile) :
            base(openFile)
        {

        }

        /// <inheritdoc/>
        protected override void ParseLine(string Line, CancellationToken? cancellationToken)
        {
            if (Line.Trim() == "**********************************EOF****************************************")
                return;
            if (!LogStarted.HasValue)
            {
                ParseStartTime(Line);
            }
            else
            {
                if (Line.Length > 8)
                {
                    var TimeString = Line[..8];
                    if (TimeSpan.TryParse(TimeString, out var Time))
                    {
                        DateTime LineTime;
                        if (LogStarted.HasValue)
                        {
                            LineTime = LogStarted.Value.Date.Add(Time);
                            if (LogStarted.Value.TimeOfDay > Time)
                            {
                                LineTime = LineTime.AddDays(1);
                            }
                        }
                        else
                        {
                            LineTime = DateTime.Now.Add(Time);
                        }
                        if (!ParseADMLine(LineTime, Line[11..], cancellationToken))
                        {
                            base.ParseLine(Line, cancellationToken);
                        }
                    }
                    else
                    {
                        base.ParseLine(Line, cancellationToken);
                    }
                }
            }
        }

        /// <summary>
        /// Дата и время начала лога
        /// </summary>
        public DateTime? LogStarted;
        /// <summary>
        /// Находим стартовую дату в логе
        /// </summary>
        /// <param name="Line"></param>
        private void ParseStartTime(string Line)
        {

            if (Line.Contains("AdminLog started on"))
            {
                var tokens = Line.Split(' ');
                if (tokens.Length > 5)
                {
                    var sTime = $"{tokens[3]} {tokens[5]}";
                    if (DateTime.TryParse(sTime, out DateTime lTime))
                    {
                        LogStarted = lTime;
                    }
                }
            }
        }

        /// <summary>
        /// События подключения игроков
        /// </summary>
        public List<PlayerConect> PlayerConnects = new();
        /// <summary>
        /// Журналы игроков в сети
        /// </summary>
        public List<PlayerList> LogPlayers = new();
        /// <summary>
        /// События отключения игроков
        /// </summary>
        public List<PlayerDisconect> PlayerDisconnects = new();
        /// <summary>
        /// Игровой чат
        /// </summary>
        public List<Chat> PlayerChat = new();
        /// <summary>
        /// Жалобы игроков
        /// </summary>
        public List<Report> PlayersReport = new();
        /// <summary>
        /// Потеря сознаний игроками
        /// </summary>
        public List<Unconscious> PlayerUnconscious = new();
        /// <summary>
        /// События когда игроки приходят в себя
        /// </summary>
        public List<Regained> PlayerRegained = new();
        /// <summary>
        /// События опиздюливания игроков
        /// </summary>
        public List<PlayerDamage> PlayerDamages = new();
        /// <summary>
        /// События убийства игроков
        /// </summary>
        public List<PlayerKilled> PlayerKilleds = new();
        /// <summary>
        /// Смерти игроков
        /// </summary>
        public List<PlayerDied> PlayerDieds = new();
        /// <summary>
        /// Самоубийства игроков
        /// </summary>
        public List<Suicide> Suicides = new();
        /// <summary>
        /// Смерти игроков от потери крови
        /// </summary>
        public List<BledOut> BledOuts = new();
        /// <summary>
        /// События размещения предметов
        /// </summary>
        public List<Placed> Placeds = new();
        /// <summary>
        /// События стройки
        /// </summary>
        public List<Built> Builts = new();
        /// <summary>
        /// События разрушения объектов
        /// </summary>
        public List<Dismantled> Dismantleds = new();

        /// <summary>
        /// Готовимся к чтению лога
        /// </summary>
        protected override void OnPreRead()
        {
            PlayerConnects.Clear();
            LogPlayers.Clear();
            PlayerDisconnects.Clear();
            PlayerChat.Clear();
            PlayersReport.Clear();
            PlayerUnconscious.Clear();
            PlayerRegained.Clear();
            PlayerDamages.Clear();
            PlayerKilleds.Clear();
            PlayerDieds.Clear();
            Suicides.Clear();
            BledOuts.Clear();
            Placeds.Clear();
            Builts.Clear();
            Dismantleds.Clear();
        }
        /// <summary>
        /// Сколько всего событий найдено в момент последнего чтения лога
        /// </summary>
        /// <returns>Число событий</returns>
        public int GetEventsCount()
        {
            int res = PlayerConnects.Count;
            res += LogPlayers.Count;
            res += PlayerDisconnects.Count;
            res += PlayerChat.Count;
            res += PlayersReport.Count;
            res += PlayerUnconscious.Count;
            res += PlayerRegained.Count;
            res += PlayerDamages.Count;
            res += PlayerKilleds.Count;
            res += PlayerDieds.Count;
            res += Suicides.Count;
            res += BledOuts.Count;
            res += Placeds.Count;
            res += Builts.Count;
            res += Dismantleds.Count;
            return res;
        }

        /// <summary>
        /// Переменная для чтения лога игроков
        /// </summary>
        private PlayerList? currentPlayerList;
        /// <summary>
        /// Распознаем строчку журнала ADM
        /// </summary>
        /// <param name="LineTime">Время записи строчки</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <param name="Line">Текст журнала</param>
        private bool ParseADMLine(DateTime LineTime, string Line, CancellationToken? cancellationToken=null)
        {
            if (currentPlayerList != null)
            {
                if (currentPlayerList.AppendLine(Line, cancellationToken))
                {
                    LogPlayers.Add(currentPlayerList);
                    currentPlayerList.Dispose();
                    currentPlayerList = null;
                    return true;
                }
                return true;
            }
            else
            {
                currentPlayerList = new() { Time = LineTime }; ;
                if (currentPlayerList.Init(Line, cancellationToken))
                {
                    return true;
                }
                else
                {
                    currentPlayerList.Dispose();
                    currentPlayerList = null;
                }
            }
            var bledOut = new BledOut() {Time = LineTime };
            if (bledOut.Init(Line, cancellationToken)) 
            {
                bledOut.Dispose();
                BledOuts.Add(bledOut);
                return true;
            }
            else
            {
                bledOut.Dispose();
            }
            var built = new Built() { Time = LineTime }; ;
            if (built.Init(Line, cancellationToken))
            {
                built.Dispose();
                Builts.Add(built);
                return true;
            }
            else
            {
                built.Dispose();
            }
            var chat = new Chat() { Time = LineTime }; ;
            if (chat.Init(Line, cancellationToken))
            {
                chat.Dispose();
                PlayerChat.Add(chat);
                return true;
            }
            else
            {
                chat.Dispose();
            }
            var dismantled = new Dismantled() { Time = LineTime }; ;
            if (dismantled.Init(Line, cancellationToken))
            {
                dismantled.Dispose();
                Dismantleds.Add(dismantled);
                return true;
            }
            else
            {
                dismantled.Dispose();
            }
            var placed = new Placed() { Time = LineTime }; ;
            if (placed.Init(Line, cancellationToken))
            {
                placed.Dispose();
                Placeds.Add(placed);
                return true;
            }
            else
            {
                placed.Dispose();
            }
            var playerConect = new PlayerConect() { Time = LineTime }; ;
            if (playerConect.Init(Line, cancellationToken))
            {
                playerConect.Dispose();
                PlayerConnects.Add(playerConect);
                return true;
            }
            else
            {
                playerConect.Dispose();
            }
            var playerDamage = new PlayerDamage() { Time = LineTime }; ;
            if (playerDamage.Init(Line, cancellationToken))
            {
                playerDamage.Dispose();
                PlayerDamages.Add(playerDamage);
                return true;
            }
            else
            {
                playerDamage.Dispose();
            }
            var playerDied = new PlayerDied() { Time = LineTime }; ;
            if (playerDied.Init(Line, cancellationToken))
            {
                playerDied.Dispose();
                PlayerDieds.Add(playerDied);
                return true;
            }
            else
            {
                playerDied.Dispose();
            }
            var playerDisconect = new PlayerDisconect() { Time = LineTime }; ;
            if (playerDisconect.Init(Line, cancellationToken))
            {
                playerDisconect.Dispose();
                PlayerDisconnects.Add(playerDisconect);
                return true;
            }
            else
            {
                playerDisconect.Dispose();
            }
            var playerKilled = new PlayerKilled() { Time = LineTime }; ;
            if (playerKilled.Init(Line, cancellationToken))
            {
                playerKilled.Dispose();
                PlayerKilleds.Add(playerKilled);
                return true;
            }
            else
            {
                playerKilled.Dispose();
            }
            var regained = new Regained() { Time = LineTime }; ;
            if (regained.Init(Line, cancellationToken))
            {
                regained.Dispose();
                PlayerRegained.Add(regained);
                return true;
            }
            else
            {
                regained.Dispose();
            }
            var report = new Report() { Time = LineTime }; ;
            if (report.Init(Line, cancellationToken))
            {
                report.Dispose();
                PlayersReport.Add(report);
                return true;
            }
            else
            {
                report.Dispose();
            }
            var suicide = new Suicide() { Time = LineTime }; ;
            if (suicide.Init(Line, cancellationToken))
            {
                suicide.Dispose();
                Suicides.Add(suicide);
                return true;
            }
            else
            {
                suicide.Dispose();
            }
            var unconscious = new Unconscious() { Time = LineTime }; ;
            if (unconscious.Init(Line, cancellationToken))
            {
                unconscious.Dispose();
                PlayerUnconscious.Add(unconscious);
                return true;
            }
            else
            {
                unconscious.Dispose();
            }
            return false;
        }
    }
}
