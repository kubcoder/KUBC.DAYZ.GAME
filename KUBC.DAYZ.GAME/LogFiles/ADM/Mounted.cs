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
    public class Mounted : ItemLogEntity
    {
        /// <summary>
        /// Куда это прикрутили
        /// </summary>
        public string Construction { get; set; } = string.Empty;

    }

    /// <summary>
    /// Парсер события <see cref="Mounted"/>
    /// </summary>
    public class MountedParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Mounted";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Mounted()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                ReadToChar(' ', true, cancellation);
                ReadToChar(' ', true, cancellation);
                ReadToChar(' ', true, cancellation);
                var w = ReadToChar(' ', true, cancellation);
                if (!string.IsNullOrEmpty(w))
                {
                    res.ItemName = w.Trim();
                    w = ReadToChar(' ', true, cancellation);
                    if (w == "on")
                    {
                        w = ReadToChar(' ', true, cancellation);
                        if (!string.IsNullOrEmpty(w))
                        {
                            res.Construction = w.Trim();
                        }
                    }
                    return res;
                }
            }
            return null;
        }
    }
}
