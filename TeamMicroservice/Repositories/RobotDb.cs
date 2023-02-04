using Microsoft.EntityFrameworkCore;
using TeamMicroservice.Models;

namespace TeamMicroservice.Repositories;

public class RobotDb : DbContext
{
    public RobotDb(DbContextOptions<RobotDb> options) : base(options) { }
    
    public virtual DbSet<Robot> Robots { get; set; }
}