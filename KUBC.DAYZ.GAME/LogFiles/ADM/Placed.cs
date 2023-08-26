using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие размещения итема игроком
    /// </summary>
    public class Placed
    {
        /// <summary>
        /// Время размещения итема
        /// </summary>
        public DateTime PlacedTime { get; set; } = DateTime.Now;
        /// <summary>
        /// Ник нейм игрока
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;

        /// <summary>
        /// Место где размещено
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Что имено разместил игрок
        /// </summary>
        public string ItemName { get; set; } = string.Empty;

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
            var x = new XmlSerializer(typeof(Placed));
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
        public static Placed? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(Placed));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (Placed)rObj;
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
        /// Получаем описание размещения итема
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время когда строчка была записана</param>
        /// <returns>Размещение если удалось прочитать</returns>
        public static Placed? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("placed"))
            {
                var tData = Line[7..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new Placed() { PlacedTime = Time };
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
                    for (int i = 0; i < 8; i++)
                        Reader.Read();
                    while (rSym > 0)
                    {
                        rSym = Reader.Read();
                        if (rSym > 0)
                            res.ItemName += (char)rSym;
                    }
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }
    }
}
