using System.Net;
using TeamMicroservice.Models;

namespace TeamMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamMicroservice.DTOS;
using TeamMicroservice.Repositories;

[ApiController]
[Route("api/[controller]")]
public class TeamController : Controller
{
    private readonly TeamDb teamDb;


    
    public TeamController(TeamDb teamDb)
    {
        this.teamDb = teamDb;
    }
    
    [HttpGet("GetTeams")]
    public async Task<ActionResult<List<TeamDTO>>> GetTeams()
    {
        var list = await teamDb.Teams.Select(
            s => new TeamDTO
            {
                Id = s.Id,
                Name = s.Name,
                CaptainId = s.CaptainId,
                Captain = s.Captain,
                Robots = s.Robots
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
    
    [HttpGet("GetTeamById")]
    public async Task<ActionResult<TeamDTO>> GetTeamById(int id)
    {
        TeamDTO team = await teamDb.Teams.Select(
                s => new TeamDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    CaptainId = s.CaptainId,
                    Captain = s.Captain,
                    Robots = s.Robots
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (team == null)
        {
            return NotFound();
        }
        else
        {
            return team;
        }
    }
    
    [HttpPost("InsertTeam")]
    public async Task<HttpStatusCode> InsertTeam(TeamDTO team)
    {
        var entity = new Team()
        {
            Id = team.Id,
            Name = team.Name,
            CaptainId = team.CaptainId
        };

        teamDb.Teams.Add(entity);
        await teamDb.SaveChangesAsync();

        return HttpStatusCode.Created;
    }
    
    [HttpPut("UpdateTeam")]
    public async Task<HttpStatusCode> UpdateTeam(TeamDTO team)
    {
        var entity = await teamDb.Teams.FirstOrDefaultAsync(s => s.Id == team.Id);

        entity.Name = team.Name;

        await teamDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

    [HttpDelete("DeleteTeam/{Id}")]
    public async Task<HttpStatusCode> DeleteTeam(int Id)
    {
        var entity = new Team()
        {
            Id = Id
        };
        teamDb.Teams.Attach(entity);
        teamDb.Teams.Remove(entity);
        await teamDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

}