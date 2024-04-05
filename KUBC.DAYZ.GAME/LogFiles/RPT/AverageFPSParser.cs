using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Парсер FPS
    /// </summary>
    public class AverageFPSParser : RPTStringParser
    {
        private const string START = "Average server FPS";

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Contains(START))
            {
                if (Init(logLine, cancellation))
                {
                    var FPSString = ReadToChar(' ', true, cancellation);
                    if (!string.IsNullOrEmpty(FPSString)) 
                    {
                        var Culture = System.Globalization.CultureInfo.InvariantCulture;
                        if (float.TryParse(FPSString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out float fps))
                        {
                            Dispose();
                            if (logTime!=null)
                            {
                                return new AverageFPS() { FPS = fps, Time = logTime.Value };
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
