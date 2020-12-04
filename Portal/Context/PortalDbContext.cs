using System;
using Microsoft.EntityFrameworkCore;
using Portal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Portal.Context
{
    public class PortalDbContext : IdentityDbContext<User>
    {
        public PortalDbContext()
        {
        }
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options)
        {

        }
        public virtual DbSet<CalendarInfo> CalendarInfos { get; set; }
    }
}
