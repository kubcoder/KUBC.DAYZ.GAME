using KUBC.DAYZ.GAME.LogFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME
{
    /// <summary>
    /// Конфигурация файлов
    /// </summary>
    public abstract class FileConfig : IFileConfig
    {
        /// <summary>
        /// Папочка с которой работаем
        /// </summary>
        protected DirectoryInfo _path;



        /// <summary>
        /// Инциализация класса файла/файлов конфигурации
        /// </summary>
        /// <param name="path">Рабочая папка настроек</param>
        public FileConfig(DirectoryInfo path)
        {
            this._path = path;
        }
        /// <inheritdoc/>
        public DirectoryInfo Path { get => _path; set => _path=value; }
        DirectoryInfo IFileConfig.Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <inheritdoc/>
        public abstract IEnumerable<FileInfo> GetFiles();
        /// <inheritdoc/>
        public abstract IConfig? Load();
        /// <summary>
        /// Получить рабочую директорию
        /// </summary>
        /// <returns>Папочка с которой работаем</returns>
        protected virtual DirectoryInfo GetWorkPath() => _path;
        /// <summary>
        /// Получить информацию о файле по его имени в рабочей папочке
        /// </summary>
        /// <param name="FileName">Имя файла</param>
        /// <returns>Описание файла с полным именем в рабочей папочке</returns>
        protected FileInfo GetFile(string FileName)
        {
            return new FileInfo($"{GetWorkPath().FullName}\\{FileName}");
        }
        /// <summary>
        /// Открыть файл для чтения
        /// </summary>
        /// <param name="file">Файл который нужно открыть</param>
        /// <returns>Поток для чтения файла</returns>
        protected StreamReader OpenFile(FileInfo file)
        {
            return new StreamReader(file.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
    }
}
