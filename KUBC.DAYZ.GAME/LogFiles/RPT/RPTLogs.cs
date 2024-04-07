using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Коллекция логов RPT
    /// </summary>
    /// <param name="PathProfiles">Папка профилей игры</param>
    public class RPTLogs(DirectoryInfo PathProfiles) : GameLogs(PathProfiles)
    {
        /// <inheritdoc/>
        protected override string GetFindString()
        {
            return "*.rpt";
        }
    }
}
