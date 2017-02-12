using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset5_tehtava1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // .net core console applicationissa pitää määritellä näin, että 
            // merkistö toimii UTF8
            Console.OutputEncoding = Encoding.UTF8;

            /*
             * Toteuta pieni henkilörekisteri-ohjelma. 
             * Ohjelmassa tulee olla: 
             * 1. Person-luokka yksittäisen henkilön tietojen ylläpitämiseen ja tulostamiseen. 
             * 2. Persons-luokka henkilökokoelman ylläpitämiseen: 
             * - Persons-luokassa yksittäiset henkilöt on tallennettu persons-nimiseen List-kokoelmaan 
             * - persons-listaan tulee voida lisätä henkilöitä AddPerson-metodilla 
             * - persons-listasta tulee voida palauttaa henkilö listan alkion indeksillä GetPerson-metodilla
             * - persons-listasta tulee voida hakea henkilö henkilötunnuksen avulla FindPerson-metodilla 
             * - kaikki listan henkilöt tulee voida tulostaa tietoneen PrintData-metodilla
             * - Toteuta pääohjema Person- ja Persons-luokan ominaisuuksien testaamiseen. 
             * - Tietoja ei tarvitse kysyä käyttäjältä. 
             */

            // Alusta persons
            Persons persons = new Persons();

            #region AddPerson

            // Alusta henkilöitä
            Person p1 = new Person();
            p1.Name = "Aku Ankka";
            p1.SocialSecurityId = "AA1";

            Person p2 = new Person();
            p2.Name = "Hannu Hanhi";
            p2.SocialSecurityId = "HH2";

            Person p3 = new Person();
            p3.Name = "Roope Ankka";
            p3.SocialSecurityId = "RA3";

            // Tyhjä person
            Person p4 = null;

            // Laita listaan  henkilöt
            bool added = persons.AddPerson(p1);

            if (added)
                Console.WriteLine(string.Format("Henkilön {0} lisäys onnistui.", p1.Name));
            else
                Console.WriteLine("Henkilön lisäys epäonnistui.");

            added = persons.AddPerson(p2);

            if (added)
                Console.WriteLine(string.Format("Henkilön {0} lisäys onnistui.", p2.Name));
            else
                Console.WriteLine("Henkilön lisäys epäonnistui.");

            added = persons.AddPerson(p3);

            if (added)
                Console.WriteLine(string.Format("Henkilön {0} lisäys onnistui.", p3.Name));
            else
                Console.WriteLine("Henkilön lisäys epäonnistui.");

            added = persons.AddPerson(p4);

            if (added)
                Console.WriteLine(string.Format("Henkilön {0} lisäys onnistui.", p4.Name));
            else
                Console.WriteLine("Henkilön lisäys epäonnistui, koske henkilö oli null.");

            Console.WriteLine("");

            #endregion AddPerson

            #region GetPerson

            // Yritä lukea henkilö väärällä indexilla
            Person personWrongIndex = persons.GetPerson(5);

            if (personWrongIndex == null)
                Console.WriteLine("Huomio: Henkilöä ei löytynyt indexillä 5.");
            else
                Console.WriteLine("Huomio: Jotain meni vikaan, kun henkilöä ei pitäisi löytyä väärällä indexilla.");

            // Lue henkilö olemassa olevalla indexilla
            Person personCorrectIndex = persons.GetPerson(2);

            if (personCorrectIndex == null)
                Console.WriteLine("Huomio: Henkilöä ei löytynyt indexillä 2.");
            else
            {
                Console.WriteLine("Henkilö löytyi indexilla 2:");
                personCorrectIndex.PrintData();
            }

            #endregion GetPerson

            #region FindPerson

            // Hae henkilö väärällä sotulla
            Person personWithWrongSotu = persons.FindPerson("00");

            if (personWithWrongSotu == null)
                Console.WriteLine("Huomio: Henkilöä ei löytynyt sotulla 00.");
            else
            {
                Console.WriteLine("Henkilö löytyi sotulla 00:");
                personWithWrongSotu.PrintData();
            }

            // Hae henkilö oikealla sotulla
            Person personWithCorrectSotu = persons.FindPerson("AA1");

            if (personWithCorrectSotu == null)
                Console.WriteLine("Huomio: Henkilöä ei löytynyt sotulla AA1.");
            else
            {
                Console.WriteLine("Henkilö löytyi sotulla AA1:");
                personWithCorrectSotu.PrintData();
            }

            #endregion FindPerson

            #region PrintData

            // Tulosta listan sisältö
            persons.PrintData();

            #endregion PrintData

            Console.ReadKey();                  
        }
    }
}
