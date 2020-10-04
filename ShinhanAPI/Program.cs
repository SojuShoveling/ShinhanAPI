using Microsoft.Win32;
using System;
using System.Diagnostics;

namespace ShinhanAPI
{
    public class Program
    {
        private static string ProgramPath = null;

        public static string GetProgramPath()
        {
            if (ProgramPath != null)
                return ProgramPath;
            try
            {
                RegistryKey SoftwareKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", false);
                string[] arrStrKeyName = SoftwareKey.GetSubKeyNames();

                for (int i = 0; i < arrStrKeyName.Length; i++)
                {
                    if (SoftwareKey.OpenSubKey(arrStrKeyName[i]).GetValue("ProductGuid", "").ToString().Contains("{E23A8EFB-5585-4BE1-B641-80664985C582}")) //신한i 인디 Guid
                    {
                        string displayIcon = SoftwareKey.OpenSubKey(arrStrKeyName[i]).GetValue("DisplayIcon", null).ToString();
                        if (string.IsNullOrWhiteSpace(displayIcon))
                            return null;
                        else
                            return displayIcon.Split(',')[0];
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            return null;
        }

        public static bool Check_Running()
        {
            Process[] processes = Process.GetProcessesByName("giexpertmain");
            if (processes.Length > 0)
                return true;
            return false;
        }
    }
}