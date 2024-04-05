using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Интерфейс парсинга событий из журнала
    /// </summary>
    public interface ILogEntityFabric
    {
        /// <summary>
        /// Создать событие из строчки лога, если это возможно
        /// </summary>
        /// <param name="logLine">Строчка лога</param>
        /// <param name="cancellation">Токен отмены чтения строчки линии</param>
        /// <returns>Элемент данных прочитанный из лога, или NULL если этого сделать не получилося</returns>
        public ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null);
    }
}
