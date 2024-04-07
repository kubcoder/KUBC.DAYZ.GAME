using KUBC.DAYZ.GAME.LogFiles.RPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Console
{
    /// <summary>
    /// Класс консольного файла игры
    /// </summary>
    public class Log : FileWithTime
    {
        /// <summary>
        /// Парсер лога
        /// </summary>
        private readonly ConsoleParser parser = new();
        /// <inheritdoc/>
        protected override ILogEntityFabric LogParser
        {
            get
            {
                if (CustomParser!=null)
                    return CustomParser;
                return parser;
            }
        }

        /// <summary>
        /// Дополнительный парсер который можно установить
        /// каким то значением. Установка в данное поле значение
        /// подменяет стандартный парсер <see cref="ConsoleParser"/>
        /// </summary>
        public ILogEntityFabric? CustomParser;

        /// <inheritdoc/>
        public override void OpenFile(FileInfo file)
        {
            base.OpenFile(file);
            LogStarted = file.CreationTime;
        }

        /// <summary>
        /// Добавить парсер в коллекцию
        /// </summary>
        /// <param name="parser">Добавляемый парсер</param>
        public override void SetParser(ILogEntityFabric parser)
        {
            this.parser.InserParser(parser);
        }
        /// <inheritdoc/>
        protected override bool FindStartTime(string Line)
        {
            return false;
        }
    }
}
