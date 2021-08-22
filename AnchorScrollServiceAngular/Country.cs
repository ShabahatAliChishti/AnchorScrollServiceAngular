using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnchorScrollServiceAngular
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //all cities that belong to country
        public List<City> Cities { get; set; }
    }
}