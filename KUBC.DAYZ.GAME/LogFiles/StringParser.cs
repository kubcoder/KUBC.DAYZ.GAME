using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Шаблон парсера строки
    /// </summary>
    public abstract class StringParser : ILogEntityFabric, IDisposable
    {
        /// <inheritdoc/>
        public abstract ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null);

        /// <summary>
        /// Поток чтения строчки лога
        /// </summary>
        /// <remarks>
        /// Инициализируется при создании строчки
        /// </remarks>
        protected StringReader? Reader;

        /// <summary>
        /// Строчка которую читаем
        /// </summary>
        protected string? CurrentLine;


        /// <summary>
        /// Инициализируем объект. Обычно это заканчивается
        /// подготовкой строки к чтению
        /// </summary>
        /// <param name="Line">Строчка лога из которой выполняем инициализацию</param>
        /// <param name="cancellation">Токен отмены действия</param>
        /// <returns>Истина если инициализация прошла успешно</returns>
        protected virtual bool Init(string Line, CancellationToken? cancellation = null)
        {
            Reader = new StringReader(Line);
            CurrentLine = Line;
            return true;
        }

        /// <summary>
        /// Уничтожаем класс, т.е. закрываем поток чтения и убиваем его нафиг
        /// </summary>
        public virtual void Dispose()
        {
            Reader?.Close();
            Reader?.Dispose();
            CurrentLine = null;
        }

        /// <summary>
        /// Символ последним прочитанный из потока
        /// </summary>
        protected char? LastSymbol;

        /// <summary>
        /// Прочитать букву из потока
        /// </summary>
        protected void Read()
        {
            if (Reader != null)
            {
                var code = Reader.Read();
                if (code != -1)
                {
                    LastSymbol = (char)code;
                }
                else
                {
                    LastSymbol = null;
                }
            }
        }

        /// <summary>
        /// Читать поток пока идут пробрассываемые символы
        /// </summary>
        /// <param name="SkipChar">Символы которые нужно пробросить</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Истина если успешно добрались до отличного от SkipChar символа"/>, если была отмена или конец потока возвращаем ложь</returns>
        protected bool SkipChar(char SkipChar = ' ', CancellationToken? cancellation = null)
        {
            Read();
            bool End = !LastSymbol.HasValue;
            if (!End)
            {
                End = (LastSymbol != SkipChar);
                if (End)
                    return true;
            }
            while (!End)
            {
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    return false;
                }
                Read();
                End = !LastSymbol.HasValue;
                if (!End)
                {
                    End = (LastSymbol != SkipChar);
                    if (End)
                        return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Читать поток пока не будет найден искомый символ
        /// </summary>
        /// <param name="StopChar">До какого символа выполнять проброс данных</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Истина если успешно добрались до символа StopChar"/>, если была отмена или конец потока возвращаем ложь</returns>
        protected bool SkipToChar(char StopChar = ' ', CancellationToken? cancellation = null)
        {
            Read();
            bool End = !LastSymbol.HasValue;
            if (!End)
            {
                if (LastSymbol == StopChar)
                    return true;
            }
            while (!End)
            {
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    return false;
                }
                Read();
                End = !LastSymbol.HasValue;
                if (!End)
                {
                    End = (LastSymbol == StopChar);
                    if (End) return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Прочитать строку до первого вхождения символа
        /// </summary>
        /// <param name="StopChar">На каком символе остановится</param>
        /// <param name="cancellation">Токен отмены действия</param>
        /// <param name="SkipLast">Пробросить последний символ и принудительно перейти к следующему</param>
        /// <returns>Результирующая строка</returns>
        protected string? ReadToChar(char StopChar, bool SkipLast = false, CancellationToken? cancellation = null)
        {
            var res = new StringBuilder();
            if (SkipLast)
            {
                Read();
            }
            if (LastSymbol.HasValue)
            {
                if (LastSymbol.Value != StopChar)
                    res.Append(LastSymbol);
            }
            bool End = false;
            while (!End)
            {
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    return null;
                }
                Read();
                if (LastSymbol.HasValue)
                {
                    if (LastSymbol.Value != StopChar)
                        res.Append(LastSymbol);
                    else
                        End = true;
                }
                else
                    End = true;
            }
            return res.ToString();
        }
        /// <summary>
        /// Прочитать указанное кол-во буков
        /// </summary>
        /// <param name="count">Сколько буков прочитать</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Строка с указаным числом буков, или null если прочитать не удалось</returns>
        protected string? ReadChars(int count, CancellationToken? cancellation = null)
        {
            var res = new StringBuilder();
            while (true)
            {
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    return null;
                }
                Read();
                if (LastSymbol.HasValue)
                {
                    res.Append(LastSymbol);
                    if (res.Length >= count)
                        return res.ToString();
                }
                else
                {
                    return null;
                }

            }
        }
        /// <summary>
        /// Прочитать указанное кол-во буков
        /// </summary>
        /// <param name="count">Сколько буков прочитать</param>
        /// <param name="readString">Прочитаная строка</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Удалось ли прочитать требуемое число буков</returns>
        protected bool ReadChars(int count, out string readString, CancellationToken? cancellation = null)
        {
            var res = new StringBuilder();
            while (true)
            {
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    readString = res.ToString();
                    return readString.Length == count;
                }
                Read();
                if (LastSymbol.HasValue)
                {
                    res.Append(LastSymbol);
                    if (res.Length >= count)
                    {
                        readString = res.ToString();
                        return true;
                    }
                        
                }
                else
                {
                    readString = res.ToString();
                    return readString.Length == count;
                }

            }
        }
        /// <summary>
        /// Стиль чисел
        /// </summary>
        protected System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Number;
        /// <summary>
        /// Культура записи чисел
        /// </summary>
        protected System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");

        /// <summary>
        /// Прочитать строку до первого вхождения символа и распознать это как число с плавающей запятой
        /// </summary>
        /// <param name="StopChar">На каком символе остановится</param>
        /// <param name="cancellation">Токен отмены действия</param>
        /// <param name="SkipLast">Пробросить последний символ и принудительно перейти к следующему</param>
        /// <returns>Результирующая строка</returns>
        protected float? ReadFloat(char StopChar, bool SkipLast = false, CancellationToken? cancellation = null)
        {
            var numberString = ReadToChar(']', true, cancellation);
            if (float.TryParse(numberString, style, culture, out var number))
            {
                return number;
            }
            return null;
        }

    }
}
