using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace _5DanaUOblacima.Models
{
   

    public class Player
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Nickname { get; set; }

        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Elo { get; set; } = 1000;
        public int HoursPlayed { get; set; } = 0;
        public Guid? TeamId { get; set; }
    }
}
