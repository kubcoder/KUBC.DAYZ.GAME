using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Представление игрока в журнале админов
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// Идентификатор игрока (это заветные 44 буковки, мать их так)
        /// </summary>
        [XmlAttribute]
        public string ID { get; set; } = string.Empty;
        /// <summary>
        /// Никнейм игрока
        /// </summary>
        [XmlText]
        public string NickName { get; set; } = string.Empty;

        /// <summary>
        /// Игрок мертв
        /// </summary>
        [XmlAttribute]
        public bool IsDead { get; set; } = false;

        /// <summary>
        /// Инициализация пустого класса
        /// </summary>
        public PlayerInfo() { }

        /// <summary>
        /// Элементы которые могут попасть в имя, и их нужно подрезать
        /// </summary>
        static string[] DW = { "Player", "(DEAD)", "Chat(", "(id=" };

        /// <summary>
        /// Инициализация с "грязным" именем
        /// </summary>
        /// <param name="nickName">
        /// Никнейм игрока, который может содрежать разные дополнительные символы <see cref="DW"/>
        /// которые не относятся к его имени
        /// </param>
        public PlayerInfo(string nickName)
        {
            if (nickName.Contains("(DEAD)"))
                this.IsDead = true;
            NickName = nickName;
            foreach (var w in DW)
            {
                var i = NickName.IndexOf(w);
                if (i > -1)
                {
                    NickName = NickName.Remove(i, w.Length);
                }
            }
            NickName = NickName.Trim();
            var s = NickName.FirstOrDefault();
            if ((s == '"') || (s == '\''))
                NickName = NickName[1..];
            s = NickName.LastOrDefault();
            if ((s == '"') || (s == '\''))
            {
                NickName = NickName.Substring(0, NickName.Length - 1);
            }
        }
    }
}
