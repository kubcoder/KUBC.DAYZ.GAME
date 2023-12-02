using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие "закопали барахло"
    /// </summary>
    public class DugIn:AdmEntity
    {
        /// <summary>
        /// Игрок который что-то закопал
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где он это делал
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено закопал игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
        

        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static DugIn? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(DugIn)) as DugIn;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Dug in"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(cancellation);
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
                            w = ReadToChar('<', true, cancellation);
                            if (!string.IsNullOrEmpty(w))
                            {
                                ItemName = w;
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
