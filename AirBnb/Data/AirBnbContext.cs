using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AirBnb.Models;

namespace AirBnb.Data
{
    public class AirBnbContext : DbContext
    {
        public AirBnbContext (DbContextOptions<AirBnbContext> options)
            : base(options)
        {
        }

    }
}
