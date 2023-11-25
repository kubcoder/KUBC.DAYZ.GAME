using KUBC.DAYZ.GAME.LogFiles.RPT;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие игрок истек кровью
    /// </summary>
    public class BledOut:LogEntity
    {
        /// <summary>
        /// Время когда игрок помер
        /// </summary>
        [XmlAttribute]
        public DateTime Time { get; set; } = DateTime.Now;
        /// <summary>
        /// Игрок который вытек
        /// </summary>
        public PlayerInfo Player { get; set; } = new();
        
        /// <summary>
        /// Место где игрок помер
        /// </summary>
        public Vector Position { get; set; } = new();
        
        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static BledOut? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(BledOut)) as BledOut;
        }

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("bled out"))
            {
                base.Init(Line, cancellation);
                if (!SkipToChar('"'))
                    return false;
                var nikname = this.ReadToChar('"', true, cancellation);
                if (nikname!=null) 
                {
                    if (!SkipToChar('='))
                        return false;
                    var dayzID = this.ReadToChar(' ', true, cancellation);
                    if (dayzID!=null)
                    {
                        if (!SkipToChar('='))
                            return false;
                        var posString = this.ReadToChar(')', true, cancellation);
                        if (posString!=null) 
                        {
                            Position = Vector.FromLogString(posString);
                            Player.NickName = nikname;
                            Player.ID = dayzID;
                            Dispose();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
