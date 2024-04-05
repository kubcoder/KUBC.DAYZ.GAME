using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.LogFiles.RPT
{
    /// <summary>
    /// Инструменты для работы со логом RPT
    /// </summary>
    /// <remarks>
    /// <para>
    /// Это логи с расширением *.RPT. В него пишутся прямо данные в реальном времени,
    /// т.е. после каждой строчки вызывается Flush(). Это где то было написано у официалов
    /// где именно уже не помню.
    /// </para>
    /// <para>
    /// В этом логе пишется прямо много всего, и в большинстве случаев это самый большой
    /// файл лога (т.е. строчек прямо дохрена). Но из всего этого разнообразия мы забираем
    /// только три интересных события:
    /// </para>
    /// <list type="bullet">
    /// <item><see cref="AverageFPS"/></item>
    /// <item><see cref="UsedMemory"/></item>
    /// <item><see cref="ConnectEvent"/></item>
    /// </list>
    /// </remarks>
    internal class NamespaceDoc
    {
    }
}
