using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Информация для администраторов от игрока
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Время когда игрок написал жалобу
        /// </summary>
        public DateTime ReportTime { get; set; } = DateTime.Now;

        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Текст жалобы
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Получить событие о жалобе из лога ADM
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время строчки лога</param>
        /// <returns>Если это нужная строчка то описание жалобы, иначе null</returns>
        public static Report? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("PLAYER REPORT:"))
            {
                if (Line.Length > 34)
                {
                    var tData = Line[34..];
                    var Reader = new StringReader(tData);
                    var rSym = Reader.Read();

                    while ((rSym > 0) && (rSym != '<'))
                    {
                        rSym = Reader.Read();
                    }
                    var res = new Report() { ReportTime = Time };
                    while ((rSym > 0) && (rSym != '>'))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != '>'))
                            res.DayzID += (char)rSym;
                    }
                    if (res.DayzID.Length != 44)
                        return null;
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
        /// Проебразуем объект в строку с разметкой XML
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
            var x = new XmlSerializer(typeof(Report));
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
        public static Report? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(Report));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (Report)rObj;
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
