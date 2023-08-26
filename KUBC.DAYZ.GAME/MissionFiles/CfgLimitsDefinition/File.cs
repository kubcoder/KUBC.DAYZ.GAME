﻿using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.CfgLimitsDefinition
{
    /// <summary>
    /// Файл определения названий для спавна
    /// </summary>
    [Serializable]
    [XmlType("lists")]
    public class File
    {
        /// <summary>
        /// Категории лута
        /// </summary>
        [XmlElement("categories")]
        public Categories Categories { get; set; } = new();

        /// <summary>
        /// Тэги для спавна
        /// </summary>
        [XmlElement("tags")]
        public Tags Tags { get; set; } = new();

        /// <summary>
        /// Тематическая принадлежность...
        /// </summary>
        [XmlElement("usageflags")]
        public Flags UsageFlags { get; set; } = new();

        /// <summary>
        /// Редкость спавна... т.е. что в сложных зонах, что прямо возле спавна игроков
        /// </summary>
        [XmlElement("valueflags")]
        public Values Values { get; set; } = new();

        #region Загрузка и сохранение файла
        /// <summary>
        /// Имя файла в серверных файлах
        /// </summary>
        public const string FILENAME = "cfglimitsdefinition.xml";

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
        /// <param name="FullFileName">Полное имя файла. Т.е. буквально C:\...\db\cfglimitsdefinition.xml</param>
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
