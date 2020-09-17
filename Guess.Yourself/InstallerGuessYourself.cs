using Guess.Yourself.Properties;
using IWshRuntimeLibrary;
using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using v = System.IO;

namespace Guess.Yourself
{
    [RunInstaller(true)]
    public partial class УстановитьУгадайСебяLite : Installer
    {
        public УстановитьУгадайСебяLite()
        {
            InitializeComponent();
        }
        
        private void InstallGuessYourself_BeforeInstall(object sender, InstallEventArgs e)
        {
            v.File.WriteAllBytes($"{Environment.GetEnvironmentVariable("TEMP")}\\vcredist_x86.exe", Resources.vcredist_x86);
            v.File.WriteAllBytes($"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86))}\\Votum\\Угадай себя - Lite\\vcredist_x86.exe", Resources.vcredist_x86);
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo($"{Environment.GetEnvironmentVariable("TEMP")}\\vcredist_x86.exe");
            proc.Start();
        }

        public void CreateShortcut(string shortcutPath, string targetPath, string workingPath, string iconPath)
        {
            WshShell wshShell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)wshShell.CreateShortcut(shortcutPath);
            shortcut.Description = "Угадай Себя - Lite";
            shortcut.IconLocation = iconPath;
            shortcut.WorkingDirectory = workingPath;
            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }

        private void УстановитьУгадайСебяLite_AfterInstall(object sender, InstallEventArgs e)
        {
            string shortcutPathForDesktop = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Угадай Себя - Lite.lnk";
            string shortcutPathForCommon = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu)}\\Угадай Себя - Lite.lnk";
            string targetPath = $"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86))}\\Votum\\Угадай себя - Lite\\Угадай себя - Lite.exe";
            string workingPath = $"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86))}\\Votum\\Угадай себя - Lite";
            string iconPath = $"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86))}\\Votum\\Угадай себя - Lite\\512x512bb.ico";

            CreateShortcut(shortcutPathForDesktop, targetPath, workingPath, iconPath);
            CreateShortcut(shortcutPathForCommon, targetPath, workingPath, iconPath);

            var files = Directory.GetFiles($"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86))}\\Votum\\Угадай себя - Lite\\");
            foreach (var item in files)
            {
                if (item.EndsWith(".exe.config") || item.EndsWith(".InstallState"))
                    v.File.Delete(item);
            }
        }

        private void УстановитьУгадайСебяLite_AfterUninstall(object sender, InstallEventArgs e)
        {
            v.File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\Угадай Себя - Lite.lnk");
            v.File.Delete($"{Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu)}\\Угадай Себя - Lite.lnk");
            v.File.Delete($"{Environment.GetEnvironmentVariable("TEMP")}\\vcredist_x86.exe");
            v.File.Delete($"{Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86))}\\Votum\\Угадай себя - Lite\\vcredist_x86.exe");

            //var files = Directory.GetFiles($"{Environment.GetEnvironmentVariable("TEMP")}");
            //foreach (var item in files)
            //{
            //    if (item.IndexOf("vcredist") != 0)
            //        v.File.Delete(item);
            //}
        }
    }
}
