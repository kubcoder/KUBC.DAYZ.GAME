namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

/// <summary>
/// Набор размещаемый в инвентаре
/// </summary>
public class Cargo : Preset
{
    /// <summary>
    /// Имя узла
    /// </summary>
    public const string NODENAME = "cargo";

    /// <inheritdoc/>
    protected override string NodeName => NODENAME;
}
