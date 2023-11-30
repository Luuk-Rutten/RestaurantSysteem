using RestaurantSysteem.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataAccess
{
    public class TafelRepository
    {
        private readonly string _connectionString;

        public TafelRepository()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restaurantsysteem;Connect Timeout=30;Encrypt=False";
        }

        public TafelEntity Get(int id)
        {
            var query = $"SELECT * FROM Tafel WHERE Id = {id}";

            var result = new List<TafelEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new TafelEntity
                    {
                        Id = reader.GetInt32(0),
                        Naam = reader.GetString(1),
                        AantalPersonen = reader.GetInt32(2),
                        Allergenen = reader.GetString(3),
                        WinePairing = reader.GetBoolean(4),
                        Voertaal = reader.GetString(5),
                    });
                }

                reader.Close();
            }

            return result.FirstOrDefault();
        }

        public void Update(TafelEntity entity)
        {
            var query = $"UPDATE Tafel " +
                $"SET AantalPersonen = {entity.AantalPersonen} " +
                $", Allergenen = '{entity.Allergenen}' " +
                $", Winepairing = {Convert.ToInt32(entity.WinePairing)} " +
                $", Voertaal = '{entity.Voertaal}'" +
                $"WHERE Id = {entity.Id};";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public IEnumerable<MenuTafelEntity> GetAll()
        {
            var query = $"SELECT * FROM MenuTafel;";

            var result = new List<MenuTafelEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new MenuTafelEntity
                    {
                        MenuId = reader.GetInt32(0),
                        TafelId = reader.GetInt32(1),
                        Aantal = reader.GetInt32(2),
                    });
                }

                reader.Close();
            }

            return result;
        }

        public void InsertOrUpdateMenuTafel(MenuTafelEntity entity)
        {
            var query = @"
                MERGE INTO MenuTafel AS target
                USING (VALUES (@MenuId, @TafelId, @Aantal)) AS source (MenuId, TafelId, Aantal)
                ON target.MenuId = source.MenuId AND target.TafelId = source.TafelId
                WHEN MATCHED THEN
                    UPDATE SET target.Aantal = source.Aantal
                WHEN NOT MATCHED THEN
                    INSERT (MenuId, TafelId, Aantal) VALUES (source.MenuId, source.TafelId, source.Aantal);";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@MenuId", entity.MenuId);
                command.Parameters.AddWithValue("@TafelId", entity.TafelId);
                command.Parameters.AddWithValue("@Aantal", entity.Aantal);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }

        public void DeleteMenuTafel(int menuId, int tafelId)
        {
            var query = @"DELETE FROM MenuTafel WHERE TafelId = @TafelId AND MenuId = @MenuId";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TafelId", tafelId);
                command.Parameters.AddWithValue("@MenuId", menuId);
                connection.Open();

                command.ExecuteNonQuery();
            }
        }
    }
}
