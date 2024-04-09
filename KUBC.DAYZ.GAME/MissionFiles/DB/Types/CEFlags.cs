using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Флаги центральной экономики
    /// </summary>
    /// <remarks>
    /// Т.е. каким образом центральная экономика учитывает игровой предмет,
    /// что центральная экономика может с ним делать.
    /// </remarks>
    public class CEFlags : AXMLConfig
    {
        /// <summary>
        /// Имя узла в XML файле
        /// </summary>
        public const string NODENAME = "flags";
        /// <summary>
        /// Инициализация значения по умолчанию
        /// </summary>
        public CEFlags()
        {
            _sectName = NODENAME;
            InCargo = true;
            InHoarder = true;
            InMap = true;
            InPlayer = false;
            Crafted = false;
            Deloot = false;

        }
        #region Поля для доступа
        private const string ATTR_COUNT_IN_CARGO = "count_in_cargo";
        /// <summary>
        /// Учитывать колличество итемов в контенерах хранения (бочки, ящики, палатки)
        /// </summary>
        public bool InCargo
        {
            get
            {
                var r = GetValueAsBol(ATTR_COUNT_IN_CARGO);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_COUNT_IN_CARGO, value, nameof(InCargo));
                else
                    SetValue(ATTR_COUNT_IN_CARGO, value, nameof(InCargo));
            }
        }
        private const string ATTR_COUNT_IN_HOARDER = "count_in_hoarder";
        /// <summary>
        /// Учитывать колличество в накопителях (шо не понятно)
        /// </summary>
        public bool InHoarder
        {
            get
            {
                var r = GetValueAsBol(ATTR_COUNT_IN_HOARDER);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_COUNT_IN_HOARDER, value, nameof(InCargo));
                else
                    SetValue(ATTR_COUNT_IN_HOARDER, value, nameof(InCargo));
            }
        }
        private const string ATTR_COUNT_IN_MAP = "count_in_map";
        /// <summary>
        /// Считать сколько итемов лежит в мире, т.е. валяется где-то
        /// </summary>
        public bool InMap
        {
            get
            {
                var r = GetValueAsBol(ATTR_COUNT_IN_MAP);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_COUNT_IN_MAP, value, nameof(InMap));
                else
                    SetValue(ATTR_COUNT_IN_MAP, value, nameof(InMap));
            }
        }
        private const string ATTR_COUNT_IN_PLAYER = "count_in_player";
        /// <summary>
        /// Считать итемы которые находятся у игроков
        /// </summary>
        public bool InPlayer
        {
            get
            {
                var r = GetValueAsBol(ATTR_COUNT_IN_PLAYER);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_COUNT_IN_PLAYER, value, nameof(InPlayer));
                else
                    SetValue(ATTR_COUNT_IN_PLAYER, value, nameof(InPlayer));
            }
        }
        private const string ATTR_COUNT_CRAFTED = "crafted";
        /// <summary>
        /// Считать итемы которые находятся у игроков
        /// </summary>
        public bool Crafted
        {
            get
            {
                var r = GetValueAsBol(ATTR_COUNT_CRAFTED);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_COUNT_CRAFTED, value, nameof(Crafted));
                else
                    SetValue(ATTR_COUNT_CRAFTED, value, nameof(Crafted));
            }
        }
        private const string ATTR_COUNT_DELOOT = "deloot";
        /// <summary>
        /// Разрешить удаление итемов, для переспавна
        /// </summary>
        public bool Deloot
        {
            get
            {
                var r = GetValueAsBol(ATTR_COUNT_DELOOT);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_COUNT_DELOOT, value, nameof(Deloot));
                else
                    SetValue(ATTR_COUNT_DELOOT, value, nameof(Deloot));
            }
        }
        #endregion
    }
}
