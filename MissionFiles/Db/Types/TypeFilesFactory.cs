using KUBC.DAYZ.GAME.MissionFiles.Config.Economy.Core;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Файлы настроек игровых предметов
/// </summary>
public class TypeFilesFactory(ServerConfigFiles serverFiles)
{

    private List<FileInfo> typeFiles = [];

    private const string DEFAULT_FOLDER = "Db";

    private const string DEFAULT_FILENAME = "types.xml";

    /// <summary>
    /// Создание фабрики
    /// </summary>
    public void Create()
    {
        typeFiles.Clear();
        var ceConfigFactory = new EconomyCoreLoader(serverFiles);
        var ceCoreConfig = ceConfigFactory.Load();
        if (ceCoreConfig.CE != null)
        {
            var fileNames = ceCoreConfig.CE.Where(x => x.FileType == CEFile.VALUE_TYPE_TYPES).ToList();
            foreach (var fileName in fileNames)
            {
                typeFiles.Add(new($"{serverFiles.RootPath.FullName}\\{ceCoreConfig.CE.Folder}\\{fileName.FileName}"));
            }
        }
        if (typeFiles.Count == 0)
        {
            typeFiles.Add(new($"{serverFiles.RootPath.FullName}\\{DEFAULT_FOLDER}\\{DEFAULT_FILENAME}"));
        }
    }

    /// <summary>
    /// Список файлов с настройками игровых предметов
    /// </summary>
    public IEnumerable<FileInfo> Files => typeFiles;
}
