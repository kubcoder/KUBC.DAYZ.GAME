using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие отключения игрока 
    /// </summary>
    public class PlayerDisconect:AdmEntity
    {
        /// <summary>
        /// Игрок который отключился
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        
        
        /// <summary>
        /// Создать элемент данных отключения игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static PlayerDisconect? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(PlayerDisconect)) as PlayerDisconect;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("has been disconnected"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(')', cancellation);
                if (p != null)
                {
                    Player = p;
                    return true;
                }
            }
            return false;
        }
    }
}
