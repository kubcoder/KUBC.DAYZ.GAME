using KUBC.DAYZ.GAME.MissionFiles;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RM = KUBC.DAYZ.GAME.Tools.Resources.ItemTypes.LifeTime.LifeTimeTool;
namespace KUBC.DAYZ.GAME.Tools.ItemTypes.LifeTime;

/// <summary>
/// Инструмент применения времени жизни объектов
/// </summary>
/// <param name="logger">Инструмент протоколирования действий</param>
/// <param name="options">Настройки выполнения действий</param>
/// <param name="configFiles">Инструмент доступа к конфигурации</param>
public class LifeTimeTool(ILogger logger, Config options, ServerConfigFiles configFiles) : Tool(logger, options, configFiles)
{
    /// <summary>
    /// Отчет о выполнении действий
    /// </summary>
    public List<ReportLine> Report = [];

    private int lifeTime => options.LifeTime;

    /// <inheritdoc/>
    protected override void PreAction()
    {
        Report.Clear();
    }

    /// <inheritdoc/>
    public override void Apply(MissionFiles.Db.Types.File types)
    {
        foreach(var itemName in options.ItemNames)
        {
            var item = types.Where(x => x.Name == itemName).FirstOrDefault();
            if (item!=null)
            {
                Report.Add(new()
                {
                    ItemName = item.Name,
                    OldLifeTime = item.LifeTime,
                    NewLifeTime = lifeTime
                });
                Logger.LogInformation(RM.ChangeLifeTime, itemName, item.LifeTime, lifeTime);
                item.LifeTime = lifeTime;
            }
        }
    }
}
