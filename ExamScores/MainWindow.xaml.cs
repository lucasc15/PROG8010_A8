using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using WinForms = System.Windows.Forms;

namespace ExamScores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new ExamScores.ViewModel();
            DataContext = vm;
        }
        private void btnLoadDataFolder(object sender, RoutedEventArgs e)
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            WinForms.DialogResult dataFolder = fbd.ShowDialog();
            //string tmpFolder = @"C:\Users\curra\Documents\school\prog8020\assignments\data";
            if (Directory.Exists(fbd.SelectedPath))
            {
                vm.LoadData(fbd.SelectedPath);
            } else
            {
                Debug.Print("VALUE: ", fbd.SelectedPath);
                MessageBox.Show("Please choose a valid folder!");
            }
            //vm.LoadData(tmpFolder);
        }
    }
    public class StringDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(double))
            {
                return string.Format("{0:0.00}", value);
            }
            else
            {
                return "";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
