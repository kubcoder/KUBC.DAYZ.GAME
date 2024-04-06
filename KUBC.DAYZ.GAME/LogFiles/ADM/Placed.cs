using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие размещения итема игроком
    /// </summary>
    public class Placed : ItemLogEntity
    {
    }

    /// <summary>
    /// Парсер события <see cref="Placed"/>
    /// </summary>
    public class PlacedParser : ADMPositionParser
    {
        private const string START = "placed";

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (logLine.Contains(START))
            {
                if (Init(logLine, cancellation))
                {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                    var res = new Placed()
                    {
                        Player = Player,
                        Position = Position,
                        Time = logTime.GetValueOrDefault()
                    };
#pragma warning restore CS8601
                    ReadToChar(' ', true, cancellation);
                    if (Reader != null)
                    {
                        res.ItemName = Reader.ReadToEnd();
                    }
                    return res;
                }
            }
            return null;
        }
    }
}
