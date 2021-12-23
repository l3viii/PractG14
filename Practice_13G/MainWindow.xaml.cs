using System;
using System.Collections.Generic;
using System.IO;
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
using LibMatrix;
using Microsoft.Win32;

namespace Practice_13G
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            loginWindow.ShowDialog();
            if (loginWindow.CorrectPassword == true)
            {
                if (File.Exists("config.ini"))
                {
                    using (StreamReader reader = new StreamReader("config.ini"))
                    {
                        _myArray = new MyArray(int.Parse(reader.ReadLine()), int.Parse(reader.ReadLine()));
                        InitializeComponent();
                        dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
                    }
                }
                else MessageBox.Show("Файла конфигурации нет");
            }
            else
            {
                 Close();
            }
        }

        LoginWindow loginWindow = new LoginWindow();

        private MyArray _myArray;

        private void GetSum_Click(object sender, RoutedEventArgs e)
        {
            int sum = 0;
            for (int i = 2; i < _myArray.RowLength; i+=2)
            {
                for (int j = 0; j < _myArray.ColumnLength; j++)
                {
                    sum += _myArray[j, i];
                }
            }
            sumResult.Text = sum.ToString();
        }

        private void FillUpArray_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(row.Text) || String.IsNullOrEmpty(column.Text))
            {
                MessageBox.Show("Введите размер матрицы");
            }
            else
            {
                _myArray = new MyArray(int.Parse(row.Text), int.Parse(column.Text));
                sizeRow.Text = $"Строк {row.Text}";
                sizeColumn.Text = $"Столбцов {column.Text}";
                _myArray.Fill();
                dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
            }
        }

        private void SaveArray_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                MessageBox.Show("Заполните матрицу", "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Title = "Экспорт массива";

            if (saveFileDialog.ShowDialog() == true)
            {
                _myArray.Export(saveFileDialog.FileName);
            }
            dataGrid.ItemsSource = null;
        }

        private void OpenArray_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовый документ (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.DefaultExt = ".txt";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Title = "Импорт массива";

            if (openFileDialog.ShowDialog() == true)
            {
                _myArray.Import(openFileDialog.FileName);
            }
            dataGrid.ItemsSource = _myArray.ToDataTable().DefaultView;
        }

        private void AboutProgramm_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дроздов Г. ", "Информация о разработчике", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CellIndex_Click(object sender, MouseEventArgs e)
        {
            selectedCell.Text = $"[{dataGrid.Items.IndexOf(dataGrid.CurrentItem)};" +
                $"{dataGrid.CurrentColumn.DisplayIndex}]";
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            row.Clear();
            column.Clear();
            sumResult.Clear();
        }

        private void TBoxChangeValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            sumResult.Clear();
        }

        private void Options_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            options.ShowDialog();
        }
    }
}
