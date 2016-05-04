using NUnit.Framework;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;

namespace TestUtil
{
    class SecurityUtilTest
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
                SecurityUtil.Read(new FileInfo("test1.txt"), AccessControlType.Allow);
            });
            // ファイルが存在しない場合にApplicationExceptionが発ししたらOK
            StringAssert.StartsWith("【Read(File)】ファイル", ex.Message);
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
                SecurityUtil.Read(new FileInfo("test.txt"), AccessControlType.Allow);
            });
            StringAssert.StartsWith("【Read(File)】", ex.Message);
            StringAssert.Contains("test.txt", ex.Message);
            StringAssert.EndsWith("の設定で例外が発生しました。", ex.Message);
        }

        [Test]
        [Category("ファイル")]
        public void ファイル読込禁止()
        {
            FileCopyUtil.FileCopy(@"テストデータフォルダ\test1.txt");
            SecurityUtil.Read(new FileInfo("test1.txt"), AccessControlType.Deny);

            var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            {
                File.Open("test1.txt", FileMode.Open).Close();
            });
            // ファイルオープンでUnauthorizedAccessExceptionが発生したらOK
            StringAssert.Contains("test1.txt", ex.Message);
        }

        [Test]
        [Category("ファイル")]
        public void ファイル読込許可()
        {
            FileCopyUtil.FileCopy(@"テストデータフォルダ\test1.txt");
            SecurityUtil.Read(new FileInfo("test1.txt"), AccessControlType.Allow);

            File.Open("test1.txt", FileMode.Open).Close();
            // ファイルオープンでUnauthorizedAccessExceptionが発生しなければOK
            Assert.Pass();
        }
        #endregion

        #region フォルダ読込

        [Test]
        [Category("フォルダ")]
        public void フォルダ読込失敗()
        {
            // フォルダコピーを行わない
            // FileCopyUtil.DirectoryCopy(@"テストデータフォルダ");

            var ex = Assert.Throws<ApplicationException>(() =>
            {
                SecurityUtil.Read(new DirectoryInfo("テストデータフォルダ"), AccessControlType.Allow);
            });
            // フォルダが存在しない場合にApplicationExceptionが発生したらOK
            StringAssert.StartsWith("フォルダ", ex.Message);
            StringAssert.Contains("テストデータフォルダ", ex.Message);
            StringAssert.EndsWith("が存在しません。", ex.Message);
        }

        /// <summary>
        /// フォルダの権限付与できない場合のテスト
        /// </summary>
        /// 準備：実行ディレクトリに「test」フォルダを作成し、フルコントロールを拒否にして、所有者を変更する。
        [Test, Explicit]
        [Category("フォルダ")]
        public void フォルダ権限設定失敗()
        {
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                SecurityUtil.Read(new DirectoryInfo("test"), AccessControlType.Allow);
            });
            StringAssert.StartsWith("【Read(Dir)】", ex.Message);
            StringAssert.Contains("test", ex.Message);
            StringAssert.EndsWith("の設定で例外が発生しました。", ex.Message);
        }

        [Test]
        [Category("フォルダ")]
        public void フォルダ読込禁止()
        {
            FileCopyUtil.DirectoryCopy(@"テストデータフォルダ");
            SecurityUtil.Read(new DirectoryInfo("テストデータフォルダ"), AccessControlType.Deny);

            var ex = Assert.Throws<UnauthorizedAccessException>(() =>
            {
                Directory.GetFiles("テストデータフォルダ");
            });
            // 権限が無いままだと削除できないため、権限を付与
            SecurityUtil.Read(new DirectoryInfo("テストデータフォルダ"), AccessControlType.Allow);
            // フォルダ内のファイル列挙でUnauthorizedAccessExceptionが発生したらOK
            StringAssert.Contains("テストデータフォルダ", ex.Message);
        }

        [Test]
        [Category("フォルダ")]
        public void フォルダ読込許可()
        {
            FileCopyUtil.DirectoryCopy(@"テストデータフォルダ");
            SecurityUtil.Read(new DirectoryInfo("テストデータフォルダ"), AccessControlType.Allow);
   
            Directory.GetFiles("テストデータフォルダ");
            // フォルダ内のファイル列挙でUnauthorizedAccessExceptionが発生しなければOK

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

