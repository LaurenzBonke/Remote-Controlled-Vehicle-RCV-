using Microsoft.AspNetCore.Mvc;
using pigames.Models;
using Microsoft.EntityFrameworkCore;    
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.AspNetCore.Identity;


namespace Piapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        // 1. Datenbank-Context einbinden (Name anpassen, falls er beim Scaffolding anders genannt wurde!)
        private readonly PigamesosContext _context;

        public TeamController(PigamesosContext context)
        {
            _context = context;
        }

        // GET-Befehl: Alle Teams holen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            // Holt alle Teams als Liste aus der Datenbank
            var teams = await _context.Teams.ToListAsync();
            return Ok(teams);
        }
        [HttpPost]
        public async Task<ActionResult> postTeam([FromBody] Team team)
        {
            var newTeam = await _context.Teams.FindAsync(team.TeamId);
            if (newTeam != null)
            {
                return BadRequest($"Team mit der Selben ID:{team.TeamId} existiert bereits");
            }

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeams), new { id = team.TeamId }, team);
        }

        [HttpPost("v2")]
        public async Task<ActionResult> postTeamv2([FromBody] Team team)
        {
            if (string.IsNullOrWhiteSpace(team.TeamName))
            {
                return BadRequest("Das Feld TeamName darf NICHT lerr sein");
            }
            var teamcheck = await _context.Teams.FindAsync(team.TeamId);
            if (teamcheck != null)
            {
                return Conflict($"Team mit der Id:{team.TeamId} exsistiert bereits!");

            }
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeams), new { id = team.TeamId }, team);
        }

        
   
        [HttpPut("UpdateTeamStammdaten")]
        public async Task<ActionResult> UpdateTeamStammdaten(int id, [FromBody] Team updatedTeam)
        {
            if (updatedTeam == null || string.IsNullOrWhiteSpace(updatedTeam.TeamName))
            {
                return BadRequest("Ungültige Daten: Der Teamname darf nicht leer sein.");
            }

            var existingTeam = await _context.Teams.FindAsync(id);
            if (existingTeam == null)
            {
                return NotFound($"Team mit der ID {id} wurde nicht gefunden.");
            }

            existingTeam.TeamName = updatedTeam.TeamName;
            await _context.SaveChangesAsync();
            return Ok(existingTeam);
        }
       




        
}
}
