using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataAccess.Entities
{
    public class TafelEntity
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public int AantalPersonen { get; set;}
        public string Allergenen { get; set; }
        public bool WinePairing { get; set; }
        public string Voertaal { get; set; }
    }
}
