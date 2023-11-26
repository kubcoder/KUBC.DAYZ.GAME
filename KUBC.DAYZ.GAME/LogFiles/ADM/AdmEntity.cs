﻿using KUBC.DAYZ.GAME.MissionFiles.CfgPlayerSpawnPoints;
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
        /// <param name="EndChar">По какому символу ориентироваться о конце индентфикатора</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <param name="SkipTo">Пробросить до символа. Если этого не указано то выполняем чтение первого слова, и потом его проверяем</param>
        /// <returns>Информация о игроке, или null если прочитать не удалось</returns>
        protected PlayerInfo? ReadPlayer(char EndChar = ')', CancellationToken? cancellation=null, char? SkipTo=null)
        {
            string? w;
            if (SkipTo.HasValue)
            {
                if (!SkipToChar('"', cancellation))
                    return null;
            }
            else
            {
                w = ReadToChar(' ', true, cancellation);
                if (w.Trim()!="Player")
                    return null;
            }
            w = ReadToChar('(', true, cancellation);
            if (w != null)
            {
                var pi = new PlayerInfo(w.Trim());

                if (!SkipToChar('=', cancellation))
                    return null;
                var dayzID = this.ReadToChar(EndChar, true, cancellation);
                if (dayzID != null)
                {

                    pi.ID = dayzID;
                    return pi;
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
