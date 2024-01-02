using fast_food.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace fast_food.Areas.Identity.Data
{
    public class SeedData
    {
        public async static Task Initialize(IServiceProvider serviceProvider)
        {
            FastFoodDb context = new FastFoodDb(serviceProvider.GetRequiredService<DbContextOptions<FastFoodDb>>());

            context.Database.Migrate();
            ItemStoreProcedure(context);

            #region Insert Data Manually (one by one)
            //if (!context.Item.Any()) // If there are no items in the database, seed it
            //{
            //    Item itemOne = new Item
            //    {
            //        Id = new Guid(),
            //        Code = "1",
            //        Name = "Deluxe Wonton Soup",
            //        Description = "A large pork wonton soup containing chicken, pork, and shrimp. An appetizer for 1-2 person/people.",
            //        Price = 12.95m
            //    };
            //    context.Item.Add(itemOne);

            //    Item itemTwo = new Item
            //    {
            //        Id = new Guid(),
            //        Code = "2",
            //        Name = "Wonton Soup",
            //        Description = "A small pork wonton soup. An appetizer for 1 person.",
            //        Price = 6.50m
            //    };
            //    context.Item.Add(itemTwo);

            //    context.SaveChanges();
            //}
            #endregion

            #region Insert Data By Procedure
            if (!context.Item.Any()) // If there are no items in the database, seed it
            {
                void InsertItem()
                {
                    string connectionString = "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Database=FastFoodDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";

                    string itemFilePath = @"C:\Users\Taman\OneDrive\Documentos\Programming\GitHub (tamanchichan)\Projects\fast-food\fast-food\item.txt";

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
                                string description = values[2];
                                decimal price = decimal.Parse(values[3]);

                                using (SqlCommand command = new SqlCommand("InsertItem", connection))
                                {
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@id", id);
                                    command.Parameters.AddWithValue("@code", code);
                                    command.Parameters.AddWithValue("@name", name);
                                    command.Parameters.AddWithValue("@description", description);
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

            if (!context.Cart.Any())
            {
                Cart cart = new Cart()
                {
                    Id = Guid.NewGuid()
                };

                context.Cart.Add(cart);
                context.SaveChanges();
            }
        }
        private static void ItemStoreProcedure(FastFoodDb context)
        {
            string procedure =
            @"
                CREATE OR ALTER PROCEDURE InsertItem
                    @id UNIQUEIDENTIFIER,
                    @code NVARCHAR(100),
                    @name NVARCHAR(100),
                    @description NVARCHAR(100) = NULL,
                    @price DECIMAL(18,2)
                AS
                BEGIN
                    INSERT INTO Item (Id, Code, Name, Description, Price)
                    VALUES (@id, @code, @name, @description, @price);
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
