using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Текстовый файл лога
    /// </summary>
    public abstract class File : ILogFile
    {
        /// <summary>
        /// Описание файла лога
        /// </summary>
        protected FileInfo? logFile;
        /// <inheritdoc/>
        public void OpenFile(FileInfo file)
        {
            this.logFile = file;
        }

        /// <summary>
        /// Текущая позиция в файле. Если NULL то файл не открыт для чтения
        /// </summary>
        public long? PositionRead
        {
            get
            {
                if (fileReader != null)
                {
                    return fileReader.BaseStream.Position;
                }
                return null;
            }
        }

        /// <summary>
        /// Поток для чтения файла
        /// </summary>
        private StreamReader? fileReader;

        /// <summary>
        /// Символы прочитанные из файла
        /// </summary>
        private readonly List<int> Chars = [];

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

        /// <inheritdoc/>
        public void Dispose()
        {
            if (fileReader!=null)
            {
                fileReader.Dispose();
                fileReader = null;
            }    
        }

        private ILogEntity? ReadEntity;

        /// <inheritdoc/>
        public IEnumerable<ILogEntity>? ReadToEnd(CancellationToken? cancellationToken = null)
        {
            List<ILogEntity> entities = [];
            var logLine = ReadLine(cancellationToken);
            while(logLine!=null)
            {
                if (!string.IsNullOrEmpty(logLine))
                {
                    AddLine(logLine);
                    if ((ReadEntity != null) && (ReadEntity.IsEndRead()))
                    {
                        entities.Add(ReadEntity);
                    }
                }
                logLine = ReadLine(cancellationToken);
            }
            return entities;
        }

        /// <summary>
        /// Добавляем строчку в события
        /// </summary>
        /// <param name="logLine"></param>
        private void AddLine(string logLine)
        {
            if (ReadEntity!=null)
            {
                if (!ReadEntity.IsEndRead() ) 
                {
                    ReadEntity.AppendLine(logLine);
                    return;
                }
            }
            ReadEntity = ParseLine(logLine);
        }



        /// <summary>
        /// Разобрать строчку лога
        /// </summary>
        /// <param name="LogLine">Строчку лога которую нужно разобрать</param>
        protected abstract ILogEntity? ParseLine(string LogLine);

        /// <summary>
        /// Отправить сообщение о неизвестной строчке лога
        /// </summary>
        /// <param name="LogLine"></param>
        protected void SendUnknowLine(string LogLine)
        {
            if (UnknowString!=null)
            {
                if (!IsNotRead(LogLine))
                {
                    UnknowString(this, LogLine);
                }
            }
        }
        /// <summary>
        /// Строчки которые не нужно читать
        /// </summary>
        protected List<string>? LinesNotRead;
        /// <summary>
        /// Проверить является ли строчка не нужной
        /// </summary>
        /// <param name="LogLine">Проверяемая строка</param>
        /// <returns>
        /// Истина, если строчка известна, но её абсолютно не нужно 
        /// распознавать.
        /// </returns>
        protected virtual bool IsNotRead(string LogLine)
        {
            if (LinesNotRead!=null)
            {
                return LinesNotRead.Contains(LogLine);
            }
            return false;
        }

        /// <summary>
        /// Событие вызваемое когда строка лога не была распознана
        /// </summary>
        public event EventHandler<string>? UnknowString;

        /// <summary>
        /// Проверить распознанный элемент данных на предмет адекватности
        /// </summary>
        /// <param name="entity">Элемент данных</param>
        /// <param name="LogLine">Из какой строчки он получен</param>
        /// <returns>Истина если есть подозрение что данные не адекватны</returns>
        protected virtual bool IsWarning(ILogEntity entity, string LogLine)
        {
            return false;
        }
        /// <summary>
        /// Проверить полученные данные на адекватность
        /// </summary>
        /// <param name="entity">Полученные данные</param>
        /// <param name="LogLine">Строчка лога</param>
        protected virtual void CheckLogEntity(ILogEntity entity, string LogLine)
        {
            if (WarningString!=null) 
            {
                if (IsWarning(entity, LogLine))
                    WarningString(this, new WarningDataEventArgs(entity, LogLine));
            }
        }

        /// <summary>
        /// Событие вызваемое когда строка лога была прочитана, но возможно данные не адекватно распознаны
        /// </summary>
        public event EventHandler<WarningDataEventArgs>? WarningString;

        /// <inheritdoc/>
        private string? ReadLine(CancellationToken? cancellationToken)
        {
            if (fileReader==null)
            {
                if (logFile!=null)
                {
                    fileReader = new StreamReader(logFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
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
                    {
                        if (IsEndLine)
                        {
                            return GetLine();
                        }
                        else
                        {
                            return null;
                        }
                    }
                    if (IsEndLine)
                        return GetLine();

                }
            }
            return null;
        }

        /// <summary>
        /// Получить текущую строчку лога
        /// </summary>
        /// <returns>Полная строчка лога</returns>
        protected string GetLine()
        {
            string l = string.Empty;
            foreach (var c in Chars) { l += (char)c; }
            Chars.Clear();
            return l.Trim();
        }

        /// <summary>
        /// Метод вызывается в момент открытия файла для чтения
        /// </summary>
        protected virtual void OnFileOpen(){}
    }
}
