using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие смерти игрока
    /// </summary>
    public class PlayerDied
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
        /// Вода в игроке
        /// </summary>
        public float Water { get; set; } = 0;
        /// <summary>
        /// Энергия
        /// </summary>
        public float Energy { get; set; } = 0;
        /// <summary>
        /// Колличество открытых ран
        /// </summary>
        public int Bleeding { get; set; } = 0;
        /// <summary>
        /// Место где игрок помер
        /// </summary>
        public Vector Position { get; set; } = new();
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
            var x = new XmlSerializer(typeof(PlayerDied));
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
        public static PlayerDied? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(PlayerDied));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (PlayerDied)rObj;
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
        /// Получаем описание смерти игрока из строчки лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время когда строчка была записана</param>
        /// <returns>Смерть игрока если удалось прочитать</returns>
        public static PlayerDied? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("died. Stats>"))
            {
                var tData = Line[7..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new PlayerDied() { DieTime = Time };
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

                    var Culture = System.Globalization.CultureInfo.InvariantCulture;
                    while ((rSym > 0) && (rSym != ':')) { rSym = Reader.Read(); }
                    Reader.Read();
                    rSym = 1;
                    var sNum = string.Empty;
                    while ((rSym > 0) && (rSym != ' '))
                    {

                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                            sNum += (char)rSym;
                    }
                    if (float.TryParse(sNum, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out var water))
                    {
                        res.Water = water;
                    }
                    while ((rSym > 0) && (rSym != ':')) { rSym = Reader.Read(); }
                    sNum = string.Empty;
                    Reader.Read();
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' '))
                    {

                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                            sNum += (char)rSym;
                    }
                    if (float.TryParse(sNum, System.Globalization.NumberStyles.Float, Culture.NumberFormat, out var energy))
                    {
                        res.Energy = energy;
                    }
                    while ((rSym > 0) && (rSym != ':')) { rSym = Reader.Read(); }
                    sNum = string.Empty;
                    Reader.Read();
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' '))
                    {

                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                            sNum += (char)rSym;
                    }
                    if (int.TryParse(sNum, System.Globalization.NumberStyles.Integer, Culture.NumberFormat, out var bleeding))
                    {
                        res.Bleeding = bleeding;
                    }
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }
    }
}
