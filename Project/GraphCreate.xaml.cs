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
    /// Логика взаимодействия для GraphCreate.xaml
    /// </summary>
    public partial class GraphCreate : Window
    {
        public GraphCreate()
        {
            InitializeComponent();
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            try
            {
                if (!(FIO.Text == "" && Pos.Text == "" && Cause.Text == "" && Event.Text == "" && Learn.Text == "" && Date_st.Text == "" && Date_fin.Text == ""))
                {
                    string[] dateFM = Date_fin.Text.Split('.');
                    string[] dateSM = Date_st.Text.Split('.');

                    if (Convert.ToInt32(dateFM[2]) >= Convert.ToInt32(dateSM[2]) && Convert.ToInt32(dateFM[1]) >= Convert.ToInt32(dateSM[1]) && Convert.ToInt32(dateFM[0]) >= Convert.ToInt32(dateSM[0]))
                    {
                        MainWindow.SqlShell(ref dbConnection, string.Format("INSERT INTO GRAPH(FIO, POS, CAUSE, EVENT, RESP_TRAIN, DATE_ST, DATE_FIN, USER_ID) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7})", FIO.Text, Pos.Text, Cause.Text, Event.Text, Learn.Text, Date_st.Text, Date_fin.Text, MainWindow.userId.ToString()), false);
                        MessageBox.Show("График успешно добавлен!");
                    }
                    else
                    {
                        MessageBox.Show("Проверьте корректность дат!");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!");
                }
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введеных данных!");
            }
            dbConnection.Close();
        }
    }
}
