namespace Laboratorium5.Models
{
    public interface IAlbumService
    {
        int Add(Album album);
        void Update(Album album);
        void DeleteById(Album album);
        Album? FindById(int id);
        List<Album> FindAll();
        PagingList<Album> FindPage(int page, int size);
        void IncrementNotowanie(int albumId);
    }
}