using System.Xml.Serialization;


namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие строительства
    /// </summary>
    public class Built
    {
        /// <summary>
        /// Время строительства
        /// </summary>
        public DateTime BuiltTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Ник нейм игрока
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Место где построено
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Объект строительства
        /// </summary>
        public string Construction { get; set; } = string.Empty;
        /// <summary>
        /// Какой элемент был построен
        /// </summary>
        public string Element { get; set; } = string.Empty;
        /// <summary>
        /// Какой инструмент использован
        /// </summary>
        public string Tool { get; set; } = string.Empty;

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
            var x = new XmlSerializer(typeof(Built));
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
        public static Built? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(Built));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (Built)rObj;
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
        /// Получаем описание строительства
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время когда строчка была записана</param>
        /// <returns>Стройка если удалось прочитать</returns>
        public static Built? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("Built"))
            {
                var tData = Line[7..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new Built() { BuiltTime = Time };
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
                    while ((rSym > 0) && (rSym != ' ')) { rSym = Reader.Read(); }
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                        {
                            res.Element += (char)rSym;
                        }
                    }
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' ')) { rSym = Reader.Read(); }
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                        {
                            res.Construction += (char)rSym;
                        }
                    }

                    bool NextStep = false;
                    while (!NextStep)
                    {
                        var tW = string.Empty;
                        rSym = 1;
                        while ((rSym > 0) && (rSym != ' '))
                        {
                            rSym = Reader.Read();
                            if ((rSym > 0) && (rSym != ' '))
                                tW += (char)rSym;
                        }
                        if (string.IsNullOrEmpty(tW.Trim()))
                            NextStep = true;
                        else
                        {
                            if (tW.Trim() == "with")
                                NextStep = true;
                            else
                            {
                                res.Construction += $" {tW}";
                            }
                        }
                    }

                    rSym = 1;
                    while (rSym > 0)
                    {
                        rSym = Reader.Read();
                        if (rSym > 0)
                        {
                            res.Tool += (char)rSym;
                        }
                    }
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }
    }
}
