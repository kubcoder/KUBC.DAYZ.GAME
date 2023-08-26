using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Описание цвета DAYZ
    /// </summary>
    public class ARGB
    {
        /// <summary>
        /// Непрозрачность. 255 не прозрачный, 0 совсем прозрачный
        /// </summary>
        public byte A = 255;
        /// <summary>
        /// Красная компонента
        /// </summary>
        public byte R = 0;
        /// <summary>
        /// Зеленая компонента
        /// </summary>
        public byte G = 0;
        /// <summary>
        /// Синяя компонента
        /// </summary>
        public byte B = 0;
        /// <summary>
        /// Получить пердстваление цвета в виде строки
        /// </summary>
        public string ValueAsString
        {
            get
            {
                return string.Format("0x{0}{1}{2}{3}", A.ToString("X2"), R.ToString("X2"), G.ToString("X2"), B.ToString("X2"));
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length == 10)
                    {
                        if (!byte.TryParse(value.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out A))
                            A = 255;
                        if (!byte.TryParse(value.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out R))
                            R = 0;
                        if (!byte.TryParse(value.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out G))
                            G = 0;
                        if (!byte.TryParse(value.Substring(8, 2), System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out B))
                            B = 0;
                    }
                    else
                    {
                        SetBlack();
                    }
                }
                else
                {
                    SetBlack();
                }
            }
        }
        /// <summary>
        /// Получаем хеш код цвета
        /// </summary>
        /// <returns>Целое число, которое есть цвет</returns>
        public override int GetHashCode()
        {
            var buff = new byte[4] { A, R, G, B };
            return BitConverter.ToInt32(buff, 0);
        }
        /// <summary>
        /// Сравниваем два объекта
        /// </summary>
        /// <param name="obj">Сравниваемый объект</param>
        /// <returns>Истина если объекты равны</returns>
        public override bool Equals(object? obj)
        {
            if (obj != null)
            {
                try
                {
                    var eq = (ARGB)obj;
                    return eq.GetHashCode() == GetHashCode();
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// Установить черный цвет
        /// </summary>
        public void SetBlack()
        {
            A = 255;
            R = 0;
            G = 0;
            B = 0;
        }

    }
}
