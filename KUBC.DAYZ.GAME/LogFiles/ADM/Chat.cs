using KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Игрок написал в чат
    /// </summary>
    public class Chat : AdmEntity
    {
        /// <summary>
        /// Игрок который говорил
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Текст чата
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Chat"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(cancellation);
                if (p != null)
                {
                    Player = p;
                    if (!SkipToChar(':', cancellation))
                        return false;
                    if (Reader != null)
                    {
                        Text = Reader.ReadToEnd().Trim();
                    }
                    if (!string.IsNullOrEmpty(Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Создать элемент данных чата из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Chat? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Chat)) as Chat;
        }
    }
}
