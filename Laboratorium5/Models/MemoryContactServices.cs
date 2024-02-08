namespace Laboratorium5.Models
{
    public class MemoryContactServices : IContactService
    {
        private readonly Dictionary<int, Contact> _contacts = new Dictionary<int, Contact>()
        {
            {1,new Contact() {Id=1,Name="Adam",Email="test@test.pl",Phone="123123123" } },
            {2,new Contact() {Id=2,Name="Adam2",Email="test2@test.pl",Phone="321321321" } },
        };
        private int _id=3;
        public void Add(Contact contact)
        {
            contact.Id= _id++;
            _contacts[contact.Id] = contact;
        }

        public void DeleteById(Contact contact)
        {
            _contacts.Remove(contact.Id);
        }

        public List<Contact> FindAll()
        {
            return _contacts.Values.ToList();
        }

        public Contact? FindById(int id)
        {
            return _contacts[id];
        }

        public void Update(Contact contact)
        {
            _contacts[contact.Id] = contact;
        }
    }
}
