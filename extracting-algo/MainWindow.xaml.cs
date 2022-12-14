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
            this.Title = "Converter";
            //Install the required packages if not present
            string strCmdText;
            strCmdText = "/C py ./pip/__main__.py install pandas openpyxl && PowerShell -Command \"Add-Type -AssemblyName PresentationFramework;[System.Windows.MessageBox]::Show('All required packages were installed', 'Success')";
            try
            {
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            }
            catch
            {
                MessageBox.Show("Python is not installed, but can be at\nhttps://www.python.org/downloads/", "Error - Python not found", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            globals.pathToHTML = textbox.Text;
            globals.pathToExcel = textbox2.Text;
            globals.pathToOutput = textbox3.Text;
            if(!(File.Exists(globals.pathToHTML) || File.Exists(globals.pathToExcel) || Directory.Exists(globals.pathToOutput)))
            {
                this.Title= "Converter - Error";
                MessageBox.Show("Invalid file or directory name!", "Error" , MessageBoxButton.OK, MessageBoxImage.Error);
                this.Title= "Converter";
                return;
            }
            this.Title = "Starting... - Converter";
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

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }
    }
}
