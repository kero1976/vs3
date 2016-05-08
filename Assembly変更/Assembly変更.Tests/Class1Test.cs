using NUnit.Framework;
using System;
using System.IO;
using System.Security.AccessControl;
using TestUtil;

namespace AssemblyChange
{
    public class Class1Test
    {
        #region ***テスト
        [Test]
        public void 正常系()
        {
            // テストデータを実行時フォルダに同名でコピー
            FileCopyUtil.FileCopy(@"テストデータフォルダ\テストデータ1.txt", "AssemblyInfo.cs");
            // カレントディレクトリを指定
            AssemblyChange change = new AssemblyChange(new DirectoryInfo("."));
            change.Change("AssemblyVersion", "2.3.4.0");
            change.Change("AssemblyCompany", "kero");
            change.Write();
            FileAssert.AreEqual("AssemblyInfo.cs", @"..\..\テストデータフォルダ\テスト結果1.txt");
        }

        [Test]
        public void 異常系_キーなし()
        {
            // テストデータを実行時フォルダに同名でコピー
            FileCopyUtil.FileCopy(@"テストデータフォルダ\テストデータ1.txt", "AssemblyInfo.cs");
            // カレントディレクトリを指定
            AssemblyChange change = new AssemblyChange(new DirectoryInfo("."));
            var ex = Assert.Throws<ApplicationException>(() =>
           {
               change.Change("AssemblyVersion2", "2.3.4.0");
           });
            Console.WriteLine(ex.Message);
            StringAssert.StartsWith("シーケンスに、一致する要素は含まれてません。Key:AssemblyVersion2", ex.Message);
        }

        [Test]
        public void 異常系_キー重複()
        {
            // テストデータを実行時フォルダに同名でコピー
            FileCopyUtil.FileCopy(@"テストデータフォルダ\テストデータ2.txt", "AssemblyInfo.cs");
            // カレントディレクトリを指定
            AssemblyChange change = new AssemblyChange(new DirectoryInfo("."));
            var ex = Assert.Throws<ApplicationException>(() =>
            {
                change.Change("AssemblyVersion", "2.3.4.0");
            });
            StringAssert.StartsWith("シーケンスに複数の一致する要素が含まれています。Key:AssemblyVersion", ex.Message);
        }

        [Test]
        public void ReadConfigテスト2()
        {
            Program.ReadConfig();
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
            //FileDeleteUtil.DeleteFile();
        }
        #endregion
    }
}
