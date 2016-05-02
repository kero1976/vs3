using NUnit.Framework;
using System;
using System.IO;
using 単体テスト用ユーティリティ;


namespace TestUtil
{
    public class FileCopyUtilTest
    {
        #region ファイル名指定なし

        [Test]
        public void コピー先指定なし1()
        {
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
        }

        [Test]
        public void コピー先指定なし2()
        {
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test2.txt"));
        }

        [Test]
        public void コピー先指定なし上書き確認1()
        {
            File.Create("test1.txt").Close();
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.AreEqual("test1", File.ReadAllText("test1.txt"));
        }

        [Test]
        public void コピー先指定なし上書き確認2()
        {
            File.Create("test2.txt").Close();
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test2.txt"));
            Assert.AreEqual("test2", File.ReadAllText("test2.txt"));
        }

        [Test]
        public void コピー先指定なし上書きなし確認1()
        {
            File.Create("test1.txt").Close();
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source, false);
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.AreEqual("", File.ReadAllText("test1.txt"));
        }

        [Test]
        public void コピー先指定なし上書きなし確認2()
        {
            File.Create("test2.txt").Close();
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source, false);
            Assert.IsTrue(File.Exists("test2.txt"));
            Assert.AreEqual("", File.ReadAllText("test2.txt"));
        }

        [Test]
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
            Assert.That(ex.Message, Is.EqualTo(@"ファイル[テストデータフォルダ\test3.txt]が存在しません。"));
        }

        #endregion

        [Test]
        public void コピー先指定あり1()
        {
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source, "test3.txt");
            Assert.IsTrue(File.Exists("test3.txt"));
        }
        [Test]
        public void コピー先指定あり2()
        {
            string source = @"テストデータフォルダ\サブフォルダ\test2.txt";
            FileCopyUtil.FileCopy(source, "test4.txt");
            Assert.IsTrue(File.Exists("test4.txt"));
        }

        [Test]
        public void フォルダコピー1()
        {
            string source = @"テストデータフォルダ";
            FileCopyUtil.DirectoryCopy(source);
            Assert.IsTrue(true);
        }


        [Test]
        public void フォルダコピー名前指定()
        {
            string source = @"テストデータフォルダ";
            FileCopyUtil.DirectoryCopy(source, "テストデータ２");
            Assert.IsTrue(true);
        }
    }
}
