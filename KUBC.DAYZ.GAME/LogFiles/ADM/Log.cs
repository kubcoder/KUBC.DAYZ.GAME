namespace KUBC.DAYZ.GAME.LogFiles.ADM
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

        /// <summary>
        /// Разбираем строчку лога
        /// </summary>
        /// <param name="Line"></param>
        protected override void ParseLine(string Line)
        {
            if (!LogStarted.HasValue)
            {
                ParseStartTime(Line);
            }
            else
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
                    if (!ParseADMLine(LineTime, Line[11..]))
                    {
                        base.ParseLine(Line);
                    }
                }
                else
                {
                    base.ParseLine(Line);
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
        /// <param name="Line">Текст журнала</param>
        private bool ParseADMLine(DateTime LineTime, string Line)
        {
            if (currentPlayerList != null)
            {
                if (!currentPlayerList.AddLine(Line))
                {
                    LogPlayers.Add(currentPlayerList);
                    currentPlayerList = null;
                    return true;
                }
                return true;
            }
            else
            {
                if (PlayerList.IsStartList(Line))
                {
                    currentPlayerList = new PlayerList() { LogTime = LineTime };
                    return true;
                }
            }
            var pConect = PlayerConect.FromLog(Line, LineTime);
            if (pConect != null)
            {
                PlayerConnects.Add(pConect);
                return true;
            }
            var pDisconect = PlayerDisconect.FromLog(Line, LineTime);
            if (pDisconect != null)
            {
                PlayerDisconnects.Add(pDisconect);
                return true;
            }
            var pChat = Chat.FromLog(Line, LineTime);
            if (pChat != null)
            {
                PlayerChat.Add(pChat);
                return true;
            }
            var pRep = Report.FromLog(Line, LineTime);
            if (pRep != null)
            {
                PlayersReport.Add(pRep);
                return true;
            }
            var pUncon = Unconscious.FromLog(Line, LineTime);
            if (pUncon != null)
            {
                PlayerUnconscious.Add(pUncon);
                return true;
            }
            var pReg = Regained.FromLog(Line, LineTime);
            if (pReg != null)
            {
                PlayerRegained.Add(pReg);
                return true;
            }
            var pDmg = PlayerDamage.FromLog(Line, LineTime);
            if (pDmg != null)
            {
                PlayerDamages.Add(pDmg);
                return true;
            }
            var pKill = PlayerKilled.FromLog(Line, LineTime);
            if (pKill != null)
            {
                PlayerKilleds.Add(pKill);
                return true;
            }
            var pDie = PlayerDied.FromLog(Line, LineTime);
            if (pDie != null)
            {
                PlayerDieds.Add(pDie);
                return true;
            }
            var pSuicide = Suicide.FromLog(Line, LineTime);
            if (pSuicide != null)
            {
                Suicides.Add(pSuicide);
                return true;
            }
            var pBledOut = BledOut.FromLog(Line, LineTime);
            if (pBledOut != null)
            {
                BledOuts.Add(pBledOut);
                return true;
            }
            var pPlaced = Placed.FromLog(Line, LineTime);
            if (pPlaced != null)
            {
                Placeds.Add(pPlaced);
                return true;
            }
            var pBuilt = Built.FromLog(Line, LineTime);
            if (pBuilt != null)
            {
                Builts.Add(pBuilt);
                return true;
            }
            var pDismate = Dismantled.FromLog(Line, LineTime);
            if (pDismate != null)
            {
                Dismantleds.Add(pDismate);
                return true;
            }
            return false;
        }
    }
}
