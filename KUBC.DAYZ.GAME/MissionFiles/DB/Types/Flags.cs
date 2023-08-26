using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
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
        /// Учитывать колличество итемов в контенерах хранения (бочки, ящики, палатки)
        /// </summary>
        [XmlAttribute("count_in_cargo")]
        public int InCargo { get; set; }

        /// <summary>
        /// Учитывать колличество в накопителях (шо не понятно)
        /// </summary>
        [XmlAttribute("count_in_hoarder")]
        public int InHoarder { get; set; }

        /// <summary>
        /// Считать сколько итемов лежит в мире, т.е. валяется где-то
        /// </summary>
        [XmlAttribute("count_in_map")]
        public int InMap { get; set; }

        /// <summary>
        /// Считать итемы которые находятся у игроков
        /// </summary>
        [XmlAttribute("count_in_player")]
        public int InPlayer { get; set; }

        /// <summary>
        /// Считать итемы которые были созданы игроками
        /// </summary>
        [XmlAttribute("crafted")]
        public int Crafted { get; set; }

        /// <summary>
        /// Разрешить удаление итемов, для переспавна
        /// </summary>
        [XmlAttribute("deloot")]
        public int Deloot { get; set; }

    }
}
