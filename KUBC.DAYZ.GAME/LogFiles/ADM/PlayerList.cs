using KUBC.DAYZ.GAME.LogFiles.Crash;
using System.Numerics;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Список игроков которые в игре
    /// </summary>
    public class PlayerList : AdmEntity
    {
        /// <summary>
        /// Список игроков
        /// </summary>
        [XmlElement(ElementName ="P")]
        public List<PlayerPosition> Players { get; set; } = [];

        /// <summary>
        /// Создать элемент данных подключения игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static PlayerList? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(PlayerList)) as PlayerList;
        }

        private bool? Reading;

        /// <inheritdoc/>
        public override bool IsReadSucces()
        {
            if (Reading!=null)
            {
                return !Reading.Value;
            }
            return false;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("PlayerList log"))
            {
                Players.Clear();
                Reading = true;
                return true;
            }
            return false;
        }
        /// <inheritdoc/>
        public override bool AppendLine(string Line, CancellationToken? cancellation = null)
        {
            if (Line.TrimEnd() == "#####")
            {
                Reading = false;
                return true;
            }
            base.Init(Line, cancellation);
            var p = ReadPlayer(cancellation);
            var pos = ReadPosition(')', cancellation);
            var rp = new PlayerPosition();
            if (p != null)
                rp.Player = p;
            if (pos!=null)
                rp.Position = pos;
            if (Line.Contains("(DEAD)"))
                rp.IsDead = true;
            Players.Add(rp);
            Dispose();
            return false;
                
        }
    }
}
