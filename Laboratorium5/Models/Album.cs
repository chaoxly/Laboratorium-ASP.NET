using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;



namespace Laboratorium5.Models
{
    public class Album
    {

        public int Id { get; set; }

        public string Nazwa { get; set; }//

        public string Zespol { get; set; }//

        public int Notowanie { get; set; }//

        [DataType(DataType.Date)]
        public DateTime Data_wydania { get; set; }  //
        public ICollection<Piosenka> Piosenki { get; set; } = new HashSet<Piosenka>();
    }

   

}
