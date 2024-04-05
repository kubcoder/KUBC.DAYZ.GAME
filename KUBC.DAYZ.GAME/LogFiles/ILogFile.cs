using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Интерфейс файла лога
    /// </summary>
    public interface ILogFile : IDisposable
    {
        /// <summary>
        /// Открыть файл лога
        /// </summary>
        /// <param name="file">Файл который мы хотим открыть</param>
        public void OpenFile(FileInfo file);
        /// <summary>
        /// Выполнить чтение файла лога до конца
        /// </summary>
        /// <returns>Список загруженных событий</returns>
        public IEnumerable<ILogEntity>? ReadToEnd(CancellationToken? cancellationToken = null);
        
    }
}
