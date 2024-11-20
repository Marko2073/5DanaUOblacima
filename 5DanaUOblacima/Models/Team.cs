using System.ComponentModel.DataAnnotations;

namespace _5DanaUOblacima.Models
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TeamName { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
