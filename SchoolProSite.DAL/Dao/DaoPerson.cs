using SchoolProSite.DAL.Context;
using SchoolProSite.DAL.Entities;
using SchoolProSite.DAL.Enums;
using SchoolProSite.DAL.Exceptions;
using SchoolProSite.DAL.Interfaces;
using System.Linq;

namespace SchoolProSite.DAL.Dao
{
    public class DaoPerson : IDaoPerson
    {
        private readonly SchoolContext _context;

        public DaoPerson(SchoolContext _context)
        {
            this._context = _context;
        }

        public void SavePerson(Person person)
        {
            string message = string.Empty;

            if (!IsPersonValid(person, ref message, Operations.Save))
                throw new DaoException(message);

            this._context.People.Add(person);
            this._context.SaveChanges();
        }
        public void UpdatePerson(Person person)
        {
            string message = string.Empty;

            if (!IsPersonValid(person, ref message, Operations.Update))
                throw new DaoException(message);

            Person personToUpdated = this.GetPerson(person.PersonId);

            personToUpdated.LastName = person.LastName;
            personToUpdated.FirstName = person.FirstName;
            personToUpdated.Discriminator = person.Discriminator;

            this._context.People.Update(personToUpdated);
            this._context.SaveChanges();
        }
        public void DeletePerson(Person person)
        {
            Person personToRemove = this.GetPerson(person.PersonId);

            this._context.People.Update(personToRemove);
            this._context.SaveChanges();
        }
        public Person GetPerson(int id)
        {
            return this._context.People.Find(id);
        }
        public List<Person> GetPersons()
        {
            return this._context.People.ToList();
        }
        public bool ExistsPerson(Func<Person, bool> filter)
        {
            return this._context.People.Any(filter);
        }

        public List<Person> GetPersons(Func<Person, bool> filter)
        {
            return this._context.People.Where(filter).ToList();
        }


        private bool IsPersonValid(Person person, ref string message, Operations operations)
        {
            bool result = false;

            if (string.IsNullOrEmpty(person.LastName))
            {
                message = "El primer nombre es requerido";
                return result;
            }
            if (person.LastName.Length > 50)
            {
                message = "El primer nombre no puede ser mayor a 50 caracteres";
                return result;
            }
            if (string.IsNullOrEmpty(person.FirstName))
            {
                message = "El segundo nombre es requerido";
                return result;
            }
            if (person.FirstName.Length > 50)
            {
                message = "El segundo nombre no puede ser mayor a 50 caracteres";
                return result;
            }
            if (string.IsNullOrEmpty(person.Discriminator))
            {
                message = "La descriminacion es requerido";
                return result;
            }
            if (person.Discriminator.Length > 50)
            {
                message = "La discrimitacion no puede ser mayor a 50 caracteres";
                return result;
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
