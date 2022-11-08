using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace extracting_algo
{
    class Convert
    {
        public static Convert ConvertToExcel(string html, string excel, string output)
        {
            //Do your conversion thing
            //*cough* tutorial *cough* at https://coderwall.com/p/app3ya/read-excel-file-in-c
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(excel);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            return new Convert();
        }
    }
}
