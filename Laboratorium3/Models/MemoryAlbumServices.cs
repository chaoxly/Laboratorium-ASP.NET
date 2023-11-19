namespace Laboratorium3.Models
{
    public class MemoryAlbumServices : IAlbumService
    {
        private readonly Dictionary<int, Album> _albums= new Dictionary<int, Album>()
        {

            {1, new Album() {Id=1,Nazwa="Jeszcze 5 minut",Zespol="Kizo",Spis_piosenek=new List<string> {"Z nadzieją", "Disney","Król balu" },Notowanie=4124124,Czas_trwania=new List<TimeSpan> { new TimeSpan(0,02,41), new TimeSpan(0,03,39), new TimeSpan(0,02,41) } , Data_wydania = new DateTime(2021,11,15)} },
            {2, new Album() {Id=2,Nazwa="Uczta",Zespol="Sanah",Spis_piosenek=new List<string> {"Szary świat", "Eldorado" }, Notowanie=52323423,Czas_trwania=new List<TimeSpan> { new TimeSpan(0,03,23), new TimeSpan(0,04,11) } , Data_wydania = new DateTime(2022,08,15)}},
            



        };
        private int _id = 3;
        public void Add(Album album)
        {
            album.Id = _id++;
            _albums[album.Id] = album;
        }

        public void DeleteById(Album album)
        {
            _albums.Remove(album.Id);
        }

        public List<Album> FindAll()
        {
            return _albums.Values.OrderBy(album => album.Zespol).ToList();
        }

        public Album? FindById(int id)
        {
            return _albums[id];
        }

        public void Update(Album album)
        {
            _albums[album.Id] = album;
        }
    }
}

