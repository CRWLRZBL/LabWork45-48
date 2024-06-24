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
    /// Логика взаимодействия для LabWork46.xaml
    /// </summary>
    public partial class LabWork46 : Window
    {
        public LabWork46()
        {
            InitializeComponent();
        }

        private void ChangeListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = commandTextBox.Text;
                object result = DataAccessLayer.EditTable(query);
                changeTextBox.Text = $"Количество изменённых строк: {result}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void ChangePriceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int bookId = Convert.ToInt32(bookIdTextBox.Text);
                decimal newPrice = Convert.ToDecimal(newPriceTextBox.Text);
                bool update = DataAccessLayer.UpdatePrice(bookId, newPrice);

                if (update)
                {
                    MessageBox.Show("Данные обновленны");
                }
                else
                {
                    MessageBox.Show("Ошибка в обновлении данных");
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
