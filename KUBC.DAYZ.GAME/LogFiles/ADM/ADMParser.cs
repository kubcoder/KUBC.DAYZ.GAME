using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Парсер лога ADM
    /// </summary>
    internal class ADMParser : MultiEntityFabric
    {
        public ADMParser() 
        {
            AddExtParser(new BledOutParser());
            AddExtParser(new BuiltParser());
            AddExtParser(new ChatParser());
            AddExtParser(new DismantledParser());
            AddExtParser(new DugInParser());
            AddExtParser(new DugOutParser());
            AddExtParser(new FoldedParser());
            AddExtParser(new LoweredParser());
            AddExtParser(new RaisedParser());
            AddExtParser(new UnmountedParser());
            AddExtParser(new MountedParser());
            AddExtParser(new PackedParser());
        }
    }
}
