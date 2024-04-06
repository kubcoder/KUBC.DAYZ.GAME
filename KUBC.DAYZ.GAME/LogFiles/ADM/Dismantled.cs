using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие разрушения базы
    /// </summary>
    public class Dismantled : Built
    {
        
    }

    /// <summary>
    /// Парсер события Dismantled
    /// </summary>
    public class DismantledParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Dismantled";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Dismantled()
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
                while (w != "from")
                {
                    sB.Append(w);
                    sB.Append(' ');
                    w = ReadToChar(' ', true, cancellation);
                    if (string.IsNullOrEmpty(w))
                    {
                        w = "from";
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
