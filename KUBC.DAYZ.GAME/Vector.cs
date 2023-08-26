using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME
{
#pragma warning disable CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
    /// <summary>
    /// Представление вектора в игре
    /// </summary>
    public class Vector : List<double>
    {
        /// <summary>
        /// Инициализируем пустую координату
        /// </summary>
        public Vector()
        {
            
            /*X = 0; 
            Y = 0;
            Z = 0;*/
        }
        /// <summary>
        /// Координата X в игре. Направление Запад-Восток
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public double X
        {
            get
            {
                try
                {
                    return this[0];
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    this.AddToSize(1);
                    this[0] = value;
                }
                catch
                {

                }

            }
        }
        /// <summary>
        /// Координата Y в игре(это высота)
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public double Y
        {
            get
            {
                try
                {
                    return this[1];
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    this.AddToSize(2);
                    this[1] = value;
                }
                catch
                {

                }

            }
        }
        /// <summary>
        /// Координата Z в игре. Направление Север-ЮГ
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public double Z
        {
            get
            {
                try
                {
                    return this[2];
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    this.AddToSize(3);
                    this[2] = value;
                }
                catch
                {

                }

            }
        }
        /// <summary>
        /// Проверяем размер массива. Если он меньше то добавляем нули
        /// </summary>
        /// <param name="size">Размер массива</param>
        private void AddToSize(int size)
        {
            while (this.Count < size)
                this.Add(0);
        }

        /// <summary>
        /// Проверка на пустой вектор. В общем если X=0 то нафиг считаем что вектор пустой
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public bool IsEmpty
        {
            get
            {
                return (X == 0);
            }
        }
        /// <summary>
        /// Преобразовать объект в массив байт с разметкой JSON
        /// </summary>
        /// <returns>Массив байт JSON файла</returns>
        public byte[] ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.SerializeToUtf8Bytes(this, options);
        }

        /// <summary>
        /// Получаем вектор в виде JSON 
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string AsJSON
        {
            get
            {
                return System.Text.Encoding.UTF8.GetString(ToJson());
            }
        }
        /// <summary>
        /// Создаем вектор из представления JSON
        /// </summary>
        /// <param name="jsonString">Строка с разметкой json</param>
        /// <returns>Вектор созданный из строки JSON, может быть NUL, если не получилось прочитать</returns>
        public static Vector? FromJson(string jsonString)
        {
            try
            {
                return JsonSerializer.Deserialize<Vector>(jsonString);
            }
            catch
            {
                return null;
            }

        }
        /// <summary>
        /// Получить вектор из строки журнала
        /// </summary>
        /// <param name="logData">Строка из журнал</param>
        /// <returns>Вектор, возможен возврат NULL если прочитать строчку не удалось</returns>
        public static Vector FromLogString(string logData)
        {
            Vector res = new();
            try
            {
                var style = System.Globalization.NumberStyles.Number;
                var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
                var strXYZ = logData[1..^1];
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
            }
            catch
            {

            }
            return res;
        }
        /// <summary>
        /// Преобразуем в формат строчки лога
        /// </summary>
        /// <returns></returns>
        public string ToLogFormat()
        {
            AddToSize(3);
            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            return $"<{this[0].ToString(culture)}, {this[1].ToString(culture)}, {this[2].ToString(culture)}>";
        }
        /// <summary>
        /// Сравниваем два вектора
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            try
            {
                var uV = (Vector?)obj;
                if (uV == null)
                    return false;
                if (X != uV.X)
                    return false;
                if (Y != uV.Y)
                    return false;
                if (Z != uV.Z)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Текстовое представление вектора
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{X},{Y},{Z}";
        }


        /// <summary>
        /// Считаем расстояние до точки
        /// </summary>
        /// <param name="Point2">Точка до которой считаем расстояние от текущей</param>
        /// <returns>Дистанция до точки</returns>
        public double Distance(Vector Point2)
        {
            var sum = Math.Pow(Point2.X - X, 2);
            sum += Math.Pow(Point2.Y - Y, 2);
            sum += Math.Pow(Point2.Z - Z, 2);
            return Math.Sqrt(sum);

        }
        /// <summary>
        /// Считаем расстояние до точки без учета координаты Y. Т.е. тупо без высоты а дистанция по карте
        /// </summary>
        /// <param name="Point2">Точка до которой считаем расстояние от текущей</param>
        /// <returns>Дистанция до точки</returns>
        public double Distance2D(Vector Point2)
        {
            var sum = Math.Pow(Point2.X - X, 2);
            sum += Math.Pow(Point2.Z - Z, 2);
            return Math.Sqrt(sum);

        }

        /// <summary>
        /// Создать вектор из строки XML атрибута
        /// </summary>
        /// <param name="value">значение атрибута</param>
        /// <returns></returns>
        public static Vector FromAttribute(string value)
        {
            var t = value.Split(' ');
            var pos = new Vector();
            if (t.Length == 3)
            {
                if (float.TryParse(t[0], System.Globalization.CultureInfo.InvariantCulture, out float X))
                {
                    pos.X = X;
                }
                if (float.TryParse(t[1], System.Globalization.CultureInfo.InvariantCulture, out float Y))
                {
                    pos.Y = Y;
                }
                if (float.TryParse(t[2], System.Globalization.CultureInfo.InvariantCulture, out float Z))
                {
                    pos.Z = Z;
                }
            }
            return pos;
        }

        /// <summary>
        /// Преобразовать вектор в строку атрибута
        /// </summary>
        /// <returns></returns>
        public string ToAttribute()
        {
            return $"{FloatToAttr(X)} {FloatToAttr(Y)} {FloatToAttr(Z)}";
        }
        /// <summary>
        /// Преобразовать число с плавающей точкой в строку с обрезкой знаков
        /// </summary>
        /// <param name="value">Значение которое нужно преобразовать</param>
        /// <param name="Digits">Сколько чисел после запятой выводить в строку</param>
        /// <returns></returns>
        private static string FloatToAttr(double value, int Digits = 6)
        {
            var mValue = Math.Round(value, Digits);
            return mValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }
#pragma warning restore CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()

}
