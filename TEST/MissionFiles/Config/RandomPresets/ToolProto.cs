using KUBC.DAYZ.GAME.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.RandomPresets;

public class ToolProto : AbstractTest
{
    private MissionFiles.ServerConfigFiles serverFiles;

    private const string CONFIG_PATH = "Path";

    private ITestOutputHelper output;

    public ToolProto(ITestOutputHelper output)
    {
        this.output = output;
        var userDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        Assert.NotNull(userDocs);
        var serverPath = Config.GetValue<string>(CONFIG_PATH);
        Assert.NotNull(serverPath);
        serverFiles = new(new DirectoryInfo($"{userDocs}\\{serverPath}"));
        Assert.True(serverFiles.RootPath.Exists);
        
    }

    /// <summary>
    /// Пробуем удалять нафиг всю еду из наборов
    /// </summary>
    [Fact]
    public void RemoveFood()
    {
        var removeSection = Config.GetSection("RemoveFood");
        Assert.NotNull(removeSection);
        var toolConfig = new Tools.ItemTypes.Config();
        removeSection.Bind(toolConfig);
        var fileLoader = new RandomPresets.RandomPresetsLoader(serverFiles);
        var data = fileLoader.Load();
        output.WriteLine("Загружено {0} наборов", data.Count);
        foreach(var preset in data)
        {
            output.WriteLine("Проверяем набор {0}, который содержит {1} предметов", preset.Name, preset.Items.Count);
            var removeItems = new List<PresetItem>();
            foreach(var item in preset.Items)
            {
                if (item.Name == null)
                    continue;
                if (toolConfig.ItemNames.Contains(item.Name))
                {
                    output.WriteLine("Игровой предмет {0} найден в списке удаления, выкидываем его из набора", item.Name);
                    removeItems.Add(item);
                }
                else
                {
                    output.WriteLine("Оставляем игровой предмет {0}", item.Name);
                }
            }
            if (removeItems.Count > 0)
            {
                output.WriteLine("Готовимся к удалению {0} предметов, сейчас в наборе {1} предметов", removeItems.Count, preset.Items.Count);
                foreach(var removeItem in removeItems)
                {
                    preset.Items.Remove(removeItem);
                }
                output.WriteLine("Закончили удаление предметов, в наборе осталось {0} предметов", preset.Items.Count);
            }
        }
        fileLoader.Save(data);
    }
}
