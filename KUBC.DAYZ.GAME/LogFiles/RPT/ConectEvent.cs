using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Событие подключения игрока
    /// </summary>
    /// <remarks>
    /// Представление данных которое в логе записывается строчкой 
    /// <code>Player "Over" is connected (steamID=76561199141600564)</code>
    /// </remarks>
    public class ConectEvent : LogEntity
    {
        
        /// <summary>
        /// Steam идентификатор игрока
        /// </summary>
        public long SteamID { get; set; } = 0;
        /// <summary>
        /// Ник с которым игрок приконектился
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Время когда игрок подключился
        /// </summary>
        [XmlAttribute]
        public DateTime ConnectTime { get; set; } = DateTime.Now;

        /// <inheritdoc/>
        public override bool Init(string Line, CancellationToken? cancellation = null)
        {
            base.Init(Line, cancellation);
            if (Line.Contains("is connected"))
            {
                if (Line.Contains("steamID"))
                {
                    if (!SkipChar(' ', cancellation))
                    {
                        return false;
                    }
                    var TimeString = ReadToChar(' ', false, cancellation);
                    if (TimeSpan.TryParse(TimeString, out var pTime))
                    {
                        ConnectTime = DateTime.MinValue.Add(pTime);
                    }
                    else
                    {
                        if (DateTime.TryParse(TimeString, out var pFTime))
                        {
                            ConnectTime = pFTime;
                        }
                        else
                        {
                            ConnectTime = DateTime.Now;
                        }
                    }
                    if (!SkipToChar('"', cancellation))
                    { 
                        return false;
                    }
                    var rNickName = ReadToChar('"', false, cancellation);
                    if (string.IsNullOrEmpty(rNickName))
                    {
                        return false;
                    }
                    else
                    {
                        NickName = rNickName;
                    }

                    if (!SkipToChar('=', cancellation))
                    {
                        return false;
                    }
                    var steamID = ReadToChar(')', true, cancellation);
                    if (steamID==null)
                    {
                        return false;
                    }
                    if (long.TryParse(steamID, out var pSteamID))
                    {
                        SteamID = pSteamID;
                        Dispose();//Прихлопываем поток чтения строки
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
        public static ConectEvent? FromXML(string xml)
        {
            return ReadFromXML(xml, typeof(ConectEvent)) as ConectEvent;
        }
    }
}
