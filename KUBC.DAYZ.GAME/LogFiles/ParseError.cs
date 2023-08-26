namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Элемент ошибки парсинга
    /// </summary>
    public class ParseError
    {
        /// <summary>
        /// Исходная строчка лога
        /// </summary>
        public string SourceLine = string.Empty;
        /// <summary>
        /// Ошибка в ходе разбора лога
        /// </summary>
        public Exception? Exception;
    }
}
