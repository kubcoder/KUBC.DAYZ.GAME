using System.Xml.Linq;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие размещения итема игроком
    /// </summary>
    public class Placed : AdmEntity
    {
        
        /// <summary>
        /// Игрок который размещал
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где размещено
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено разместил игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

        
        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Placed? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Placed)) as Placed;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("placed"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(cancellation);
                if (p != null) 
                {
                    Player = p;
                    var pos = ReadPosition(')', cancellation);
                    if (pos != null)
                    {
                        Position = pos;
                        ReadToChar(' ', true, cancellation);
                        if (Reader != null)
                        {
                            ItemName = Reader.ReadToEnd();
                        }
                        return true;
                    }
                }
                
            }
            return false;
        }
    }
}
