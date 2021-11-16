using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomRestaurantSearch.Models
{
    public class FavoriteShop
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string address { get; set; }
        public string logo_image { get; set; }
        public string name { get; set; }
        
        public string url { get; set; }
    }
}
