using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие спуска флага
    /// </summary>
    public class Lowered:AdmEntity
    {
        /// <summary>
        /// Игрок который опускал флаг
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где это происходило
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Какой объект был спущен
        /// </summary>
        public string ItemName { get; set; } = string.Empty;
        /// <summary>
        /// Тип тотема на котором было выполнено действие опуска
        /// </summary>
        public string Totem { get; set; } = string.Empty;

        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Lowered? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Lowered)) as Lowered;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("has lowered"))
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
                        if (!string.IsNullOrEmpty(w))
                        {
                            ItemName = w.Trim();
                            w = ReadToChar(' ', true, cancellation);
                            w = ReadToChar(' ', true, cancellation);
                            if (!string.IsNullOrEmpty(w))
                            {
                                Totem = w.Trim();
                            }
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }
}
