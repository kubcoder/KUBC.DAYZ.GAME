using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Монтаж элемента конструкции
    /// </summary>
    /// <remarks>
    /// Замечено когда игрок прикручивает колючую проволку до забора
    /// Возможно еще где-то работает
    /// </remarks>
    public class Mounted : AdmEntity
    {
        /// <summary>
        /// Игрок который что-то прикрутил
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где он это делал
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено прикрутил игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
        /// <summary>
        /// Куда это прикрутили
        /// </summary>
        public string Construction { get; set; } = string.Empty;

        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Mounted? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Mounted)) as Mounted;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Mounted"))
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
                        if (w=="on")
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
