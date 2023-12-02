using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие когда игрок очнулся
    /// </summary>
    public class Regained: AdmEntity
    {
        /// <summary>
        /// Игрок который очнулся
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        
        /// <summary>
        /// Где произошла очухивание игрока
        /// </summary>
        public Vector Position { get; set; } = new();

        
        /// <summary>
        /// Создать элемент данных очухивания игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Regained? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Regained)) as Regained;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("regained consciousness"))
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
                        Dispose();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
