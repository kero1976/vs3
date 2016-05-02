using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using 単体テスト用ユーティリティ;
using System.IO;

namespace 単体テスト用ユーティリティTests
{
    [TestClass]
    public class FileCopyUtilTest
    {
        #region ファイル名指定なし

        [TestMethod]
        public void コピー先指定なし1()
        {
            string source = @"テストデータ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
        }

        [TestMethod]
        public void コピー先指定なし2()
        {
            string source = @"テストデータ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test2.txt"));
        }

        [TestMethod]
        public void コピー先指定なし上書き確認1()
        {
            File.Create("test1.txt").Close();
            string source = @"テストデータ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.AreEqual("test1", File.ReadAllText("test1.txt"));
        }

        [TestMethod]
        public void コピー先指定なし上書き確認2()
        {
            File.Create("test2.txt").Close();
            string source = @"テストデータ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test2.txt"));
            Assert.AreEqual("test2", File.ReadAllText("test2.txt"));
        }

        [TestMethod]
        public void コピー先指定なし上書きなし確認1()
        {
            File.Create("test1.txt").Close();
            string source = @"テストデータ\test1.txt";
            FileCopyUtil.FileCopy(source, false);
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.AreEqual("", File.ReadAllText("test1.txt"));
        }

        [TestMethod]
        public void コピー先指定なし上書きなし確認2()
        {
            File.Create("test2.txt").Close();
            string source = @"テストデータ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source, false);
            Assert.IsTrue(File.Exists("test2.txt"));
            Assert.AreEqual("", File.ReadAllText("test2.txt"));
        }

        [TestMethod]
        [ExpectedExceptionAttribute(typeof(ApplicationException))]
        public void コピー先指定なしファイル無し()
        {

            string source = @"テストデータ\test3.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test3.txt"));
        }

        #endregion

        [TestMethod]
        public void コピー先指定あり1()
        {
            string source = @"テストデータ\test1.txt";
            FileCopyUtil.FileCopy(source, "test3.txt");
            Assert.IsTrue(File.Exists("test3.txt"));
        }

        [TestMethod]
        public void コピー先指定あり2()
        {
            string source = @"テストデータ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source, "test4.txt");
            Assert.IsTrue(File.Exists("test4.txt"));
        }

        [TestMethod]
        public void フォルダコピー1()
        {
            string source = @"テストデータ";
            FileCopyUtil.DirectoryCopy(source);
            Assert.IsTrue(true);
        }


        [TestMethod]
        public void フォルダコピー名前指定()
        {
            string source = @"テストデータ";
            FileCopyUtil.DirectoryCopy(source, "テストデータ２");
            Assert.IsTrue(true);
        }
    }
}
