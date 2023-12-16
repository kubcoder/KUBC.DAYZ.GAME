using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KUBC.DAYZ.GAME.MissionFiles.DB.Types;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class TypesXML
    {
        [TestMethod]
        public void TestWriteXML()
        {
            var testItem = new Item()
            {
                Name = "testName",
                Tags =
                [
                    new Tag { Name = "tag1" },
                    new Tag { Name = "tag2" },
                    new Tag { Name = "tag3" },
                ],
                Flags = new()
            };
            var tFile = new GAME.MissionFiles.DB.Types.File();
            tFile.Add(testItem);
            Console.WriteLine(tFile.GetXML());
        }

        [TestMethod]
        public void TestReadXML() 
        {
            var file = new FileInfo("TestFiles\\MissionFiles\\types.xml");
            var types = GAME.MissionFiles.DB.Types.File.Load(file.FullName);
        }
        
        [TestMethod]
        public void TestTagModify()
        {
            var testItem = new Item()
            {
                Tags =
                [
                    new Tag { Name = "tag1" },
                    new Tag { Name = "tag2" },
                    new Tag { Name = "tag3" },
                ]
            };
            List<string> changes = ["tag1", "tag2", "tag3"];
            testItem.UpdateTags(changes);
            Assert.IsFalse(testItem.IsModified);
            List<string> changes2 = ["tag1", "tag2", "tag4"];
            testItem.UpdateTags(changes2);
            Assert.IsTrue(testItem.IsModified);
            testItem.IsModified = false;
            testItem.UpdateTags(new List<string>());
            Assert.IsTrue(testItem.IsModified);
            testItem.IsModified = false;
            List<string> changes3 = ["tag6", "tag7", "tag8"];
            testItem.UpdateTags(changes3);
            Assert.IsTrue(testItem.IsModified);
        }
    }
}
