using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlannerAPI.Model;

namespace PlannerAPI.Data
{
    public class PlannerAPIContext : DbContext
    {
        public PlannerAPIContext (DbContextOptions<PlannerAPIContext> options)
            : base(options)
        {
        }

        public DbSet<PlannerAPI.Model.Memo> Memo { get; set; }
    }
}
