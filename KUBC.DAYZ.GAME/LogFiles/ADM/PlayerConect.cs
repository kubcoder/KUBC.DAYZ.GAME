using KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие подключения игрока
    /// </summary>
    public class PlayerConect:AdmEntity
    {
        
        /// <summary>
        /// Игрок который подключился
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("is connected"))
            {
                base.Init(Line, cancellation);
                string? w;
                if (!SkipToChar('"', cancellation))
                    return false;
                w = ReadToChar('"', true, cancellation);
                if (!string.IsNullOrEmpty(w))
                {
                    Player.NickName = w.Trim();
                    if (!SkipToChar('=', cancellation))
                        return false;
                    var dayzID = this.ReadToChar(')', true, cancellation);
                    if (dayzID != null)
                    {
                        Player.ID = dayzID;
                        return true;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static PlayerConect? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(PlayerConect)) as PlayerConect;
        }

    }
}
