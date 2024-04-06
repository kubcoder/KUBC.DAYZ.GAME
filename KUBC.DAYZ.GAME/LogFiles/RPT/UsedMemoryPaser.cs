using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Парсер используемой памяти
    /// </summary>
    public class UsedMemoryPaser : RPTStringParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Used memory";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
                if (!SkipToChar(':', cancellation))
                {
                    return null;
                }
                var MemoryString = ReadToChar(' ', true, cancellation);
                if (!string.IsNullOrEmpty(MemoryString))
                {
                    var Culture = System.Globalization.CultureInfo.InvariantCulture;
                    if (long.TryParse(MemoryString, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out long memoryKB))
                    {
                        Dispose();
                        if (logTime != null)
                        {
                            return new UsedMemory() { MemoryKB = memoryKB, Time = logTime.Value };
                        }
                    }
                }
            }
            return null;
        }
    }
}
