using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 単体テスト用ユーティリティ;

namespace 単体テスト用ユーティリティTests
{
    [TestClass]
    public class TimeUtilTest
    {
        [TestMethod]
        public void 時間計測テスト()
        {
            TimerUtil time = new TimerUtil();
            Thread.Sleep(3000);
            string actual = time.Stop();
            Assert.AreEqual("経過時間(秒):3", actual);
        }
    }
}
