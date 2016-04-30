using System;
using System.Threading;

namespace ExitCodeInspect
{
    /// <summary>
    /// 指定した終了コードを指定した時間(秒)後に返す。
    /// </summary>
    /// 使い方：(EXE) (終了コード) (時間_秒) (0:Environment.Exit,1:Environment.ExitCode,2:return)
    class Program
    {
        static void Main(string[] args)
        {
            // 引数が2つ以外の場合は使い方を表示し、終了
            if (args.Length != 3)
            {
                Console.WriteLine("使い方：第一引数は終了コード。第二引数は指定時間(秒)。第三引数はタイプ。0:Exitメソッド、1:ExitCodeプロパティ");
                return;
            }
            int exitCode, second, type;

            if(!int.TryParse(args[0], out exitCode))
            {
                // 解析できない場合はデフォルトとして0をセットする。
                exitCode = 0;
            }

            if (!int.TryParse(args[1], out second))
            {
                // 解析できない場合はデフォルトとして0をセットする。
                second = 0;
            }

            if (!int.TryParse(args[2], out type))
            {
                // 解析できない場合はデフォルトとして0をセットする。
                type = 0;
            }

            // 指定秒数待機する
            Thread.Sleep(second * 1000);

            switch (type)
            {
                case 0:
                    Environment.Exit(exitCode);
                    break;
                case 1:
                    Environment.ExitCode = exitCode;
                    break;
            }
            return;
        }
    }
}
