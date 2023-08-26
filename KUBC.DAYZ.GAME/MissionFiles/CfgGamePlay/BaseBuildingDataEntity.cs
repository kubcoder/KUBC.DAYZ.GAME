namespace KUBC.DAYZ.GAME.MissionFiles.CfgGamePlay
{
    /// <summary>
    /// Настройки строительства игроками
    /// </summary>
    public class BaseBuildingDataEntity
    {
        /// <summary>
        /// Поведение голограмы размещения
        /// </summary>
        public HologramDataEntity HologramData { get; set; } = new();
        /// <summary>
        /// Настройки проверок при строительстве объектов
        /// </summary>
        public ConstructionDataEntity ConstructionData { get; set; } = new();

    }
}
