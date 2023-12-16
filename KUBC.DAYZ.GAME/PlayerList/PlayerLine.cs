using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.PlayerList
{
    /// <summary>
    /// Строчка игрока в файле списка
    /// </summary>
    public class PlayerLine
    {
        /// <summary>
        /// Идентификатор игрока
        /// </summary>
        public string ID;
        /// <summary>
        /// Примечания
        /// </summary>
        public string Notes = string.Empty;
        /// <summary>
        /// Создаем описание игрока из строчки файла
        /// </summary>
        /// <param name="line"></param>
        public PlayerLine(string line)
        {
            var t = line.Split("//");
            ID = t[0].Trim();
            if (t.Length > 1 ) 
            {
                for (int i=1; i < t.Length; i++) 
                {
                    Notes += t[i];
                }
            }
        }

        
    }
}
