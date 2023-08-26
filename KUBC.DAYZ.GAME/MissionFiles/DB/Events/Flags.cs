using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Events
{
    /// <summary>
    /// Флаги для рабоы центральной экономики.
    /// </summary>
    /// <remarks>
    /// Все флаги задаются в виде целого числа. и могут принимать значения 0 и 1
    /// </remarks>
    [Serializable]
    [XmlType("flags")]
    public class Flags
    {
        /// <summary>
        /// Разрешено удаление
        /// </summary>
        [XmlAttribute("deletable")]
        public int Deletable { get; set; }

        /// <summary>
        /// Хер пойми... 
        /// </summary>
        [XmlAttribute("init_random")]
        public int InitRandom { get; set; }

        /// <summary>
        /// Удалять уничтоженные итемы
        /// </summary>
        [XmlAttribute("remove_damaged")]
        public int RemoveDamaged { get; set; }

    }
}
