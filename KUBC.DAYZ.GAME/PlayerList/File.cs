using KUBC.DAYZ.GAME.MissionFiles.MapGroupProto;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class File(FileInfo fileInfo)
    {
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
                    if (fStream.Position !=0)
                    {
                        line = "\n\r" + line;
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
        /// Удалить игрока из файла
        /// </summary>
        /// <param name="ID">Идентификатор игрока</param>
        /// <returns>Если ошибок не было то вернется null. Иначе вернется ошибка</returns>
        public Exception? Remove(string ID) 
        {
            return null;
        }
    }
}
