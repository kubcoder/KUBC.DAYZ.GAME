using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Коллекция логов ADM
    /// </summary>
    /// <param name="PathProfiles">Папка профилей игры</param>
    public class ADMLogs(DirectoryInfo PathProfiles) : GameLogs(PathProfiles)
    {
        /// <inheritdoc/>
        protected override string GetFindString()
        {
            return "*.adm";
        }
    }
}
