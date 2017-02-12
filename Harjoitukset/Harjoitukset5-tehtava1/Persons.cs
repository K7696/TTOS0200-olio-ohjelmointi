using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset5_tehtava1
{
    public class Persons
    {
        #region Properties

        private List<Person> PersonList { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Persons()
        {
            // Alusta lista ctorissa, että sitä ei tarvi muualla alustaa
            PersonList = new List<Person>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Lisää henkilö ja palauta tieto, että 
        /// onnistuiko lisäys
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public bool AddPerson(Person person)
        {
            if (person != null)
            {
                PersonList.Add(person);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Palauta henkilö listan alkion indexilla
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Person GetPerson(int index)
        {
            // Montako henkilöä on listassa
            int count = PersonList.Count();

            // Jos listassa ei ole yhtään henkilöä, palauta null
            if (count == 0)
                return null;
            // Jos yritetään negatiivista tai index on outOfRange
            else if (index < 0 || index > (count - 1))
                return null;
            
            // Kun tähän asti on päästy, niin listasta tulisi löytyä indexillä henkilö
            return PersonList.ElementAt(index);
        }

        /// <summary>
        /// Palauta henkilö sotun perusteella
        /// </summary>
        /// <param name="socialSecurityId"></param>
        /// <returns></returns>
        public Person FindPerson(string socialSecurityId)
        {
            // Palauttaa nullin, jos henkilöä ei löydy
            return PersonList.Where(p => p.SocialSecurityId == socialSecurityId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Tulosta listan sisältö
        /// </summary>
        public void PrintData()
        {
            Console.WriteLine("");
            Console.WriteLine("Henkilölistan sisältö:");
            Console.WriteLine("");
            foreach (var item in PersonList)
            {
                Console.WriteLine(string.Format("Henkilö: "));
                item.PrintData();
                Console.WriteLine("");
            }
        }

        #endregion Public methods

    }
}
