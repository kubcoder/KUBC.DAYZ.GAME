using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Данные об используемой памяти
    /// </summary>
    public class UsedMemory
    {
        /// <summary>
        /// Время когда сохранены данные о памяти
        /// </summary>
        public DateTime MeasuredTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Используемая память в КБ
        /// </summary>
        public long MemoryKB { get; set; } = 0;

        /// <summary>
        /// Читаем данные из строки лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Данные о ФПС. Если вернули null значит строчка не подходящая</returns>
        public static UsedMemory? FromLogLine(string Line)
        {
            if (Line.Contains("Used memory"))
            {
                char rSym;
                var res = new UsedMemory();
                var reader = new StringReader(Line);
                var TimeString = string.Empty;
                bool EndStep = false;
                while (!EndStep)
                {
                    rSym = (char)reader.Read();
                    if (rSym != ' ')
                    {
                        TimeString += rSym;
                    }
                    else
                    {
                        EndStep = true;
                    }
                };
                if (TimeSpan.TryParse(TimeString, out var pTime))
                {
                    res.MeasuredTime = DateTime.MinValue.Add(pTime);
                }
                else
                {
                    if (DateTime.TryParse(TimeString, out var pFTime))
                    {
                        res.MeasuredTime = pFTime;
                    }
                    else
                    {
                        res.MeasuredTime = DateTime.Now;
                    }
                }
                rSym = (char)reader.Read();
                while (rSym != ':')
                {
                    rSym = (char)reader.Read();
                }
                rSym = (char)reader.Read();
                var MemoryString = string.Empty + rSym;
                rSym = (char)reader.Read();
                while (rSym != ' ')
                {
                    MemoryString += rSym;
                    rSym = (char)reader.Read();
                }
                MemoryString = MemoryString.Trim();
                var Culture = System.Globalization.CultureInfo.InvariantCulture;
                if (long.TryParse(MemoryString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out long kb))
                {
                    res.MemoryKB = kb;
                    return res;
                }
            }
            return null;
        }
        /// <summary>
        /// Преобразуем объект в строку с разметкой XML
        /// </summary>
        /// <returns>Представление среднего ФПС в виде XML</returns>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(typeof(UsedMemory));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных памяти из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static UsedMemory? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(UsedMemory));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (UsedMemory)rObj;
                    return d;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

    }
}
