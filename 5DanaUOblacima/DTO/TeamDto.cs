using System.ComponentModel.DataAnnotations;

namespace _5DanaUOblacima.DTO
{
    public class TeamDto
    {
        public string TeamName { get; set; }
        public List<Guid> players { get; set; } = new List<Guid>();
    }


}
