using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Абстрактный класс поиска файлов
    /// </summary>
    public abstract class LogFabric : ILogFabric
    {
        /// <inheritdoc/>
        public abstract IEnumerable<FileInfo> GetAll();

        /// <inheritdoc/>
        public virtual FileInfo? GetNewest()
        {
            IEnumerable<FileInfo> files = GetAll();
            return files.OrderByDescending(x => x.LastWriteTime).FirstOrDefault();
        }

    }
}
