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
using System.Threading;
using Npgsql;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        public NpgsqlConnection dbConnection;

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            
            if (regPass1.Password == regPass2.Password)
            {
                dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
                bool check = false;
                using (NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, String.Format("select id from users where login = '{0}'", regLogin.Text), true))
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!");
                        dbConnection.Close();
                    }
                    else check = true;
                }
                if (check)
                {
                    MainWindow.SqlShell(ref dbConnection, String.Format("INSERT INTO USERS(login,pass,name,role) VALUES('{0}', '{1}', '{2}', 'teacher')", regLogin.Text, regPass1.Password, regName.Text), false);
                    MessageBox.Show("Регистрация выполнена успешна!");
                    dbConnection.Close();
                    MainWindow authFrame = new MainWindow();
                    authFrame.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }
        }
    }
}
