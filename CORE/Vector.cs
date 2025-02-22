using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME;

/// <summary>
/// Представление вектора в игре
/// </summary>
public class Vector
{
    /// <summary>
    /// Координата X
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Координата Y
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Координата Z
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// Преобразуем в формат строчки лога
    /// </summary>
    /// <returns></returns>
    public string ToLogFormat()
    {
        var culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB");
        return $"<{X.ToString(culture)}, {Y.ToString(culture)}, {Z.ToString(culture)}>";
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
