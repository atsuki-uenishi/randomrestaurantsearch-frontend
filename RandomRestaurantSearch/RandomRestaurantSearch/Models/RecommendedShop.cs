using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomRestaurantSearch.Models
{
    public class RecommendedShop
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string name { get; set; }
        public string shopName { get; set; }
        public string foodName { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string shopUrl { get; set; }
    }
}
