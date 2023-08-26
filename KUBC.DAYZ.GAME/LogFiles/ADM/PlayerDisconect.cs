using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие отключения игрока 
    /// </summary>
    public class PlayerDisconect
    {
        /// <summary>
        /// Время когда игрок отключился
        /// </summary>
        public DateTime DisconectTime { get; set; } = DateTime.Now;
        /// <summary>
        /// С каким ником он отключился
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Получить событие о отключении игрока из лога ADM
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время строчки лога</param>
        /// <returns>Если это нужная строчка то описание события отключения, иначе null</returns>
        public static PlayerDisconect? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("has been disconnected"))
            {
                var tData = Line[7..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new PlayerDisconect() { DisconectTime = Time };
                    while ((rSym > 0) && (rSym != '"'))
                    {
                        res.NickName += (char)rSym;
                        rSym = Reader.Read();
                    }
                    while ((rSym > 0) && (rSym != '=')) { rSym = Reader.Read(); }
                    while ((rSym > 0) && (rSym != ')'))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ')'))
                            res.DayzID += (char)rSym;

                    }
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }

        /// <summary>
        /// Проебразуем объект в строку с разметкой XML
        /// </summary>
        /// <returns>Представление краша в виде XML</returns>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(typeof(PlayerDisconect));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных отключения игрока из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static PlayerDisconect? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(PlayerDisconect));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (PlayerDisconect)rObj;
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
