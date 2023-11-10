using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Список игроков которые в игре
    /// </summary>
    public class PlayerList
    {
        /// <summary>
        /// Время когда отчет был сформирован
        /// </summary>
        [XmlAttribute]
        public DateTime LogTime { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Список игроков
        /// </summary>
        public List<PlayerPosition> Players { get; set; } = new();

        /// <summary>
        ///  Строчка является началом лога игроков
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Истина если это начало лога списка игроков</returns>
        public static bool IsStartList(string Line)
        {
            if (Line.Contains("PlayerList log"))
                return true;
            return false;
        }
        /// <summary>
        /// Строчка является окончанием лога игроков
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Истина если строчка это финишь лога игроков</returns>
        public static bool IsEndList(string Line)
        {
            return Line.Contains("#####");
        }
        /// <summary>
        /// Добавляем позиции игрока в коллекцию
        /// </summary>
        /// <param name="Line">Строчка лога</param>
        /// <returns>Истина если строчка добавлена и нужно ждать следующую. Ложь означает что чтение лога игроков завершено</returns>
        public bool AddLine(string Line)
        {
            if (!IsEndList(Line))
            {
                var pPos = PlayerPosition.FromLog(Line);
                if (pPos != null)
                {
                    Players.Add(pPos);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Преобразуем объект в строку с разметкой XML
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
            var x = new XmlSerializer(typeof(PlayerList));
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
        public static PlayerList? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(PlayerList));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (PlayerList)rObj;
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
