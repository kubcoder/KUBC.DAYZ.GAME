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
            InserParser(new PlayerDamageParser());
            InserParser(new BledOutParser());
            InserParser(new BuiltParser());
            InserParser(new ChatParser());
            InserParser(new DismantledParser());
            InserParser(new DugInParser());
            InserParser(new DugOutParser());
            InserParser(new FoldedParser());
            InserParser(new LoweredParser());
            InserParser(new RaisedParser());
            InserParser(new UnmountedParser());
            InserParser(new MountedParser());
            InserParser(new PackedParser());
            InserParser(new PlacedParser());
            InserParser(new PlayerConnectParser());
            InserParser(new PlayerDisconnectParser());
            InserParser(new PlayerDiedParser());
            InserParser(new PlayerKilledParser());
        }
    }
}
