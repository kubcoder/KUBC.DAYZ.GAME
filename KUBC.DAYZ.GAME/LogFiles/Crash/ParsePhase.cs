namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Описание текущего этапа парсинга данных лога краша
    /// </summary>
    public enum ParsePhase
    {
        /// <summary>
        /// Не инициализировано
        /// </summary>
        NotInit = 0,
        /// <summary>
        /// Строчка заголовка ошибки
        /// </summary>
        Header,
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
        CLIParams,
        /// <summary>
        /// Закончили чтение ошибочки
        /// </summary>
        EndRead,
    }
}
