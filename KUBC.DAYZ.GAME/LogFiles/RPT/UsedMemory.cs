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
    public class UsedMemory(string Line, CancellationToken? cancellation = null) : LogEntity(Line, cancellation)
    {
        /// <summary>
        /// Время когда сохранены данные о памяти
        /// </summary>
        public DateTime MeasuredTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Используемая память в КБ
        /// </summary>
        public long MemoryKB { get; set; } = 0;

        /// <inheritdoc/>
        protected override void Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Used memory"))
            {
                if (!SkipChar(' ', cancellation))
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
                var MemoryString = ReadToChar(' ', true, cancellation);
                var Culture = System.Globalization.CultureInfo.InvariantCulture;
                if (long.TryParse(MemoryString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out long memoryKB))
                {
                    MemoryKB = memoryKB;
                    IsReadOk = true;
                }
            }
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
