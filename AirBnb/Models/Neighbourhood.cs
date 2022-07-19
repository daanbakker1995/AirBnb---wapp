using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirBnb.Models
{
    public partial class Neighbourhood
    {
        public string? NeighbourhoodGroup { get; set; }
        [Key]
        public string NeighbourhoodName { get; set; } = null!;
    }
}
