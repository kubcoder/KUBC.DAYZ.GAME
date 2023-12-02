using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие свертывания некого объекта, например установленной разметки забора
    /// </summary>
    public class Folded:AdmEntity
    {
        /// <summary>
        /// Игрок который размещал
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где было свертывание объекта
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено свернул игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;


        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Folded? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Folded)) as Folded;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("folded"))
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
                        ReadToChar(' ', true, cancellation);
                        if (Reader != null)
                        {
                            ItemName = Reader.ReadToEnd();
                        }
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
