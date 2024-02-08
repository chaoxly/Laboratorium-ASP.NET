using Microsoft.AspNetCore.Mvc;
using Laboratorium5;
using System.Linq;
using Microsoft.EntityFrameworkCore;

[Route("api/albums")]
[ApiController]
public class AlbumControllerApi : ControllerBase
{
    private readonly AppDbContext _context;

    public AlbumControllerApi(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetFiltered(string filter)
    {
        var filteredAlbums = _context.Albums
            .Where(a => a.Nazwa.StartsWith(filter))
            .Select(a => new { a.Id, a.Nazwa })
            .ToList();

        return Ok(filteredAlbums);
    }
}