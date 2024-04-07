using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Интерфейс работы с файлами логов
    /// </summary>
    public interface ILogFabric
    {
        /// <summary>
        /// Получить список всех файлов логов
        /// </summary>
        /// <returns>Полный список файлов</returns>
        public IEnumerable<FileInfo> GetAll();
        /// <summary>
        /// Получить самый свежий файлик
        /// </summary>
        /// <returns>Файл который доступен</returns>
        public FileInfo? GetNewest();
    }
}
