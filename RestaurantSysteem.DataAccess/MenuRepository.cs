using RestaurantSysteem.DataAccess.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataAccess
{
    public class MenuRepository
    {
        private readonly string _connectionString;
        public MenuRepository()
        {
            _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Restaurantsysteem;Connect Timeout=30;Encrypt=False";
        }

        public IEnumerable<MenuEntity> GetAll()
        {
            var query = $"SELECT m.Id, m.Name, vg.Naam, tg.Naam, hg.Naam, ng.Naam FROM Menu m " +
                $"JOIN Gerecht vg " +
                $"ON m.Voorgerecht = vg.GerechtId " +
                $"JOIN Gerecht tg " +
                $"ON m.Tussengerecht = tg.GerechtId " +
                $"JOIN Gerecht hg " +
                $"ON m.Hoofdgerecht = hg.GerechtId " +
                $"JOIN Gerecht ng " +
                $"ON m.Nagerecht = ng.GerechtId;";

            var result = new List<MenuEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new MenuEntity
                    {
                        Id = reader.GetInt32(0),
                        Naam = reader.GetString(1),
                        Voorgerecht = reader.GetString(2),
                        Tussengerecht = reader.GetString(3),
                        Hoofdgerecht = reader.GetString(4),
                        Nagerecht = reader.GetString(5)
                    });
                }

                reader.Close();
            }

            return result;
        }

        public MenuEntity Get(int id)
        {
            var query = $"SELECT m.Id, m.Name, vg.Naam, tg.Naam, hg.Naam, ng.Naam FROM Menu m " +
                $"JOIN Gerecht vg " +
                $"ON m.Voorgerecht = vg.GerechtId " +
                $"JOIN Gerecht tg " +
                $"ON m.Tussengerecht = tg.GerechtId " +
                $"JOIN Gerecht hg " +
                $"ON m.Hoofdgerecht = hg.GerechtId " +
                $"JOIN Gerecht ng " +
                $"ON m.Nagerecht = ng.GerechtId" +
                $"WHERE Id = {id}";
            var result = new List<MenuEntity>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new MenuEntity
                    {
                        Id = reader.GetInt32(0),
                        Naam = reader.GetString(1),
                        Voorgerecht = reader.GetString(2),
                        Tussengerecht = reader.GetString(3),
                        Hoofdgerecht = reader.GetString(4),
                        Nagerecht = reader.GetString(5),
                    });
                }

                reader.Close();
            }

            return result.FirstOrDefault();
        }

        //Method Signature
        // AccModifier  ReturnType  MethodName(paramType paramName, ...){}
        // public       menuEntity  GetMenu(int id)
        // {
        //body
        // }

    }
}
