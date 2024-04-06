using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие свертывания некого объекта, например установленной разметки забора
    /// </summary>
    public class Folded : ItemLogEntity
    {
    }
    /// <summary>
    /// Парсер события <see cref="Folded"/>
    /// </summary>
    public class FoldedParser : ADMPositionParser
    {
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "folded";
        }

        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                var res = new Folded()
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
            return null;
        }
    }
}
