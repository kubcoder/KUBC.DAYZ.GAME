namespace KUBC.DAYZ.GAME.MissionFiles.CfgEffectArea
{
    /// <summary>
    /// Данные зоны заражения
    /// </summary>
    public class AreaData
    {
        /// <summary>
        /// Координаты зоны заражения
        /// </summary>
        /// <remarks>
        /// Position of anchor entity, if Y coordinate is non-zero, entity and first layer of particles will not be snapped to ground
        /// Положение якоря объекта, если координата Y отлична от нуля, объект и первый слой частиц не будут привязаны к земле
        /// </remarks>
        public Vector Pos { get; set; } = new();

        /// <summary>
        /// Радиус зоны заражения
        /// </summary>
        public float Radius { get; set; } = -1;
        /// <summary>
        /// Height of cylinder area going up from anchor entity position
        /// Высота области цилиндра, идущей вверх от положения объекта привязки
        /// </summary>
        public float PosHeight { get; set; } = -1;
        /// <summary>
        /// Height of cylinder area going down from anchor entity position
        /// Высота области цилиндра, идущей вниз от положения объекта привязки
        /// </summary>
        public float NegHeight { get; set; } = -1;
        /// <summary>
        /// The amount of rings user wants inside of area ( does not include outer ring )
        /// Количество колец, которое пользователь хочет внутри области (не включает внешнее кольцо)
        /// </summary>
        public int InnerRingCount { get; set; } = 2;
        /// <summary>
        /// Distance, on a straight line, between two particle emitters on interior rings. If value is strictly superior to radius, NO rings will be spawned
        /// Расстояние по прямой линии между двумя излучателями частиц на внутренних кольцах. Если значение строго больше радиуса, колец не будет создано.
        /// </summary>
        public int InnerPartDist { get; set; } = -1;
        /// <summary>
        /// Toggle if outer ring is desired ( if yes it will be an additional ring to the inner rings )
        /// Переключите, если требуется внешнее кольцо (если да, это будет дополнительное кольцо к внутренним кольцам)
        /// </summary>
        public int OuterRingToggle { get; set; } = 1;
        /// <summary>
        /// Distance, on a straight line, between two particle emitters on most outside ring
        /// Расстояние по прямой линии между двумя излучателями частиц на самом внешнем кольце
        /// </summary>
        public int OuterPartDist { get; set; } = -1;
        /// <summary>
        /// The distance with the radius of area ( negative value will push outer ring OUTSIDE of radius )
        /// Расстояние с радиусом области (отрицательное значение вытолкнет внешнее кольцо ВНЕШНИМ радиусом)
        /// </summary>
        public int OuterOffset { get; set; } = -1;
        /// <summary>
        /// The amount of vertical layers user wants to define ( in addition to ground level )
        /// Количество вертикальных слоев, которые пользователь хочет определить (в дополнение к уровню земли)
        /// </summary>
        public int VerticalLayers { get; set; } = -1;
        /// <summary>
        /// Offset between two vertical layers
        /// Смещение между двумя вертикальными слоями
        /// </summary>
        public int VerticalOffset { get; set; } = -1;
        /// <summary>
        /// The name of the particle one wants to populate the area with
        /// Имя частицы, которой нужно заполнить область
        /// </summary>
        public string ParticleName { get; set; } = "contaminated_area_gas_bigass";


    }
}
