using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            LWelcome.Content = "   Добро пожаловать," + '\n' + "    Администратор!";
        }

        private void BGraphC_Click(object sender, RoutedEventArgs e)
        {
            GraphCreate GCFrame = new GraphCreate();
            GCFrame.Show();
        }

        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BGraphL_Click(object sender, RoutedEventArgs e)
        {
            GraphLook graphLookFrame = new GraphLook();
            graphLookFrame.Show();
        }

        private void BRepC_Click(object sender, RoutedEventArgs e)
        {
            ReportCreate repCreate = new ReportCreate();
            repCreate.Show();
        }

        private void BRepL_Click(object sender, RoutedEventArgs e)
        {
            ReportLook repLook = new ReportLook();
            repLook.Show();
        }
    }
}
