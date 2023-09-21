using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
                    cmd.Parameters.Add("@value1", videogame.name);
                    cmd.Parameters.Add("@value2", videogame.overview);
                    cmd.Parameters.Add("@value3", videogame.releaseDate);
                    cmd.Parameters.Add("@value4", videogame.softwareHouse);

                    int insertResult = cmd.ExecuteNonQuery();

                    if(insertResult > 0)
                    {
                        return true;
                    }

                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return false;
            }
        }

        public static Videogame SearchVideogameById(long id)
        {
            using( SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string searchQueryId = "SELECT name, overview, release_date, software_house_id FROM videogames WHERE id = @value1;";

                    using(SqlCommand cmd = new SqlCommand(searchQueryId, connection))
                    {
                        cmd.Parameters.Add("@value1", id);
                        using (SqlDataReader data = cmd.ExecuteReader())
                        {
                            while (data.Read())
                            {
                                return Videogame searchVideogame = new Videogame(data.GetString(data.GetOrdinal("name")), data.GetString(data.GetOrdinal("overview")), data.GetDateTime(data.GetOrdinal("release_date")), data.GetInt64(data.GetOrdinal("software_house_id")));
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return null;
            }
        }

        public static List<Videogame> SearchVideogameByName(string name)
        {
            List<Videogame> videogameId = new List<Videogame>();

            using(SqlConnection connection = DatabaseConnection())
            {
                try
                {
                    connection.Open();
                    string searchQueryName = "SELECT name, overview, release_date, software_house_id FROM videogames WHERE name = @value1;";

                    using(SqlCommand cmd = new SqlCommand(searchQueryName, connection))
                    {
                        cmd.Parameters.Add("@value", name);
                        using(SqlDataReader data = cmd.ExecuteReader())
                        {
                            while(data.Read())
                            {
                                Videogame searchVideogame = new Videogame(data.GetString(0), data.GetString(1), data.GetDateTime(3), data.GetInt64(4));
                                videogameId.Add(searchVideogame);
                            }
                        }
                    } ;

                }catch(Exception ex)
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
                    string deleteQueryId = "DELETE FROM videogame WHERE id = @value;";

                    SqlCommand cmd = new SqlCommand(deleteQueryId, connection);
                    cmd.Parameters.Add("@value", id);

                    int deletedRow = cmd.ExecuteNonQuery();

                    if(deletedRow > 0)
                    {
                        return true;
                    }

                }catch(Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }

                return false;
            }
        }
    }
}
