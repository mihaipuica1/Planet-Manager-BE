using System.Net;
using TeamMicroservice.Models;

namespace TeamMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamMicroservice.DTOS;
using TeamMicroservice.Repositories;

[ApiController]
[Route("api/[controller]")]
public class RobotController : Controller
{
    private readonly RobotDb robotDb;

    
    public RobotController(RobotDb robotDb)
    {
        this.robotDb = robotDb;
    }
    
    [HttpGet("GetRobots")]
    public async Task<ActionResult<List<RobotDTO>>> GetRobots()
    {
        var list = await robotDb.Robots.Select(
            s => new RobotDTO
            {
                Id = s.Id,
                Name = s.Name,
                TeamId = s.TeamId,
            }
        ).ToListAsync();
    
        if (list.Count < 0)
        {
            return NotFound();
        }
        else
        {
            return list;
        }
    }
    
    [HttpGet("GetRobotById")]
    public async Task<ActionResult<RobotDTO>> GetRobotById(int id)
    {
        RobotDTO robot = await robotDb.Robots.Select(
                s => new RobotDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    TeamId = s.TeamId,
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (robot == null)
        {
            return NotFound();
        }
        else
        {
            return robot;
        }
    }
    
    [HttpPost("InsertRobot")]
    public async Task<HttpStatusCode> InsertRobot(RobotDTO robot)
    {
        var entity = new Robot()
        {
            Id = robot.Id,
            Name = robot.Name,
            TeamId = robot.TeamId
        };

        robotDb.Robots.Add(entity);
        await robotDb.SaveChangesAsync();

        return HttpStatusCode.Created;
    }
    
    [HttpPut("UpdateRobot")]
    public async Task<HttpStatusCode> UpdateRobot(RobotDTO robot)
    {
        var entity = await robotDb.Robots.FirstOrDefaultAsync(s => s.Id == robot.Id);

        entity.Name = robot.Name;

        await robotDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

    [HttpDelete("DeleteRobot/{Id}")]
    public async Task<HttpStatusCode> DeleteRobot(int Id)
    {
        var entity = new Robot()
        {
            Id = Id
        };
        robotDb.Robots.Attach(entity);
        robotDb.Robots.Remove(entity);
        await robotDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

}