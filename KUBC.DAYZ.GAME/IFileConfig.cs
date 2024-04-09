using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Интерфейс поиска файлов конфигурации
    /// </summary>
    public interface IFileConfig
    {
        /// <summary>
        /// Папка размещения файлов конфигурации
        /// </summary>
        public DirectoryInfo Path { get; set; }
        /// <summary>
        /// Список файлов конфигурации
        /// </summary>
        /// <returns>Массив файлов конфигурации</returns>
        public IEnumerable<FileInfo> GetFiles();
        /// <summary>
        /// Загрузить конфигурацию
        /// </summary>
        /// <returns>Экземпляр конфигурации</returns>
        public IConfig? Load();

    }
}
