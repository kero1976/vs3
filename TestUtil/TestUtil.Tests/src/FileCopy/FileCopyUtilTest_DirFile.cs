using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil
{
    class FileCopyUtilTest_DirFile
    {

        [Test, Category("FileCopyUtil")]
        public void 正常系()
        {
            string source = @"テストデータフォルダ";

            // コピー
            FileCopyUtil.DirFileCopy(source);

            // 指定したフォルダは存在しない
            Assert.False(Directory.Exists("テストデータフォルダ"));

            // 指定したフォルダ以下のファイルが存在する
            Assert.True(File.Exists("test1.txt"));
            Assert.True(File.Exists("テストファイル.txt"));

            // 指定したフォルダ以下のフォルダが存在する
            Assert.True(Directory.Exists("サブフォルダ"));
            Assert.True(Directory.Exists("隠しフォルダ"));

            // 指定したフォルダ以下のフォルダ内のファイルが存在する
            Assert.True(File.Exists(@"サブフォルダ\test2.txt"));
        }


        [Test, Category("FileCopyUtil")]
        public void 同名コピー_フォルダなしエラー()
        {
            // 存在しないフォルダを指定
            string source = @"test";
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                FileCopyUtil.DirFileCopy(source);
            });
            // 存在しないファイルをコピーしているので存在しないはず
            Assert.IsFalse(File.Exists(source));
            Assert.That(ex.Message, Is.EqualTo(@"【DirFileCopy】コピー対象のフォルダ(test)が存在しません。"));
        }

        [Test, Category("FileCopyUtil")]
        public void 同名コピー_上書き()
        {
            string source = @"テストデータフォルダ";

            // 同名のフォルダを作成する。
            Directory.CreateDirectory("サブフォルダ");

            // コピー
            FileCopyUtil.DirFileCopy(source);

            Assert.True(Directory.Exists("サブフォルダ"));
            // 上書きなのでファイルが存在するはず
            Assert.True(File.Exists(@"サブフォルダ\test2.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void 同名コピー_上書きなし()
        {
            string source = @"テストデータフォルダ";

            // 同名のフォルダを作成する。
            Directory.CreateDirectory("サブフォルダ");

            // コピー
            FileCopyUtil.DirFileCopy(source, false);

            Assert.True(Directory.Exists("サブフォルダ"));
            // 上書きなしなのでファイルが存在しないはず
            Assert.False(File.Exists(@"サブフォルダ2\test2.txt"));
        }

        [Test, Category("FileCopyUtil")]
        public void 同名コピー_上書きエラー()
        {
            string source = @"テストデータフォルダ";

            // 同名のフォルダを作成し、削除できないようにする
            Directory.CreateDirectory("サブフォルダ");
            var fs = File.Create(@"サブフォルダ\フォルダ削除失敗用.txt");
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                FileCopyUtil.DirFileCopy(source);
            });
            // 上書きに失敗しているため、ファイルは存在しないはず。
            Assert.IsFalse(File.Exists("サブフォルダ\test2.txt"));
            // 【DirFileCopy】ではなく内部で呼び出している【DirectoryCopy】になる
            StringAssert.StartsWith("【DirectoryCopy】", ex.Message);
            StringAssert.Contains("サブフォルダ", ex.Message);
            StringAssert.EndsWith("の削除に失敗しました。", ex.Message);
            // 後処理
            fs.Close();
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
