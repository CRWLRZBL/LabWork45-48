using System.Data;
using System.Windows;

namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CheckLabel.Content = DataAccessLayer.GetConnectionString(); //tsk1 lr45
        }


        private void ExecButton_Click(object sender, RoutedEventArgs e)
        {

            if (task2RB.IsChecked == true) //tsk2 lr45
                try
                {
                    OutPutLabel.Content = DataAccessLayer.GetSqlCommand(GetCommandTextBox.Text);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task3RB.IsChecked == true) //tsk3 lr45
                try
                {
                    OutputDG.ItemsSource = DataAccessLayer.GetTable(GetCommandTextBox.Text).DefaultView;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task4RB.IsChecked == true) //tsk4 lr45
                try
                {
                    OutputDG.ItemsSource = DataAccessLayer.GetBooks();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task1_46RB.IsChecked == true) //tsk1 lr46
                try
                {
                    OutPutLabel.Content = DataAccessLayer.GetCommand46(GetCommandTextBox.Text);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task2_46RB.IsChecked == true) //tsk2 lr46
                try
                {
                    OutPutLabel.Content = DataAccessLayer.UpdateBookPriceById(Convert.ToInt32(TableIDTextBox.Text), Convert.ToDecimal(PriceTextBox.Text));
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task3_46RB.IsChecked == true) //tsk3 lr46
                try
                {
                    OutputDG.ItemsSource = DataAccessLayer.GetNewTable(TableNameTextBox.Text).DefaultView;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task4_46RB.IsChecked == true) //tsk4 lr46
                try
                {
                    DataTable table = DataAccessLayer.GetNewTable(TableNameTextBox.Text);
                    OutputDG.ItemsSource = table.DefaultView;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task1_47RB.IsChecked == true) //LR47 tsk1
                try
                {
                    OutPutLabel.Content = DataAccessLayer.CountBooksAWVP(Convert.ToDecimal(PriceTextBox.Text));
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task2_47RB.IsChecked == true) //LR47 tsk2
                try
                {
                    OutPutLabel.Content = DataAccessLayer.AddRow(GetCommandTextBox.Text);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task3_47RB.IsChecked == true) //LR47 tsk3
                try
                {
                    OutputDG.ItemsSource = DataAccessLayer.GetBooks(GenreTextBox.Text, Convert.ToDecimal(PriceTextBox.Text)).DefaultView;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task4_47RB.IsChecked == true) try
                {
                    DataAccessLayer.ChangeValues(1, 1612, "Смута");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task2_48RB.IsChecked == true) //LR48 tsk2
                try
                {
                    DataAccessLayer.NewAuthor(AuthorNameTextBox.Text, SurnameTextBox.Text, CountryTextBox.Text);
                    MessageBox.Show("New author added");
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task3_48RB.IsChecked == true) //LR48 tsk3
                try
                {
                    OutPutLabel.Content = DataAccessLayer.GetAuthorId(AuthorNameTextBox.Text, SurnameTextBox.Text, CountryTextBox.Text);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

            if (task4_48RB.IsChecked == true) //LR48 tsk4
                try
                {
                    OutputDG.ItemsSource = DataAccessLayer.ShowBooks(Convert.ToDecimal(MinPriceTextBox.Text), Convert.ToDecimal(MaxPriceTextBox.Text)).DefaultView;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }

        }

        private void saveTableButton_Click(object sender, RoutedEventArgs e)
        {
            DataView tableView = (DataView)OutputDG.ItemsSource;
            DataTable table = tableView.Table;
            DataAccessLayer.UpdateTable(ref table, TableNameTextBox.Text);
            OutPutLabel.Content = "Succesfully updated";
        }
    }
}