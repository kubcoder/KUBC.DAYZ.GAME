using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Messages
{
    /// <summary>
    /// Файл настройки сообщений игроку
    /// </summary>
    [Serializable]
    [XmlType("messages")]
    public class File : List<MessageEntity>
    {
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAME = "messages.xml";
        /// <summary>
        /// Имя папки файла types.xml в папке миссии
        /// </summary>
        public const string PATHNAME = "db";

        /// <summary>
        /// Получить имя файла
        /// </summary>
        /// <param name="MissionPath">Расположение папки миссии</param>
        /// <returns></returns>
        public static FileInfo GetFile(DirectoryInfo MissionPath)
        {
            return new FileInfo($"{MissionPath.FullName}\\{PATHNAME}\\{FILENAME}");
        }

        /// <summary>
        /// Загрузить данные файла 
        /// </summary>
        /// <param name="FullFileName">Полное имя файла. Т.е. буквально C:\...\db\messages.xml</param>
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
        /// Загрузить файл из папки миссии
        /// </summary>
        /// <param name="MissionPath">Путь к папке миссии</param>
        /// <returns></returns>
        public static File? Load(DirectoryInfo MissionPath)
        {
            return Load(GetFile(MissionPath).FullName);
        }

        /// <summary>
        /// Сохранить данные класса в файл events.xml
        /// </summary>
        /// <param name="FullFileName">Имя файла в который необходимо сохранить изменения</param>
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
    }
}
