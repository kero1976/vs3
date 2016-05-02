using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil
{
    /// <summary>
    /// 時間計測用ユーティリティ
    /// </summary>
    /// 使い方
    /// TimerUtils time = new TimerUtils();
    /// Console.Write(time.Stop());
    public class TimerUtil
    {
        private Stopwatch sw;

        public TimerUtil()
        {
            sw = new Stopwatch();
            sw.Start();
        }

        /// <summary>
        /// インスタンス作成時からの経過時間(秒)を取得する
        /// </summary>
        /// <returns>経過時間(秒)</returns>
        public string Stop()
        {
            sw.Stop();
            return String.Format("経過時間(秒):{0}", sw.ElapsedMilliseconds / 1000);
        }
    }
}
