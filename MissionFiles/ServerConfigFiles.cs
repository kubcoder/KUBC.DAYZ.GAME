using KUBC.DAYZ.GAME.MissionFiles.Db.Types;

namespace KUBC.DAYZ.GAME.MissionFiles;

/// <summary>
/// Фабрика получения имен
/// серверных файлов
/// </summary>
/// <param name="path">Полный путь к папке сервера</param>
public class ServerConfigFiles(DirectoryInfo path)
{
    /// <summary>
    /// Корневая папка
    /// конфигурации сервера
    /// </summary>
    public DirectoryInfo RootPath => path;

    /// <summary>
    /// Файл конфигурации
    /// центральной экономики
    /// </summary>
    public FileInfo CfgEconomyCore => new FileInfo($"{path.FullName}\\cfgeconomycore.xml");

    /// <summary>
    /// Файл конфигурации случайных наборов
    /// итемов
    /// </summary>
    public FileInfo CfgRandomPresets => new FileInfo($"{path.FullName}\\cfgrandompresets.xml");

    /// <summary>
    /// Получить список файлов 
    /// с настройками игровых предметов
    /// </summary>
    public IEnumerable<FileInfo> TypesFiles
    {
        get
        {
            var factory = new TypeFilesFactory(this);
            factory.Create();
            return factory.Files;
        }
    }



}
