using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Игрок написал в чат
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Время когда игрок написал в чат
        /// </summary>
        public DateTime ChatTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Ник игрока
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Текст чата
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Получить событие о чате из лога ADM
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время строчки лога</param>
        /// <returns>Если это нужная строчка то описание события чата, иначе null</returns>
        public static Chat? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("Chat("))
            {
                var tData = Line[5..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new Chat() { ChatTime = Time };
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
                    while ((rSym > 0) && (rSym != ':')) { rSym = Reader.Read(); }
                    rSym = Reader.Read();
                    while (rSym > 0)
                    {
                        res.Text += (char)rSym;
                        rSym = Reader.Read();
                    }
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }


        /// <summary>
        /// Преобразуем объект в строку с разметкой XML
        /// </summary>
        /// <returns>Представление данных в виде XML</returns>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(typeof(Chat));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных чата из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Chat? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(Chat));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (Chat)rObj;
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
