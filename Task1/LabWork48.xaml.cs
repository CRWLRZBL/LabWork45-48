using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace Task1
{
    /// <summary>
    /// Логика взаимодействия для LabWork48.xaml
    /// </summary>
    public partial class LabWork48 : Window
    {
        public LabWork48()
        {
            InitializeComponent();
        }

        private void AddAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string surname = surnameTextBox.Text;
                string name = nameTextBox.Text;
                string country = countryTextBox.Text;

                DataAccessLayer.CreateAuthor(surname, name, country);

                MessageBox.Show("Автор добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void AddAuthorIdButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string surname = surnameIdTextBox.Text;
                string name = nameTextIdBox.Text;
                string country = countryIdTextBox.Text;

                MessageBox.Show($"{DataAccessLayer.GetAuthorId(surname, name, country)}");

                MessageBox.Show("Автор добавлен!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void DataGridButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int minPrice = Convert.ToInt32(minPriceTextBox.Text);
                int maxPrice = Convert.ToInt32(maxPriceTextBox.Text);

                bookTable.ItemsSource = DataAccessLayer.GetBookPrice(minPrice, maxPrice).DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
