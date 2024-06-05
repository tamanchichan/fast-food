using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace fast_food.Data
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            FastFoodDbContext context = new FastFoodDbContext(serviceProvider.GetRequiredService<DbContextOptions<FastFoodDbContext>>());

            context.Database.Migrate();
            ItemStoreProcedure(context);


            #region Insert Data By Procedure
            if (!context.Item.Any()) // If there are no items in the database, seed it
            {
                void InsertItem()
                {
                    string connectionString = "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Database=FastFoodDb;TrustServerCertificate=True";

                    string itemFilePath = @"C:\Users\Taman\OneDrive\Documentos\Programming\GitHub (tamanchichan)\Projects\fast-food\fast-food\Items.txt";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (StreamReader reader = new StreamReader(itemFilePath))
                        {
                            while (!reader.EndOfStream)
                            {
                                string[] values = reader.ReadLine().Split('|');
                                Guid id = Guid.NewGuid();
                                string code = values[0];
                                string name = values[1];
                                decimal price = decimal.Parse(values[2]);

                                using (SqlCommand command = new SqlCommand("InsertItem", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@id", id);
                                    command.Parameters.AddWithValue("@code", code);
                                    command.Parameters.AddWithValue("@name", name);
                                    command.Parameters.AddWithValue("@price", price);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                InsertItem();
            }
            #endregion
        }
        private static void ItemStoreProcedure(FastFoodDbContext context)
        {
            string procedure =
            @"
                CREATE OR ALTER PROCEDURE InsertItem
                    @id UNIQUEIDENTIFIER,
                    @code NVARCHAR(100),
                    @name NVARCHAR(100),
                    @price DECIMAL(18,2)
                AS
                BEGIN
                    INSERT INTO Item (Id, Code, Name, Price)
                    VALUES (@id, @code, @name, @price);
                END
            ";

            using (var command = context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = procedure;
                context.Database.OpenConnection();
                command.ExecuteNonQuery();
                context.Database.CloseConnection();
            }
        }
    }
}
