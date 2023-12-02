using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Информация о игроке
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        [XmlAttribute]
        public string ID { get;set; } = string.Empty;
        /// <summary>
        /// Никнейм игрока
        /// </summary>
        [XmlText]
        public string NickName {  get; set; } = string.Empty;
        /// <summary>
        /// Инициализация пустого класса
        /// </summary>
        public PlayerInfo()
        {

        }

        static string[] DW = { "Player", "(DEAD)", "Chat(", "(id=" };

        /// <summary>
        /// Инициализация класса с указанием ник нейма игрока
        /// </summary>
        /// <remarks>
        /// При инициализации проверяем кавычки
        /// </remarks>
        /// <param name="nickName">Никнейм</param>
        public PlayerInfo(string nickName)
        {
            NickName = nickName;
            foreach(var w in DW) 
            {
                var i = NickName.IndexOf(w);
                if (i>-1)
                {
                    NickName = NickName.Remove(i, w.Length);
                }
            }
            NickName = NickName.Trim();
            var s = NickName.FirstOrDefault();
            if ((s == '"')||(s == '\''))
                NickName = NickName[1..];
            s = NickName.LastOrDefault();
            if ((s == '"')||(s == '\''))
            {
                NickName = NickName.Substring(0, NickName.Length - 1);
            }
        }

    }
}
