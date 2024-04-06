using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Упаковка объекта для переноски
    /// </summary>
    public class Packed : ItemLogEntity
    {
        /// <summary>
        /// Инструмент которым пользовался игрок
        /// </summary>
        /// <remarks>
        /// На данный момент в коде игры жестко задано что
        /// это всегда hands, но раз появилось значит возможно это расширят...
        /// но не факт.
        /// </remarks>
        public string Tool { get; set; } = string.Empty;
    }

    /// <summary>
    /// Парсер события <see cref="Packed"/>
    /// </summary>
    public class PackedParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "packed";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {

            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Packed()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                ReadToChar(' ', true, cancellation);
                var w = ReadToChar(' ', true, cancellation);
                var sB = new StringBuilder();
                if (!string.IsNullOrEmpty(w))
                {
                    sB.Append(w.Trim());
                }
                while (!string.IsNullOrEmpty(w))
                {
                    w = ReadToChar(' ', true, cancellation);
                    if (w == "with")
                    {
                        w = null;
                    }
                    else
                    {
                        sB.Append(' ');
                        sB.Append(w);

                    }
                }
                res.ItemName = sB.ToString();
                sB.Clear();
                w = ReadToChar(' ', true, cancellation);
                if (!string.IsNullOrEmpty(w))
                {
                    sB.Append(w.Trim());
                }
                while (!string.IsNullOrEmpty(w))
                {
                    w = ReadToChar(' ', true, cancellation);
                    sB.Append(' ');
                    sB.Append(w);
                }
                res.Tool = sB.ToString().TrimEnd();
                return res;
            }
            return null;
        }
    }
}
