using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Класс описания итема в types.xml
    /// </summary>
    [Serializable]
    [XmlType("type")]
    public class Item
    {
        /// <summary>
        /// Название класса итема
        /// </summary>
        [XmlAttribute(AttributeName = "name")]
        public string? Name { get; set; }

        /// <summary>
        /// Номинальное кол-во итема на карте
        /// </summary>
        [XmlElement("nominal")]
        public int? Nominal { get; set; }

        /// <summary>
        /// Время жизни итема в секундах. 
        /// Т.е. сколько он живет на сервере от момента появления. По истечении этого срока обычно удаляется.
        /// </summary>
        [XmlElement("lifetime")]
        public int? LifeTime { get; set; }

        /// <summary>
        /// Через сколько времени выполнить переразмещение элементов. Используется центральной экономикой.
        /// </summary>
        [XmlElement("restock")]
        public int? Restock { get; set; }

        /// <summary>
        /// Минимальное число итема в игре после снижения до этого кол-ва центральная экономика начинает спавн данного итема 
        /// </summary>
        [XmlElement("min")]
        public int? Min { get; set; }

        /// <summary>
        /// Минимальное наполнение итема при спавне (т.е. наполнение итема)
        /// </summary>
        [XmlElement("quantmin")]
        public int? QuantMin { get; set; }

        /// <summary>
        /// Максимальное наполнение итема при спавне (т.е. наполнение итема)
        /// </summary>
        [XmlElement("quantmax")]
        public int? QuantMax { get; set; }

        /// <summary>
        /// Цена итема при спавне лута. Чем выше число тем чаще экономика будет заниматься данным итемом
        /// </summary>
        [XmlElement("cost")]
        public int? Cost { get; set; }

        /// <summary>
        /// Флаги спавна.
        /// </summary>
        /// <remarks>
        /// В каких расположениях учитывается кол-во итемов, разрешено ли удаление несобранного лута. детально описано в классе типа
        /// </remarks>
        [XmlElement(ElementName = "flags", IsNullable = false)]
        public Flags? Flags { get; set; }

        /// <summary>
        /// Категория итема. Возможные категории определены в файле cfglimitsdefinition.xml
        /// </summary>
        [XmlElement(ElementName = "category", IsNullable = false)]
        public List<Category>? Category { get; set; }

        /// <summary>
        /// В каких зонах Tier разрешен спавн данного итема
        /// </summary>
        /// <remarks>
        /// Возможные значения определены в файле cfglimitsdefinition.xml
        /// </remarks>
        [XmlElement(ElementName = "value", IsNullable = false)]
        public List<Value>? Values { get; set; }


        /// <summary>
        /// В каких объектах может спавнится данный лут, например военные объекты, полиция или что то еще.
        /// </summary>
        /// <remarks>
        /// Возможные значения определены в файле cfglimitsdefinition.xml
        /// </remarks>
        [XmlElement(ElementName = "usage", IsNullable = false)]
        public List<Usage>? Usages { get; set; }

        /// <summary>
        /// Таг спавна. Т.е. где размещать, на полу или прямо на земле.
        /// </summary>
        /// <remarks>
        /// Возможные значения определены в файле cfglimitsdefinition.xml
        /// </remarks>
        [XmlElement(ElementName = "tag", IsNullable = false)]
        public List<Tag>? Tags { get; set; } 
    }
}
