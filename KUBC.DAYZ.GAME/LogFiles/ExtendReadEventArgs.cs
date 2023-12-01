using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Аргументы события внешнего чтения лога
    /// </summary>
    /// <param name="cancellationToken">Токен отмены действия</param>
    /// <param name="Line">Строчка которую нужно прочитать</param>
    public class ExtendReadEventArgs(string Line, CancellationToken? cancellationToken)
    {
        /// <summary>
        /// Строчка лога для разбора
        /// </summary>
        public string Line = Line;
        /// <summary>
        /// Токен отмены действия
        /// </summary>
        public CancellationToken? cancellation = cancellationToken;
    }
}
