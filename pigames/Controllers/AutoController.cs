using Microsoft.AspNetCore.Mvc;
using pigames.Models;
using Microsoft.EntityFrameworkCore;

namespace Piapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly PigamesosContext _context;

        public AutoController(PigamesosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetAutos()
        {
            var members = await _context.Vehicles.ToListAsync();
            return Ok(members);
        }


        [HttpPut("auto_zuweisen")]
        public async Task<ActionResult> putAuto([FromBody] int teamid, int autoid)
        {
            // schaut es das team gibt
            var team = await _context.Teams.FindAsync(teamid);
            if (team == null)
            {
                return NotFound();
            }
            // schaut ob es das auto findet
            var auto = await _context.Vehicles.FindAsync(autoid);
            if (auto == null)
            {
                return NotFound();
            }
            // Prüfung: Ist das Auto bereits einem anderen Team zugewiesen?
            if (auto.TeamId != teamid)
            {
                return Conflict($"Das Auto ist bereits dem Team {auto.TeamId} zugewiesen");
            }
            // Prüfung ob das Team bereits ein anderes Auto hat 
            var teamHatSchonEinAuto = await _context.Vehicles.AnyAsync(v => v.TeamId == teamid && v.VehicleId != autoid);
            if (teamHatSchonEinAuto)
            {
                return Conflict("Das team hat schon ein Auto");
            }

            auto.TeamId = teamid;
            await _context.SaveChangesAsync();
            return Ok(team);

        }

    }
}
