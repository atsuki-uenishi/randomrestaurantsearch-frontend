using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace RandomRestaurantSearch.Models
{

    public class Rootobject
    {
        public Results results { get; set; }
    }

    public class Results
    {
        public Shop[] shop { get; set; }
    }

    public class Shop
    {
        [JsonIgnore]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string address { get; set; }
        public string logo_image { get; set; }
        public string name { get; set; }
        public Urls urls { get; set; }
    }

    public class Urls
    {
        public string pc { get; set; }
    }

}