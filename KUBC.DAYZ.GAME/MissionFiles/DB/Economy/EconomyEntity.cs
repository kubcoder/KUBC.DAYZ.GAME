using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Economy
{
    /// <summary>
    /// Элемент конфигурации экономики
    /// </summary>
    [XmlRoot("event")]
    public class EconomyEntity:AXMLConfig
    {
   
        private const string ATTR_INIT = "init";
        /// <summary>
        /// Инициализировать объект
        /// </summary>
        public bool Init
        {
            get
            {
                var r = GetValueAsBol(ATTR_INIT);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                if (value)
                    SetValue(ATTR_INIT, value);
                else
                    SetValue(ATTR_INIT, value);
            }
        }
        private const string ATTR_Load = "load";
        /// <summary>
        /// Загружать объект из хранилища
        /// </summary>
        public bool Load
        {
            get
            {
                var r = GetValueAsBol(ATTR_Load);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                SetValue(ATTR_Load, value);
            }
        }
        private const string ATTR_Respawn = "respawn";
        /// <summary>
        /// Респавн объектов
        /// </summary>
        public bool Respawn
        {
            get
            {
                var r = GetValueAsBol(ATTR_Respawn);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                SetValue(ATTR_Respawn, value);
            }
        }
        private const string ATTR_Save = "save";
        /// <summary>
        /// Сохранять объекты в хранилище
        /// </summary>
        public bool Save
        {
            get
            {
                var r = GetValueAsBol(ATTR_Save);
                if (r.HasValue)
                    return r.Value;
                return true;
            }
            set
            {
                SetValue(ATTR_Save, value);
            }
        }

        /// <summary>
        /// Создать новый элемент центральной экономики
        /// </summary>
        /// <param name="name">Имя элемента</param>
        public EconomyEntity(string? name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _sectName = name;
            }
            Init= true;
            Load = true;
            Respawn = true;
            Save = false;
        }

        
    }
}
