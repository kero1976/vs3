using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil.src
{
    class DebugUtilTest
    {
        [Test]
        public void 目視確認()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(100);
                Console.WriteLine(DebugUtil.DebugString("テスト" + i));
            }
            //Parallel.For(0, 10, (i) =>
            //  {
            //      System.Threading.Thread.Sleep(100);
            //      Console.WriteLine(DebugUtil.DebugString("テスト" + i));
            //  });

            DebugUtil.DebugPrint("コンソール出力テスト");
        }
    }
}
