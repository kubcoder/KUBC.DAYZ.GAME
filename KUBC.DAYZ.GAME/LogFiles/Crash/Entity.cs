using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.Crash
{
    /// <summary>
    /// Описание краша. Т.е. что за краш, где случился. Т.е. вся информация из лога ошибок
    /// </summary>
    public class Entity
    {

        /// <summary>
        /// Заголовок сервера
        /// </summary>
        public string HeaderLine { get; set; } = string.Empty;
        /// <summary>
        /// Строки сообщения об ошибке
        /// </summary>
        public List<string> Messages { get; set; } = new List<string>();
        /// <summary>
        /// Стак вызова
        /// </summary>
        public List<string> StackTrace { get; set; } = new List<string>();
        /// <summary>
        /// Параметры запуска
        /// </summary>
        public string CLIParams { get; set; } = string.Empty;
        /// <summary>
        /// Проверяем что краш критический, и мешает запуску игры
        /// </summary>
        /// <returns>Истина если был краш невозможности запустить игру</returns>
        public bool ChekNotCompile()
        {
            foreach (var msg in Messages)
                if (msg.Contains("Can't compile"))
                    return true;
            return false;
        }
        /// <summary>
        /// Получить время краша
        /// </summary>
        /// <returns></returns>
        public DateTime? GetCrashTime()
        {
            if (!string.IsNullOrEmpty(HeaderLine))
            {
                var tokens = HeaderLine.Split(',');
                if (tokens.Length > 1)
                {
                    if (DateTime.TryParse(tokens[1], out var crashTime))
                        return crashTime;
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
            var x = new XmlSerializer(typeof(Entity));
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            x.Serialize(wrt, this, xns);
            wrt.Close();
            return sb.ToString();
        }
        /// <summary>
        /// Создать элемент данных краша из строки XML
        /// </summary>
        /// <param name="xml">Строка данных с разметкой XML</param>
        /// <returns>Элемент данных или NULL если прочитать не удалось</returns>
        public static Entity? FromXML(string xml)
        {
            try
            {
                var x = new XmlSerializer(typeof(Entity));
                var reader = new StringReader(xml);
                var rObj = x.Deserialize(reader);
                reader.Close();
                if (rObj != null)
                {
                    var d = (Entity)rObj;
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
