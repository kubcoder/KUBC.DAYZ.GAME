using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{

    
    /// <summary>
    /// Список игроков которые в игре
    /// </summary>
    public class PlayerList : LogEntity
    {
        /// <summary>
        /// Список игроков
        /// </summary>
        [XmlElement(ElementName = "P")]
        public List<PositionLogEntity> Players { get; set; } = [];

        /// <summary>
        /// Чтение завершили
        /// </summary>
        private bool EndRead = false;

        private readonly PlayerListItemParser PlayerParser = new();

        /// <inheritdoc/>
        public override bool AppendLine(string Line)
        {
            if (Line.Contains("#####"))
            {
                EndRead = true;
                return true;
            }
            if (PlayerParser.CreateEntity(Line) is PositionLogEntity entity)
            {
                Players.Add(entity);
            }
            return false;
        }
        /// <inheritdoc/>
        public override bool IsEndRead()
        {
            return EndRead;
        }
    }
    /// <summary>
    /// Парсер начала списка игроков
    /// </summary>
    public class PlayerListParser : ADMStringParser
    {
        
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "PlayerList log";
        }
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
                return new PlayerList()
                {
                    Time = logTime.GetValueOrDefault()
                };
            }
            return null;
        }
    }
    /// <summary>
    /// Читатель строчки с игроком в какой то позиции
    /// </summary>
    public class PlayerListItemParser : ADMPositionParser
    {
        /// <inheritdoc/>
        public override ILogEntity? CreateEntity(string logLine, CancellationToken? cancellation = null)
        {
            if (Init(logLine, cancellation))
            {
#pragma warning disable CS8601 // Возможные null отсечены в родительском классе
                return new PositionLogEntity()
                {
                    Player = Player,
                    Position = Position,
                    Time = logTime.GetValueOrDefault()
                };
#pragma warning restore CS8601
            }
            return null;
        }
        /// <inheritdoc/>
        protected override string GetTAG()
        {
            return "Player";
        }
    }
}
