using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Парсер строчки лога ADM.
    /// </summary>
    /// <remarks>
    /// Добавлено что при инициализации выполняется чтение даты или времени 
    /// когда была добавлена строчка если это не получилось, разбор 
    /// парсером будет прерван.
    /// </remarks>
    public abstract class ADMStringParser : LineWithTimeParser
    {
        /// <inheritdoc/>
        protected override bool ReadTime(CancellationToken? cancellation = null)
        {
            if (!SkipChar(' ', cancellation))
            {
                return false;
            }
            var TimeString = ReadToChar(' ', false, cancellation);
            if (TimeSpan.TryParse(TimeString, out var pTime))
            {
                logTime = DateTime.MinValue.Add(pTime);
            }
            else
            {
                return false;
            }
            if (!SkipChar('|', cancellation))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Прочитать имя игрока и его идентфикатор
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Информация о игроке, или null если прочитать не удалось</returns>
        protected PlayerInfo? ReadPlayer(CancellationToken? cancellation = null)
        {
            var pi = ReadPlayerName(cancellation);
            if (pi != null)
            {
                var id = ReadChars(44, cancellation);
                if (id != null)
                {
                    pi.ID = id;
                    return pi;
                }
            }
            return null;
        }
        private const string startID = "(id=";
        /// <summary>
        /// Прочитать имя игрока
        /// </summary>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns>Информация о игроке, или null если прочитать не удалось</returns>
        protected PlayerInfo? ReadPlayerName(CancellationToken? cancellation = null)
        {
            var name = string.Empty;
            Read();
            while (LastSymbol.HasValue)
            {
                name += LastSymbol.Value;
                if (name.Length > startID.Length)
                {
                    if (name.EndsWith(startID))
                    {
                        var pi = new PlayerInfo(name);
                        return pi;
                    }
                }
                if ((cancellation != null) && (cancellation.Value.IsCancellationRequested)) { return null; }
                Read();
            }
            return null;
        }

        /// <summary>
        /// Прочитать позицию в которой произошло событие
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
                Vector res = new ();
                var style = System.Globalization.NumberStyles.Number;
                var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                var strXYZ = posString[1..^1];
                var XYZ = strXYZ.Split(',');
                if (double.TryParse(XYZ[0], style, culture, out double x))
                    res.X = x;
                else
                    res.X = 0;
                if (double.TryParse(XYZ[1], style, culture, out double y))
                    res.Y = y;
                else
                    res.Y = 0;
                if (double.TryParse(XYZ[2], style, culture, out double z))
                    res.Z = z;
                else
                    res.Z = 0;
                return res;
            }
            return null;
        }

    }
}
