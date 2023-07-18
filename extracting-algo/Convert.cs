using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;


namespace extracting_algo
{
    class Converter
    {
        public static string GetRecentFile(string directoryPath)
        {
            string searchPattern = "Quality-Check*.doc";
            string expandedDirectoryPath = Environment.ExpandEnvironmentVariables(directoryPath);
            var directory = new DirectoryInfo(expandedDirectoryPath);
            var matchingFiles = directory.GetFiles(searchPattern);
            if(matchingFiles.Length == 0)
            {
                MessageBox.Show("No files found in Download folder!", "No files found", MessageBoxButton.OK, MessageBoxImage.Error);
                return "NotFound";
            }

            return matchingFiles.OrderByDescending(f => f.LastWriteTime).First().ToString();
        }
        public static Converter ConvertToExcel(string html, string excel, string output, bool downloadFromNet)
        {
            if (downloadFromNet)
            {
                //implemented in chrome extension
                MessageBox.Show("Use the web extension!", "No", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Converter();
            }

            System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
            proc.FileName = @"C:\windows\system32\cmd.exe";
            proc.Arguments = "/c py ./main.py --xlsx \"" + globals.pathToExcel + "\" --html \"" + globals.pathToHTML + "\" --output \"" + globals.pathToOutput + "\"";
            proc.CreateNoWindow = !globals.isInDebug;
            Console.WriteLine("\nExecuted " + proc.Arguments.ToString());
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(proc);
            process.WaitForExit();
            if (process.ExitCode != 0)
            {
                MessageBox.Show("An error occured while executing", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Conversion successful!", "Finished", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            if (File.Exists(globals.pathToOutput + ".json")&&!Properties.filenames.Default.SaveJSON)
            {
                File.Delete(globals.pathToOutput + ".json");
                //MessageBox.Show("would delete " + globals.pathToOutput + ".json cuz "+ Properties.filenames.Default.SaveJSON);
            }
            

            return new Converter();
        }
    }
}
