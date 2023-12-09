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
        /// Признак что набор флагов был изменен
        /// </summary>
        [XmlIgnore] public bool IsModify = false;
        
        /// <summary>
        /// Учитывать колличество итемов в контенерах хранения (бочки, ящики, палатки)
        /// </summary>
        [XmlAttribute("count_in_cargo")]
        public int InCargo 
        {
            get
            {
                if (CountInCargo)
                    return 1;
                return 0;
            }
            set
            {
                var oldValue = CountInCargo;
                if (value > 0)
                    CountInCargo = true;
                else
                    CountInCargo = false;
                if (oldValue!=CountInCargo)
                    IsModify = true;
            }
        }
        /// <summary>
        /// Учитывать колличество итемов в контенерах хранения (бочки, ящики, палатки)
        /// </summary>
        [XmlIgnore] public bool CountInCargo = false;

        /// <summary>
        /// Учитывать колличество в накопителях (шо не понятно)
        /// </summary>
        [XmlAttribute("count_in_hoarder")]
        public int InHoarder
        {
            get
            {
                if (CountInHoarder)
                    return 1;
                return 0;
            }
            set
            {
                var oldValue = CountInHoarder;
                if (value > 0)
                    CountInHoarder = true;
                else
                    CountInHoarder = false;
                if (oldValue != CountInHoarder)
                    IsModify = true;
            }
        }
        /// <summary>
        /// Учитывать колличество в накопителях (шо не понятно)
        /// </summary>
        [XmlIgnore] public bool CountInHoarder = false;

        /// <summary>
        /// Считать сколько итемов лежит в мире, т.е. валяется где-то
        /// </summary>
        [XmlAttribute("count_in_map")]
        public int InMap
        {
            get
            {
                if (CountInMap)
                    return 1;
                return 0;
            }
            set
            {
                var oldValue = CountInMap;
                if (value > 0)
                    CountInMap = true;
                else
                    CountInMap = false;
                if (oldValue != CountInMap)
                    IsModify = true;
            }
        }
        /// <summary>
        /// Считать сколько итемов лежит в мире, т.е. валяется где-то
        /// </summary>
        [XmlIgnore] public bool CountInMap = false;


        /// <summary>
        /// Считать итемы которые находятся у игроков
        /// </summary>
        [XmlAttribute("count_in_player")]
        public int InPlayer
        {
            get
            {
                if (CountInPlayer)
                    return 1;
                return 0;
            }
            set
            {
                var oldValue = CountInPlayer;
                if (value > 0)
                    CountInPlayer = true;
                else
                    CountInPlayer = false;
                if (oldValue != CountInPlayer)
                    IsModify = true;
            }
        }
        /// <summary>
        /// Считать итемы которые находятся у игроков
        /// </summary>
        [XmlIgnore]
        public bool CountInPlayer = false;

        /// <summary>
        /// Считать итемы которые были созданы игроками
        /// </summary>
        [XmlAttribute("crafted")]
        public int Crafted
        {
            get
            {
                if (CountCrafted)
                    return 1;
                return 0;
            }
            set
            {
                var oldValue = CountCrafted;
                if (value > 0)
                    CountCrafted = true;
                else
                    CountCrafted = false;
                if (oldValue != CountCrafted)
                    IsModify = true;
            }
        }
        /// <summary>
        /// Считать итемы которые были созданы игроками
        /// </summary>
        [XmlIgnore] public bool CountCrafted = false;   

        /// <summary>
        /// Разрешить удаление итемов, для переспавна
        /// </summary>
        [XmlAttribute("deloot")]
        public int Deloot
        {
            get
            {
                if (DelootEnable)
                    return 1;
                return 0;
            }
            set
            {
                var oldValue = DelootEnable;
                if (value > 0)
                    DelootEnable = true;
                else
                    DelootEnable = false;
                if (oldValue != DelootEnable)
                    IsModify = true;
            }
        }
        /// <summary>
        /// Разрешить удаление итемов, для переспавна
        /// </summary>
        [XmlIgnore] public bool DelootEnable = false;
    }
}
