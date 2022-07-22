﻿using Microsoft.AspNetCore.Mvc;

namespace AirBnb.Models
{
    public class ListingsFilterOptions
    {
        public string Neighbourhood { get; set; } = default;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 0;
        public int MinReviews { get; set; } = 0;
        public int MaxReviews { get; set; } = 0;
        public string RoomType { get; set; } = default;
    }
}
