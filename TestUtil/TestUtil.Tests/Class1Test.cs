using NUnit.Framework;
using System.IO;
using 単体テスト用ユーティリティ;

namespace TestUtil
{
    public class Class1Test
    {
        #region ***テスト
        [Test]
        public void test1()
        {
            // テストデータを実行時フォルダに同名でコピー
            FileCopyUtil.FileCopy(@"テストデータフォルダ\テストファイル.txt");
            string actual = File.ReadAllText("テストファイル.txt");
            string expected = "1";
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void test2()
        {

        }

        #endregion

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
            FileDeleteUtil.DeleteFile();
        }
        #endregion
    }
}
