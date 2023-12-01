namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Лог RPT игры
    /// </summary>
    public class Log : File
    {
        /// <summary>
        /// Патерн поиска файла лога
        /// </summary>
        public const string FileSearch = "*.RPT";
        /// <summary>
        /// Шаблон поиска строчки с указанием текущего времени
        /// </summary>
        private const string KEYFINDSTARTTIME = "Current time:";

        /// <summary>
        /// Открываем файл лога RPT
        /// </summary>
        /// <param name="openFile">файл который нужно открыть</param>
        public Log(FileInfo openFile) :
            base(openFile)
        {

        }
        /// <summary>
        /// Список игроков которые подключились
        /// </summary>
        public List<ConectEvent> PlayersConect = new();
        /// <summary>
        /// Данные среднего ФПС сервера
        /// </summary>
        public List<AverageFPS> FPSHistory = new();
        /// <summary>
        /// Данные использования памяти сервером
        /// </summary>
        public List<UsedMemory> MemHistory = new();
        /// <summary>
        /// Очищаем список игроков
        /// </summary>
        protected override void OnPreRead()
        {
            PlayersConect.Clear();
            FPSHistory.Clear();
            MemHistory.Clear();
        }
        /// <inheritdoc/>
        
        protected override void ParseLine(string Line, CancellationToken? cancellationToken = null)
        {
            if (LogStarted == null)
            {
                FindStartTime(Line);
            }
            else
            {
                ConectEvent cE = new ();
                if (cE.Init(Line, cancellationToken))
                {
                    cE.ConnectTime = CorrectTime(cE.ConnectTime);
                    PlayersConect.Add(cE);
                }
                else
                {
                    cE.Dispose();
                    var aFPS = new AverageFPS();
                    if (aFPS.Init(Line, cancellationToken))
                    {
                        aFPS.MeasuredTime = CorrectTime(aFPS.MeasuredTime);
                        FPSHistory.Add(aFPS);
                    }
                    else
                    {
                        aFPS.Dispose();
                        var aMem = new UsedMemory();
                        if (aMem.Init(Line, cancellationToken))
                        {
                            aMem.MeasuredTime = CorrectTime(aMem.MeasuredTime);
                            MemHistory.Add(aMem);
                        }
                        else
                        {
                            aMem.Dispose();
                            base.ParseLine(Line, cancellationToken);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Корректируем дату и время.
        /// В частности если в лог пишется только время то добавляем дату
        /// начала записи лога.
        /// </summary>
        /// <param name="sTime">Дата и время из события</param>
        /// <returns>Правильное дата и время</returns>
        private DateTime CorrectTime(DateTime sTime)
        {
            if (sTime.Year == 1)
            {
                if (LogStarted != null)
                {
                    if (sTime.TimeOfDay < LogStarted.Value.TimeOfDay)
                    {
                        sTime = LogStarted.Value.Date.Add(sTime.TimeOfDay);
                        return sTime.AddDays(1);
                    }
                    else
                    {
                        return LogStarted.Value.Date.Add(sTime.TimeOfDay);
                    }
                }
                else
                {
                    return DateTime.Today.Date.Add(sTime.TimeOfDay);
                }
            }
            else
            {
                return sTime;
            }
            
        }

        /// <summary>
        /// Дата и время начала лога
        /// </summary>
        public DateTime? LogStarted;

        private void FindStartTime(string Line)
        {

            if (Line.Contains(KEYFINDSTARTTIME))
            {
                var textTime = Line.Substring(KEYFINDSTARTTIME.Length + 1).Trim();
                if (DateTime.TryParse(textTime, out var sTime))
                {
                    LogStarted = sTime;
                }
            }
        }
    
    }
}
