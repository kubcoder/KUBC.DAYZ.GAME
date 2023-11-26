using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие самоубийства игрока
    /// </summary>
    public class Suicide:AdmEntity
    {
        /// <summary>
        /// Игрок который самоубился
        /// </summary>
        public PlayerInfo Player { get; set; } = new();

        /// <summary>
        /// Место где игрок помер
        /// </summary>
        public Vector Position { get; set; } = [];

        /// <summary>
        /// Создать элемент данных очухивания игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Suicide? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Suicide)) as Suicide;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("committed suicide"))
            {
                base.Init(Line, cancellation);
                var p = ReadPlayer(' ', cancellation);
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
