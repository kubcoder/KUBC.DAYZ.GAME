﻿using System;
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
        
        /// <summary>
        /// Тута мы трошки тестим как оно себя ведет когда нужно добавить гамера в списочек
        /// </summary>
        [TestMethod]
        public void AddToFile()
        {
            var file = new GAME.PlayerList.File(new FileInfo("testlist.txt"));
            var ex = file.Add("1111111111112222222222222333333333XXXXXXAAAA", null);
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("2111111111112222222222222333333333XXXXXXAAAA", "заметка");
            Assert.IsNull(ex, ex?.Message);
        }
        /// <summary>
        /// Тута мы трошки тестим как оно себя ведет когда нужно удалить гамера из списочка
        /// </summary>
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
        /// <summary>
        /// Тута мы трошки тестим получается ли найти нужные файлики
        /// </summary>
        [TestMethod]
        public void TestFoundFiles()
        {
            var path = new DirectoryInfo("C:\\Program Files (x86)\\Steam\\steamapps\\common\\DayZServer");
            var bans = GAME.PlayerList.File.GetBans(path);
            Assert.IsTrue(bans.Exists);
            var white = GAME.PlayerList.File.GetWhiteList(path);
            Assert.IsTrue(white.Exists);
        }
        [TestMethod]
        public void ReadAllFile()
        {
            var fileInfo = new FileInfo("testlist.txt");
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            var file = new GAME.PlayerList.File(fileInfo);
            var ex = file.Add("1111111111112222222222222333333333XXXXXXAAAA", null);
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("2111111111112222222222222333333333XXXXXXAAAA", "заметка 2");
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("3111111111112222222222222333333333XXXXXXAAAA", "заметка 3");
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("4111111111112222222222222333333333XXXXXXAAAA", "заметка 4");
            Assert.IsNull(ex, ex?.Message);
            ex = file.Add("5111111111112222222222222333333333XXXXXXAAAA", null);
            Assert.IsNull(ex, ex?.Message);
            Console.WriteLine("===================== Исходный файл ==========================");
            using (var cReader = fileInfo.OpenText())
            {
                Console.Write(cReader.ReadToEnd());
            }
            Console.WriteLine();
            Console.WriteLine("===================== Прочитанный файлик ==========================");
            var players = file.GetPlayers();
            foreach(var p in players)
            {
                Console.WriteLine($"{p.ID}({p.Notes})");
            }    
        }
    }
}
