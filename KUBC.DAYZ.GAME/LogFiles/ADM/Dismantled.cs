using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие разрушения базы
    /// </summary>
    public class Dismantled : AdmEntity
    {
        /// <summary>
        /// Игрок который Разбирал
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        /// <summary>
        /// Место где разрушено
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Объект строительства
        /// </summary>
        public string? Construction { get; set; }
        /// <summary>
        /// Какой элемент был разрушен
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
        public static Dismantled? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(Dismantled)) as Dismantled;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("Dismantled"))
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
                        if (!SkipToChar(' '))
                            return true;
                        Element = string.Empty;
                        var w = ReadToChar(' ', true, cancellation);
                        while (w != "from")
                        {
                            Element += w;
                            Element += " ";
                            w = ReadToChar(' ', true, cancellation);
                            if (string.IsNullOrEmpty(w))
                                w = "from";
                        }
                        Element = Element.TrimEnd();
                        w = ReadToChar(' ', true, cancellation);
                        Construction = string.Empty;
                        while (w != "with")
                        {
                            Construction += w;
                            Construction += " ";
                            w = ReadToChar(' ', true, cancellation);
                            if (string.IsNullOrEmpty(w))
                                w = "with";
                        }
                        Construction = Construction.TrimEnd();
                        if (Reader != null)
                        {
                            Tool = Reader.ReadToEnd().Trim();
                        }
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
