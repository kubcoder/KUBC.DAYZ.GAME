using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Класс файла лога
    /// </summary>
    public class Log : FileWithTime
    {
        /// <summary>
        /// Набор парсеров для лога ADM
        /// </summary>
        private ADMParser parser = new ADMParser();
        /// <inheritdoc/>
        protected override ILogEntityFabric LogParser => parser;
        
        /// <inheritdoc/>
        protected override bool FindStartTime(string Line)
        {
            throw new NotImplementedException();
        }
        
    }
}
