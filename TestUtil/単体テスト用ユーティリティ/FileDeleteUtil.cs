using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil
{
    /// <summary>
    /// ファイル削除用ユーティリティクラス
    /// </summary>
    /// 実行時フォルダにある特定のファイル、フォルダを削除する。
    public static class FileDeleteUtil
    {
        /// <summary>
        /// 実行時フォルダ内のdll,pdb以外のファイルを削除する。
        /// </summary>
        public static void DeleteFile()
        {
            // カレントディレクトリのファイルの一覧を取得
            DirectoryInfo dir = new DirectoryInfo(".");
            foreach (var f in dir.GetFiles())
            {
                switch (f.Extension.ToUpper())
                {
                    case ".DLL":
                        break;
                    case ".PDB":
                        break;
                    default:
                        try
                        {
                            f.Delete();
                        }
                        catch (Exception e)
                        {
                            string message = string.Format("【DeleteFile】{0}が削除できませんでした。", f.Name);
                            throw new ApplicationException(message, e);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 実行時フォルダ内のディレクトリを削除する
        /// </summary>
        /// 削除できないものについては
        public static void DeleteDirectory()
        {
            var dir = new DirectoryInfo(".");

            foreach (var d in dir.EnumerateDirectories())
            {
                try
                {
                    d.Delete(true);
                }
                catch (Exception e)
                {
                    string message = string.Format("【DeleteDirectory】{0}の削除に失敗。{1}", d.Name, e.Message);
                    Console.WriteLine(message);
                }
            }
        }
    }
}
