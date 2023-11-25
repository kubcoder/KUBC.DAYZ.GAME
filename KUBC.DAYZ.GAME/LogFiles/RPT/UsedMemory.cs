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
    public class UsedMemory : LogEntity
    {
        /// <summary>
        /// Время когда сохранены данные о памяти
        /// </summary>
        [XmlAttribute]
        public DateTime MeasuredTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Используемая память в КБ
        /// </summary>
        [XmlText]
        public long MemoryKB { get; set; } = 0;

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            base.Init(Line, cancellation);
            if (Line.Contains("Used memory"))
            {
                if (!SkipChar(' ', cancellation))
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
                var MemoryString = ReadToChar(' ', true, cancellation);
                var Culture = System.Globalization.CultureInfo.InvariantCulture;
                if (long.TryParse(MemoryString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out long memoryKB))
                {
                    MemoryKB = memoryKB;
                    Dispose();
                    return true;
                }
            }
            return false;
        }

        
        
        /// <summary>
        /// Создать элемент данных памяти из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static UsedMemory? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(UsedMemory)) as UsedMemory;
        }

    }
}
