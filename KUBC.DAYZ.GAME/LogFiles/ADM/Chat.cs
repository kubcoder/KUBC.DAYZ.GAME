using KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Игрок написал в чат
    /// </summary>
    public class Chat : LogEntity
    {
        /// <summary>
        /// Время когда игрок написал в чат
        /// </summary>
        [XmlAttribute]
        public DateTime Time { get; set; } = DateTime.Now;
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
                if (!SkipToChar('"'))
                    return false;
                var nikname = ReadToChar('"', true, cancellation);
                if (nikname != null)
                {
                    if (!SkipToChar('='))
                        return false;
                    var dayzID = ReadToChar(')', true, cancellation);
                    if (dayzID != null)
                    {
                        if (!SkipToChar(':'))
                            return false;
                        if (Reader!=null)
                        {
                            Text = Reader.ReadToEnd().Trim();
                        }
                        if (!string.IsNullOrEmpty(Text))
                        {
                            Player.NickName = nikname;
                            Player.ID = dayzID;
                            return true;
                        }
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
