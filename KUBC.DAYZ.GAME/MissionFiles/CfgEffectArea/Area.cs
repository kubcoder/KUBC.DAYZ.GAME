namespace KUBC.DAYZ.GAME.MissionFiles.CfgEffectArea
{
    /// <summary>
    /// Настройка статической зоны заражения
    /// </summary>
    public class Area
    {
        /// <summary>
        /// Название зоны заражения. Нужно для понимания что настраиваем, и для отладки
        /// </summary>
        public string AreaName { get; set; } = string.Empty;
        /// <summary>
        /// Тип зоны заражения. Имя класса который создается в игре
        /// </summary>
        public string Type { get; set; } = "ContaminatedArea_Static";
        /// <summary>
        /// Имя класса триггера. Если таковой не нужен поле оставляем пустым
        /// </summary>
        public string TriggerType { get; set; } = "ContaminatedTrigger";
        /// <summary>
        /// Настройки зоны заражения
        /// </summary>
        public AreaData Data { get; set; } = new();
    }
}
