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
        

        /// <inheritdoc/>
        protected override void ParseLine(string Line, CancellationToken? cancellation = null)
        {
            if (CurrentCrash!=null)
            {
                if (CurrentCrash.AppendLine(Line, cancellation))
                {
                    Crashes.Add(CurrentCrash);
                    CurrentCrash.Dispose();
                    CurrentCrash = null;
                }
            }
            else
            {
                CurrentCrash = new Entity();
                if(!CurrentCrash.Init(Line, cancellation))
                {
                    CurrentCrash.Dispose();
                    CurrentCrash = null;
                }
            }
        }
        

    }
}
