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
                        JOIN Neighborhood ON [Owner].Id = Neighborhood.Id
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
                                email = reader.GetString(reader.GetOrdinal("Email")),
                                phone = reader.GetString(reader.GetOrdinal("Phone")),
                                address = reader.GetString(reader.GetOrdinal("Address")),
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
                                email = reader.GetString(reader.GetOrdinal("Email")),
                                phone = reader.GetString(reader.GetOrdinal("Phone")),
                                address = reader.GetString(reader.GetOrdinal("Address")),
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
    }
}