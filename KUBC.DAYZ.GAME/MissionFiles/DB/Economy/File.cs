using System.Xml;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Economy
{
    /// <summary>
    /// Файл настройки экономики
    /// </summary>
    public class File : List<Entity>
    {
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAME = "economy.xml";
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
        /// Загрузить файл из папки миссии
        /// </summary>
        /// <param name="MissionPath">Путь к папке миссии</param>
        /// <returns></returns>
        public static File? Load(DirectoryInfo MissionPath)
        {
            return Load(GetFile(MissionPath).FullName);
        }

        /// <summary>
        /// Имя корневого элемента
        /// </summary>
        const string ROOTNAME = "economy";
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
                Indent = true
            });

            wrt.WriteStartElement(ROOTNAME);
            foreach (var entity in this)
            {
                entity.WriteToXML(wrt);
            }
            wrt.WriteEndElement();
            wrt.Flush();
            writer.Close();
        }
        /// <summary>
        /// Загрузить данные файла events.xml
        /// </summary>
        /// <param name="FullFileName">Полное имя файла. Т.е. буквально C:\...\db\types.xml</param>
        public static File? Load(string FullFileName)
        {
            StreamReader reader = new(FullFileName);
            var r = new File();
            try
            {
                var xml = XmlReader.Create(reader);
                bool ActivyRead = false;
                while (xml.Read())
                {
                    if (xml.IsStartElement())
                    {
                        if (xml.Name == ROOTNAME)
                        {
                            ActivyRead = true;
                        }
                        else
                        {
                            if (ActivyRead)
                            {
                                r.Add(Entity.FromReader(xml));
                            }
                        }
                    }
                }
            }
            catch
            {
                reader.Close();
                return null;
            }
            reader.Close();
            if (r.Count > 0)
                return r;
            return null;
        }


    }
}
