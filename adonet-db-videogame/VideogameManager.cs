using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace adonet_db_videogame
{
    public class VideogameManager
    {
        public void InserisciVideogame(Videogame videogame)
        {
            string stringaDiConnsessione = "Data Source=localhost;Initial Catalog=Videogames;Integrated Security=True;";

            using (SqlConnection connessioneSql = new SqlConnection(stringaDiConnsessione))
            {
                try
                {
                    connessioneSql.Open();

                    // dichiarazione query da eseguire
                    string query = "INSERT INTO videogames (name, overview, release_date, created_at, updated_at, software_house_id) VALUES (@dato1 ,@dato2 ,@dato3 ,@dato4 ,@dato5 , @dato6)";
                    SqlCommand cmd = new SqlCommand(query, connessioneSql);
                    cmd.Parameters.Add(new SqlParameter("@dato1", videogame.Name));
                    cmd.Parameters.Add(new SqlParameter("@dato2", videogame.Overview));
                    cmd.Parameters.Add(new SqlParameter("@dato3", videogame.Relase_date));
                    cmd.Parameters.Add(new SqlParameter("@dato4", videogame.Create_at));
                    cmd.Parameters.Add(new SqlParameter("@dato5", videogame.Update_at));
                    cmd.Parameters.Add(new SqlParameter("@dato6", videogame.Software_house_id));

                    int affectedRows = cmd.ExecuteNonQuery();
                    Console.WriteLine("Gioco correttamente inserito");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public void CercaPerId(int id)
        {
            string stringaDiConnsessione = "Data Source=localhost;Initial Catalog=Videogames;Integrated Security=True;";
            using SqlConnection connessioneSql = new SqlConnection(stringaDiConnsessione);

            try
            {
                connessioneSql.Open();
                string query = @"SELECT *
                    FROM videogames
                    WHERE videogames.id = @value";
                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.Add(new SqlParameter("@value", id));
                using SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int indiceVideogameName = reader.GetOrdinal("name");
                    int indiceVideogameId = reader.GetOrdinal("id");
                    Console.WriteLine($"Videogame {reader.GetString(indiceVideogameName)}, ID:  {reader.GetInt64(indiceVideogameId)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void RicercaPerStringa(string parola)
        {
            string stringaDiConnsessione = "Data Source=localhost;Initial Catalog=Videogames;Integrated Security=True;";
            using SqlConnection connessioneSql = new SqlConnection(stringaDiConnsessione);

            try
            {
                connessioneSql.Open();
                string query = @"SELECT *
                    FROM videogames
                    WHERE name LIKE @nome";
                using SqlCommand cmd = new SqlCommand(query, connessioneSql);
                cmd.Parameters.AddWithValue("@nome", '%' + parola + '%');
                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int indiceVideogameName = reader.GetOrdinal("name");
                    int indiceVideogameOverw = reader.GetOrdinal("overview");
                    Console.WriteLine($"Videogame Name:  {reader.GetString(indiceVideogameName)}, Overview:  {reader.GetString(indiceVideogameOverw)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Delete(int id)
        {
            string stringaDiConnsessione = "Data Source=localhost;Initial Catalog=Videogames;Integrated Security=True;";

            using (SqlConnection connessione = new SqlConnection(stringaDiConnsessione))
            {
                try
                {
                    connessione.Open();

                    string deletePegiLabelVideogameQuery = @"DELETE FROM pegi_label_videogame WHERE videogame_id = @id;";

                    string deleteDeviceVideogameQuery = @"DELETE FROM device_videogame WHERE videogame_id = @id;";

                    string deleteCategoryVideogameQuery = @"DELETE FROM category_videogame WHERE videogame_id = @id;";

                    // Prima eliminiamo le recensioni associate al videogioco
                    string deleteReviewsQuery = "DELETE FROM reviews WHERE videogame_id = @id";

                    // Query per eliminare il videogioco
                    string deleteVideogameQuery = @"DELETE FROM videogames WHERE id = @id;";

                    string deleteTournamentAssociationsQuery = "DELETE FROM tournament_videogame WHERE videogame_id = @id";

                    using (SqlCommand deleteTournamentAssociationsCmd = new SqlCommand(deleteTournamentAssociationsQuery, connessione))
                    {
                        deleteTournamentAssociationsCmd.Parameters.AddWithValue("@id", id);
                        deleteTournamentAssociationsCmd.ExecuteNonQuery();
                    }

                    // Eseguire prima la query per eliminare le righe dalla tabella pegi_label_videogame
                    using (SqlCommand deletePegiLabelVideogameCmd = new SqlCommand(deletePegiLabelVideogameQuery, connessione))
                    {
                        deletePegiLabelVideogameCmd.Parameters.Add(new SqlParameter("@id", id));
                        deletePegiLabelVideogameCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteReviewsCmd = new SqlCommand(deleteReviewsQuery, connessione))
                    {
                        deleteReviewsCmd.Parameters.AddWithValue("@id", id);
                        deleteReviewsCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteCategoryVideogameCmd = new SqlCommand(deleteCategoryVideogameQuery, connessione))
                    {
                        deleteCategoryVideogameCmd.Parameters.Add(new SqlParameter("@id", id));
                        deleteCategoryVideogameCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteDeviceVideogameCmd = new SqlCommand(deleteDeviceVideogameQuery, connessione))
                    {
                        deleteDeviceVideogameCmd.Parameters.Add(new SqlParameter("@id", id));
                        deleteDeviceVideogameCmd.ExecuteNonQuery();
                    }

                    using (SqlCommand deleteVideogameCmd = new SqlCommand(deleteVideogameQuery, connessione))
                    {
                        deleteVideogameCmd.Parameters.Add(new SqlParameter("@id", id));
                        deleteVideogameCmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Videogioco eliminato con successo.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

    }
    
}
