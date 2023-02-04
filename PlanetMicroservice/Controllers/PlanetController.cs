using System.Net;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlanetMicroservice.Clients.DTOS;
using PlanetMicroservice.DTOS;
using PlanetMicroservice.Repositories;
using Newtonsoft.Json;

namespace PlanetManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanetController : Controller
{
    private readonly PlanetDb planetDb;
    private List<TeamDTO> teamList;

    public PlanetController(PlanetDb planetDb)
    {
        this.planetDb = planetDb;
        this.teamList = new List<TeamDTO>();
        GetTeams();
    }

    protected async void GetTeams()
    {
        HttpClient client = new HttpClient();
        var teamList = await client.GetStringAsync("http://localhost:5001/api/Team/GetTeams");

        List<TeamDTO> teamListDeserialized = JsonConvert.DeserializeObject<List<TeamDTO>>(teamList);
        this.teamList = teamListDeserialized;
    }
    
    [HttpGet("GetPlanets")]
    public async Task<ActionResult<List<PlanetDTO>>> Get()
    {
        GetTeams();
        var list = await planetDb.Planets.Select(
            s => new PlanetDTO
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Status = s.Status,
                TeamId = s.TeamId
            }
        ).ToListAsync();

        foreach (PlanetDTO dto in list)
        {
            dto.TeamDto = teamList.Find(team => team.Id == dto.TeamId);
        }
    
        if (list.Count < 0)
        {
            return NotFound();
        }
        else
        {
            return list;
        }
    }
    
    [HttpGet("GetPlanetById")]
    public async Task<ActionResult<PlanetDTO>> GetUserById(int id)
    {
        PlanetDTO planet = await planetDb.Planets.Select(
                s => new PlanetDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Status = s.Status,
                    TeamId = s.TeamId,
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (planet == null)
        {
            return NotFound();
        }
        else
        {
            return planet;
        }
    }
    
    [HttpPost("InsertPlanet")]
    public async Task<HttpStatusCode> InsertPlanet(PlanetDTO planet)
    {
        var entity = new Planet()
        {
            Id = planet.Id,
            Name = planet.Name,
            Description = planet.Description,
            Status = planet.Status,
            TeamId = planet.TeamId,
        };

        planetDb.Planets.Add(entity);
        await planetDb.SaveChangesAsync();

        return HttpStatusCode.Created;
    }
    
    [HttpPut("UpdatePlanet")]
    public async Task<HttpStatusCode> UpdatePlanet(PlanetDTO planet)
    {
        var entity = await planetDb.Planets.FirstOrDefaultAsync(s => s.Id == planet.Id);

        entity.Name = planet.Name;
        entity.Description = planet.Description;
        entity.Status = planet.Status;
        entity.TeamId = planet.TeamId;

        await planetDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

    [HttpDelete("DeletePlanet/{Id}")]
    public async Task<HttpStatusCode> DeletePlanet(int Id)
    {
        var entity = new Planet()
        {
            Id = Id
        };
        planetDb.Planets.Attach(entity);
        planetDb.Planets.Remove(entity);
        await planetDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
}