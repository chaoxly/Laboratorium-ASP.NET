using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;



namespace Laboratorium5.Models
{    public class Piosenka
    {
        [HiddenInput]
        [Key]
        public int Id { get; set; }

        public string Tytul { get; set; }

        public TimeSpan Czas_trwania { get; set; }

        public int AlbumId { get; set; }
        public Album? Album { get; set; } 
    }

}
