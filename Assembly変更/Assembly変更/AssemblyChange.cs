using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyChange
{
    /// <summary>
    /// 指定したフォルダの「AssemblyInfo.cs」ファイルの内容を変更する。
    /// </summary>
    public class AssemblyChange
    {
        /// <summary>
        /// 対象の「AssemblyInfo.cs」
        /// </summary>
        private FileInfo file;

        private List<string> datas;

        public AssemblyChange(DirectoryInfo dir)
        {
            file = new FileInfo(Path.Combine(dir.FullName, "AssemblyInfo.cs"));
            datas = File.ReadAllLines(file.FullName).ToList();
        }

        public void Change(string key, string value)
        {
            // 対象データを探す、なければ例外とする。
            string oldStr = null;
            try
            {
                oldStr = datas.Single(data => data.StartsWith("[assembly: " + key));
            }
            catch (Exception e)
            {
                string message = string.Format("{0}。Key:{1}", e.Message, key);
                throw new ApplicationException(message, e);
            }

            // 変更後のデータを作成
            string newStr = string.Format("[assembly: {0}(\"{1}\")]", key, value);
            // 置換。oldStrができているので、必ずデータはあるはず。
            for(int i = 0; i < datas.Count; i++)
            {
                if (datas[i].Equals(oldStr))
                {
                    datas[i] = newStr;
                }
            }
        }

        public void Write()
        {
            try
            {
                File.WriteAllLines(file.FullName, datas);
            }catch(Exception e){
                throw new ApplicationException("Write()で例外が発生しました。", e);
            }
            
        }
    }
}
