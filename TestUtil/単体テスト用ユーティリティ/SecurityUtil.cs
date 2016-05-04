using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace TestUtil
{
    /// <summary>
    /// ファイル・フォルダアクセス権限用ユーティリティクラス
    /// </summary>
    public static class SecurityUtil
    {
        /// <summary>
        /// アクセス権を設定するユーザー。
        /// </summary>
        /// 実行時ユーザーを指定
        private static string user = Environment.UserDomainName + "\\" + Environment.UserName;

        /// <summary>
        /// Read権限の設定
        /// </summary>
        /// <param name="file">指定するファイル</param>
        /// <param name="type">許可/拒否</param>
        public static void Read(FileInfo file, AccessControlType type)
        {
            if (!file.Exists)
            {
                string message = string.Format("【SecurityRead(File)】ファイル({0})が存在しません。", file.FullName);
                throw new ApplicationException(message);
            }

            // セキュリティ設定。Readの拒否を付与、削除で権限を設定する。
            FileSecurity security;
            try
            {
                security = file.GetAccessControl();
            }
            catch (Exception e)
            {
                string message = string.Format("【SecurityRead(File)】ファイル({0})の設定で例外が発生しました。", file.FullName);
                throw new ApplicationException(message, e);
            }
            
            var rule = new FileSystemAccessRule(
                user, FileSystemRights.Read, AccessControlType.Deny);

            switch (type)
            {
                case AccessControlType.Allow:
                    // 読取OKにするので、拒否するルールを削除
                    security.RemoveAccessRule(rule);
                    break;
                case AccessControlType.Deny:
                    // 読取NGにするので、拒否するルールを付与
                    security.AddAccessRule(rule);
                    break;
            }
            // 変更したファイルセキュリティをファイルに設定
            file.SetAccessControl(security);
        }

        /// <summary>
        /// Read権限の設定
        /// </summary>
        /// <param name="dir">指定するフォルダ</param>
        /// <param name="type">許可/拒否</param>
        public static void Read(DirectoryInfo dir, AccessControlType type)
        {
            if (!dir.Exists)
            {
                string message = string.Format("【SecurityRead(Dir)】フォルダ({0})が存在しません。", dir.FullName);
                throw new ApplicationException(message);
            }

            // セキュリティ設定。Readの拒否を付与、削除で権限を設定する。
            DirectorySecurity security;
            try
            {
                security = dir.GetAccessControl();
            }
            catch (Exception e)
            {
                string message = string.Format("【SecurityRead(Dir)】フォルダ({0})の設定で例外が発生しました。", dir.Name);
                throw new ApplicationException(message, e);
            }
            var rule = new FileSystemAccessRule(
                user, 
                FileSystemRights.Read,
                InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                PropagationFlags.None,
                AccessControlType.Deny);
            switch (type)
            {
                case AccessControlType.Allow:
                    // 読取OKにするので、拒否するルールを削除
                    security.RemoveAccessRule(rule);
                    break;
                case AccessControlType.Deny:
                    // 読取NGにするので、拒否するルールを付与
                    security.AddAccessRule(rule);
                    break;
            }
            // 変更したファイルセキュリティをファイルに設定
            dir.SetAccessControl(security);
        }


        /// <summary>
        /// Delete権限の設定
        /// </summary>
        /// ファイルに対して「削除禁止」をしても、フォルダに"サブフォルダーとファイルの削除"が許可されている場合は
        /// 削除できてしまう。そのため、フォルダに対して、"サブフォルダーとファイルの削除"の権限を外す必要がある。
        /// <param name="file">指定するファイル</param>
        /// <param name="type">許可/拒否</param>
        public static void Delete(FileInfo file, AccessControlType type)
        {
            if (!file.Exists)
            {
                string message = string.Format("【SecurityDelete(File)】ファイル({0})が存在しません。", file.FullName);
                throw new ApplicationException(message);
            }

            // セキュリティ設定。Readの拒否を付与、削除で権限を設定する。
            FileSecurity securityFile;
            try
            {
                securityFile = file.GetAccessControl();
            }
            catch (Exception e)
            {
                string message = string.Format("【SecurityDelete(File)】ファイル({0})の設定で例外が発生しました。", file.FullName);
                throw new ApplicationException(message, e);
            }
            DirectorySecurity securityDir;
            try
            {
                securityDir = file.Directory.GetAccessControl();
            }
            catch (Exception e)
            {
                string message = string.Format("【SecurityDelete(File)】フォルダ({0})の設定で例外が発生しました。", file.DirectoryName);
                throw new ApplicationException(message, e);
            }

            var ruleFile = new FileSystemAccessRule(
                user, FileSystemRights.Delete, AccessControlType.Deny);

            var ruleDir = new FileSystemAccessRule(
                user, FileSystemRights.DeleteSubdirectoriesAndFiles, AccessControlType.Deny);
            switch (type)
            {
                case AccessControlType.Allow:
                    // 削除OKにするので、拒否するルールを削除
                    securityFile.RemoveAccessRule(ruleFile);
                    securityDir.RemoveAccessRule(ruleDir);
                    break;
                case AccessControlType.Deny:
                    // 削除NGにするので、拒否するルールを付与
                    securityFile.AddAccessRule(ruleFile);
                    securityDir.AddAccessRule(ruleDir);
                    break;
            }
            // 変更したファイルセキュリティをファイルに設定
            file.SetAccessControl(securityFile);
            file.Directory.SetAccessControl(securityDir);
        }
    }
}
