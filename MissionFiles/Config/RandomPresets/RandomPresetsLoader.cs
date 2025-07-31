namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

/// <summary>
/// Загрузчик файла 
/// конфигурации наборов
/// </summary>
/// <param name="serverFiles"></param>
public class RandomPresetsLoader(ServerConfigFiles serverFiles) : XMLFileTool<CfgRandomPresets>
{
    /// <inheritdoc/>
    protected override FileInfo GetFile() => serverFiles.CfgRandomPresets;
}
