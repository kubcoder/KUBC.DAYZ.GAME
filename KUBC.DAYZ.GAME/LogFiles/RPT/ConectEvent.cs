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
    public class ConectEvent
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
        /// <summary>
        /// Получить строчку подключения из лога
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        public static ConectEvent? FromLogLine(string Line)
        {
            if (Line.Contains("is connected"))
            {
                if (Line.Contains("steamID"))
                {
                    char rSym;
                    var res = new ConectEvent();
                    var reader = new StringReader(Line);
                    var TimeString = string.Empty;
                    bool EndStep = false;
                    while (!EndStep)
                    {
                        rSym = (char)reader.Read();
                        if (rSym != ' ')
                        {
                            TimeString += rSym;
                        }
                        else
                        {
                            EndStep = true;
                        }
                    };
                    if (TimeSpan.TryParse(TimeString, out var pTime))
                    {
                        res.ConnectTime = DateTime.MinValue.Add(pTime);
                    }
                    else
                    {
                        if (DateTime.TryParse(TimeString, out var pFTime))
                        {
                            res.ConnectTime = pFTime;
                        }
                        else
                        {
                            res.ConnectTime = DateTime.Now;
                        }
                    }
                    while ((char)reader.Read() != '"') { };
                    EndStep = false;
                    while (!EndStep)
                    {
                        rSym = (char)reader.Read();
                        if (rSym != '"')
                        {
                            res.NickName += rSym;
                        }
                        else
                        {
                            EndStep = true;
                        }
                    }
                    while ((char)reader.Read() != '=') { };
                    var SteamIDString = string.Empty;
                    EndStep = false;
                    while (!EndStep)
                    {
                        rSym = (char)reader.Read();
                        if (rSym != ')')
                        {
                            SteamIDString += rSym;
                        }
                        else
                        {
                            EndStep = true;
                        }
                    }
                    if (long.TryParse(SteamIDString, out var pSteamID))
                    {
                        res.SteamID = pSteamID;
                        return res;
                    }

                }
            }
            return null;
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
