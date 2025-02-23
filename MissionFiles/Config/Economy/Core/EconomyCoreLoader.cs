using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.Economy.Core;

/// <summary>
/// Загрузчик файла
/// конфигурации экономики
/// </summary>
public class EconomyCoreLoader(ServerConfigFiles serverFiles) : XMLFileTool<CfgEconomyCore>
{
    /// <inheritdoc/>
    protected override FileInfo GetFile() => serverFiles.CfgEconomyCore;
}
