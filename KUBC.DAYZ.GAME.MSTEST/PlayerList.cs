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

        [TestMethod]
        public void RemoveFromFile()
        {
            var fileInfo = new FileInfo("testlist.txt");
            if (fileInfo.Exists) 
            {
                fileInfo.Delete();
            }
            var file = new GAME.PlayerList.File(fileInfo);
            var ex = file.Add("1111111111112222222222222333333333XXXXXXAAAA", null);
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("2111111111112222222222222333333333XXXXXXAAAA", "заметка");
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("3111111111112222222222222333333333XXXXXXAAAA", "заметка");
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("4111111111112222222222222333333333XXXXXXAAAA", "заметка");
            Assert.IsNull(ex, ex?.Message);
            Console.WriteLine("===================== Исходный файл ==========================");
            using (var cReader = fileInfo.OpenText())
            {
                Console.Write(cReader.ReadToEnd());
            }
            Console.WriteLine("===================== Опа файлик закончился то ===============");
            var targetID = "4111111111112222222222222333333333XXXXXXAAAA";
            Assert.IsTrue(file.InList(targetID));
            ex = file.Remove(targetID);
            Assert.IsNull(ex, ex?.Message);
            Console.WriteLine("===================== Исправленный файл ======================");
            using (var cReader = fileInfo.OpenText())
            {
                Console.Write(cReader.ReadToEnd());
            }
            Console.WriteLine("==================== Опа файлик закончился то ===============");
            Assert.IsFalse(file.InList(targetID));
        }
    }
}
