namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Описание текущего этапа парсинга данных лога краша
    /// </summary>
    public enum ParsePhase
    {
        /// <summary>
        /// Строчка заголовка ошибки
        /// </summary>
        Header = 0,
        /// <summary>
        /// Строчки сообщений
        /// </summary>
        Message,
        /// <summary>
        /// Путь ошибки
        /// </summary>
        StackTrace,
        /// <summary>
        /// Параметры запуска сервера
        /// </summary>
        CLIParams
    }
}
