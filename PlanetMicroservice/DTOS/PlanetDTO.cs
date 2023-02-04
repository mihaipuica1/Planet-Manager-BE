using PlanetMicroservice.Clients.DTOS;

namespace PlanetMicroservice.DTOS;

public class PlanetDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Status { get; set; }
    public int? TeamId { get; set; }
    public TeamDTO? TeamDto { get; set; }
}