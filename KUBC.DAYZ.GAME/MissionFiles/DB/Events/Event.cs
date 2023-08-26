using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Events
{
    /// <summary>
    /// Класс описания элемента события в events.xml
    /// </summary>
    [Serializable]
    [XmlType("event")]
    public class Event
    {
        /// <summary>
        /// Имя события
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Префикс названия событий для зданий и статических объектов
        /// </summary>
        public const string PREFIX_Static = "Static";
        /// <summary>
        /// Префикс названия события для спавна итемов
        /// </summary>
        public const string PREFIX_Item = "Item";
        /// <summary>
        /// Префикс названия события для спавна животных
        /// </summary>
        public const string PREFIX_Animals = "Animals";
        /// <summary>
        /// Префикс названия события для спавна зомбиков
        /// </summary>
        public const string PREFIX_Infected = "Indected";
        /// <summary>
        /// Префикс названия события для спавна фруктов и камней
        /// </summary>
        public const string PREFIX_Trajectory = "Trajectory";

        /// <summary>
        /// Сколько событий может быть созданоы
        /// </summary>
        [XmlElement("nominal")]
        public int Nominal { get; set; } = 0;

        /// <summary>
        /// Минимальное кол-во дочерних элементов которые могут быть созданы для событияы
        /// </summary>
        [XmlElement("min")]
        public int Min { get; set; } = 0;
        /// <summary>
        /// Максимальное кол-во дочерних элементов которые могут быть созданы для событияы
        /// </summary>
        [XmlElement("max")]
        public int Max { get; set; } = 0;
        /// <summary>
        /// Время жизни события. Сколько секунд событие будет существовать на сервере после создания.ы
        /// </summary>
        [XmlElement("lifetime")]
        public int LifeTime { get; set; } = 0;
        /// <summary>
        /// Время в секндах между попытками создать новое событие когда число данных событий на карте достигнет минимального значения.
        /// </summary>
        [XmlElement("restock")]
        public int Restok { get; set; } = 0;
        /// <summary>
        /// Безопасный радиус. Если игрок находится в в данном радиусе событие не может быть создано
        /// </summary>
        [XmlElement("saferadius")]
        public float SafeRadius { get; set; } = 0;
        /// <summary>
        /// Минимальное расстояние от других подобных событий
        /// </summary>
        [XmlElement("distanceradius")]
        public float DistanceRadius { get; set; } = 0;
        /// <summary>
        /// Дистанция до ближайшего игрока которая разрешает удалять событие по истечении времени жизни события
        /// </summary>
        [XmlElement("cleanupradius")]
        public float CleanupRadius { get; set; } = 0;
        /// <summary>
        /// Дополнительное событие которое должно быть вызвано с данным событием. Например спавн зомби вокруг итема
        /// </summary>
        [XmlElement("secondary")]
        public string? Secondary { get; set; }
        /// <summary>
        /// Флаги управления событием
        /// </summary>
        [XmlElement("flags")]
        public Flags Flags { get; set; } = new();

        /// <summary>
        /// Как рассчитвается положение. Оно может отсчитваться от позиции игрока (<see cref="POS_Player"/>) или всегда фиксированное (<see cref="POS_Fixed"/>)
        /// </summary>
        [XmlElement("position")]
        public string Position { get; set; } = POS_Fixed;
        /// <summary>
        /// Ключевое слово определяющее что позиция события отсчитывается от игрока
        /// </summary>
        public const string POS_Player = "player";
        /// <summary>
        /// Ключевое слово определяющее что позиция события фиксированная
        /// </summary>
        public const string POS_Fixed = "fixed";
        /// <summary>
        /// Определяет как будет определятся максимальное число дочерних элементов в событии
        /// </summary>
        [XmlElement("limit")]
        public string Limit { get; set; } = LIMIT_Custom;
        /// <summary>
        /// Определяется внешним файлом (файлы территорий)
        /// </summary>
        public const string LIMIT_Custom = "custom";
        /// <summary>
        /// Определяется аттрибутами <see cref="Child.Min"/> и <see cref="Child.Max"/> для каждого дочернего элемента
        /// </summary>
        public const string LIMIT_Child = "child";
        /// <summary>
        /// Определяется лимитами события <see cref="Max"/> и <see cref="Min"/>
        /// </summary>
        public const string LIMIT_Parent = "parent";
        /// <summary>
        /// Совмещает значения родительского и дочернего элемента
        /// </summary>
        public const string LIMIT_Mixed = "mixed";
        /// <summary>
        /// Включен (1) или выключен (0) спавн события
        /// </summary>
        [XmlElement("active")]
        public int Active { get; set; } = 0;
        /// <summary>
        /// Это дочерние элементы которые будут появлятся вместе с событием
        /// </summary>
        [XmlArray("children")]
        public List<Child> Children { get; set; } = new();


    }
}
