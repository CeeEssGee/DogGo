using DogGo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace DogGo.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly IConfiguration _config;

        // The constructor accepts an IConfiguration object as a parameter. This class comes from the ASP.NET framework and is useful for retrieving things out of the appsettings.json file like connection strings.
        public OwnerRepository(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public List<Owner> GetAllOwners()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT [Owner].Id AS 'OwnerId', [Owner].Name AS 'OwnerName', [Address], Email, Phone, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
                        FROM [Owner]
                        JOIN Neighborhood ON [Owner].NeighborhoodId = Neighborhood.Id
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<Owner> owners = new List<Owner>();
                        while (reader.Read())
                        {
                            Owner owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Name = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                                }
                            };

                            owners.Add(owner);
                        }

                        return owners;
                    }
                }
            }
        }

        // MYCODE
        public Owner GetOwnerById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT [Owner].Id AS 'OwnerId', [Owner].Name AS 'OwnerName', [Address], Email, Phone, NeighborhoodId, Neighborhood.Name as 'NeighborhoodName'
                        FROM [Owner]
                        JOIN Neighborhood ON [Owner].Id = Neighborhood.Id
                        WHERE [Owner].Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Owner owner = new Owner
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                                Name = reader.GetString(reader.GetOrdinal("OwnerName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId")),
                                Neighborhood = new Neighborhood
                                {
                                    Name = reader.GetString(reader.GetOrdinal("NeighborhoodName")),
                                    Id = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                                }
                            };

                            return owner;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }

        // GIVEN CODE
        //public Owner GetOwnerById(int id)
        //{
        //    using (SqlConnection conn = Connection)
        //    {
        //        conn.Open();

        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                SELECT Id, [Name], Email, Address, Phone, NeighborhoodId
        //                FROM Owner
        //                WHERE Id = @id";

        //            cmd.Parameters.AddWithValue("@id", id);

        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    Owner owner = new Owner()
        //                    {
        //                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                        Name = reader.GetString(reader.GetOrdinal("Name")),
        //                        Email = reader.GetString(reader.GetOrdinal("Email")),
        //                        Address = reader.GetString(reader.GetOrdinal("Address")),
        //                        Phone = reader.GetString(reader.GetOrdinal("Phone")),
        //                        NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
        //                    };

        //                    return owner;
        //                }

        //                return null;
        //            }
        //        }
        //    }
        //}

        public Owner GetOwnerByEmail(string email)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, [Name], Email, Address, Phone, NeighborhoodId
                        FROM Owner
                        WHERE Email = @email";

                    cmd.Parameters.AddWithValue("@email", email);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Owner owner = new Owner()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Address = reader.GetString(reader.GetOrdinal("Address")),
                                Phone = reader.GetString(reader.GetOrdinal("Phone")),
                                NeighborhoodId = reader.GetInt32(reader.GetOrdinal("NeighborhoodId"))
                            };

                            return owner;
                        }

                        return null;
                    }
                }
            }
        }

        public void AddOwner(Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Owner ([Name], Email, Phone, Address, NeighborhoodId)
                    OUTPUT INSERTED.ID
                    VALUES (@name, @email, @phoneNumber, @address, @neighborhoodId);
                ";

                    cmd.Parameters.AddWithValue("@name", owner.Name);
                    cmd.Parameters.AddWithValue("@email", owner.Email);
                    cmd.Parameters.AddWithValue("@phoneNumber", owner.Phone);
                    cmd.Parameters.AddWithValue("@address", owner.Address);
                    cmd.Parameters.AddWithValue("@neighborhoodId", owner.NeighborhoodId);

                    int id = (int)cmd.ExecuteScalar();

                    owner.Id = id;
                }
            }
        }

        public void UpdateOwner(Owner owner)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE Owner
                            SET 
                                [Name] = @name, 
                                Email = @email, 
                                Address = @address, 
                                Phone = @phone, 
                                NeighborhoodId = @neighborhoodId
                            WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@name", owner.Name);
                    cmd.Parameters.AddWithValue("@email", owner.Email);
                    cmd.Parameters.AddWithValue("@address", owner.Address);
                    cmd.Parameters.AddWithValue("@phone", owner.Phone);
                    cmd.Parameters.AddWithValue("@neighborhoodId", owner.NeighborhoodId);
                    cmd.Parameters.AddWithValue("@id", owner.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteOwner(int ownerId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Owner
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", ownerId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}