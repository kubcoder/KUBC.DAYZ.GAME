using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Событие убйства игрока
    /// </summary>
    public class PlayerKilled
    {
        /// <summary>
        /// Время когда игрок был убит
        /// </summary>
        public DateTime DamageTime { get; set; } = DateTime.Now;
        /// <summary>
        /// С каким ником он подключился
        /// </summary>
        public string NickName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор игрока в DAYZ
        /// </summary>
        public string DayzID { get; set; } = string.Empty;
        /// <summary>
        /// Место где игрок выхватил пиздюлю
        /// </summary>
        public Vector Position { get; set; } = new();

        /// <summary>
        /// Имя источника повреждений
        /// </summary>
        /// <remarks>Это может быть название AI, ник игрока или какой то итем, хотя может быть и просто FallDamage</remarks>
        public string DSName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор источника повреждений. Заполняется только если это есть игрок и пишется сюда DAYZID
        /// </summary>
        public string DSID { get; set; } = string.Empty;
        /// <summary>
        /// Где находился нападающий игрок
        /// </summary>
        public Vector DSPos { get; set; } = new();

        /// <summary>
        /// Какое оружие использовал нападающий
        /// </summary>
        public string Weapon { get; set; } = string.Empty;
        /// <summary>
        /// С какой дистанции был нанесен урон
        /// </summary>
        public float Distance { get; set; } = 0;

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
            var x = new XmlSerializer(typeof(PlayerKilled));
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
        public static PlayerKilled? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(PlayerKilled));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (PlayerKilled)rObj;
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
        /// Получаем описание убийства игрока из строчки лога
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <param name="Time">Время когда строчка была записана</param>
        /// <returns>Убйство игрока если удалось прочитать</returns>
        public static PlayerKilled? FromLog(string Line, DateTime Time)
        {
            if (Line.Contains("killed by"))
            {
                var tData = Line[7..];
                if (tData[0] == '"')
                {
                    var Reader = new StringReader(tData);
                    Reader.Read();
                    var rSym = Reader.Read();
                    var res = new PlayerKilled() { DamageTime = Time };
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

                    for (int i = 0; i < 11; i++)
                        Reader.Read();
                    var DN = string.Empty;
                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                            DN += (char)rSym;
                    }
                    if (DN == "Player")
                    {
                        while ((rSym > 0) && (rSym != '"')) { rSym = Reader.Read(); }
                        rSym = 1;
                        while ((rSym > 0) && (rSym != '"'))
                        {
                            rSym = Reader.Read();
                            if ((rSym > 0) && (rSym != '"'))
                                res.DSName += (char)rSym;
                        }
                        while ((rSym > 0) && (rSym != '=')) { rSym = Reader.Read(); }
                        while ((rSym > 0) && (rSym != ' '))
                        {
                            rSym = Reader.Read();
                            if ((rSym > 0) && (rSym != ' '))
                                res.DSID += (char)rSym;
                        }
                        while ((rSym > 0) && (rSym != '=')) { rSym = Reader.Read(); }
                        sPos = string.Empty;
                        while ((rSym > 0) && (rSym != ')'))
                        {

                            rSym = Reader.Read();
                            if ((rSym > 0) && (rSym != ')'))
                                sPos += (char)rSym;

                        }
                        res.DSPos = Vector.FromLogString(sPos);

                    }
                    else
                    {
                        res.DSName = DN;
                    }
                    rSym = Reader.Read();
                    if (rSym < 0)
                    {
                        Reader.Close();
                        return res;
                    }
                    var tS = string.Empty;
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                        tS += (char)rSym;
                    }
                    rSym = 1;
                    while ((rSym > 0) && (rSym != 'f'))
                    {
                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != 'f'))
                            res.Weapon += (char)rSym;
                    }
                    if (rSym < 0)
                    {
                        Reader.Close();
                        return res;
                    }

                    while ((rSym > 0) && (rSym != ' '))
                    {
                        rSym = Reader.Read();
                    }

                    var sDist = string.Empty;
                    rSym = 1;
                    while ((rSym > 0) && (rSym != ' '))
                    {

                        rSym = Reader.Read();
                        if ((rSym > 0) && (rSym != ' '))
                            sDist += (char)rSym;
                    }
                    if (float.TryParse(sDist.Trim(), System.Globalization.NumberStyles.Float, Culture.NumberFormat, out var dist))
                    {
                        res.Distance = dist;
                    }
                    Reader.Close();
                    return res;
                }
            }
            return null;
        }

    }
}
