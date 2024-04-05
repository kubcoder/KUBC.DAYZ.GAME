using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles
{
    /// <summary>
    /// Элемент данных лога
    /// </summary>
    public interface ILogEntity
    {
        /// <summary>
        /// Проверка на завершение чтение данных
        /// </summary>
        /// <returns>Если истина то событие полностью прочитано и событие можно использовать далее</returns>
        public bool IsEndRead();
        /// <summary>
        /// Добавить еще одну строчку данных. Если элемент данных может быть в нескольких строчках
        /// </summary>
        /// <param name="Line">Данные добавляемые в событие</param>
        /// <returns>Истина если чтение данных закончено, ложь если нужно добавить еще строчка</returns>
        public bool AppendLine(string Line);
    }
}
