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
    public class AverageFPS(string Line, CancellationToken? cancellation = null) : LogEntity(Line, cancellation)
    {
        /// <summary>
        /// Время когда сохранены данные FPS
        /// </summary>
        [XmlElement]
        public DateTime MeasuredTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Измеренный ФПС
        /// </summary>
        [XmlElement]
        public float FPS { get; set; } = 0;



        /// <inheritdoc/>
        protected override void Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Average server FPS"))
            {
                if(!SkipChar(' ', cancellation))
                {
                    return;
                }
                var TimeString = ReadToChar(' ', false, cancellation);
                if (TimeSpan.TryParse(TimeString, out var pTime))
                {
                    MeasuredTime = DateTime.MinValue.Add(pTime);
                }
                else
                {
                    if (DateTime.TryParse(TimeString, out var pFTime))
                    {
                        MeasuredTime = pFTime;
                    }
                    else
                    {
                        MeasuredTime = DateTime.Now;
                    }
                }
                if (!SkipToChar(':', cancellation))
                {
                    return;
                }
                var FPSString = ReadToChar(' ', true, cancellation);
                var Culture = System.Globalization.CultureInfo.InvariantCulture;
                if (float.TryParse(FPSString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out float fps))
                {
                    FPS = fps;
                    IsReadOk = true;
                }
            }
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
