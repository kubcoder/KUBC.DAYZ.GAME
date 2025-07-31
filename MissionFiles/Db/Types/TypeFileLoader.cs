namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Загрузчик XML файла
/// </summary>
public class TypeFileLoader(FileInfo typesFile) : XMLFileTool<File>
{
    /// <inheritdoc/>
    protected override FileInfo GetFile() => typesFile;

}
