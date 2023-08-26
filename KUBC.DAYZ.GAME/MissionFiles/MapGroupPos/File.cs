using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.MapGroupPos
{
    /// <summary>
    /// Класс для работы с файлом mapgrouppos.xml
    /// </summary>
    [Serializable]
    [XmlType("map")]
    public class File
    {
        /// <summary>
        /// Координаты кластеры в мире
        /// </summary>
        [XmlElement("group")]
        public List<Group> Groups { get; set; } = new();
        /// <summary>
        /// Следующий файл
        /// </summary>
        [XmlElement("include")]
        public MapGroupCluster.Include? NextFile { get; set; }

        #region Загрузка и сохранение файла
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAMEPATERN = "mapgrouppos";

        /// <summary>
        /// Получить имя файла
        /// </summary>
        /// <param name="MissionPath">Расположение папки миссии</param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(DirectoryInfo MissionPath)
        {
            //return new FileInfo($"{MissionPath.FullName}\\{FILENAME}");
            var r = new List<FileInfo>();
            var files = MissionPath.EnumerateFiles($"{FILENAMEPATERN}*.xml");
            if (files != null)
            {
                foreach (var file in files)
                {
                    r.Add(file);
                }
            }
            return r;
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
