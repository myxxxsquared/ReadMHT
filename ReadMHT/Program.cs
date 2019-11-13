using System;
using System.IO;
using System.Text.RegularExpressions;

namespace ReadMHT
{
	class Program
	{
		static void Main(string[] args)
		{
			const string filename = @"C:\Users\Admin\Desktop\sin(๑• . •๑)(945757525) .mht";
			const string foulder = @"C:\Users\Admin\Desktop\read\";
			const string spiltstring = "------";
            const string regularexpression = "^=(.*)\r\nContent-Type:(.*)\r\nContent-Transfer-Encoding:(.*)\r\nContent-Location:(.*)\r\n((?:.|\n|\r)*)";

			string[] strings = Regex.Split(File.ReadAllText(filename), spiltstring);
			foreach (var str in strings)
			{
				var match = Regex.Match(str, regularexpression);
				if (!match.Success)
					continue;

				var groups = match.Groups;

				//groups[1].Value //题头
				//groups[2].Value //类型
				//groups[3].Value //编码方式
				//groups[4].Value //文件名
				//groups[5].Value //内容

				byte[] file_to_write = Convert.FromBase64String(groups[5].Value);
				var write_stream = File.OpenWrite(foulder + groups[4].Value);
				write_stream.Write(file_to_write, 0, file_to_write.Length);
			}
		}
	}
}
