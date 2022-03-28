using Swimming_Club_2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Swimming_Club_2.Services
{
    public class AdministrationDAO
    {
        string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=aspnet-SwimmingClub-2F155C4C-613A-462B-8FFD-695C35811A33;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public List<Role> GetAll()
        {
            List<Role> rolesFound = new List<Role>();

            string statement = "SELECT * FROM dbo.AspNetRoles";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(statement, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        rolesFound.Add(new Role
                        {
                            Id = (string)reader[0],
                            Name = (string)reader[1]                            
                        });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return rolesFound;
        }

        public int Insert(Role role)
        {
            int x = -1;

            string statement = "INSERT INTO dbo.AspNetRoles (Id, Name) VALUES (@Id, @name)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(statement, connection);
                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@name", role.Name);
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
            return x;
        }
        public Role FindById(string Id)
        {
            Role role = null;
            string statement = "SELECT * FROM dbo.AspNetRoles WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(statement, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar).Value = Id;
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        role = new Role
                        {
                            Id = Convert.ToString(reader[1]),
                            Name = Convert.ToString(reader[2]),
                        };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return role;
        }

        public int Update(Role role)
        {
            int x = -1;

            string sqlCommnadText = "UPDATE dbo.AspNetRoles SET Name = @Name WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlCommnadText, connection);
                command.Parameters.AddWithValue("@Id", role.Id);
                command.Parameters.AddWithValue("@Name", role.Name);             

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
            return x;
        }

        public int Delete(string Id)
        {
            int x = -1;
            string sqlQuery = "DELETE FROM dbo.AspNetRoles WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.Add("@Id", System.Data.SqlDbType.NVarChar).Value = Id;
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
            return x;
        }


    }
}
