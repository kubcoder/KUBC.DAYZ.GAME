namespace KUBC.DAYZ.GAME.MissionFiles.CfgUndergroundTriggers
{
    /// <summary>
    /// В общем это какие то градации тригера...
    /// Т.е. триггер сработал, и далее внуренние позиции которые меняют освещение...
    /// Но я ХЗ могу ошибаться
    /// </summary>
    public class Breadcrumb
    {
        /// <summary>
        /// Координаты точки
        /// </summary>
        public Vector? Position { get; set; }
        /// <summary>
        /// Освещенность
        /// </summary>
        public float EyeAccommodation { get; set; } = 0;
        /// <summary>
        /// Вот ХЗ
        /// </summary>
        public int UseRaycast { get; set; } = 0;
        /// <summary>
        /// Радиус точки... судя по всему -1 это отключение функции нахуй...
        /// </summary>
        public float Radius { get; set; } = -1;
    }
}
