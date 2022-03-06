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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            LWelcome.Content = "    Добро пожаловать," + '\n' + MainWindow.userName + "!";
        }

        private void BExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BFinCurses_Click(object sender, RoutedEventArgs e)
        {
            FinishedCurses FinCursesFrame = new FinishedCurses();
            FinCursesFrame.Show();
        }

        private void BAddCurses_Click(object sender, RoutedEventArgs e)
        {
            AddFinishedCurses AddCourses = new AddFinishedCurses();
            AddCourses.Show();
        }

        private void BCreateSched_Click(object sender, RoutedEventArgs e)
        {
            ScheduleCreate SchFrame = new ScheduleCreate();
            SchFrame.Show();
        }

        private void BSeeSched_Click(object sender, RoutedEventArgs e)
        {
            ScheduleLoooook SchLFrame = new ScheduleLoooook();
            SchLFrame.Show();
        }
    }
}
