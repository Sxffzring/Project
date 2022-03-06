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
using Npgsql;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для AddFinishedCurses.xaml
    /// </summary>
    public partial class AddFinishedCurses : Window
    {
        public AddFinishedCurses()
        {
            InitializeComponent();
        }
        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            try
            {
                string[] dateFM = dateFinished.Text.Split('.');
                string[] dateSM = dateStart.Text.Split('.');
                if (Convert.ToInt32(dateFM[2]) >= Convert.ToInt32(dateSM[2]) && Convert.ToInt32(dateFM[1]) >= Convert.ToInt32(dateSM[1]) && Convert.ToInt32(dateFM[0]) >= Convert.ToInt32(dateSM[0]))
                {
                    NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, String.Format("insert into Finished_curses(name_course, name_org, svided, num_hour, date_st, date_fin, user_id) values('{0}', '{1}', '{2}', {3}, '{4}', '{5}', {6})", name.Text, nameOrg.Text, svided.Text, numHours.Text, dateStart.Text, dateFinished.Text, MainWindow.userId), false);
                    MessageBox.Show("Курс успешно записан!");
                    this.Close();
                }
                else
                    MessageBox.Show("Проверьте корректность дат!");
            }
            catch
            {
                MessageBox.Show("Произошла ошибка!");
            }
            dbConnection.Close();
        }
    }
}
