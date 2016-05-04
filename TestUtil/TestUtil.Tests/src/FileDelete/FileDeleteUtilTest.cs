using NUnit.Framework;
using System;
using System.IO;

namespace TestUtil
{
    public class FileDeleteUtilTest
    {
        [Test]
        public void ファイル削除()
        {
            string source = @"テストデータフォルダ\test1.txt";
            FileCopyUtil.FileCopy(source);
            Assert.IsTrue(File.Exists("test1.txt"));
            FileDeleteUtil.DeleteFile();
            Assert.IsFalse(File.Exists("test1.txt"));
        }
        [Test]
        public void ファイル削除できない()
        {
            var fs = File.Create("test1.txt");

            var ex = Assert.Throws<ApplicationException>(() =>
            {
                FileDeleteUtil.DeleteFile();
            });

            // ロックしているので削除できないため存在する
            Assert.IsTrue(File.Exists("test1.txt"));
            Assert.That(ex.Message, Is.EqualTo("【DeleteFile】test1.txtが削除できませんでした。"));
            fs.Close();
        }

        [Test]
        public void ディレクトリ削除()
        {
            Directory.CreateDirectory("テスト");
            FileDeleteUtil.DeleteDirectory();
            Assert.IsFalse(Directory.Exists("テスト"));
        }
    }
}
