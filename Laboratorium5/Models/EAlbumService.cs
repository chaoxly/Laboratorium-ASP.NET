using Microsoft.EntityFrameworkCore;

namespace Laboratorium5.Models
{
    public class EAlbumService : IAlbumService
    {
        private readonly AppDbContext _context;

        public EAlbumService(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Album album)
        {
            _context.Albums.Add(album);
            _context.SaveChanges();
            return album.Id;
        }

        public void Update(Album album)
        {
            var actualAlbum = _context.Albums.Find(album.Id);
            if (actualAlbum != null)
            {
                _context.Entry(actualAlbum).CurrentValues.SetValues(album);
                _context.SaveChanges();
            }
        }


        public void DeleteById(Album album)
        {
            _context.Albums.Remove(album);
            _context.SaveChanges();
        }

        public Album? FindById(int id)
        {
            return _context.Albums.Include(a=>a.Piosenki).FirstOrDefault(a=>a.Id == id);
        }

        public List<Album> FindAll()
        {
            return _context.Albums.Include(a => a.Piosenki).ToList();
        }
        public PagingList<Album> FindPage(int page, int size)
        {
            var query = _context.Albums.AsQueryable(); 
            var totalItems = query.Count();
            var albums = query.Skip((page - 1) * size).Take(size).ToList(); 
            return new PagingList<Album>(albums, totalItems, page, size); 
        }

        public void IncrementNotowanie(int albumId)
        {
            var album = _context.Albums.Find(albumId);
            if (album != null)
            {
                album.Notowanie++;
                _context.SaveChanges();
            }
        }

    }
}