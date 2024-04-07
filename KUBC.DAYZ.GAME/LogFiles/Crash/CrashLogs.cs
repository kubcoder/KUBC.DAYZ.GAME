using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Коллекция логов ошибок
    /// </summary>
    /// <param name="PathProfiles">Папка профилей игры</param>
    public class CrashLogs(DirectoryInfo PathProfiles) : GameLogs(PathProfiles)
    {
        /// <inheritdoc/>
        protected override string GetFindString()
        {
            return "crash*.log";
        }
    }
}
