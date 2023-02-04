namespace TeamMicroservice.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CaptainId { get; set; }
    
    public Captain Captain { get; set; }
    public List<Robot> Robots { get; set; }
}