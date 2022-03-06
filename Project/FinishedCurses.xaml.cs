using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для FinishedCurses.xaml
    /// </summary>

    public partial class FinishedCurses : Window
    {
        public FinishedCurses()
        {
            InitializeComponent();
        }

        //класс для добавления строки в курсах
        public class CRow
        {
            public long id { get; set; }
            public int num_hour { get; set; }
            public string name_course { get; set; }
            public string name_org { get; set; }
            public string svided { get; set; }
            public string date_st { get; set; }
            public string date_fin { get; set; }

            public CRow(long id, string name_course, string name_org, string svided, int num_hour, string date_st, string date_fin)
            {
                this.id = id;
                this.name_course = name_course;
                this.name_org = name_org;
                this.svided = svided;
                this.num_hour = num_hour;
                this.date_st = date_st;
                this.date_fin = date_fin;
            }
        }

        NpgsqlConnection dbConnection;

        private void Grid_Initialized(object sender, EventArgs e)
        {
            BindingList<CRow> collection = new BindingList<CRow>();
            dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            using (NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, "select * from finished_curses where user_id = " + MainWindow.userId.ToString(), true))
            {
                long el = 1;
                while (reader.Read())
                {
                    CRow row = new CRow(el, (string)reader["name_course"], (string)reader["name_org"], (string)reader["svided"], (int)reader["num_hour"], ((DateTime)reader["date_st"]).ToString("dd/MM/yyyy"), ((DateTime)reader["date_fin"]).ToString("dd/MM/yyyy"));
                    collection.Add(row);
                    el++;
                }
                table.ItemsSource = collection;
                dbConnection.Close();
            }
        }
    }
}