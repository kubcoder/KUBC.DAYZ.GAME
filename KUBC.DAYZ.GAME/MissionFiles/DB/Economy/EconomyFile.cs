using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Economy
{
    /// <summary>
    /// Файл настройки экономики
    /// </summary>
    public class EconomyFile(DirectoryInfo Path) : FileXMLConfig(Path)
    {
        /// <inheritdoc/>
        public override IEnumerable<FileInfo> GetFiles()
        {
            var files = new List<FileInfo>();
            files.Add(GetFile("economy.xml"));
            return files;
        }
        /// <inheritdoc/>
        public override IConfig? Load()
        {
            var file = GetFiles().FirstOrDefault();
            if ((file != null)&&(file.Exists))
            {
                using(StreamReader reader = OpenFile(file))
                {
                    XmlSerializer serializer = new(typeof(EconomyConfig));
                    var economy = serializer.Deserialize(reader) as EconomyConfig;
                    return economy;
                }
            }
            return null;
        }
    }
}
