using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSysteem.DataModel
{
    class Restaurant
    {
        public Tafel[] Tafels = new Tafel[6];

        public Restaurant()
        {
            for (int i = 0; i < Tafels.Length; i++)
                Tafels[i] = new Tafel(i + 1);
        }

    }
}
