using KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Базовый класс элемента лога администратора
    /// </summary>
    public class AdmEntity : LogEntity
    {
        /// <summary>
        /// Время события
        /// </summary>
        [XmlAttribute]
        public DateTime Time { get; set; } = DateTime.MinValue;
        /// <summary>
        /// Прочитать имя игрока и его идентфикатор
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Информация о игроке, или null если прочитать не удалось</returns>
        protected PlayerInfo? ReadPlayer(CancellationToken? cancellation = null)
        {
            if (CurrentLine!=null)
            {
                var idStart = CurrentLine.IndexOf("(id=");
                if (idStart>-1)
                {
                    var name = ReadChars(idStart, cancellation);
                    if (!string.IsNullOrEmpty(name))
                    {
                        var pi = new PlayerInfo(name);
                        ReadChars(4, cancellation);
                        var id = ReadChars(44, cancellation);
                        if (id!=null)
                        {
                            pi.ID = id;
                            return pi;
                        }    
                    }
                }

            }
            return null;
        }


        /// <summary>
        /// Прочитать позикию в которой произошло событие
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <param name="EndChar">По какому символу ориентироваться что чтение координаты завершено</param>
        /// <returns>Вектор если получилось прочитать, иначе null</returns>
        protected Vector? ReadPosition(char EndChar = ')', CancellationToken? cancellation = null)
        {
            if (!SkipToChar('=', cancellation))
                return null;
            var posString = this.ReadToChar(EndChar, true, cancellation);
            if (posString != null)
            {
                return Vector.FromLogString(posString);
            }
            return null;
        }
    }
}
