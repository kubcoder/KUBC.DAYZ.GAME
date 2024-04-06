using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Фабрика создания данных из лога в котором предусмотрено дофига событий
    /// </summary>
    public class MultiEntityFabric : ILogEntityFabric
    {
        /// <summary>
        /// Список доступных парсеров
        /// </summary>
        protected List<ILogEntityFabric> Creators = [];

        /// <inheritdoc/>
        public ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            foreach(var creator in Creators) 
            {
                var entity = creator.CreateEntity(logLine, cancellation);
                if (entity != null) 
                {
                    return entity;
                }
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested))
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// Добавить новый парсер
        /// </summary>
        /// <param name="eParser">добавляемый парсер</param>
        public void InserParser(ILogEntityFabric eParser)
        {
            Creators.Add(eParser);
        }
    }
}
