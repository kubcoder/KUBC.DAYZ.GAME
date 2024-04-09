using KUBC.DAYZ.GAME.MissionFiles.DB.Economy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles.DB
{
    /// <summary>
    /// Файлы раздела DB
    /// </summary>
    public class DBFiles:SubPathConfig
    {
        /// <summary>
        /// Настройки центральной экономики
        /// </summary>
        public EconomyFile Economy;
        /// <summary>
        /// Инициализируем секцию настроек DB
        /// </summary>
        /// <param name="MissionPath">Папочка миссий</param>
        public DBFiles(DirectoryInfo MissionPath):base(MissionPath)
        {
            Economy = new EconomyFile(GetWorkPath());
        }
        /// <inheritdoc/>
        public override IEnumerable<FileInfo> GetFiles()
        {
            return new List<FileInfo>();
        }
        /// <inheritdoc/>
        public override IConfig? Load()
        {
            return null;
        }
        /// <inheritdoc/>
        protected override string GetPathName()
        {
            return "db";
        }
    }
}
