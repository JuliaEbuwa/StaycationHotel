using System.Data.SqlClient;

namespace StaycationHotel.Helper
{
    public class GenerateId
    {
        private readonly string connectionString;

        public GenerateId(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public static int GetNextId()
        {
            // Your database connection string
            string connectionString = "Data Source=DECAGON\\SQLEXPRESS;Initial Catalog=StayCationHotel;Integrated Security=True;TrustServerCertificate=True;";



            // Your SQL query to get the current maximum ID
            string query = "SELECT MAX(UserID) FROM Users";



            // Create a new connection to the database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();



                // Create a new command object with your query and connection
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Execute the query and get the result
                    object result = command.ExecuteScalar();



                    // If the result is null, return 1 as the next ID
                    if (result == null || result == DBNull.Value)
                    {
                        return 1;
                    }
                    else
                    {
                        // Otherwise, parse the result as an integer and add 1 to get the next ID
                        return (int)result + 1;
                    }
                }
            }
        }
    }
}
