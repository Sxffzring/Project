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
    /// Логика взаимодействия для GraphLook.xaml
    /// </summary>
    public partial class GraphLook : Window
    {
        public GraphLook()
        {
            InitializeComponent();
        }

        public class Row
        {
            public string FIO { get; set; }
            public string Pos { get; set; }
            public string Cause { get; set; }
            public string Event { get; set; }
            public string Resp_train { get; set; }
            public string Date_st { get; set; }
            public string Date_fin { get; set; }

            public Row(string FIO, string Pos, string Cause, string Event, string Resp_train, string Date_st, string Date_fin)
            {
                this.FIO = FIO;
                this.Pos = Pos;
                this.Cause = Cause;
                this.Event = Event;
                this.Resp_train = Resp_train;
                this.Date_st = Date_st;
                this.Date_fin = Date_fin;
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            BindingList<Row> collection = new BindingList<Row>();
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            using (NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, "SELECT * FROM GRAPH", true))
            {
                while (reader.Read())
                {
                    Row row = new Row(reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), ((DateTime)reader[6]).ToString("dd/MM/yyyy"), ((DateTime)reader[7]).ToString("dd/MM/yyyy"));
                    collection.Add(row);
                }
            }
            table.ItemsSource = collection;
            dbConnection.Close();
        }
    }
}
