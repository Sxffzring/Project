using System;
using System.Collections.Generic;
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
using System.Threading;
using Npgsql;

namespace Project
{
    public partial class MainWindow : Window
    {
        public static string userName;
        public static string userRole;
        public static long userId;
        public static string connectionString = "host=localhost;user id=postgres;database=postgres;port=5432;password=1320;Trust Server Certificate=true";

        public NpgsqlConnection dbConnection;

        public static NpgsqlConnection ConnectToDB(string ConnectionString)
        {
            NpgsqlConnection Connection = new NpgsqlConnection(ConnectionString);
            Connection.Open();
            return Connection;
        }

        public static NpgsqlDataReader? SqlShell(ref NpgsqlConnection sqlConnection, string command, bool check)
        {
            if (check)
            {
                NpgsqlCommand ex = new NpgsqlCommand(command, sqlConnection);
                return ex.ExecuteReader();
            }
            else
            {
                NpgsqlCommand ex = new NpgsqlCommand(command, sqlConnection);
                ex.ExecuteNonQuery();
                return null;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BAuth_Click(object sender, RoutedEventArgs e)
        {
            dbConnection = ConnectToDB(connectionString);
            using (NpgsqlDataReader authReader = SqlShell(ref dbConnection, String.Format("SELECT * FROM USERS WHERE LOGIN='{0}' AND PASS='{1}'", authLog.Text, authPass.Password), true))
            {
                if (authReader.Read())
                {
                    try
                    {
                        userName = authReader.GetString(3);
                        userRole = authReader.GetString(4);
                        userId = authReader.GetInt64(0);
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка!");
                    }
                    finally
                    {
                        MessageBox.Show("Добро пожаловать!");
                        if (userRole == "teacher")
                        {
                            ClientWindow clientFrame = new ClientWindow();
                            clientFrame.Show();
                        }
                        else if(userRole == "admin")
                        {
                            AdminWindow AdminFrame = new AdminWindow();
                            AdminFrame.Show();
                        }
                        this.Close();
                    }
                }
                else
                    MessageBox.Show("Неверный логин или пароль!");
            }
            dbConnection.Close();
        }

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            Registration registrationFrame = new Registration();
            registrationFrame.Show();
            this.Close();
        }
    }
}
