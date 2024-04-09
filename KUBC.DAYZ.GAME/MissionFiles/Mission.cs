using KUBC.DAYZ.GAME.MissionFiles.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MissionFiles
{
    /// <summary>
    /// Миссия сервера
    /// </summary>
    public class Mission : FileConfig
    {
        /// <summary>
        /// Конфигурация базы данных игровой ситуации
        /// </summary>
        public DBFiles DB;

        /// <summary>
        /// Инициализация файлов миссии
        /// </summary>
        /// <param name="MissonPath">Папка с файлами миссий</param>
        public Mission(DirectoryInfo MissonPath):base(MissonPath)
        {
            DB = new(MissonPath);
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
    }
}
