using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Класс поиска логов игры
    /// </summary>
    /// <param name="PathProfiles">
    /// Папка профилей игры
    /// </param>
    public abstract class GameLogs(DirectoryInfo PathProfiles):LogFabric
    {
        /// <summary>
        /// Получить строку для поиска файлов
        /// </summary>
        /// <returns></returns>
        protected abstract string GetFindString();

        /// <summary>
        /// Проверить является ли файл целевым файлом лога
        /// </summary>
        /// <param name="file">Проверяемый файл</param>
        /// <returns>Истина если файл лога</returns>
        protected virtual bool IsLogFile(FileInfo file)
        {
            return true;
        }

        /// <inheritdoc/>
        public override IEnumerable<FileInfo> GetAll()
        {
            List<FileInfo> logs = [];
            var findFiles = PathProfiles.GetFiles(GetFindString());
            foreach (var file in findFiles) 
            {
                if (IsLogFile(file))
                {
                    logs.Add(file);
                }
            }
            return logs;
        }

    }
}
