using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace KUBC.DAYZ.GAME.MissionFiles.Config.Economy.Core;

public class ConfigFile
{
    [Fact]
    public void ReadWriteFile()
    {
        var fileInfo = new FileInfo("MissionFiles\\Config\\Economy\\Core\\cfgeconomycore.xml");
        Assert.True(fileInfo.Exists);
        var cfg = new File();
        using (var file = fileInfo.OpenRead())
        {
            using (var reader = XmlReader.Create(file))
            {
                cfg.ReadXml(reader);
            }
        }
        var extFile = new FileInfo("cfgeconomycore.xml");
        using (var file = extFile.Create())
        {
            using (var writer = XmlWriter.Create(file, new() { Indent = true }))
            {
                cfg.WriteXml(writer);
            }
        }

    }
}
