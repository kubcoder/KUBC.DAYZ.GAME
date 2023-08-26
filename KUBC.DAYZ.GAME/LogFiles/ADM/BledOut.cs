using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие игрок истек кровью
    /// </summary>
    public class BledOut
    {
        /// <summary>
        /// Время когда игрок помер
        /// </summary>
        public DateTime DieTime { get; set; } = DateTime.Now;
        /// <summary>
        /// С каким ником он подключился
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;

        /// <summary>
        /// Место где игрок помер
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Преобразуем объект в строку с разметкой XML
        /// </summary>
        /// <returns>Представление в виде XML</returns>
        public string GetXML()
        {
            var sb = new StringWriter();
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(sb, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                Indent = true
            });
            var x = new XmlSerializer(typeof(BledOut));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static BledOut? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(BledOut));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (BledOut)rObj;
                    return d;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получаем описание суицида игрока из строчки лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время когда строчка была записана</param>
        /// <returns>Суицид игрока если удалось прочитать</returns>
        public static BledOut? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("bled out"))
            {
                var tData = Line[7..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new BledOut() { DieTime = Time };
                    while ((rSym > 0) && (rSym != '"'))
                    {
                        res.NickName += (char)rSym;
                        rSym = Reader.Read();
                    }
                    while ((rSym > 0) && (rSym != '=')) { rSym = Reader.Read(); }
                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                            res.DayzID += (char)rSym;
                    }
                    while ((rSym > 0) && (rSym != '=')) { rSym = Reader.Read(); }
                    var sPos = string.Empty;
                    while ((rSym > 0) && (rSym != ')'))
                    {

                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ')'))
                            sPos += (char)rSym;

                    }
                    res.Position = Vector.FromLogString(sPos);
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }
    }
}
