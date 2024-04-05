using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
#pragma warning disable CS0659 // Да мы пошли тупым путем, сравниваем значения а не считаем hash код... может это не правильно но это быстро
    /// <summary>
    /// Представление вектора в игре
    /// </summary>
    [JsonConverter(typeof(VectorJsonConverter))]
    public class Vector : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие вызываемое при обновлении данных
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        private double x = 0;
        private double y = 0;
        private double z = 0;

        /// <summary>
        /// Отправить уведомление о изменении данных
        /// </summary>
        /// <param name="propertyName"></param>
        protected void SendNotify(string? propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Координата X
        /// </summary>
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                if (x != value)
                {
                    x = value;
                    SendNotify(nameof(X));
                }
            }
        }
        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y != value)
                {
                    y = value;
                    SendNotify(nameof(Y));
                }
            }
        }
        /// <summary>
        /// Координата Z
        /// </summary>
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                if (z != value)
                {
                    z = value;
                    SendNotify(nameof(Z));
                }
            }
        }

        /// <summary>
        /// Преобразуем в формат строчки лога
        /// </summary>
        /// <returns></returns>
        public string ToLogFormat()
        {
            var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
            return $"<{X.ToString(culture)}, {Y.ToString(culture)}, {Z.ToString(culture)}>";
        }

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            var vector = obj as Vector;
            if (vector!=null)
            {
                if (X!=vector.X) return false;
                if (Y!=vector.Y) return false;
                if (Z!=vector.Z) return false;
                return true;
            }
            return false;
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
    }
#pragma warning restore CS0659 // Тип переопределяет Object.Equals(object o), но не переопределяет Object.GetHashCode()
    
    /// <summary>
    /// Интерфейс записи и сохранения в JSON
    /// </summary>
    public class VectorJsonConverter : JsonConverter<Vector>
    {
        /// <inheritdoc/>
        public override Vector? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int index = 0;
            Vector r = new();
            while (reader.TokenType != JsonTokenType.EndArray)
            {
                if (reader.TokenType == JsonTokenType.Number)
                {
                    double pos = 0;
                    if (reader.TryGetDouble(out pos))
                    {
                        switch (index)
                        {
                            case 0:
                                r.X = pos;
                                break;
                            case 1:
                                r.Y = pos;
                                break;
                            case 2:
                                r.Z = pos;
                                break;

                        }
                    }
                    index++;
                }
                reader.Read();
            }
            return r;
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, Vector value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            writer.WriteNumberValue(value.X);
            writer.WriteNumberValue(value.Y);
            writer.WriteNumberValue(value.Z);
            writer.WriteEndArray();
        }
    }
}
