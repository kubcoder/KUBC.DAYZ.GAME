using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Набор файлов находящийся в подпапке
    /// </summary>
    public abstract class SubPathConfig:FileConfig
    {
        /// <summary>
        /// Рабочая папка класса
        /// </summary>
        protected DirectoryInfo workPath;

        /// <summary>
        /// Инициализировать набор файлов
        /// </summary>
        /// <param name="pPath">Родительская папка</param>
        public SubPathConfig(DirectoryInfo pPath):base(pPath) 
        {
            workPath = pPath.CreateSubdirectory(GetPathName());
        }
        /// <summary>
        /// Получить название вложенной папочки
        /// </summary>
        /// <returns>Имя вложенной папки</returns>
        protected abstract string GetPathName();
        /// <inheritdoc/>
        protected override DirectoryInfo GetWorkPath()
        {
            return workPath;
        }
    }
}
