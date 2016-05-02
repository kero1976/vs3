using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 単体テスト用ユーティリティ;

namespace 単体テスト用ユーティリティTests
{
    [TestClass]
    public class FileDeleteUtilTest
    {
        [TestMethod]
        public void ファイル削除()
        {
            string source = @"テストデータ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
            FileDeleteUtil.DeleteFile();
            Assert.IsFalse(File.Exists("test1.txt"));
        }

        [ExpectedExceptionAttribute(typeof(ApplicationException))]
        [TestMethod]
        public void ファイル削除できない()
        {
            File.Create("test1.txt");
            FileDeleteUtil.DeleteFile();
            Assert.IsFalse(File.Exists("test1.txt"));
        }

        [TestMethod]
        public void ディレクトリ削除()
        {
            Directory.CreateDirectory("test");
            FileDeleteUtil.DeleteDirectory();
            Assert.IsFalse(Directory.Exists("test"));
        }
    }
}
