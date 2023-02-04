using TeamMicroservice.Models;

namespace TeamMicroservice.DTOS;

public class TeamDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int CaptainId { get; set; }
    
    public Captain Captain { get; set; }
    public List<Robot> Robots { get; set; }
}