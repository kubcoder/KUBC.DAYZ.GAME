using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Sripts
{
    /// <summary>
    /// Лог выполнения скриптов
    /// </summary>
    /// <exception cref="NullReferenceException">
    /// Если вы попытаетесь парсить лог, но не создадите екземпляр <see cref="Parser"/> ваше приложение будет сильно крашить!!!
    /// </exception>
    public class Log : File
    {
        /// <summary>
        /// Парсер лога
        /// </summary>
        public ILogEntityFabric? Parser;
        
        /// <inheritdoc/>
        protected override ILogEntity? ParseLine(string LogLine)
        {
            if (Parser!=null)
            {
                return Parser.CreateEntity(LogLine);
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
