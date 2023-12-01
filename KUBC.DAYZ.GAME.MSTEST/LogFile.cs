using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUBC.DAYZ.GAME.MSTEST
{
    [TestClass]
    public class LogFile
    {
        public static System.IO.FileInfo TestFile
        {
            get
            {
                return new FileInfo("TestFiles\\TextFile1.txt");
            }
        }

        [TestMethod]
        public void TestReadEndLine()
        {
            var fileReader = new StreamReader(TestFile.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            while (!fileReader.EndOfStream)
            {
                char b = (char)fileReader.Read();                
            }
        }
    }
}
