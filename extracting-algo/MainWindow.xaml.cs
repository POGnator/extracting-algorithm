using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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
using Microsoft.Office.Interop.Excel;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace extracting_algo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Converter";
            textbox.Text = Properties.filenames.Default.htmlPath;
            textbox2.Text = Properties.filenames.Default.xlsxPath;
            textbox3.Text = Properties.filenames.Default.outputPath;
            saveJSON.IsChecked = Properties.filenames.Default.SaveJSON;
            useExtFile.IsChecked = Properties.filenames.Default.useFileFromExtension;
            //Install the required packages if not present
            string strCmdText = "/C ./pip/__main__.py install pandas openpyxl";
            System.Diagnostics.ProcessStartInfo cmdproc = new System.Diagnostics.ProcessStartInfo();
            cmdproc.FileName = "CMD.exe";
            cmdproc.Arguments = strCmdText;
            cmdproc.CreateNoWindow = true;
            System.Diagnostics.Process cmdprocess = System.Diagnostics.Process.Start(cmdproc);
            cmdprocess.WaitForExit();
            if (cmdprocess.ExitCode != 0)
            {
                MessageBox.Show("An error occured while installing the pip packages", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /*else
            {
                MessageBox.Show("success");
            }*/
        }

        private void BrowseFiles(object sender, RoutedEventArgs e)
        {
            var tag = (System.Windows.Controls.TextBox)this.FindName((sender as FrameworkElement).Tag.ToString());
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            if (tag.Name == "textbox2" || tag.Name == "textbox3")
            {
                openFileDlg.Filter = "Excel files (*.xlsx)|*.xlsx";
            }
            else if (tag.Name == "textbox")
            {
                openFileDlg.Filter = "Word Documents|*.doc|Hypertext Markup Language|*.html|All files|*";
            }
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                tag.Text = openFileDlg.FileName;
                Console.WriteLine(tag.Text);
            }
        }

        private void StartConversion(object sender, RoutedEventArgs e)
        {
            /*
            textbox - HTML
            textbox2 - xlsx
            textbox3 - output
             */
            // Write default settings
            Properties.filenames.Default.htmlPath = textbox.Text;
            Properties.filenames.Default.xlsxPath = textbox2.Text;
            Properties.filenames.Default.outputPath = textbox3.Text;
            Properties.filenames.Default.SaveJSON = saveJSON.IsChecked ?? false;
            Properties.filenames.Default.useFileFromExtension = useExtFile.IsChecked ?? false;
            Properties.filenames.Default.Save();


            Console.WriteLine("Files:");
            //haha spaghetti code
            if(Properties.filenames.Default.useFileFromExtension)
            {
                string pathName = @"%userprofile%\Downloads\";
                //regex time, are you ready?
                try
                {
                    //Console.WriteLine(Converter.GetFileNameWithHighestNumber(@"C:\Users\Gabriel\Downloads\"));
                    globals.pathToHTML = pathName + Converter.GetFileNameWithHighestNumber(pathName);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    MessageBox.Show("An error occured while trying to find the input .doc", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                globals.pathToHTML = textbox.Text;
            }
            bool isLink = globals.pathToHTML.StartsWith("http://") || globals.pathToHTML.StartsWith("https://");
            Console.WriteLine("HTML:\n" + globals.pathToHTML + "\n- Exists: " + File.Exists(globals.pathToHTML).ToString() + "\n- Input file is a link: " + isLink);

            globals.pathToExcel = textbox2.Text;
            Console.WriteLine("\nXLSX:\n" + globals.pathToExcel + "\n- Exists: " + File.Exists(globals.pathToExcel).ToString());
            
            globals.pathToOutput = textbox3.Text;
            Console.WriteLine("\nOUTPUT:\n" + globals.pathToOutput + "\n- Exists: " + File.Exists(globals.pathToOutput).ToString());
            
            if (!(File.Exists(globals.pathToHTML) || File.Exists(globals.pathToExcel) || File.Exists(globals.pathToOutput)))
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
                Converter.ConvertToExcel(globals.pathToHTML, globals.pathToExcel, globals.pathToOutput, isLink);
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
                Console.WriteLine(dialog);
            }
        }

        private void ShowHelp(object sender, RoutedEventArgs e)
        {
            Window1 win1 = new Window1();
            win1.Show();
        }

        private void debugConsole(object sender, RoutedEventArgs e)
        {
            [DllImport("Kernel32")]
            static extern void AllocConsole();

            [DllImport("Kernel32")]
            static extern void FreeConsole();
            AllocConsole();
            Console.WriteLine("Debug mode started");
            globals.isInDebug = true;
        }

        private void setInactive(object sender, RoutedEventArgs e)
        {
            browseInput.IsEnabled = true;
            textbox.IsEnabled = true;
        }

        private void setActive(object sender, RoutedEventArgs e)
        {
            browseInput.IsEnabled = false;
            textbox.IsEnabled = false;
        }
    }
}
