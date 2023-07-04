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

namespace extracting_algo
{
    class Convert
    {
        public static Convert ConvertToExcel(string html, string excel, string output, bool downloadFromNet)
        {
            // If the input file is a webpage, first rip the users cookies and then download the file using those
            if (downloadFromNet)
            {
                //TODO: implement
                MessageBox.Show("Not yet implemented!", "Not implemented", MessageBoxButton.OK, MessageBoxImage.Error);
                return new Convert();
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
            

            return new Convert();
        }
    }
}
