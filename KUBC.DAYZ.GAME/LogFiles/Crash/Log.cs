namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Файл лога ошибок
    /// </summary>
    public class Log : File
    {
        /// <summary>
        /// Патерн поиска файла лога
        /// </summary>
        public const string FileSearch = "crash*.log";

        /// <summary>
        /// Открываем файл лога крашей
        /// </summary>
        /// <param name="openFile">файл который нужно открыть</param>
        public Log(FileInfo openFile) :
            base(openFile)
        {

        }
        /// <summary>
        /// Текущие событие краша
        /// </summary>
        private Entity? CurrentCrash;
        /// <summary>
        /// Список обнаруженых крашей
        /// </summary>
        public List<Entity> Crashes = new();
        /// <summary>
        /// Найдены ли ошибки краша при компиляции скриптов в текущих найденых крашах
        /// </summary>
        public bool IsNotCompile
        {
            get
            {
                foreach (var crash in Crashes)
                    if (crash.ChekNotCompile())
                        return true;
                return false;
            }
        }

        /// <summary>
        /// Очищаем список найденых крашей
        /// </summary>
        protected override void OnPreRead()
        {
            this.Crashes.Clear();
        }
        /// <summary>
        /// В какой фазе находится процесс чтения
        /// </summary>
        private ParsePhase Phase = ParsePhase.CLIParams;

        /// <summary>
        /// Читаем строчку лога
        /// </summary>
        /// <param name="Line"></param>
        protected override void ParseLine(string Line)
        {
            if (Line == "------------------------------------")
            {
                if (CurrentCrash != null)
                {
                    Crashes.Add(CurrentCrash);
                }
                else
                {
                    CurrentCrash = new Entity();
                    Phase = ParsePhase.Header;
                }
            }
            else
            {
                if (CurrentCrash != null)
                {
                    switch (Phase)
                    {
                        case ParsePhase.Header:
                            CurrentCrash.HeaderLine = Line;
                            Phase++;
                            break;
                        case ParsePhase.Message:
                            if (Line.Contains("Stack trace"))
                            {
                                Phase++;
                            }
                            else
                            {
                                if (Line.Contains("Runtime mode"))
                                {
                                    Phase = ParsePhase.CLIParams;
                                }
                                else
                                {
                                    var mLine = Line.Trim();
                                    if (!string.IsNullOrEmpty(mLine))
                                        CurrentCrash.Messages.Add(mLine);
                                }

                            }
                            break;
                        case ParsePhase.StackTrace:
                            if (Line.Contains("Runtime mode"))
                            {
                                Phase++;
                            }
                            else
                            {
                                var sLine = Line.Trim();
                                if (!string.IsNullOrEmpty(sLine))
                                    CurrentCrash.StackTrace.Add(sLine);
                            }
                            break;
                        case ParsePhase.CLIParams:
                            CurrentCrash.CLIParams = Line.Trim();
                            Crashes.Add(CurrentCrash);
                            CurrentCrash = null;
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Обрабатываем окончание чтение файла
        /// </summary>
        protected override void OnPostRead()
        {
            if (this.CurrentCrash != null)
            {
                Crashes.Add(CurrentCrash);
                CurrentCrash = null;
            }
        }

    }
}
