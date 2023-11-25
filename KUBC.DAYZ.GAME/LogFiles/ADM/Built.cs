using System.Xml.Serialization;


namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие строительства
    /// </summary>
    public class Built:LogEntity
    {
        /// <summary>
        /// Время строительства
        /// </summary>
        public DateTime Time { get; set; } = DateTime.Now;
        /// <summary>
        /// Игрок который строил
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Место где построено
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Объект строительства
        /// </summary>
        public string? Construction { get; set; }
        /// <summary>
        /// Какой элемент был построен
        /// </summary>
        public string? Element { get; set; }
        /// <summary>
        /// Какой инструмент использован
        /// </summary>
        public string? Tool { get; set; }

        
        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Built? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Built)) as Built;
        }
        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Built"))
            {
                base.Init(Line, cancellation);
                if (!SkipToChar('"'))
                    return false;
                var nikname = ReadToChar('"', true, cancellation);
                if (nikname != null)
                {
                    if (!SkipToChar('='))
                        return false;
                    var dayzID = ReadToChar(' ', true, cancellation);
                    if (dayzID != null)
                    {
                        if (!SkipToChar('='))
                            return false;
                        var posString = ReadToChar(')', true, cancellation);
                        if (posString != null)
                        {
                            Position = Vector.FromLogString(posString);
                            Player.NickName = nikname;
                            Player.ID = dayzID;
                            if (!SkipToChar(' '))
                                return true;
                            Element = ReadToChar(' ', true, cancellation);
                            if (!SkipToChar(' '))
                                return true;
                            Construction = this.ReadToChar(' ', true, cancellation);
                            if (!SkipToChar(' '))
                                return true;
                            if (Reader!=null)
                            {
                                Tool = Reader.ReadToEnd().Trim();
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
