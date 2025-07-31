using Microsoft.Extensions.Logging;

using RM = KUBC.DAYZ.GAME.Tools.Resources.ItemTypes.Remover.RemoveTool;

namespace KUBC.DAYZ.GAME.Tools.ItemTypes.Remover;

/// <summary>
/// Инструмент для удаления
/// игровых предметов из спавна 
/// центральной экономики
/// </summary>
/// <param name="logger">Инструмент протоколирования действий</param>
/// <param name="options">Настройки выполнения действий</param>
/// <param name="configFiles">Инструмент доступа к конфигурации</param>
public class RemoveTool(ILogger logger, Config options, MissionFiles.ServerConfigFiles configFiles) : Tool(logger, options, configFiles)
{

    /// <summary>
    /// Отчет о выполнении операции
    /// </summary>
    public Dictionary<string, int> Report = [];


    /// <inheritdoc/>
    protected override void PreAction()
    {
        Report.Clear();
    }

    private void AddToReport(string name, int count)
    {
        if (Report.ContainsKey(name))
            Report[name] += count;
        else
            Report.Add(name, count);
    }

    /// <inheritdoc/>
    public override void Apply(MissionFiles.Db.Types.File types)
    {
        foreach (var itemName in Options.ItemNames)
        {
            var count = types.RemoveAll(x => x.Name == itemName);
            Logger.LogInformation(RM.FoundItems, count, itemName);
            AddToReport(itemName, count);
        }
    }
}
