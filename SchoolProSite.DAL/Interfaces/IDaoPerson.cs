using SchoolProSite.DAL.Entities;

namespace SchoolProSite.DAL.Interfaces
{
    public interface IDaoPerson
    {
        void SavePerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        Person GetPerson(int id);
        List<Person> GetPersons();
        bool ExistsPerson(Func<Person, bool> filter);

        List<Person> GetPersons(Func<Person, bool> filter);
    }
}
