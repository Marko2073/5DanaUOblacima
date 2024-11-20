using _5DanaUOblacima.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PlayersController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPlayers()
        {
            var players = _context.Players.Select(p => new
            {
                p.Id,
                p.Nickname,
                p.Wins,
                p.Losses,
                p.Elo,
                p.HoursPlayed,
                p.TeamId
            }).ToList();

            return Ok(players);
        }

        [HttpPost("create")]
        public IActionResult CreatePlayer([FromBody] Player player)
        {
            if (_context.Players.Any(p => p.Nickname == player.Nickname))
            {
                throw new System.Exception("Nickname must be unique.");
                
                return Conflict("Nickname must be unique.");
                

            }
                

            _context.Players.Add(player);
            _context.SaveChanges();
            return Ok(player);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlayer(Guid id)
        {
            var player = _context.Players.Find(id);
            if (player == null)
                return NotFound();

            return Ok(player);
        }
    }
}
