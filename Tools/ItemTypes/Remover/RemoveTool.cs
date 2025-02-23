using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RM = KUBC.DAYZ.GAME.Tools.Resources.ItemTypes.Remover.RemoveTool;

namespace KUBC.DAYZ.GAME.Tools.ItemTypes.Remover;

/// <summary>
/// Инструмент для удаления
/// игровых предметов из спавна 
/// центральной экономики
/// </summary>
public class RemoveTool(ILogger logger, Config options, MissionFiles.ServerConfigFiles configFiles)
{

    /// <summary>
    /// Отчет о выполнении операции
    /// </summary>
    public Dictionary<string, int> Report = [];
    
    /// <summary>
    /// Выполнить процедуру очистки 
    /// игровых предметов
    /// </summary>
    public void Remove()
    {
        Report.Clear();
        foreach(var fileInfo in configFiles.TypesFiles)
        {
            RemoveFromFile(fileInfo);
        }
    }

    private void RemoveFromFile(FileInfo fileInfo)
    {
        logger.LogInformation(RM.StartCheckFile, fileInfo.Name);
        var fileTool = new MissionFiles.Db.Types.TypeFileLoader(fileInfo);
        var types = fileTool.Load();
        RemoveItems(types);
        if (options.Enable)
        {
            logger.LogInformation(RM.SaveFile, fileInfo.Name);
            fileTool.Save(types);
        }
    }

    

    private void RemoveItems(MissionFiles.Db.Types.File types)
    {
        foreach(var itemName in options.ItemNames)
        {
            var count = types.RemoveAll(x => x.Name == itemName);
            logger.LogInformation(RM.FoundItems, count, itemName);
            AddToReport(itemName, count);
        }
    }

    private void AddToReport(string name, int count)
    {
        if (Report.ContainsKey(name))
            Report[name] += count;
        else
            Report.Add(name, count);
    }
}
