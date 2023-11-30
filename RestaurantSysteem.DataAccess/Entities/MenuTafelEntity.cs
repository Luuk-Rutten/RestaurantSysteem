using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataAccess.Entities
{
    public class MenuTafelEntity
    {
        public int MenuId { get; set; }
        public int TafelId { get; set; }
        public int Aantal { get; set; }
    }
}
