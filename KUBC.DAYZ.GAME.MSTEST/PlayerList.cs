using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    /// <summary>
    /// Тестируем интерфейсы работы с списками игроковы
    /// </summary>
    [TestClass]
    public class PlayerList
    {
        
        [TestMethod]
        public void AddToFile()
        {
            var file = new GAME.PlayerList.File(new FileInfo("testlist.txt"));
            var ex = file.Add("1111111111112222222222222333333333XXXXXXAAAA", null);
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("2111111111112222222222222333333333XXXXXXAAAA", "заметка");
            Assert.IsNull(ex, ex?.Message);
        }
    }
}
