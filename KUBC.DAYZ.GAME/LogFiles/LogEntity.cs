using KUBC.DAYZ.GAME.LogFiles.RPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Элемент лога
    /// </summary>
    public class LogEntity : IDisposable
    {
        /// <summary>
        /// Признак что в конструкторе класса строчка была разобрана успешно
        /// </summary>
        [XmlIgnore]
        public bool IsReadOk = false;
        /// <summary>
        /// Поток чтения строчки лога
        /// </summary>
        /// <remarks>
        /// Инициализируется при создании строчки
        /// </remarks>
        protected StringReader? Reader;
        /// <summary>
        /// Инициализируем элемент лога
        /// </summary>
        /// <param name="cancellation">Токен отмены разбора строчки</param>
        /// <param name="Line">Первая строчка лога для разбора</param>
        public LogEntity(string Line, CancellationToken? cancellation = null)
        {
            Reader = new StringReader(Line);
            Init(Line, cancellation);
        }
        /// <summary>
        /// Инициализация пустого класса
        /// </summary>
        public LogEntity()
        {

        }

        /// <summary>
        /// Инициализируем объект
        /// </summary>
        /// <param name="Line">Строчка лога из которой выполняем инициализацию</param>
        /// <param name="cancellation">Токен отмены действия</param>
        protected virtual void Init(string Line, CancellationToken? cancellation = null)
        {

        }

        /// <summary>
        /// Уничтожаем класс, т.е. закрываем поток чтения и убиваем его нафиг
        /// </summary>
        public virtual void Dispose()
        {
            
            Reader?.Close();
            Reader?.Dispose();
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
            if (Reader!=null)
            {
                LastSymbol = (char)Reader.Read();
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
            if (!End) { End = (LastSymbol == StopChar); }
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
            string res = string.Empty;
            if (SkipLast)
            {
                Read();
            }
            if (LastSymbol.HasValue)
            {
                if (LastSymbol.Value != StopChar)
                    res += LastSymbol;
            }
            bool End = false;
            while(!End)
            {
                if ((cancellation != null)&&(cancellation.Value.IsCancellationRequested))
                {
                    return null;
                }
                Read();
                if (LastSymbol.HasValue)
                {
                    if (LastSymbol.Value != StopChar)
                        res += LastSymbol;
                    else
                        End = true;
                }
                else
                    End = true;
            }
            return res;
        }
        /// <summary>
        /// Получить событие в виде XML
        /// </summary>
        /// <returns>Элемент лога в представлении XML</returns>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(this.GetType());
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
    }
}
