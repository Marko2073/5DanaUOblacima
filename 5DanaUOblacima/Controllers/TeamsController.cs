using _5DanaUOblacima.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TeamsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            var teams = _context.Teams.Select(t => new
            {
                t.Id,
                t.TeamName,
                Players = t.Players.Select(p => new
                {
                    p.Id,
                    p.Nickname,
                    p.Wins,
                    p.Losses,
                    p.Elo,
                    p.HoursPlayed,
                    p.TeamId
                }).ToList()
            }).ToList();

            return Ok(teams);
        }



        [HttpPost]
        public IActionResult CreateTeam([FromBody] Team team)
        {
            if (_context.Teams.Any(t => t.TeamName == team.TeamName))
                return Conflict("Team name must be unique.");

            if (team.Players.Count != 5)
                return BadRequest("A team must have exactly 5 players.");

            var playerIds = team.Players.Select(p => p.Id).ToList();
            var players = _context.Players.Where(p => playerIds.Contains(p.Id)).ToList();

            if (players.Count != 5)
                return BadRequest("One or more players not found.");

            if (players.Any(p => p.TeamId != null))
                return BadRequest("One or more players are already in a team.");

            foreach (var player in players)
            {
                player.TeamId = team.Id;
            }

            team.Players = players;
            _context.Teams.Add(team);
            _context.SaveChanges();
            return Ok(team);
        }

        [HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
        {
            var team = _context.Teams
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    t.Id,
                    t.TeamName,
                    Players = t.Players.Select(p => new
                    {
                        p.Id,
                        p.Nickname,
                        p.Wins,
                        p.Losses,
                        p.Elo,
                        p.HoursPlayed,
                        p.TeamId
                    })
                })
                .FirstOrDefault();

            if (team == null)
                return NotFound();

            return Ok(team);
        }
    }
}
