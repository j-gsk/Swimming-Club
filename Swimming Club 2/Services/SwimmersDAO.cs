using Swimming_Club_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Swimming_Club_2.Services
{
    class SwimmersDAO : ISwimmerDataService
    {
        readonly string connectionString = @" Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SwimmingClub;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        
        public IEnumerable<Swimmer> GetAll()
        {
            List<Swimmer> allSwimmers = new List<Swimmer>();

            string statement = "SELECT Id, FirstName, LastName, DOB, Registration, EmailAddress FROM dbo.Swimmers";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(statement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allSwimmers.Add(new Swimmer
                        {
                            Id = (int)reader[0],
                            FirstName = (string)reader[1],
                            LastName = (string)reader[2],
                            DOB = (DateTime)reader[3],
                            Registration = (string)reader[4],
                            EmailAddress = (string)reader[5]
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return allSwimmers;
        }

        public Swimmer Delete(int id)
        {
            int x = -1;
            string sqlQuery = "DELETE FROM dbo.Swimmers WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                try
                {
                    connection.Open();
                    x = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return GetOne(id);
        } 

        public Swimmer GetOne(int Id)
        {
            Swimmer swimmer = null;
            string sqlCommnadText = "SELECT * FROM dbo.Swimmers WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlCommnadText, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = Id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        swimmer = new Swimmer
                        {
                            Id = (int)reader[0],
                            FirstName = Convert.ToString(reader[1]),
                            LastName = Convert.ToString(reader[2]),
                            DOB = Convert.ToDateTime(reader[3]),
                            Registration = Convert.ToString(reader[4]),
                            EmailAddress = Convert.ToString(reader[5]),
                            
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return swimmer;
        }

        public Swimmer Insert(Swimmer swimmer)
        {
            int x = -1;
            string sqlQuery = "INSERT INTO dbo.Swimmers(FirstName, LastName, DOB, Registration, EmailAddress) VALUES (@FirstName, @LastName, @DOB, @Registration, @EmailAddress)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                //command.Parameters.Add("@product.Id", System.Data.SqlDbType.Int).Value = product.Id;
                //command.Parameters.Add("@Name", System.Data.SqlDbType.NChar, 50).Value = product.Name;
                //command.Parameters.Add("@Price", System.Data.SqlDbType.Decimal).Value = product.Price;
                //command.Parameters.Add("@Description", System.Data.SqlDbType.Int).Value = product.Description;
                command.Parameters.AddWithValue("@FirstName", swimmer.FirstName);
                command.Parameters.AddWithValue("@LastName", swimmer.LastName);
                command.Parameters.AddWithValue("@DOB", swimmer.DOB);
                command.Parameters.AddWithValue("@Registration", swimmer.Registration);
                command.Parameters.AddWithValue("@EmailAddress", swimmer.EmailAddress);


                try
                {
                    connection.Open();
                    x = Convert.ToInt32(command.ExecuteScalar());

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return swimmer;

            }
        }

        public Swimmer Update(Swimmer swimmer)
        {          

            string sqlCommnadText = "UPDATE dbo.Swimmers SET FirstName = @FirstName, LastName = @LastName, DOB = @DOB, Registration = @Registration, EmailAddress = @EmailAddress WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlCommnadText, connection);
                command.Parameters.AddWithValue("@Id", swimmer.Id);
                command.Parameters.AddWithValue("@FirstName", swimmer.FirstName);
                command.Parameters.AddWithValue("@LastName", swimmer.LastName);
                command.Parameters.AddWithValue("@DOB", swimmer.DOB);
                command.Parameters.AddWithValue("@Registration", swimmer.Registration);
                command.Parameters.AddWithValue("@EmailAddress", swimmer.EmailAddress);
                
                try
                {
                    connection.Open();
                    int x = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return this.GetOne(swimmer.Id);
        }

        public IEnumerable<Swimmer> SearchByLastName(string name)
        {
            List<Swimmer> swimmersFound = new List<Swimmer>();

            string statement = "SELECT * FROM dbo.Swimmers WHERE LastName LIKE @lastName";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(statement, connection);
                command.Parameters.AddWithValue("@lastName", '%' + name + '%');

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        swimmersFound.Add(new Swimmer
                        {
                            Id = (int)reader[0],
                            FirstName = (string)reader[1],
                            LastName = (string)reader[2],
                            DOB = (DateTime)reader[3],
                            Registration = (string)reader[4],
                            EmailAddress = (string)reader[5]

                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return swimmersFound;
        }       

        public List<Discipline> GetPRsBySwimmerId(int swimmerId)
        {
            List<Discipline> output = new List<Discipline>();
            
            string statement = "SELECT DisciplineId, Time FROM dbo.Disciplines WHERE SwimmerId = @SwimmerId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(statement, connection);
                command.Parameters.AddWithValue("@SwimmerId", swimmerId);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Discipline discipline = new Discipline();
                        
                        discipline.DisciplineId = (string)reader[0];
                        discipline.Time = TimeSpan.FromMilliseconds((int)reader[1]);

                        output.Add(discipline);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }                
            }
            return output;
        }

    }
}