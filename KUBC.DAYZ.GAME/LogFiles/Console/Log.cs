namespace KUBC.DAYZ.GAME.LogFiles.Console
{
    /// <summary>
    /// Лог вывода консоли
    /// </summary>
    public class Log : File
    {
        /// <summary>
        /// Открываем файл лога скриптов
        /// </summary>
        /// <param name="openFile">файл который нужно открыть</param>
        public Log(FileInfo openFile) :
            base(openFile)
        {

        }
    }
}
