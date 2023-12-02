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
        /// Поток чтения строчки лога
        /// </summary>
        /// <remarks>
        /// Инициализируется при создании строчки
        /// </remarks>
        protected StringReader? Reader;
        /// <summary>
        /// Инициализируем элемент лога
        /// </summary>
        public LogEntity()
        {
            
            
        }
        /// <summary>
        /// Строчка которую читаем
        /// </summary>
        protected string? CurrentLine;
        

        /// <summary>
        /// Инициализируем объект. Т.е. выполняем чтение строки
        /// Если этот объект однострочный, обычно этим все и заканчивается
        /// </summary>
        /// <param name="Line">Строчка лога из которой выполняем инициализацию</param>
        /// <param name="cancellation">Токен отмены действия</param>
        public virtual bool Init(string Line, CancellationToken? cancellation = null)
        {
            Reader = new StringReader(Line);
            CurrentLine = Line;
            return false;
        }
        /// <summary>
        /// Добавить строчку к событию
        /// </summary>
        /// <remarks>
        /// Данный метод используется для много строчных объектов
        /// </remarks>
        /// <param name="Line">Добавляемая строчка лога</param>
        /// <param name="cancellation">Токен отмены действия</param>
        /// <returns>
        /// Возвращается истина если дочитали событие до финиша, т.е. все
        /// дальше будет уже другое событие.
        /// Если вернулась ложь то событие еще ждет дочитывания
        /// </returns>
        public virtual bool AppendLine(string Line, CancellationToken? cancellation = null)
        {
            return true;
        }
        /// <summary>
        /// Признак что закончено чтение событий, и новые строчки уже не нужны
        /// </summary>
        /// <returns>Истина если было завершено чтение события из лога</returns>
        public virtual bool IsReadSucces()
        {
            return true;
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
        /// Прочитать указанное кол-во буков
        /// </summary>
        /// <param name="count">Сколько буков прочитать</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Строка с указаным числом буков, или null если прочитать не удалось</returns>
        protected string? ReadChars(int count, CancellationToken? cancellation = null)
        {
            var res = string.Empty;
            while (true)
            {
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    return null;
                }
                Read();
                if (LastSymbol.HasValue)
                {
                    res+= LastSymbol;
                    if (res.Length >= count)
                        return res;
                }
                else
                {
                    return null;
                }
                
            }
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
        /// <summary>
        /// Загрузить объект из XML
        /// </summary>
        /// <param name="xml">Строчка с разметкой XML</param>
        /// <param name="type">Тип который нужно прочитать</param>
        /// <returns>Прочитанный объект</returns>
        protected static object? ReadFromXML(string xml, Type type)
        {
            try
            {
                var x = new XmlSerializer(type);
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                return rObj;
            }
            catch
            {
                return null;
            }
        }
    }
}
