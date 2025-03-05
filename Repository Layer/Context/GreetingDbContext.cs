using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository_Layer.Entity;

namespace Repository_Layer.Context
{
    public class GreetingDbContext:DbContext
    {
        public GreetingDbContext(DbContextOptions<GreetingDbContext> options) : base(options) { }

        public virtual DbSet<GreetingEntity> Users { get; set; } 

        

    }
}
