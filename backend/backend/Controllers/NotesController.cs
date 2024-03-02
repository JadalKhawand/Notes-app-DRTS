using backend.Context;
using backend.Dtos.Note;
using backend.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private ApplicationDbContext _context { get; }

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> CreateNote([FromBody] NoteCreateUpdateDto dto)
        {
            var newNote = new NoteEntity()
            {
                Title = dto.Title,
                Content = dto.Content,
            };
            await _context.Notes.AddAsync(newNote);
            await _context.SaveChangesAsync();

            return Ok(newNote);

        }
        // Read

        [HttpGet]
        [Route("Get")]

        public async Task<OkObjectResult> GetNotes()
        {
            var notes = await _context.Notes.OrderByDescending(q => q.UpdatedAt).ToListAsync();
            return Ok(notes);
        }

        // Update specific Note
        [HttpPut]
        [Route("Update/{id}")]
        public async Task< IActionResult> UpdateNote( [FromRoute] int id, [FromBody] NoteCreateUpdateDto dto)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(q => q.Id == id);
            if(note is null)
            {
                return NotFound("Note not found");
            }
            note.Title = dto.Title;
            note.Content = dto.Content;
            note.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(note);   
        }

        // Delete
        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteNote([FromRoute] long id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(q => q.Id == id);
            if(note is null)
            {
                return NotFound("Note was not found");
            }
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok("Note deleted successfully");
        }


    }
}
