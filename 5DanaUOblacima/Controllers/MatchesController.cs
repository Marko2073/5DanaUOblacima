using _5DanaUOblacima.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _5DanaUOblacima.Controllers
{
    [Route("matches")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public MatchesController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllMatches()
        {
            var matches = _context.Matches.ToList();
            return Ok(matches);
        }

        [HttpPost]
        public IActionResult CreateMatch([FromBody] Match match)
        {
            var team1 = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == match.Team1Id);
            var team2 = _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == match.Team2Id);

            if (team1 == null || team2 == null)
                return BadRequest("One or both teams not found.");

            if (match.Duration < 1)
                return BadRequest("Match duration must be at least 1 hour.");

            if (match.WinningTeamId != null)
            {
                var isTeam1Winner = match.WinningTeamId == team1.Id;
                CalculateEloForTeams(team1, team2, isTeam1Winner, match.Duration);
            }

            _context.Matches.Add(match);
            _context.SaveChanges();
            return Ok(match);
        }

        private void CalculateEloForTeams(Team team1, Team team2, bool isTeam1Winner, int duration)
        {
            var team1Elo = team1.Players.Average(p => p.Elo);
            var team2Elo = team2.Players.Average(p => p.Elo);

            foreach (var player in team1.Players)
            {
                var result = isTeam1Winner ? 1 : 0;
                UpdatePlayerElo(player, team2Elo, result, duration);
            }

            foreach (var player in team2.Players)
            {
                var result = isTeam1Winner ? 0 : 1;
                UpdatePlayerElo(player, team1Elo, result, duration);
            }
        }

        private void UpdatePlayerElo(Player player, double opponentElo, int result, int duration)
        {
            var expectedScore = 1 / (1 + Math.Pow(10, (opponentElo - player.Elo) / 400));
            var kFactor = player.HoursPlayed switch
            {
                < 500 => 50,
                < 1000 => 40,
                < 3000 => 30,
                < 5000 => 20,
                _ => 10
            };
            player.Elo += (int)(kFactor * (result - expectedScore));
            player.HoursPlayed += duration;

            if (result == 1) player.Wins++;
            else player.Losses++;
        }
    }
}
