namespace KUBC.DAYZ.GAME.LogFiles.Scripts
{
    /// <summary>
    /// Файл лога скриптов игры
    /// </summary>
    public class Log : File
    {
        /// <summary>
        /// Патерн поиска файла лога
        /// </summary>
        public const string FileSearch = "script*.log";


        /// <summary>
        /// Открываем файл лога скриптов
        /// </summary>
        /// <param name="openFile">файл который нужно открыть</param>
        /// <param name="maxFileSize">Допустимый размер файла</param>
        public Log(FileInfo openFile, long maxFileSize = 104857600) :
            base(openFile, maxFileSize)
        {

        }
    }
}
