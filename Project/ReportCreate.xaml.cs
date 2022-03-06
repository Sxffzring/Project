using OfficeOpenXml;
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
using System.IO;
using Npgsql;
using OfficeOpenXml.Style;

namespace Project
{
    /// <summary>
    /// Логика взаимодействия для ReportCreate.xaml
    /// </summary>
    public partial class ReportCreate : Window
    {
        public ReportCreate()
        {
            InitializeComponent();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            NpgsqlConnection dbConnection = MainWindow.ConnectToDB(MainWindow.connectionString);
            using (ExcelPackage report = new ExcelPackage())
            {
                ExcelWorksheet workshit = report.Workbook.Worksheets.Add("Отчет за период");

                workshit.Cells["C5"].Value = "№ П/П";
                workshit.Cells["D5"].Value = "ФИО";
                workshit.Cells["E5"].Value = "Должность";
                workshit.Cells["F5"].Value = "Причина направления";
                workshit.Cells["G5"].Value = "Мероприятие";
                workshit.Cells["H5"].Value = "Ответственный за обучение";
                workshit.Cells["I5"].Value = "Дата начала";
                workshit.Cells["J5"].Value = "Дата окончания";
                try
                {
                    using (NpgsqlDataReader reader = MainWindow.SqlShell(ref dbConnection, String.Format("SELECT * FROM GRAPH WHERE DATE_FIN BETWEEN '{0}' AND '{1}'", ((DateTime)Date1.SelectedDate).ToString("dd/MM/yyyy"), ((DateTime)Date2.SelectedDate).ToString("dd/MM/yyyy")), true))
                    {
                        int row = 6;

                        while (reader.Read())
                        {
                            for (int column = 1; column <= 5; column++)
                            {
                                workshit.Cells[row, 3].Value = row - 5;
                                workshit.Cells[row, 3 + column].Value = reader[column];
                            }
                            workshit.Cells[row, 9].Value = ((DateTime)reader[6]).ToString("dd/MM/yyyy");
                            workshit.Cells[row, 10].Value = ((DateTime)reader[7]).ToString("dd/MM/yyyy");
                            row++;
                        }
                    }

                    FileInfo FReport = new FileInfo(@"C:\Отчеты\Отчет_" + ((DateTime)Date1.SelectedDate).ToString("dd/MM/yyyy") + "-" + ((DateTime)Date2.SelectedDate).ToString("dd/MM/yyyy") + ".xlsx");
                    report.SaveAs(FReport);
                    MessageBox.Show("Отчет сформирован!");
                }
                catch
                {
                    MessageBox.Show("Ошибка формирования отчета!");
                }
            }
            dbConnection.Close();
        }
    }
}