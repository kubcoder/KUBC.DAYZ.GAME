namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Базовый класс для работы с логом.
    /// </summary>
    /// <remarks>
    /// Данный класс реализует базовые функции для работы с файлом лога. Классы которые работают с определенными типами логов наследуются от данного класса
    /// </remarks>
    public class File
    {
        /// <summary>
        /// Текущий лог
        /// </summary>
        private readonly FileInfo OpenFile;
        /// <summary>
        /// Максимальный размер файла который может быть прочитан. 
        /// </summary>
        /// <remarks>
        /// Значение по умолчанию 100МБ = 104857600 байт.
        /// </remarks>
        private readonly long MaxFileSize;
        /// <summary>
        /// Текущая строчка лога.
        /// </summary>
        /// <remarks>
        /// Тут храним на какой строчке лога остановились при предыдущем чтении. Что бы следующий раз читать с новой строчки
        /// </remarks>
        private long CurrentLine;
        /// <summary>
        /// Список ошибок возникающих при парсинге
        /// </summary>
        public List<ParseError> Errors = new();
        /// <summary>
        /// Текущий размер файла лога
        /// </summary>
        public long? LogFileSize
        {
            get
            {
                if (OpenFile != null)
                {
                    OpenFile.Refresh();
                    if (OpenFile.Exists)
                    {
                        return OpenFile.Length;
                    }
                }
                return null;
            }
        }
        /// <summary>
        /// На сколько большой файл. При значениях выше 1.0 прекращается чтение файла.
        /// </summary>
        /// <remarks>
        /// Значение расчитывается в процентах (долях единицы) путем деления текущего размера файла на допустимый размер <see cref="MaxFileSize"/>
        /// </remarks>
        public double LogFilePrecent
        {
            get
            {
                var fs = LogFileSize;
                if (fs.HasValue)
                {
                    return fs.Value / MaxFileSize;
                }
                return 0.0;
            }
        }

        /// <summary>
        /// Проверяем корректность открытого файла.
        /// </summary>
        /// <remarks>Т.е. файл который считаем последним совпадает ли с именем того что открыт в процессоре, если имена не совпадают (появился новый файл лога) то логично переинициализировать объект</remarks>
        /// <param name="NewFile">Имя файла который служба считает самым новым</param>
        /// <returns>Истина если сейчас открыт правильный файл</returns>
        public bool IsFileCorrect(FileInfo NewFile)
        {
            return OpenFile.FullName == NewFile.FullName;
        }
        /// <summary>
        /// Признак того что файл 
        /// </summary>
        public bool IsFirstRead
        {
            get
            {
                return CurrentLine == 0;
            }
        }

        /// <summary>
        /// Создаем экземпляр чтения файла
        /// </summary>
        /// <param name="openFile">Имя открываемого файла</param>
        /// <param name="maxFileSize">Максимальный размер файлика. Если файл будет больше то чтение не будет выполнено</param>
        public File(FileInfo openFile, long maxFileSize = 104857600)
        {
            OpenFile = openFile;
            MaxFileSize = maxFileSize;
            CurrentLine = 0;
        }
        /// <summary>
        /// Признак что файл существует
        /// </summary>
        public bool IsFileExist
        {
            get
            {
                return OpenFile.Exists;
            }
        }
        /// <summary>
        /// Файл может быть прочитан
        /// </summary>
        public bool IsCanRead
        {
            get
            {
                if (OpenFile.Exists)
                {
                    if (OpenFile.Length < MaxFileSize)
                        return true;
                }
                return false;
            }
        }


        /// <summary>
        /// Читаем файл
        /// </summary>
        /// <remarks>
        /// Сколько строчек лога было прочитано
        /// </remarks>
        public int Read()
        {
            if (IsCanRead)
            {
                OnPreRead();
                Errors.Clear();
                int cLine = 0;
                int ReadLines = 0;
                string? Line;
                using var stream = new StreamReader(OpenFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                if (stream != null)
                {
                    while ((Line = stream.ReadLine()) != null)
                    {
                        cLine++;
                        if (cLine > CurrentLine)
                        {
                            ReadLines++;
                            try
                            {
                                if (!string.IsNullOrEmpty(Line))
                                    ParseLine(Line);
                            }
                            catch (Exception ex)
                            {
                                Errors.Add(new ParseError() { Exception = ex, SourceLine = Line });
                            }
                        }
                    }
                    stream.Close();
                    CurrentLine = cLine;

                    return ReadLines;
                }
            }
            return 0;
        }

        /// <summary>
        /// Метод вызывается перед началом чтения файла лога.
        /// </summary>
        /// <remarks>
        /// В целевых файлах по идее необходимо выполнить подготовку к чтению данных лога
        /// </remarks>
        protected virtual void OnPreRead()
        {

        }
        /// <summary>
        /// Метод вызывается по завершении чтения файла лога.
        /// </summary>
        protected virtual void OnPostRead()
        {

        }
        /// <summary>
        /// Событие вызываемое при необходимости чтения строчки лога
        /// </summary>
        public event EventHandler<string>? ReadLine;

        /// <summary>
        /// Распознаем строку журнала
        /// </summary>
        /// <param name="Line">Строчка которую нужно распознать</param>
        protected virtual void ParseLine(string Line)
        {
            ReadLine?.Invoke(this, Line);
        }
        /// <summary>
        /// Получаем последний лог (ну в который писалось позже всего) по заданному шаблону поиска имени файла
        /// </summary>
        /// <param name="Path">Папка в которой шукаем файлы</param>
        /// <param name="Filter">Фильтр поиска файла лога</param>
        /// <returns>Информация о файле. Если файлов не найдено то будет возвращен null</returns>
        public static FileInfo? GetLastLog(DirectoryInfo Path, string Filter)
        {
            var Files = Path.GetFiles(Filter);
            if (Files != null)
            {
                if (Files.Length > 0)
                {
                    var LastFile = Files[0];
                    for (int i = 1; i < Files.Length; i++)
                    {
                        if (LastFile.LastWriteTime < Files[i].LastWriteTime)
                            LastFile = Files[i];
                    }
                    return LastFile;
                }
            }
            return null;
        }
    }
}
