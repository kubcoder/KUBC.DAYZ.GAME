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
    public class ConectEvent(string Line, CancellationToken? cancellation = null) : LogEntity(Line, cancellation)
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
        public DateTime ConnectTime { get; set; } = DateTime.Now;

        /// <inheritdoc/>
        protected override void Init(string Line, CancellationToken? cancellation = null)
        {
            if (Line.Contains("is connected"))
            {
                if (Line.Contains("steamID"))
                {
                    if (!SkipChar(' ', cancellation))
                    {
                        return;
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
                        return;
                    }
                    var rNickName = ReadToChar('"', false, cancellation);
                    if (string.IsNullOrEmpty(rNickName))
                    {
                        return;
                    }
                    else
                    {
                        NickName = rNickName;
                    }

                    if (!SkipToChar('=', cancellation))
                    {
                        return;
                    }
                    var steamID = ReadToChar(')', true, cancellation);
                    if (steamID==null)
                    {
                        return;
                    }
                    if (long.TryParse(steamID, out var pSteamID))
                    {
                        SteamID = pSteamID;
                        IsReadOk = true;
                        Dispose();//Прихлопываем поток чтения строки
                    }
                }
            }
        }

        /// <summary>
        /// Проебразуем объект в строку с разметкой XML
        /// </summary>
        /// <returns>Представление подключения игрока в виде XML</returns>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(typeof(ConectEvent));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных подключения игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static ConectEvent? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(ConectEvent));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (ConectEvent)rObj;
                    return d;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
