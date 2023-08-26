using System.Xml;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Economy
{
    /// <summary>
    /// Элемент настройки экономики
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Имя элемента
        /// </summary>
        public string ElementName { get; set; } = "custom";
        /// <summary>
        /// Инициализировать элемент на старте
        /// </summary>
        public bool Init { get; set; } = true;
        /// <summary>
        /// Загружать данные из хранилища
        /// </summary>
        public bool Load { get; set; } = false;
        /// <summary>
        /// Включать респавн элемента
        /// </summary>
        public bool Respawn { get; set; } = false;
        /// <summary>
        /// Сохранять данные в хранилище
        /// </summary>
        public bool Save { get; set; } = false;
        /// <summary>
        /// Сохранить данные элемента в поток XML
        /// </summary>
        /// <param name="writer">Поток XML</param>
        public void WriteToXML(XmlWriter writer)
        {
            writer.WriteStartElement(ElementName);
            if (Init)
            {
                writer.WriteAttributeString(EN_Init, "1");
            }
            else
            {
                writer.WriteAttributeString(EN_Init, "0");
            }
            if (Load)
            {
                writer.WriteAttributeString(EN_Load, "1");
            }
            else
            {
                writer.WriteAttributeString(EN_Load, "0");
            }
            if (Respawn)
            {
                writer.WriteAttributeString(EN_Respawn, "1");
            }
            else
            {
                writer.WriteAttributeString(EN_Respawn, "0");
            }
            if (Save)
            {
                writer.WriteAttributeString(EN_Save, "1");
            }
            else
            {
                writer.WriteAttributeString(EN_Save, "0");
            }
            writer.WriteEndElement();
        }

        const string EN_Init = "init";
        const string EN_Load = "load";
        const string EN_Respawn = "respawn";
        const string EN_Save = "save";
        /// <summary>
        /// Прочитать данные из потока XML
        /// </summary>
        /// <param name="reader">поток для чтения</param>
        /// <returns>Результат чтения</returns>
        public static Entity FromReader(XmlReader reader)
        {
            var e = new Entity() { ElementName = reader.Name };
            var a = reader.GetAttribute(EN_Init);
            if (a != null)
            {
                if (a == "1")
                    e.Init = true;
            }
            a = reader.GetAttribute(EN_Load);
            if (a != null)
            {
                if (a == "1")
                    e.Load = true;
            }
            a = reader.GetAttribute(EN_Respawn);
            if (a != null)
            {
                if (a == "1")
                    e.Respawn = true;
            }
            a = reader.GetAttribute(EN_Save);
            if (a != null)
            {
                if (a == "1")
                    e.Save = true;
            }
            return e;
        }
    }
}
