using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Описание события когда игрок потерял сознание
    /// </summary>
    public class Unconscious:AdmEntity
    {
        /// <summary>
        /// Игрок который потерял сознание
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Где произошла потеря сознания
        /// </summary>
        public Vector Position { get; set; } = new();

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("is unconscious"))
            {
                base.Init(Line, cancellation);

                var p = ReadPlayer( cancellation);
                if (p != null)
                {
                    Player = p;
                    var pos = ReadPosition(')',cancellation);
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


        /// <summary>
        /// Создать элемент данных подключения игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Unconscious? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Unconscious)) as Unconscious;
        }
    }
}
