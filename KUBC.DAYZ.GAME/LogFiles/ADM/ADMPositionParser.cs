using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.ADM
{
    /// <summary>
    /// Парсер который после чтения игрока, еще пытается прочитать позицию произошедшего
    /// </summary>
    public abstract class ADMPositionParser : ADMPlayerParser
    {
        /// <summary>
        /// Где произошли сложности
        /// </summary>
        protected Vector? Position;

        /// <inheritdoc/>
        protected override bool Init(string Line, CancellationToken? cancellation = null)
        {
            Position = null;
            if (base.Init(Line, cancellation))
            {
                Position = ReadPosition(')', cancellation); 
                return Position != null;
            }
            return false;
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
                Vector res = new();
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
