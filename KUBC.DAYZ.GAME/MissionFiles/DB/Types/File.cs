﻿using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Инструмент для работы с файлом types.xml
    /// <para>
    /// Данный класс представляет собой коллекцию итемов загруженных из файла. 
    ///
    /// </para>
    /// </summary>
    [Serializable]
    [XmlType("types")]
    public class File : List<Item>
    {
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAME = "types.xml";
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
        /// Загрузить данные файла types.xml
        /// </summary>
        /// <remarks>
        /// Не важно как прошла загрузка, но данный класс будет очищен полностью
        /// </remarks>
        /// <param name="FullFileName">Полное имя файла. Т.е. буквально C:\...\db\types.xml</param>
        /// <returns>Если при загрузке была ошибка, то результат будет описание данной ошибки. Если все прошло гладко то возвращается пустая строка</returns>
        public static File? Load(string FullFileName)
        {

            if (!System.IO.File.Exists(FullFileName))
                return null;
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
        /// Сохранить данные класса в файл types.xml
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
            var xns = new XmlSerializerNamespaces();
            xns.Add(string.Empty, string.Empty);
            XmlSerializer serializer = new(typeof(File));
            serializer.Serialize(wrt, this, xns);
            wrt.Flush();
            writer.Close();
        }



    }
}
