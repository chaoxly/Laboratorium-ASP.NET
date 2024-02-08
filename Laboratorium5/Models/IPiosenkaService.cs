namespace Laboratorium5.Models
{        public interface IPiosenkaService
        {
            int Add(Piosenka piosenka);
            void Update(Piosenka piosenka);
            void DeleteById(Piosenka piosenka);
            Piosenka? FindById(int id);
            List<Piosenka> FindAll();
          
    }
    
}
