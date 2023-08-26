using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgWeather
{
    /// <summary>
    /// Класс файла cfgweather.xml
    /// </summary>
    [Serializable]
    [XmlType("weather")]
    public class File
    {
        /// <summary>
        /// Сбрасывать состояние погоды при старте сервера.
        /// </summary>
        /// <remarks>
        /// Если значение true, то при каждом запуске сервера погода будет сбрасываться до
        /// значения по умолчанию. Если значение false погода будет загружаться из хранилища
        /// игровой ситуации, и рестарт сервера не приведёт к изменению погоды. Т.е. какая была до
        /// рестарта, такая и останется после рестарта.
        /// </remarks>
        [XmlAttribute("reset")]
        public bool Reset { get; set; } = false;
        /// <summary>
        /// Если значение true то будут применятся настройки из данного файла.
        /// Иначе погода будет настроена со значениями по умолчанию.
        /// </summary>
        [XmlAttribute("enable")]
        public bool Enable { get; set; } = true;

        /// <summary>
        /// Облачность. Значение 0 - нет облаков на небе. 1 совсем все в тучах.
        /// </summary>
        [XmlElement("overcast")]
        public WeatherComponent Overcast { get; set; } = new();

        /// <summary>
        /// Туман. Значение 0 - хорошая видимость, тумана нет совсем. 1 ежики кричат ЛОШАДКА...
        /// </summary>
        [XmlElement("fog")]
        public WeatherComponent Fog { get; set; } = new();

        /// <summary>
        /// Настройки дождя.
        /// </summary>
        [XmlElement("rain")]
        public RainComponent Rain { get; set; } = new();
        /// <summary>
        /// Настройки ветра
        /// </summary>
        [XmlElement("wind")]
        public Wind Wind { get; set; } = new();
        /// <summary>
        /// Настройка грозы
        /// </summary>
        [XmlElement("storm")]
        public Shtorm Shtorm { get; set; } = new();

        #region Загрузка и сохранение файла
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAME = "cfgweather.xml";

        /// <summary>
        /// Получить имя файла
        /// </summary>
        /// <param name="MissionPath">Расположение папки миссии</param>
        /// <returns></returns>
        public static FileInfo GetFile(DirectoryInfo MissionPath)
        {
            return new FileInfo($"{MissionPath.FullName}\\{FILENAME}");
        }
        /// <summary>
        /// Загрузить файл из папки миссии
        /// </summary>
        /// <param name="MissionPath">Путь к папке миссии</param>
        /// <returns></returns>
        public static File? Load(DirectoryInfo MissionPath)
        {
            return Load(GetFile(MissionPath).FullName);
        }
        /// <summary>
        /// Загрузить данные файла 
        /// </summary>
        /// <param name="FullFileName">Полное имя файла.</param>
        public static File? Load(string FullFileName)
        {
            StreamReader reader = new(FullFileName);
            XmlSerializer xml = new(typeof(File));
            try
            {
                var file = xml.Deserialize(reader);
                if (file != null)
                {
                    reader.Close();
                    return (File)file;
                }
            }
            catch
            {
                reader.Close();
                return null;
            }
            reader.Close();
            return null;
        }

        /// <summary>
        /// Сохраняем данные в файл
        /// </summary>
        /// <param name="FullFileName"></param>
        public void Save(string FullFileName)
        {
            var writer = new StreamWriter(FullFileName);
            System.Xml.XmlWriter wrt = System.Xml.XmlWriter.Create(writer, new System.Xml.XmlWriterSettings()
            {
                OmitXmlDeclaration = false,
                Indent = true,
            });
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            XmlSerializer serializer = new(typeof(File));
            serializer.Serialize(wrt, this, xns);
            wrt.Flush();
            writer.Close();
        }
        #endregion
    }
}
