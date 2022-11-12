using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace extracting_algo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Install the required packages if not present
            System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
            proc.FileName = @"C:\windows\system32\cmd.exe";
            proc.Arguments = @"\c py ./pip/__main__.py install pandas openpyxl";
            System.Diagnostics.Process.Start(proc);
        }

        private void BrowseFiles(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                var tag = (TextBox)this.FindName((sender as FrameworkElement).Tag.ToString());
                tag.Text = openFileDlg.FileName;
            }
        }

        private void StartConversion(object sender, RoutedEventArgs e)
        {
            this.Title = "Starting... - Converter";
            globals.pathToHTML = textbox.Text;
            globals.pathToExcel = textbox2.Text;
            globals.pathToOutput = textbox3.Text;
            //Debug
            var confirm = MessageBox.Show("Path to HTML: "+ globals.pathToHTML + "\nPath to Excel: " + globals.pathToExcel + "\nPath to output folder: " + globals.pathToOutput, "Is this information right?", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (confirm == MessageBoxResult.OK)
            {
                //Convert
                Convert.ConvertToExcel(globals.pathToHTML, globals.pathToExcel, globals.pathToOutput);
            }
            this.Title = "Converter";
        }

        private void BrowseFolder(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                textbox3.Text = dialog.FileName;
            }
        }
    }
}
