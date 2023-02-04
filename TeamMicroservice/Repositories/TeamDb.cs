using Microsoft.EntityFrameworkCore;
using TeamMicroservice.Models;

namespace TeamMicroservice.Repositories;

public class TeamDb : DbContext
{
    public TeamDb(DbContextOptions<TeamDb> options) : base(options) { }
    
    public virtual DbSet<Team> Teams { get; set; }
}