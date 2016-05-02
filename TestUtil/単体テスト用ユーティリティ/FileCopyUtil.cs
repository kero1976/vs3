using System;
using System.IO;

namespace TestUtil
{
    /// <summary>
    /// ファイルコピー用ユーティリティクラス
    /// </summary>
    /// プロジェクト内のテスト用ファイル、フォルダ類を実行時フォルダにコピーする。
    /// 実行時フォルダは「bin\Debug」or「bin\Release」を想定している。
    public static class FileCopyUtil
    {
        /// <summary>
        /// 実行時フォルダからプロジェクトフォルダへのパスを指定
        /// </summary>
        private static string base_dir = @"..\..\";

        /// <summary>
        /// 実行時フォルダからプロジェクトフォルダへのパスを指定
        /// </summary>
        public static string BASE_DIR
        {
            get { return base_dir; }
            set { base_dir = value; }
        }

        /// <summary>
        /// コピー先ディレクトリをカレント(実行時)ディレクトリに別名でコピーする。
        /// </summary>
        /// 上書きフラグがtrueの場合(指定無しはtrue)の場合は既存ディレクトリがあれば削除する。
        /// 上書きフラグがfalseの場合は、既存ディレクトリがあれば何もしない。
        /// <param name="sourceDirectory">コピー元ディレクトリ</param>
        /// <param name="destDirectory">コピー先ディレクトリ</param>
        /// <param name="overrideFlg">上書きフラグ(初期値:true)</param>
        public static void DirectoryCopy(string sourceDirectory, string destDirectory, bool overrideFlg = true)
        {
            var dir = new DirectoryInfo(Path.Combine(BASE_DIR, sourceDirectory));
            if (!dir.Exists)
            {
                string message = string.Format("コピー対象のフォルダ({0})が存在しません。", sourceDirectory);
                throw new ApplicationException(message);
            }
            if (Directory.Exists(destDirectory))
            {
                if (overrideFlg)
                {
                    try
                    {
                        Directory.Delete(destDirectory, true);
                    }
                    catch (Exception e)
                    {
                        string message = string.Format("フォルダ({0})のコピーで上書きに失敗しました。", sourceDirectory);
                        throw new ApplicationException(message, e);
                    }
                }
                else
                {
                    return;
                }
            }
            CopyDirectory(dir.FullName, destDirectory);
        }

        /// <summary>
        /// コピー先ディレクトリをカレント(実行時)ディレクトリに同名でコピーする。
        /// </summary>
        /// 上書きフラグがtrueの場合(指定無しはtrue)の場合は既存ディレクトリがあれば削除する。
        /// 上書きフラグがfalseの場合は、既存ディレクトリがあれば何もしない。
        /// <param name="sourceDirectory">コピー元ディレクトリ</param>
        /// <param name="overrideFlg">上書きフラグ(初期値:true)</param>
        public static void DirectoryCopy(string sourceDirectory, bool overrideFlg = true)
        {
            var dir = new DirectoryInfo(Path.Combine(BASE_DIR, sourceDirectory));
            if (!dir.Exists)
            {
                string message = string.Format("コピー対象のフォルダ({0})が存在しません。", sourceDirectory);
                throw new ApplicationException(message);
            }
            if (Directory.Exists(dir.Name))
            {
                if (overrideFlg)
                {
                    try
                    {
                        Directory.Delete(dir.Name, true);
                    }
                    catch (Exception e)
                    {
                        string message = string.Format("フォルダ({0})のコピーで上書きに失敗しました。", sourceDirectory);
                        throw new ApplicationException(message, e);
                    }
                }
                else
                {
                    return;
                }
            }
            CopyDirectory(dir.FullName, dir.Name);
        }

        /// <summary>
        /// コピー元ファイルをカレント(実行時)ディレクトリに同名でコピーする。
        /// </summary>
        /// 上書きフラグがtrueの場合(指定無しはtrue)の場合は既存ファイルがあれば削除する。
        /// 上書きフラグがfalseの場合は、既存ファイルがあれば何もしない。
        /// <param name="sourceFile">コピー元ファイル</param>
        /// <param name="overrideFlg">上書きフラグ(初期値:true)</param>
        public static void FileCopy(string sourceFile, bool overrideFlg = true)
        {
            FileInfo file = new FileInfo(sourceFile);
            FileCopy(sourceFile, file.Name, overrideFlg);
        }

        /// <summary>
        /// コピー元ファイルをカレント(実行時)ディレクトリに指定名称でコピーする。
        /// </summary>
        /// 上書きフラグがtrueの場合(指定無しはtrue)の場合は既存ファイルがあれば削除する。
        /// 上書きフラグがfalseの場合は、既存ファイルがあれば何もしない。
        /// <param name="sourceFile">コピー元ファイル</param>
        /// <param name="destFile">コピー先ファイル</param>
        /// <param name="overrideFlg">上書きフラグ(初期値:true)</param>
        public static void FileCopy(string sourceFile, string destFile, bool overrideFlg = true)
        {
            string sourceFilePath = Path.Combine(BASE_DIR, sourceFile);
            var file = new FileInfo(sourceFilePath);

            // コピー元ファイルが存在しない場合はエラー
            if (!file.Exists)
            {
                string message = string.Format("ファイル[{0}]が存在しません。", sourceFile);
                throw new ApplicationException(message);
            }
            // カレントディレクトリに既にファイルが存在するか？
            if (File.Exists(destFile))
            {
                if (overrideFlg)
                {
                    // ファイルが存在していて上書きモードの場合は既存ファイルを削除
                    File.Delete(destFile);
                }
                else
                {
                    // ファイルが存在していて、上書きモードでない場合は何もしない
                    return;
                }
            }

            File.Copy(sourceFilePath, destFile);
        }

        #region Privateメソッド

        /// <summary>
        /// ディレクトリをコピーする
        /// </summary>
        /// <param name="sourceDirName">コピーするディレクトリ</param>
        /// <param name="destDirName">コピー先のディレクトリ</param>
        private static void CopyDirectory(
            string sourceDirName, string destDirName)
        {
            var srcDir = new DirectoryInfo(sourceDirName);
            var destDir = new DirectoryInfo(destDirName);

            // コピー先のディレクトリがないときは作る
            if (!destDir.Exists)
            {
                destDir.Create();
                // 属性もコピー
                destDir.Attributes = srcDir.Attributes;
            }

            // コピー元のディレクトリにあるファイルをコピー
            foreach (var file in srcDir.EnumerateFiles())
            {
                file.CopyTo(Path.Combine(destDir.FullName, file.Name));
            }

            // コピー元のディレクトリにあるディレクトリについて、再帰的に呼び出す
            foreach (var dir in srcDir.EnumerateDirectories())
            {
                CopyDirectory(dir.FullName, Path.Combine(destDir.FullName, dir.Name));
            }
        }
        #endregion
    }
}
