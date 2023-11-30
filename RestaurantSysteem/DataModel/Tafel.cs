using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataModel
{
    public class Tafel
    {
        public Tafel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public int AantalPersonen { get; set; }
        public string Allergenen { get; set; }
        public bool WinePairing { get; set; }
        public string Voertaal { get; set; }
        public IDictionary<Menu, int> GekozenMenus { get; set; }
    }
}
