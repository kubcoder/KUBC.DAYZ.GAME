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
    public class AverageFPS : LogEntity
    {
        /// <summary>
        /// Время когда сохранены данные FPS
        /// </summary>
        [XmlAttribute]
        public DateTime MeasuredTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Измеренный ФПС
        /// </summary>
        [XmlText]
        public float FPS { get; set; } = 0;



        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            base.Init(Line, cancellation);
            if (Line.Contains("Average server FPS"))
            {
                if(!SkipChar(' ', cancellation))
                {
                    return false;
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
                    return false;
                }
                var FPSString = ReadToChar(' ', true, cancellation);
                var Culture = System.Globalization.CultureInfo.InvariantCulture;
                if (float.TryParse(FPSString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out float fps))
                {
                    FPS = fps;
                    Dispose();
                    return true;
                }
            }
            return false;
        }

        
        /// <summary>
        /// Создать элемент данных среднего фпс из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static AverageFPS? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(AverageFPS)) as AverageFPS;
        }
    }
    
}
