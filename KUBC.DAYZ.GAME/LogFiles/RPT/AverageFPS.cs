using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Средний ФПС 
    /// </summary>
    public class AverageFPS
    {
        /// <summary>
        /// Время когда сохранены данные FPS
        /// </summary>
        public DateTime MeasuredTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Измеренный ФПС
        /// </summary>
        public float FPS { get; set; } = 0;

        /// <summary>
        /// Читаем данные из строки лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Данные о ФПС. Если вернули null значит строчка не подходящая</returns>
        public static AverageFPS? FromLogLine(string Line)
        {
            if (Line.Contains("Average server FPS"))
            {
                char rSym;
                var res = new AverageFPS();
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
                while(rSym!=':')
                {
                    rSym = (char)reader.Read();
                }
                rSym = (char)reader.Read();
                var FPSString = string.Empty + rSym;
                rSym = (char)reader.Read();
                while (rSym!=' ')
                {
                    FPSString += rSym;
                    rSym = (char)reader.Read();
                    
                }
                FPSString = FPSString.Trim();
                var Culture = System.Globalization.CultureInfo.InvariantCulture;
                if (float.TryParse(FPSString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out float fps))
                {
                    res.FPS = fps;
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
            var x = new XmlSerializer(typeof(AverageFPS));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных среднего фпс из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static AverageFPS? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(AverageFPS));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (AverageFPS)rObj;
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
