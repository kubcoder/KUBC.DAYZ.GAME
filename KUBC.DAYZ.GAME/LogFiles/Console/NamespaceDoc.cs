using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.Console
{
    /// <summary>
    /// Инструменты для работы с логом файла консольного вывода.
    /// </summary>
    /// <remarks>
    /// Этот лог включается параметром конфигурации <code>logFile="logfilename.log"</code>
    /// Подробности о включении данного лога тут: https://community.bistudio.com/wiki/DayZ:Server_Configuration
    /// <para>
    /// ВАЖНО!!! В библиотеку не включен класс поиска и загрузки файлов логов консоли, так как он один
    /// его имя фиксировано задается в файле конфигурации сервера. И при каждом запуске сервера он создается заново.
    /// </para>
    /// <para>
    /// Парсера как такового нет, но вы можете самостоятельного его прикрутить до файла, все необходимые интерфейсы реализованы.
    /// Выбор стратегии за вами, вы можете добавлять свои парсеры в строенную коллекцию, либо полностью заменить парсер на свой.
    /// </para>
    /// </remarks>
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class NamespaceDoc
    {
    }
}
