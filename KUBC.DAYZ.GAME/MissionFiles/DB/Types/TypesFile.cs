using KUBC.DAYZ.GAME.MissionFiles.DB.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace KUBC.DAYZ.GAME.MissionFiles.DB.Types
{
    /// <summary>
    /// Файл настройки игровых предметов
    /// </summary>
    /// <param name="Path"></param>
    public class TypesFile(DirectoryInfo Path) : FileXMLConfig(Path)
    {
        /// <inheritdoc/>
        public override IEnumerable<FileInfo> GetFiles()
        {
            var files = new List<FileInfo>();
            files.Add(GetFile("types.xml"));
            return files;
        }
        /// <inheritdoc/>
        public override IConfig? Load()
        {
            var file = GetFiles().FirstOrDefault();
            if ((file != null) && (file.Exists))
            {
                using (StreamReader reader = OpenFile(file))
                {
                    XmlSerializer serializer = new(typeof(ItemTypes));
                    var economy = serializer.Deserialize(reader) as ItemTypes;
                    return economy;
                }
            }
            return null;
        }
    }
}
