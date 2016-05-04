using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil.src.FileCopy
{
    class FileCopyUtilTest_File
    {
        #region ファイル名指定なし

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なし1()
        {
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なし2()
        {
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test2.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なし上書き確認1()
        {
            File.Create("test1.txt").Close();
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.AreEqual("test1", File.ReadAllText("test1.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なし上書き確認2()
        {
            File.Create("test2.txt").Close();
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test2.txt"));
            Assert.AreEqual("test2", File.ReadAllText("test2.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なし上書きなし確認1()
        {
            File.Create("test1.txt").Close();
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source, false);
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.AreEqual("", File.ReadAllText("test1.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なし上書きなし確認2()
        {
            File.Create("test2.txt").Close();
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source, false);
            Assert.IsTrue(File.Exists("test2.txt"));
            Assert.AreEqual("", File.ReadAllText("test2.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void コピー先指定なしファイル無し()
        {
            // 存在しないファイル
            string source = @"テストデータフォルダ\test3.txt";

            var ex = Assert.Throws<ApplicationException>(() =>
            {
                FileCopyUtil.FileCopy(source);
            });

            // 存在しないファイルをコピーしているので存在しないはず
            Assert.IsFalse(File.Exists(source));
            Assert.That(ex.Message, Is.EqualTo(@"【FileCopy】ファイル[テストデータフォルダ\test3.txt]が存在しません。"));
        }

        #endregion

        [Test, Category("FileCopyUtil")]
        public void コピー先指定あり1()
        {
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source, "test3.txt");
            Assert.IsTrue(File.Exists("test3.txt"));
        }
        [Test, Category("FileCopyUtil")]
        public void コピー先指定あり2()
        {
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source, "test4.txt");
            Assert.IsTrue(File.Exists("test4.txt"));
        }

        #region 前処理・後処理
        [TestFixtureSetUp]
        public void 全体前処理()
        {
        }

        [TestFixtureTearDown]
        public void 全体後処理()
        {
        }

        [SetUp]
        public void 個別前処理()
        {
        }

        [TearDown]
        public void 個別後処理()
        {
            // テストデータを削除
            FileDeleteUtil.DeleteDirectory();
            FileDeleteUtil.DeleteFile();
        }
        #endregion
    }
}
