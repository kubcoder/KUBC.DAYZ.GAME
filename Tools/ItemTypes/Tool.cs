using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.Tools.ItemTypes;
using RM = KUBC.DAYZ.GAME.Tools.Resources.ItemTypes.Tool;

/// <summary>
/// Абстрактный класс инструмента
/// </summary>
/// <param name="logger">Инструмент протоколирования действий</param>
/// <param name="options">Настройки выполнения действий</param>
/// <param name="configFiles">Инструмент доступа к конфигурации</param>
public abstract class Tool(ILogger logger, Config options, MissionFiles.ServerConfigFiles configFiles)
{
    /// <summary>
    /// Интерфейс для протоколирования действий
    /// </summary>
    protected ILogger Logger => logger;

    /// <summary>
    /// Общие настройки выполнения действий
    /// </summary>
    protected Config Options => options;

    /// <summary>
    /// Применить инструмент для 
    /// файлов конфигурации
    /// </summary>
    public void Apply()
    {
        PreAction();
        foreach (var fileInfo in configFiles.TypesFiles)
        {
            Apply(fileInfo);
        }
    }

    /// <summary>
    /// Применить инструмент для файла
    /// конфигурации
    /// </summary>
    /// <param name="fileInfo">для какого файла применять инструмент</param>
    public virtual void Apply(FileInfo fileInfo)
    {
        Logger.LogInformation(RM.StartCheckFile, fileInfo.Name);
        var fileTool = new MissionFiles.Db.Types.TypeFileLoader(fileInfo);
        var types = fileTool.Load();
        Apply(types);
        if (Options.Enable)
        {
            Logger.LogInformation(RM.SaveFile, fileInfo.Name);
            fileTool.Save(types);
        }
    }

    /// <summary>
    /// Применить инструмент для набора 
    /// настроек игровых предметов
    /// </summary>
    /// <param name="types"></param>
    public abstract void Apply(MissionFiles.Db.Types.File types);
    

    /// <summary>
    /// Действия перед пременением инструмента
    /// </summary>
    protected virtual void PreAction()
    {

    }

    
}
