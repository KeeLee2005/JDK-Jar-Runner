using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace JarRunner
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 指定的JDK路径
            string jdkPath = @"D:\AppData\local\Programs\GraalvmJDK\JDK24\bin\java.exe";

            // 检查参数
            if (args.Length == 0)
            {
                MessageBox.Show("请选择要运行的JAR文件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string jarPath = args[0];

            // 验证文件是否为JAR文件
            if (!jarPath.ToLower().EndsWith(".jar") || !File.Exists(jarPath))
            {
                MessageBox.Show("所选文件不是有效的JAR文件。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 验证JDK是否存在
            if (!File.Exists(jdkPath))
            {
                MessageBox.Show("找不到Java虚拟机。请检查JDK安装。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 设置进程启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = jdkPath,
                    Arguments = $"-jar \"{jarPath}\"",
                    WorkingDirectory = Path.GetDirectoryName(jarPath),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // 启动进程
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动JAR文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}