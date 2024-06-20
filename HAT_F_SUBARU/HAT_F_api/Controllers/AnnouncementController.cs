using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HAT_F_api.Models;
using HAT_F_api.CustomModels;

namespace HAT_F_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase {
        private readonly HatFContext _context;

        public AnnouncementController(HatFContext context)
        {
            _context = context;
        }

        // GET: api/Announcement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
          if (_context.Announcements == null)
          {
              return NotFound();
          }
            return await _context.Announcements.ToListAsync();
        }


        [HttpGet("view")]
        public async Task<ApiResponse<IEnumerable<Announcement>>> getClientAnnouncements()
        {
          if (_context.Announcements == null)
          {
              return null;
          }
          return new ApiOkResponse<IEnumerable<Announcement>>(await _context.Announcements
            .Where(a => a.Displayed == true
                && a.StartDate <= DateTime.Now
                && a.EndDate >= DateTime.Now
                && a.Deleted == false)
            .OrderByDescending(a => a.ImportanceLevel)
            .ThenByDescending(a => a.StartDate)
            .ToListAsync()
            );
        }
        // GET: api/Announcement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
          if (_context.Announcements == null)
          {
              return NotFound();
          }
            var Announcement = await _context.Announcements.FindAsync(id);

            if (Announcement == null)
            {
                return NotFound();
            }

            return Announcement;
        }

        // PUT: api/Announcement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(int id, Announcement Announcement)
        {
            if (id != Announcement.AnnouncementsId)
            {
                return BadRequest();
            }

            _context.Entry(Announcement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnnouncementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Announcement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Announcement>> PostAnnouncement(Announcement Announcement)
        {
          if (_context.Announcements == null)
          {
              return Problem("Entity set 'HatFContext.Announcements'  is null.");
          }
            _context.Announcements.Add(Announcement);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AnnouncementExists(Announcement.AnnouncementsId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAnnouncement", new { id = Announcement.AnnouncementsId }, Announcement);
        }

        // DELETE: api/Announcement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            if (_context.Announcements == null)
            {
                return NotFound();
            }
            var Announcement = await _context.Announcements.FindAsync(id);
            if (Announcement == null)
            {
                return NotFound();
            }

            Announcement.Deleted = true;
            _context.Entry(Announcement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnnouncementExists(int id)
        {
            return (_context.Announcements?.Any(e => e.AnnouncementsId == id)).GetValueOrDefault();
        }
    }
}

