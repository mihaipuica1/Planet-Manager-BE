using System.Net;
using TeamMicroservice.Models;

namespace TeamMicroservice.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamMicroservice.DTOS;
using TeamMicroservice.Repositories;

[ApiController]
[Route("api/[controller]")]
public class CaptainController : Controller
{
    private readonly CaptainDb captainDb;

    
    public CaptainController(CaptainDb captainDb)
    {
        this.captainDb = captainDb;
    }
    
    [HttpGet("GetCaptains")]
    public async Task<ActionResult<List<CaptainDTO>>> GetCaptains()
    {
        var list = await captainDb.Captains.Select(
            s => new CaptainDTO()
            {
                Id = s.Id,
                Name = s.Name,
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
    
    [HttpGet("GetCaptainById")]
    public async Task<ActionResult<CaptainDTO>> GetCaptainById(int id)
    {
        CaptainDTO captain = await captainDb.Captains.Select(
                s => new CaptainDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                })
            .FirstOrDefaultAsync(s => s.Id == id);

        if (captain == null)
        {
            return NotFound();
        }
        else
        {
            return captain;
        }
    }
    
    [HttpPost("InsertCaptain")]
    public async Task<HttpStatusCode> InsertCaptain(CaptainDTO captain)
    {
        var entity = new Captain()
        {
            Id = captain.Id,
            Name = captain.Name,
        };

        captainDb.Captains.Add(entity);
        await captainDb.SaveChangesAsync();

        return HttpStatusCode.Created;
    }
    
    [HttpPut("UpdateCaptain")]
    public async Task<HttpStatusCode> UpdateCaptain(CaptainDTO captain)
    {
        var entity = await captainDb.Captains.FirstOrDefaultAsync(s => s.Id == captain.Id);

        entity.Name = captain.Name;

        await captainDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

    [HttpDelete("DeleteCaptain/{Id}")]
    public async Task<HttpStatusCode> DeleteCaptain(int Id)
    {
        var entity = new Captain()
        {
            Id = Id
        };
        captainDb.Captains.Attach(entity);
        captainDb.Captains.Remove(entity);
        await captainDb.SaveChangesAsync();
        return HttpStatusCode.OK;
    }

}