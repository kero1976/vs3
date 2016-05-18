using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil
{
    /// <summary>
    /// デバッグ支援用ユーティリティクラス
    /// </summary>
    public static class DebugUtil
    {
        /// <summary>
        /// デバッグ用文字列に成形する。
        /// </summary>
        /// 【HH:MI:SS.sss】【PID:プロセスID】【SID:スレッドID】デバッグ用文字列
        /// <param name="msg">デバッグ用文字列</param>
        /// <returns>デバッグ用文字列</returns>
        public static string DebugString(string msg)
        {
            return string.Format("【{0}.{1:000}】【PID:{2}】【SID:{3:00}】{4}",
                System.DateTime.Now.ToLongTimeString(),
                System.DateTime.Now.Millisecond,
                System.Diagnostics.Process.GetCurrentProcess().Id,
                System.Threading.Thread.CurrentThread.ManagedThreadId,
                msg);
        }

        /// <summary>
        /// デバッグ用文字列をコンソールに出力する。
        /// </summary>
        /// <param name="msg">デバッグ用文字列</param>
        public static void DebugPrint(string msg)
        {
            Console.WriteLine(DebugString(msg));
        }
    }
}
