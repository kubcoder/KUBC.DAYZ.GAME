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
        /// <param name="maxFileSize">Допустимый размер файла</param>
        public Log(FileInfo openFile, long maxFileSize = 104857600) :
            base(openFile, maxFileSize)
        {

        }
    }
}
