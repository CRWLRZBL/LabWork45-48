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
using System.Windows.Shapes;

namespace Task1
{
    /// <summary>
    /// Логика взаимодействия для LabWork47.xaml
    /// </summary>
    public partial class LabWork47 : Window
    {
        public LabWork47()
        {
            InitializeComponent();
        }


        private void GetAmountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var query = Convert.ToDecimal(amountTextBox.Text);
                int result = DataAccessLayer.GetAmount(query);
                MessageBox.Show($"Получено книг: {result}");
            }
            catch(Exception ex) 
            {
                MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }

        private void ShowTableButton_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                string name = nameTextBox.Text;
                string surname = surnameTextBox.Text;
                var result = DataAccessLayer.AddAuthor(name, surname);
                MessageBox.Show($"Автор добавлен: {result}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении запроса: {ex.Message}");
            }

        }
    }
}
