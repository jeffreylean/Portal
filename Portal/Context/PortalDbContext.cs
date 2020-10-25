using System;
using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.Context
{
    public class PortalDbContext :DbContext
    {
        public PortalDbContext()
        {
        }
        public PortalDbContext(DbContextOptions<PortalDbContext> options):base(options)
        {
           
        }
        public virtual DbSet<CalendarInfo> CalendarInfos { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
