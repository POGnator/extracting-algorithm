using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace extracting_algo
{
    class Convert
    {
        public static Convert ConvertToExcel(string html, string excel, string output)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
                proc.FileName = @"C:\windows\system32\cmd.exe";
                proc.Arguments = "/c py ./main.py --html " + globals.pathToHTML + " --xlsx " + globals.pathToExcel + " --output " + globals.pathToOutput;
                System.Diagnostics.Process.Start(proc);
            }
            catch
            {
                MessageBox.Show("Error while opening main.py");
            }
            return new Convert();
        }
    }
}
