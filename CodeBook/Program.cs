using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeBook
{
    static class Program
    {
        [DllImport("Shel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (!IsAssociated())
            {

            }
            else
            {
                Associate();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {
                Application.Run(new Form1());
            }
            else
            {
                Application.Run(new Form1(args[0]));
            }
        }
        public static bool IsAssociated()
        {
            return (Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.CodeBook", false) == null);
        }
        public static void Associate()
        {
            RegistryKey FileReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\.CodeBook");
            RegistryKey AppReg = Registry.CurrentUser.CreateSubKey("Software\\Classes\\CodeBook.exe");
            RegistryKey AppAssoc = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\.CodeBook");

            FileReg.CreateSubKey("DefaultIcon").SetValue("", Application.StartupPath + "\\icon.ico");
            FileReg.CreateSubKey("PerceivedType").SetValue("", "Text");


            AppReg.CreateSubKey("shell\\open\\command").SetValue("","\""+Application.ExecutablePath+"\" %1");
            AppReg.CreateSubKey("shell\\edit\\command").SetValue("","\""+Application.ExecutablePath+"\" %1");
            AppReg.CreateSubKey("DefaultIcon").SetValue("", Application.StartupPath + "\\icon.ico");

            AppAssoc.CreateSubKey("UserChoice").SetValue("Progid","Applications\\CodeBook.exe");

            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
