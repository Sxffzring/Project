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
    /// Логика взаимодействия для ScheduleCreate.xaml
    /// </summary>
    public partial class ScheduleCreate : Window
    {
        public ScheduleCreate()
        {
            InitializeComponent();
            List<String> ClassList = new List<String>() 
            {
                "*", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"
            };

            List<String> SubjectList = new List<string>()
            { 
                "*",
                "Русский язык",
                "Английский язык", 
                "Математика",
                "Химия", 
                "Физика",
                "Литература",
                "Физическая культура",
                "ОБЖ",
                "Обществознание",
                "ИКТ",
                "История",
                "Биология"
            };

            List<string> Audiences = new List<string>() { "*" };

            c1.ItemsSource = ClassList;
            c2.ItemsSource = ClassList;
            c3.ItemsSource = ClassList;
            c4.ItemsSource = ClassList;
            c5.ItemsSource = ClassList;
            c6.ItemsSource = ClassList;
            c7.ItemsSource = ClassList;

            s1.ItemsSource = SubjectList;
            s2.ItemsSource = SubjectList;
            s3.ItemsSource = SubjectList;
            s4.ItemsSource = SubjectList;
            s5.ItemsSource = SubjectList;
            s6.ItemsSource = SubjectList;
            s7.ItemsSource = SubjectList;

            a1.ItemsSource = Audiences;
            a2.ItemsSource = Audiences;
            a3.ItemsSource = Audiences;
            a4.ItemsSource = Audiences;
            a5.ItemsSource = Audiences;
            a6.ItemsSource = Audiences;
            a7.ItemsSource = Audiences;

            for (int i = 101; i <= 115; i++)
                Audiences.Add(i.ToString());
            for (int i = 201; i <= 215; i++)
                Audiences.Add(i.ToString());
            for (int i = 301; i <= 315; i++)
                Audiences.Add(i.ToString());
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            try
            {
                using (NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, String.Format("SELECT * FROM SCHEDULE WHERE USER_ID='{0}' AND DATE='{1}'", MainWindow.userId.ToString(), ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy")), true))
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Расписание на этот день уже существует!");
                    }
                    else
                    {
                        reader.Close();
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(1, '{0}', '{1}', '{2}', '{3}', {4})", s1.Text, a1.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(2, '{0}', '{1}', '{2}', '{3}', {4})", s2.Text, a2.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(3, '{0}', '{1}', '{2}', '{3}', {4})", s3.Text, a3.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(4, '{0}', '{1}', '{2}', '{3}', {4})", s4.Text, a4.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(5, '{0}', '{1}', '{2}', '{3}', {4})", s5.Text, a5.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(6, '{0}', '{1}', '{2}', '{3}', {4})", s6.Text, a6.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO SCHEDULE(NUM, SUBJECT, AUDIENCE, CLASS, DATE, USER_ID) VALUES(7, '{0}', '{1}', '{2}', '{3}', {4})", s7.Text, a7.Text, c1.Text, ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy"), MainWindow.userId.ToString()), false);
                        MessageBox.Show("Расписание успешно записано!"); 
                    }
                }
            }
            catch
            {
                MessageBox.Show("Введите дату расписания!");
            }
            dbConnection.Close();
        }

        private void BDel_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            MainWindow.SqlShell(ref dbConnection, String.Format("DELETE FROM SCHEDULE WHERE USER_ID='{0}' AND DATE='{1}'", MainWindow.userId.ToString(), ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy")), false);
            MessageBox.Show("Удаление расписания прошло успешно!");
        }
    }
}
