using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Лог ошибок сервера
    /// </summary>
    public class Log : File
    {
        /// <summary>
        /// Парсер поиска нового краша
        /// </summary>
        private readonly CrashParser parser = new ();

        /// <inheritdoc/>
        protected override ILogEntity? ParseLine(string LogLine)
        {
            return parser.CreateEntity(LogLine);
        }
    }
}
