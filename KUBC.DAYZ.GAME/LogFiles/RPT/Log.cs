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
                ConectEvent cE = new (Line, cancellationToken);
                if (cE.IsReadOk)
                {
                    cE.ConnectTime = CorrectTime(cE.ConnectTime);
                    PlayersConect.Add(cE);
                }
                else
                {
                    var aFPS = new AverageFPS(Line);
                    if (aFPS.IsReadOk)
                    {
                        aFPS.MeasuredTime = CorrectTime(aFPS.MeasuredTime);
                        FPSHistory.Add(aFPS);
                    }
                    else
                    {
                        var aMem = new UsedMemory(Line);
                        if (aMem.IsReadOk)
                        {
                            aMem.MeasuredTime = CorrectTime(aMem.MeasuredTime);
                            MemHistory.Add(aMem);
                        }
                        else
                        {
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
