using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataAccess.Entities
{
    public class MenuEntity
    {
        public int Id { get; set; }

        public string Naam { get; set; }

        public string Voorgerecht { get; set; }

        public string Tussengerecht { get; set; }

        public string Hoofdgerecht { get; set; }

        public string Nagerecht { get; set; }

    }
}
