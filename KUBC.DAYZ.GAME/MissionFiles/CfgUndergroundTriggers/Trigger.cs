namespace KUBC.DAYZ.GAME.MissionFiles.CfgUndergroundTriggers
{
    /// <summary>
    /// Описание триггера изменения освещения в подземелье
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// Координата центра триггера
        /// </summary>
        public Vector Position { get; set; } = new();
        /// <summary>
        /// Ориентация триггера
        /// </summary>
        public Vector Orientation { get; set; } = new();
        /// <summary>
        /// Размер тригера
        /// </summary>
        public Vector Size { get; set; } = new();
        /// <summary>
        /// Уровень темноты...
        /// На сколько я понял 0 это света нихуя нет, 1 полностью свет от мира
        /// </summary>
        public float EyeAccommodation { get; set; } = 0;
        /// <summary>
        /// Градации освещения в нутри триггера
        /// </summary>
        public List<Breadcrumb> Breadcrumbs { get; set; } = new();
        /// <summary>
        /// Скорость смены освещения
        /// </summary>
        public float InterpolationSpeed { get; set; } = 0;
    }
}
