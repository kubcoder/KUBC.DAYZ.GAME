using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    internal class Parser : MultiEntityFabric
    {
        /// <summary>
        /// Креативим новый парсер
        /// </summary>
        public Parser() 
        {
            Creators.Add(new AverageFPSParser());
        }
    }
}
