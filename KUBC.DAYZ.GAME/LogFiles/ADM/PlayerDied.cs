using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие смерти игрока
    /// </summary>
    public class PlayerDied : AdmEntity
    {

        /// <summary>
        /// Игрок который упокоился
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Вода в игроке
        /// </summary>
        public float Water { get; set; } = 0;
        /// <summary>
        /// Энергия
        /// </summary>
        public float Energy { get; set; } = 0;
        /// <summary>
        /// Колличество открытых ран
        /// </summary>
        public int Bleeding { get; set; } = 0;
        /// <summary>
        /// Место где игрок помер
        /// </summary>
        public Vector Position { get; set; } = new();
        
        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static PlayerDied? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(PlayerDied)) as PlayerDied;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("died. Stats>"))
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
                        if (SkipToChar(':', cancellation))
                        {
                            var style = System.Globalization.NumberStyles.Number;
                            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                            var w = ReadToChar(' ', true, cancellation);
                            if (float.TryParse(w, style, culture, out var water))
                            {
                                Water = water;
                            }
                            if (SkipToChar(':', cancellation))
                            {
                                w = ReadToChar(' ', true, cancellation);
                                if (float.TryParse(w, style, culture, out var energy))
                                {
                                    Energy = energy;
                                }
                                if (SkipToChar(':', cancellation))
                                {
                                    w = ReadToChar(' ', true, cancellation);
                                    if (int.TryParse(w, out var bs))
                                    {
                                        Bleeding = bs;
                                    }
                                }

                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
