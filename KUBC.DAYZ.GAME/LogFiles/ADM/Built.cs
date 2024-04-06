using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие строительства объекта
    /// </summary>
    public class Built : PositionLogEntity
    {
        /// <summary>
        /// Объект
        /// </summary>
        public string? Construction { get; set; }
        /// <summary>
        /// Какой элемент был построен/разрушен
        /// </summary>
        public string? Element { get; set; }
        /// <summary>
        /// Какой инструмент использован
        /// </summary>
        public string? Tool { get; set; }
    }
    /// <summary>
    /// Парсер события Built
    /// </summary>
    public class BuiltParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Built";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Built()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
                if (!SkipToChar(' ', cancellation))
                    return res;
                var sB = new StringBuilder();
                var w = ReadToChar(' ', true, cancellation);
                while (w != "on")
                {
                    sB.Append(w);
                    sB.Append(" ");
                    w = ReadToChar(' ', true, cancellation);
                    if (string.IsNullOrEmpty(w))
                    {
                        w = "on";
                    }
                }
                res.Element = sB.ToString().Trim();
                w = ReadToChar(' ', true, cancellation);
                sB.Clear();
                while (w != "with")
                {
                    sB.Append(w);
                    sB.Append(" ");
                    w = ReadToChar(' ', true, cancellation);
                    if (string.IsNullOrEmpty(w))
                    {
                        w = "with";
                    }
                }
                res.Construction = sB.ToString().TrimEnd();
                if (Reader != null)
                {
                    res.Tool = Reader.ReadToEnd().Trim();
                }
                return res;
            }
            return null;
        }
    }
}
