using Microsoft.EntityFrameworkCore;
using TeamMicroservice.Models;

namespace TeamMicroservice.Repositories;

public class CaptainDb : DbContext
{
    public CaptainDb(DbContextOptions<CaptainDb> options) : base(options) { }
    
    public virtual DbSet<Captain> Captains { get; set; }
}