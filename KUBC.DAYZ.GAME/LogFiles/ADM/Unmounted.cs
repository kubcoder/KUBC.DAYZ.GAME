using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Демонтаж навесных элементов
    /// </summary>
    public class Unmounted:AdmEntity
    {
        /// <summary>
        /// Игрок который что-то открутил
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где он это делал
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено открутил игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
        /// <summary>
        /// Откуда это скрутили
        /// </summary>
        public string Construction { get; set; } = string.Empty;

        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Unmounted? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Unmounted)) as Unmounted;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Unmounted"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(' ', cancellation);
                if (p != null)
                {
                    Player = p;
                    var pos = ReadPosition(')', cancellation);
                    if (pos != null)
                    {
                        Position = pos;
                        var w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        w = ReadToChar(' ', true, cancellation);
                        if (!string.IsNullOrEmpty(w))
                        {
                            ItemName = w.Trim();
                        }
                        w = ReadToChar(' ', true, cancellation);
                        if (w == "from")
                        {
                            w = ReadToChar(' ', true, cancellation);
                            if (!string.IsNullOrEmpty(w))
                            {
                                Construction = w.Trim();
                            }
                        }
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
