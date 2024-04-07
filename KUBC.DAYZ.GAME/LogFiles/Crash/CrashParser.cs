using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Парсер краша
    /// </summary>
    public class CrashParser : StringParser
    {
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Equals(GetTAG()))
            {
                return new CrashEntity();
            }
            return null;
        }

        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "------------------------------------";
        }
    }
}
