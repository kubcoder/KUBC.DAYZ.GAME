using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class MissionFiles
    {
        /// <summary>
        /// Тестируем загрузку файла конфигурации зон заражения
        /// </summary>
        [TestMethod]
        public void Cfgeffectarea()
        {
            var cfg = GAME.MissionFiles.CfgEffectArea.File.Load(new DirectoryInfo("TestFiles\\MissionFiles"));
        }
    }
}
