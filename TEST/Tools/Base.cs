using KUBC.DAYZ.GAME.MissionFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.Tools;

/// <summary>
/// Тесты базовых инструментов
/// </summary>
public class Base : AbstractTest
{

    /// <summary>
    /// Проверяем правильность работы
    /// инструмента получения имен файлов
    /// </summary>
    [Fact]
    public void DefaultTypes()
    {
        TestTypes(1);
        TestTypes(2);
    }

    private void TestTypes(int instance)
    {
        var configFiles = new ServerConfigFiles(GetInstance(instance));
        var typesFiles = configFiles.TypesFiles;
        foreach (var file in typesFiles)
            Assert.True(file.Exists);
    }
}
