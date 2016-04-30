using NUnit.Framework;
using System.Diagnostics;
using System.IO;
using 単体テスト用ユーティリティ;

namespace ExitCodeInspect
{
    public class Class1Test
    {
        #region ***テスト

        [TestCase("10 3 0", 10)]
        [TestCase("11 3 1", 11)]
        [TestCase("0 3 1", 0)]
        [TestCase("0 0 0", 0)]
        public void test(string args, int exitCode)
        {
            Process proc = new Process();

            proc.StartInfo.FileName = "ExitCodeInspect.exe";
            proc.StartInfo.Arguments = args;
            proc.Start();
            proc.WaitForExit();

            Assert.That(proc.ExitCode, Is.EqualTo(exitCode));
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
