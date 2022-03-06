using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для ScheduleLoooook.xaml
    /// </summary>
    public partial class ScheduleLoooook : Window
    {
        public ScheduleLoooook()
        {
            InitializeComponent();
        }

        public class Row
        {
            public int Num { get; set; }
            public string Subject { get; set; }
            public string Audience { get; set; }
            public string Class { get; set; }

            public Row(int Num, string Subject, string Audience, string Class)
            {
                this.Num = Num;
                this.Subject = Subject;
                this.Audience = Audience;
                this.Class = Class;
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            BindingList<Row> collection = new BindingList<Row>();
            using (NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, String.Format("SELECT * FROM SCHEDULE WHERE USER_ID={0} AND DATE='{1}'", MainWindow.userId.ToString(), ((DateTime)DatePicker.SelectedDate).ToString("dd/MM/yyyy")), true))
            {
                while (reader.Read())
                    collection.Add(new Row((int)reader[1], reader[2].ToString(), reader[3].ToString(), reader[4].ToString()));
            }
            dbConnection.Close();
            table.ItemsSource = collection;
        }
    }
}
