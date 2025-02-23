using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles.Db.Types;

/// <summary>
/// Загрузчик XML файла
/// </summary>
public class TypeFileLoader(FileInfo typesFile) : XMLFileTool<File>
{
    /// <inheritdoc/>
    protected override FileInfo GetFile() => typesFile;
    
}
