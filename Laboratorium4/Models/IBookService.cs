namespace Laboratorium4.Models
{
    public interface IBookService
    {
        int Add(Contact book);
        void Delete(int id);
        void Update(Contact book);
        List<Contact> FindAll();
        Contact? FindById(int id);
    }
}
