using System.ComponentModel.DataAnnotations;

namespace _5DanaUOblacima.Models
{
    public class Match
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid Team1Id { get; set; }

        [Required]
        public Guid Team2Id { get; set; }

        public Guid? WinningTeamId { get; set; }
        public int Duration { get; set; } 
    }
}
