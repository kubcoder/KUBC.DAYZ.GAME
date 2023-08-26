namespace KUBC.DAYZ.GAME.MissionFiles.CfgEffectArea
{
    /// <summary>
    /// Настройки игрока в зоне заражения
    /// </summary>
    public class PlayerData
    {
        /// <summary>
        /// The name of the "around" particle to spawn around player when in trigger -> requires trigger to do anything
        /// </summary>
        public string AroundPartName { get; set; } = "graphics/particles/contaminated_area_gas_around";
        /// <summary>
        /// The name of the "tiny" particle to spawn around player when in trigger -> requires trigger to do anything
        /// </summary>
        public string TinyPartName { get; set; } = "graphics/particles/contaminated_area_gas_around_tiny";
        /// <summary>
        /// The typename of the Post Process Effect to apply on the player camera when inside the area
        /// </summary>
        public string PPERequesterType { get; set; } = "PPERequester_ContaminatedAreaTint";
    }
}
