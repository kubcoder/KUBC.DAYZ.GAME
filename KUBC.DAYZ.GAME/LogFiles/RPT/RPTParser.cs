using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    internal class RPTParser : MultiEntityFabric
    {
        /// <summary>
        /// Креативим новый парсер
        /// </summary>
        public RPTParser() 
        {
            Creators.Add(new AverageFPSParser());
            Creators.Add(new UsedMemoryPaser());
            Creators.Add(new ConnectEventParser());
        }
    }
}
