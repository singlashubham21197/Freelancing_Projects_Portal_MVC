using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Freelancing_Projects_Portal_MVC.Models;

namespace Freelancing_Projects_Portal_MVC.Data
{
    public class Freelancing_Projects_Portal_DBContext : DbContext
    {
        public Freelancing_Projects_Portal_DBContext (DbContextOptions<Freelancing_Projects_Portal_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Freelancing_Projects_Portal_MVC.Models.Client> Client { get; set; }

        public DbSet<Freelancing_Projects_Portal_MVC.Models.Bid> Bid { get; set; }

        public DbSet<Freelancing_Projects_Portal_MVC.Models.Developer> Developer { get; set; }

        public DbSet<Freelancing_Projects_Portal_MVC.Models.Project> Project { get; set; }
    }
}
