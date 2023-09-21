using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public static class VideogameManager
    {
        private static string stringConnection = "Data Source=localhost;Initial Catalog=Videogames;Integrated Security=True;Pooling=False";

        public static SqlConnection DatabaseConnection()
        {
            SqlConnection connection = new SqlConnection(stringConnection);

            return connection;
        }

        public static bool InsertNewVideogame(Videogame videogame)
        {

            using(SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO videogames (name,overview,release_date,software_house_id) VALUES (@value1, @value2, @value3, @value4);";

                    SqlCommand cmd = new SqlCommand(insertQuery, connection);
                    cmd.Parameters.Add(new SqlParameter("@value1", videogame.Name));
                    cmd.Parameters.Add(new SqlParameter("@value2", videogame.Overview));
                    cmd.Parameters.Add(new SqlParameter("@value3", videogame.ReleaseDate));
                    cmd.Parameters.Add(new SqlParameter("@value4", videogame.SoftwareHouse));

                    int insertResult = cmd.ExecuteNonQuery();

                    if(insertResult > 0)
                    {
                        return true;
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return false;
            }
        }

        public static Videogame SearchVideogameById(long id)
        {
            using(SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string searchQueryId = "SELECT id, name, overview, release_date, software_house_id FROM videogames WHERE id = @value1;";

                    using(SqlCommand cmd = new SqlCommand(searchQueryId, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@value1", id));
                        using(SqlDataReader data = cmd.ExecuteReader())
                        {
                            while(data.Read())
                            {
                                Videogame searchVideogame = new Videogame(data.GetInt64(data.GetOrdinal("id")), data.GetString(data.GetOrdinal("name")), data.GetString(data.GetOrdinal("overview")), data.GetDateTime(data.GetOrdinal("release_date")), data.GetInt64(data.GetOrdinal("software_house_id")));
                                return searchVideogame;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return null;
            }
        }

        public static List<Videogame> SearchVideogameByName(string name)
        {
            List<Videogame> videogameId = new List<Videogame>();

            name = $"%{name}%";

            using(SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string searchQueryName = "SELECT id,name, overview, release_date, software_house_id FROM videogames WHERE name LIKE(@value1);";

                    using(SqlCommand cmd = new SqlCommand(searchQueryName, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@value1", name));
                        using(SqlDataReader data = cmd.ExecuteReader())
                        {
                            while(data.Read())
                            {
                                Videogame searchVideogame = new Videogame(data.GetInt64(0), data.GetString(1), data.GetString(2), data.GetDateTime(3), data.GetInt64(4));
                                videogameId.Add(searchVideogame);
                            }
                        }
                    };

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return videogameId;
            }
        }

        public static bool DeleteVideogameById(long id)
        {
            using(SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string deleteQueryId = "DELETE FROM videogames WHERE id = @value;";

                    SqlCommand cmd = new SqlCommand(deleteQueryId, connection);
                    cmd.Parameters.Add(new SqlParameter("@value", id));

                    int deletedRow = cmd.ExecuteNonQuery();

                    if(deletedRow > 0)
                    {
                        return true;
                    }

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return false;
            }
        }

        public static List<SoftwareHouse> GetSoftwareHouses()
        {
            List<SoftwareHouse> softwareHouses = new List<SoftwareHouse>();

            using(SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string softwareHouseQuery = "SELECT id, name FROM software_houses;";

                    using(SqlCommand cmd = new SqlCommand(softwareHouseQuery, connection))
                    {
                        using(SqlDataReader data = cmd.ExecuteReader())
                        {
                            while(data.Read())
                            {
                                SoftwareHouse house = new SoftwareHouse(data.GetInt64(0), data.GetString(1));
                                softwareHouses.Add(house);
                            }
                        };
                    };

                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return softwareHouses;
        }
    }
}
