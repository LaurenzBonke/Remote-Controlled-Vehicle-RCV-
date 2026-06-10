using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pigames.Models;

namespace Piapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly PigamesosContext _context;

        public MemberController(PigamesosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            var members = await _context.Members.ToListAsync();
            return Ok(members);
        }

        [HttpPut("Member_zuweisen/")]
        public async Task<ActionResult> PutTeam(int teamid, int memberid)
        {
            var team = await _context.Teams.FindAsync(teamid);
            if (team == null)
                return NotFound($"Team mit der Id:{teamid} existiert nicht!");

            var member = await _context.Members.FindAsync(memberid);
            if (member == null)
                return NotFound($"Member mit der Id:{memberid} existiert nicht!");

            member.TeamId = teamid;
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [HttpDelete("mitglied_löschen/")]
        public async Task<ActionResult> DeleteMember(int teamid, int memberid)
        {
            var team = await _context.Teams.FindAsync(teamid);
            if (team == null)
                return NotFound($"Team mit der Id:{teamid} existiert nicht!");

            var member = await _context.Members.FindAsync(memberid);
            if (member == null)
                return NotFound($"Member mit der Id:{memberid} existiert nicht!");

            if (member.TeamId != teamid)
                return NotFound($"Member mit der Id:{memberid} ist nicht in Team mit der Id:{teamid}!");

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}