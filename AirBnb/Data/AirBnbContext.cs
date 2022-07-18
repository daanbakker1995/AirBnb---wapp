using Microsoft.EntityFrameworkCore;

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
