using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Информация о подозрении на неправльный парсинг данных
    /// </summary>
    /// <param name="entity">Какой элемент данных вызвал подозрение</param>
    /// /// <param name="logLine">Какая строчка использовалась для получения этих данных</param>
    public class WarningDataEventArgs(ILogEntity entity, string logLine):EventArgs
    {
        /// <summary>
        /// Какой элемент данных вызвал подозрение
        /// </summary>
        public ILogEntity Entity = entity;
        /// <summary>
        /// Какая строчка использовалась для получения этих данных
        /// </summary>
        public string LogLine = logLine;

        
    }
}
