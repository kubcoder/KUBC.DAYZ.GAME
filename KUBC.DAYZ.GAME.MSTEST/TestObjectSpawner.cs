using KUBC.DAYZ.GAME.MissionFiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST;

[TestClass]
public class TestObjectSpawner
{

    [TestMethod]
    public void Json()
    {
        var file = new ObjectSpawnerFile();
        file.Objects.Add(new()
        {
            Name = "testClass",
            Position = new Vector() { X = 100, Y = 200, Z = 300 },
            YPR = new Vector() { X = 1, Y = 2, Z = 3 }
        });
        var json = JsonSerializer.Serialize<ObjectSpawnerFile>(file, new JsonSerializerOptions() { WriteIndented = true});
        Console.WriteLine(json);
        var lObj = JsonSerializer.Deserialize<ObjectSpawnerFile>(json);
        var item = lObj.Objects.FirstOrDefault();
        Assert.IsNotNull(item);

    }
}
