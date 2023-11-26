using KUBC.DAYZ.GAME.LogFiles.RPT;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие игрок истек кровью
    /// </summary>
    public class BledOut:AdmEntity
    {
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
                
                var p = ReadPlayer(' ', cancellation);
                if (p!= null) 
                {
                    Player = p;
                    var pos = ReadPosition(')', cancellation);
                    if (pos!=null)
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
