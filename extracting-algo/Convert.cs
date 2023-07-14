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
        public static string GetFileNameWithHighestNumber(string directoryPath)
        {
            string expandedPath = Environment.ExpandEnvironmentVariables(directoryPath);
            string[] fileNames = Directory.GetFiles(expandedPath);
            string regexPattern = @"^Quality-Check.*?(\d+)?\.doc$";
            Regex regex = new Regex(regexPattern);

            string highestFileName = null;
            int highestNumber = -1;

            foreach (string fileName in fileNames)
            {
                string baseFileName = Path.GetFileName(fileName);
                Match match = regex.Match(baseFileName);

                if (match.Success)
                {
                    if (match.Groups[1].Success)
                    {
                        int currentNumber = Convert.ToInt32(match.Groups[1].Value);

                        if (currentNumber > highestNumber)
                        {
                            highestNumber = currentNumber;
                            highestFileName = baseFileName;
                        }
                    }
                    else
                    {
                        highestFileName = baseFileName;
                    }
                }
            }

            return highestFileName;
        }
        public static Converter ConvertToExcel(string html, string excel, string output, bool downloadFromNet)
        {
            if (downloadFromNet)
            {
                //TODO: implement
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
