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
        /// Список ошибок возникающих при парсинге
        /// </summary>
        public List<ParseError> Errors = new();
        /// <summary>
        /// Поток для чтения файла
        /// </summary>
        private StreamReader? fileReader;

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
        /// Создаем экземпляр чтения файла
        /// </summary>
        /// <param name="openFile">Имя открываемого файла</param>
        public File(FileInfo openFile)
        {
            OpenFile = openFile;
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
                return OpenFile.Exists;
            }
        }


        /// <summary>
        /// Символы прочитанные из файла
        /// </summary>
        private List<int> Chars = new();

        /// <summary>
        /// Получить текущую строчку лога
        /// </summary>
        /// <returns>Полная строчка лога</returns>
        protected string GetLine()
        {
            string l = string.Empty;
            foreach(char c in Chars) { l+= c; }
            Chars.Clear();
            return l;
        }
        /// <summary>
        /// Дочитали до конца строки
        /// </summary>
        private bool IsEndLine
        {
            get
            {
                return Chars.LastOrDefault() == '\n';
            }
        }

        /// <summary>
        /// Читать строчку из потока
        /// </summary>
        /// <returns>Истина если строчка была дочитана до конца, иначе ложь</returns>
        protected bool ReadFromFile(CancellationToken? cancellationToken = null)
        {
            if (fileReader == null)
            {
                if (IsCanRead)
                {
                    fileReader = new StreamReader(OpenFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                    if (fileReader != null)
                    {
                        OnFileOpen();

                    }
                }
            }
            if (fileReader!=null)
            {
                while (!fileReader.EndOfStream)
                {
                    Chars.Add(fileReader.Read());
                    if ((cancellationToken != null) && (cancellationToken.Value.IsCancellationRequested))
                        return false;
                    if (IsEndLine)
                        return true;
                        
                }
            }
            return false;
        }


        /// <summary>
        /// Читаем файл
        /// </summary>
        /// <param name="SkipPreRead">
        /// При чтении файла пропустить предварительные действия.
        /// Используется для того что бы сохранить события в памяти когда при сохранении в БД
        /// был сбой, и нужно попробывать чуть позже.
        /// </param>
        /// <param name="cancellationToken">Токен отмены. Нужен что бы принудительно
        /// прервать выполнение чтения файла лога из вызывающй программы</param>
        /// <returns>Сколько строчек лога было прочитано</returns>
        public int Read(bool SkipPreRead = false, CancellationToken? cancellationToken = null)
        {
            int ReadLines = 0;
            Errors.Clear();
            if (!SkipPreRead)
            {
                OnPreRead();
            }
            while (ReadFromFile(cancellationToken))
            {
                ReadLines++;
                var Line = GetLine();
                try
                {
                    ParseLine(Line, cancellationToken);
                }
                catch (Exception ex)
                {
                    Errors.Add(new ParseError() { Exception = ex, SourceLine = Line });
                }
            }
            OnPostRead();
            return ReadLines;
        }
        /// <summary>
        /// Закрываем чтение файла. Т.е. грохаем поток чтения
        /// </summary>
        public void CloseFile()
        {
            if (fileReader!= null)
                fileReader.Close();
        }

        /// <summary>
        /// Метод вызывается после успешного открытия файла. Один раз когда указатель еще в нулях.
        /// </summary>
        /// <remarks>
        /// В целевых файлах по идее необходимо выполнить подготовку к чтению данных лога
        /// </remarks>
        protected virtual void OnFileOpen()
        {

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
        public event EventHandler<ExtendReadEventArgs>? ReadLine;

        /// <summary>
        /// Распознаем строку журнала
        /// </summary>
        /// <param name="Line">Строчка которую нужно распознать</param>
        /// <param name="cancellationToken">Токен отмены разбора строки</param>
        protected virtual void ParseLine(string Line, CancellationToken? cancellationToken)
        {
            ReadLine?.Invoke(this, new ExtendReadEventArgs(Line, cancellationToken));
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
