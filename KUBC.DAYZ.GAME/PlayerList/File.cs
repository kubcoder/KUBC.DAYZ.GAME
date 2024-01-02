using KUBC.DAYZ.GAME.MissionFiles.MapGroupProto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KUBC.DAYZ.GAME.PlayerList
{
    /// <summary>
    /// Класс файла списка игроков
    /// </summary>
    /// <remarks>
    /// Это может быть черный или белый список
    /// </remarks>
    /// <param name="fileInfo">С каким файлом работаем</param>
    public class File(FileInfo fileInfo) : IDisposable
    {
        /// <summary>
        /// Получить файл банов сервера
        /// </summary>
        /// <param name="serverPath">Папочка где валяется экземпляр сервера</param>
        /// <returns>Описание файла банов</returns>
        public static FileInfo GetBans(DirectoryInfo serverPath)
        {
            return new FileInfo($"{serverPath.FullName}\\ban.txt");
        }
        /// <summary>
        /// Получить файл белого списка сервера
        /// </summary>
        /// <param name="serverPath">Папочка где валяется экземпляр сервера</param>
        /// <returns>Описание файла белого списка</returns>
        public static FileInfo GetWhiteList(DirectoryInfo serverPath)
        {
            return new FileInfo($"{serverPath.FullName}\\whitelist.txt");
        }

        /// <summary>
        /// Добавить игрока в список
        /// </summary>
        /// <param name="ID">Идентификатор игрока</param>
        /// <param name="Notes">Примечания к добавлению</param>
        /// <returns>Если ошибок не было то вернется null. Иначе вернется ошибка</returns>
        public Exception? Add(string ID, string? Notes)
        {
            try
            {
                using (var fStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
                {
                    fStream.Seek(0, SeekOrigin.End);
                    string line = $"{ID} //{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} {Notes}";
                    if (fStream.Position != 0)
                    {
                        line = "\n" + line;
                    }
                    byte[] record = Encoding.Default.GetBytes(line);
                    fStream.Write(record);
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        

        /// <summary>
        /// Уничтожить класс
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Проверить наличие игрока в списке
        /// </summary>
        /// <remarks>
        /// Метод может вызывать исключение, ловите его в месте где вызываете метод
        /// </remarks>
        /// <param name="ID">Идентификатор уебка</param>
        /// <returns>Истина если игрок в списке, ложь если он не в списке</returns>
        public bool InList(string ID)
        {
            using (var rStream = fileInfo.OpenText())
            {
                var Line = rStream.ReadLine();
                while (Line != null)
                {
                    PlayerLine pi = new(Line);
                    if (pi.ID.Equals(ID))
                    {
                        return true;
                    }
                    Line = rStream.ReadLine();
                }
            }
            return false;
        }

        /// <summary>
        /// Получить список игроков в файле
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PlayerLine> GetPlayers()
        {
            List<PlayerLine> r = [];
            using (var rStream = fileInfo.OpenText())
            {
                var Line = rStream.ReadLine();
                while (Line != null)
                {
                    r.Add(new(Line));
                    Line = rStream.ReadLine();
                }
            }
            return r;
        }

        /// <summary>
        /// Удалить игрока из файла
        /// </summary>
        /// <param name="ID">Идентификатор игрока</param>
        /// <returns>Если ошибок не было то вернется null. Иначе вернется ошибка</returns>
        public Exception? Remove(string ID)
        {
            try
            {
                bool Found = false;
                var before = new List<string>();
                using (var rStream = fileInfo.OpenText())
                {
                    var Line = rStream.ReadLine();
                    while (Line != null)
                    {
                        if (!Found)
                        {
                            PlayerLine pi = new(Line);
                            if (pi.ID.Equals(ID))
                            {
                                Found = true;
                            }
                            else
                            {
                                before.Add(Line);
                            }
                        }
                        else
                        {
                            before.Add(Line);
                        }
                        Line = rStream.ReadLine();
                    }
                }
                if (Found)
                {
                    using (var wStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
                    {
                        foreach (var line in before)
                        {
                            byte[] record = Encoding.Default.GetBytes($"\n{line}");
                            wStream.Write(record);
                        }
                        wStream.SetLength(wStream.Position);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
