using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.Env
{
    /// <summary>
    /// Класс работы с файлом территорий
    /// </summary>
    [Serializable]
    [XmlType("territory-type")]
    public class File
    {
        /// <summary>
        /// Список территорий спавна
        /// </summary>
        [XmlElement("territory")]
        public List<Territory> Territories { get; set; } = new();

        #region Загрузка и сохранение файла
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAMEPATERN = "*.xml";
        /// <summary>
        /// Название папки в папке мисии сервера
        /// </summary>
        public const string PATHNAME = "env";
        /// <summary>
        /// Получить папку с файлами территорий
        /// </summary>
        /// <param name="MissionPath">Папка миссии сервера</param>
        /// <returns></returns>
        public static DirectoryInfo GetPath(DirectoryInfo MissionPath)
        {
            return new DirectoryInfo($"{MissionPath.FullName}\\{PATHNAME}");
        }

        /// <summary>
        /// Получить имя файла
        /// </summary>
        /// <param name="MissionPath">Расположение папки миссии</param>
        /// <returns></returns>
        public static List<FileInfo> GetFiles(DirectoryInfo MissionPath)
        {
            var r = new List<FileInfo>();
            var files = GetPath(MissionPath).EnumerateFiles($"{FILENAMEPATERN}");
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
