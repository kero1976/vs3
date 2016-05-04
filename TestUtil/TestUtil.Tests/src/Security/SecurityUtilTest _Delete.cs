using NUnit.Framework;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;

namespace TestUtil
{
    class SecurityUtilTest_Delete
    {
        #region ファイル読込
        [Test]
        [Category("ファイル")]
        public void ファイル読込失敗()
        {
            // ファイルコピーを行わない
            // FileCopyUtil.FileCopy(@"テストデータフォルダ\test1.txt");

            var ex = Assert.Throws<ApplicationException>(() =>
            {
                SecurityUtil.Delete(new FileInfo("test1.txt"), AccessControlType.Allow);
            });
            // ファイルが存在しない場合にApplicationExceptionが発ししたらOK
            StringAssert.StartsWith("【SecurityDelete(File)】ファイル", ex.Message);
            StringAssert.Contains("test1.txt", ex.Message);
            StringAssert.EndsWith("が存在しません。", ex.Message);
        }

        /// <summary>
        /// ファイルの権限付与できない場合のテスト
        /// </summary>
        /// 準備：実行ディレクトリに「test.txt」ファイルを作成し、フルコントロールを拒否にして、所有者を変更する。
        [Test, Explicit]
        [Category("ファイル")]
        public void ファイル権限設定失敗()
        {
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                SecurityUtil.Delete(new FileInfo("test.txt"), AccessControlType.Allow);
            });
            StringAssert.StartsWith("【SecurityDelete(File)】", ex.Message);
            StringAssert.Contains("test.txt", ex.Message);
            StringAssert.EndsWith("の設定で例外が発生しました。", ex.Message);
        }

        /// <summary>
        /// ファイル削除禁止のテスト。
        /// </summary>
        [Test]
        [Category("ファイル")]
        public void ファイル削除禁止()
        {
            FileCopyUtil.FileCopy(@"テストデータフォルダ\test1.txt");
            SecurityUtil.Delete(new FileInfo("test1.txt"), AccessControlType.Deny);

            var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            {
                File.Delete("test1.txt");
            });
            // ファイル削除でUnauthorizedAccessExceptionが発生したらOK
            StringAssert.Contains("test1.txt", ex.Message);

            SecurityUtil.Delete(new FileInfo("test1.txt"), AccessControlType.Allow);
        }

        [Test]
        [Category("ファイル")]
        public void ファイル読込許可()
        {
            FileCopyUtil.FileCopy(@"テストデータフォルダ\test1.txt");
            SecurityUtil.Delete(new FileInfo("test1.txt"), AccessControlType.Allow);

            File.Open("test1.txt", FileMode.Open).Close();
            // ファイルオープンでUnauthorizedAccessExceptionが発生しなければOK
            Assert.Pass();
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
            FileDeleteUtil.DeleteDirectory();
            FileDeleteUtil.DeleteFile();
        }
        #endregion
    }
}

