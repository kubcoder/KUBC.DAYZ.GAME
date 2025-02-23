using KUBC.DAYZ.GAME.MissionFiles.Db.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// Файл конфигруации
    /// центральной экономики
    /// </summary>
    public FileInfo CfgEconomyCore => new FileInfo($"{path.FullName}\\cfgeconomycore.xml");

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
