using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;

namespace Task1
{
    public static class DataAccessLayer
    {
        public static string ServerName { get; set; } = @"prserver\SQLEXPRESS";
        public static string DataBaseName { get; set; } = "ispp2111";
        public static string Login { get; set; } = "ispp2111";
        public static string Password { get; set; } = "2111";
        public static string ConnectionString { get; } = GetConnectionString();

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder builder = new()  //LR45 tsk1
            {
                DataSource = @"prserver\SQLEXPRESS", // server
                UserID = "ispp2111", // login
                Password = "2111",
                InitialCatalog = "ispp2111", // DB
                TrustServerCertificate = true
            };
            return builder.ConnectionString;
        }

        public static object GetSqlCommand(string query) //LR45 tsk2
        {
            if (query == null)
                return "There is no command";

            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            SqlCommand executeCommand = new(query, connection);
            return executeCommand.ExecuteScalar();
        }


        public static DataTable GetTable(string query) //LR45 tsk3
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            SqlCommand command = new(query, connection);
            using SqlDataAdapter adapter = new(query, connection);

            DataTable dataTable = new();
            adapter.Fill(dataTable);
            return dataTable;
        }

        public static List<Book> GetBooks() //LR45 tsk4
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            var query = "SELECT * FROM Book";
            SqlCommand command = new(query, connection);
            var reader = command.ExecuteReader();
            List<Book> books = new();
            while (reader.Read())
            {
                var book = new Book
                {
                    Id = Convert.ToInt32(reader["BookId"]),
                    Title = reader["Title"].ToString(),
                    Price = Convert.ToDouble(reader["Price"])
                };
                books.Add(book);
            }
            return books;
        }

        //LR46
        public static int GetCommand46(string query) //LR46 tsk1
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            SqlCommand executeCommand = new(query, connection);
            return executeCommand.ExecuteNonQuery();
        }

        public static bool UpdateBookPriceById(int bookId, decimal newPrice) //LR46 tsk2
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = $"UPDATE Book SET Price = {newPrice} WHERE BookId = {bookId}";

            SqlCommand command = new(query, connection);
            using SqlDataAdapter updater = new(query, connection);

            DataTable dataTable = new();
            updater.Fill(dataTable);
            return command.ExecuteNonQuery() > 0;
        }

        public static DataTable GetNewTable(string tableName) //LR46 tsk3
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = $"SELECT * FROM {tableName}";

            SqlCommand command = new(query, connection);
            using SqlDataAdapter adapter = new(query, connection);

            DataTable dataTable = new();
            adapter.Fill(dataTable);
            return dataTable;
        }

        public static void UpdateTable(ref DataTable table, string tableName) //LR46 tsk4
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = $"SELECT * FROM {tableName}";


            using SqlDataAdapter adapter = new(query, connection);
            SqlCommandBuilder builder = new(adapter);
            adapter.Update(table);
            table.Clear();
            adapter.Fill(table);
        }

        public static int CountBooksAWVP(decimal price) //LR47 tsk1
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            string query = $"SELECT COUNT(*) FROM Book WHERE Price < @price";

            SqlCommand command = new(query, connection);
            command.Parameters.AddWithValue("@price", price);

            return (int)command.ExecuteScalar();
        }

        public static int AddRow(string query) //LR47 tsk2
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            SqlCommand command = new($"{query};SET @id=SCOPE_IDENTITY()", connection);
            SqlParameter lastIdentity = new("@id", SqlDbType.Int);
            lastIdentity.Direction = ParameterDirection.Output;
            command.Parameters.Add(lastIdentity);

            command.ExecuteNonQuery();

            return (int)lastIdentity.Value;
        }

        public static DataTable GetBooks(string genre, decimal price) //LR47 tsk3
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            var query = $"SELECT * FROM Book WHERE Genre = @genre AND Price < @price";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@genre", genre);
            command.Parameters.AddWithValue("@price", price);

            SqlDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            return dataTable;
        }

        public static void ChangeValues(int bookId, decimal price, string title) //LR47 tsk4
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            var query = $"UPDATE Book SET Title = @title, Price = @price WHERE BookId = @bookId";
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@bookId", bookId);
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@price", price);
            command.ExecuteNonQuery();

            MessageBox.Show("Data was changed");
        }

        public static void NewAuthor(string surname, string name, string country)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            var query = "NewAuthor";
            SqlCommand command = new(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@country", country);

            command.ExecuteNonQuery();
        }

        public static int GetAuthorId(string surname, string name, string country)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            var query = "AddAuthorWithID";

            SqlCommand command = new(query, connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@country", country);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        public static DataTable ShowBooks(decimal minPrice, decimal maxPrice)
        {
            using SqlConnection connection = new(ConnectionString);
            connection.Open();

            var query = "ShowBooks";

            DataTable table = new();

            using SqlDataAdapter adapter = new(query, connection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            adapter.SelectCommand.Parameters.AddWithValue("@minPrice", minPrice);
            adapter.SelectCommand.Parameters.AddWithValue("@maxPrice", maxPrice);

            adapter.Fill(table);

            return table;
        }
    }
}
