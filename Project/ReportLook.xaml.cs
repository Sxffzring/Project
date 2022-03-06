using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для ReportLook.xaml
    /// </summary>
    public partial class ReportLook : Window
    {
        public ReportLook()
        {
            InitializeComponent();
        }

        string[] path = Directory.GetFiles(@"C:\Отчеты");

        private void Grid_Initialized(object sender, EventArgs e)
        {
            List<string> names = new List<string>();
            foreach (string el in path)
            {
                names.Add(el.Split("\\")[2]);
            }
            FList.ItemsSource = names;
        }

        private void BLook_Click(object sender, RoutedEventArgs e)
        {
            string commandText = @"C:\Отчеты\"+FList.Text;
            var proc = new Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}
