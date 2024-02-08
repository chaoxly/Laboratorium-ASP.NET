using Laboratorium5.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorium5.Models
{
    public class MemoryAlbumServices : IPiosenkaService
    {
        private readonly AppDbContext _context;

        public MemoryAlbumServices(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Piosenka piosenka)
        {
            _context.Songs.Add(piosenka);
            _context.SaveChanges();
            return piosenka.Id;
        }
        public void DeleteById(Piosenka piosenka)
        {
            _context.Songs.Remove(piosenka);
            _context.SaveChanges();
        }

        public List<Piosenka> FindAll()
        {
            return _context.Songs.Include(s => s.Album).ToList();
        }


        public Piosenka? FindById(int id)
        {
            return _context.Songs.FirstOrDefault(a => a.Id == id);

        }

        public void Update(Piosenka piosenka)
        {
            var actualSong = _context.Songs.Find(piosenka.Id);
            if (actualSong != null)
            {
                _context.Entry(actualSong).CurrentValues.SetValues(piosenka);
                _context.SaveChanges();
            }

        }
       

    }
}

