namespace PlanetMicroservice.Clients.DTOS;

public class TeamDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CaptainId { get; set; }
    
    public CaptainDTO Captain { get; set; }
    public List<RobotDTO> Robots { get; set; }
}