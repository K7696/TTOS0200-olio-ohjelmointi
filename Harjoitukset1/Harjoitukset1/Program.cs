using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset1
{
    public enum Luvut
    {
        Yksi = 1,
        Kaksi = 2,
        Kolme = 3
    }

    class Program
    {
        #region Assignments

        /// <summary>
        /// Assignment 1
        /// Tee ohjelma, joka tulostaa käyttäjän antamaa lukua (1, 2 tai 3) vastaavan 
        /// luvun sanana (yksi, kaksi tai kolme). Jos käyttäjä syöttää jonkin muun luvun, 
        /// tulee näytölle tulostaa teksti: "joku muu luku".
        /// </summary>
        private static void assignment1()
        {
            int luku;

            Console.Write("Anna luku > ");

            while (!int.TryParse(Console.ReadLine(), out luku))
            {
                Console.Write("Anna luku > ");
            }

            StringBuilder message = new StringBuilder();

            if (luku == (int)Luvut.Yksi)
            {
                message.AppendFormat("Annoit luvun {0}", Luvut.Yksi);
            }
            else if (luku == (int)Luvut.Kaksi)
            {
                message.AppendFormat("Annoit luvun {0}", Luvut.Kaksi);
            }
            else if (luku == (int)Luvut.Kolme)
            {
                message.AppendFormat("Annoit luvun {0}", Luvut.Kolme);
            }
            else
            {
                message.Append("Annoit jonku muun luvun");
            }

            Console.WriteLine(message.ToString());
        }

        /// <summary>
        /// Assignment 2
        /// Tee ohjelma, jossa annetaan oppilaalle koulunumero seuraavan taulukon mukaan 
        /// (pistemäärä kysytään ja ohjelma tulostaa numeron):
        /// </summary>
        private static void assignment2()
        {            
            Console.WriteLine("Anna pistemäärä >");
            int scores = 0;
            bool parse = int.TryParse(Console.ReadLine(), out scores); 

            if(parse == false || scores < 0 || scores > 12)
            {
                Console.WriteLine("Virhe: Pistemäärän pitää olla välillä 0 - 12.");
            }
            else
            {
                if(scores == 0 || scores == 1)
                    Console.WriteLine("Koulunumero on 0");
                else if (scores == 2 || scores == 3)
                    Console.WriteLine("Koulunumero on 1");
                else if (scores == 4 || scores == 5)
                    Console.WriteLine("Koulunumero on 2");
                else if (scores == 6 || scores == 7)
                    Console.WriteLine("Koulunumero on 3");
                else if (scores == 8 || scores == 9)
                    Console.WriteLine("Koulunumero on 4");
                else if (scores > 9 && scores < 13)
                    Console.WriteLine("Koulunumero on 5");
            }
        }

        /// <summary>
        /// Assignment 3
        /// Tee ohjelma, joka kysyy käyttäjältä kolme lukua ja tulostaa niiden summan ja keskiarvon.
        /// </summary>
        private static void assignment3()
        {
            double numb1 = 0;
            double numb2 = 0;
            double numb3 = 0;

            Console.WriteLine("Anna ensimmäinen luku >");

            bool parse = double.TryParse(Console.ReadLine(), out numb1);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa");
                Console.WriteLine("Anna ensimmäinen luku >");
                parse = double.TryParse(Console.ReadLine(), out numb1);
            }

            Console.WriteLine("Anna toinen luku >");

            parse = double.TryParse(Console.ReadLine(), out numb2);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa");
                Console.WriteLine("Anna toinen luku >");
                parse = double.TryParse(Console.ReadLine(), out numb2);
            }

            Console.WriteLine("Anna kolmas luku >");

            parse = double.TryParse(Console.ReadLine(), out numb3);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa");
                Console.WriteLine("Anna kolmas luku >");
                parse = double.TryParse(Console.ReadLine(), out numb3);
            }

            double sum = numb1 + numb2 + numb3;
            double average = sum / 3;

            Console.WriteLine(string.Format("Lukujen summa on {0} ja keskiarvo on {1}.", sum, average));
        }

        /// <summary>
        /// Assignment 4
        /// Tee ohjelma, jossa kysytään käyttäjältä tämän ikä. Jos ikä on alle 18 vuotta, 
        /// tulosta "alaikäinen", jos se on 18 - 65 vuotta, tulosta "aikuinen", muussa 
        /// tapauksessa tulosta "seniori".
        /// </summary>
        private static void assignment4()
        {
            Console.WriteLine("Anna ikäsi >");

            int age = 0;

            bool parse = int.TryParse(Console.ReadLine(), out age);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi ikä ei kelpaa, koska luvun pitää olla kokonaisluku, joka on suurempi kuin 0.");
                Console.WriteLine("Anna ikäsi >");
                parse = int.TryParse(Console.ReadLine(), out age);
            }

            if (age < 18)
                Console.WriteLine("alaikäinen");
            else if (age >= 18 && age <= 65)
                Console.WriteLine("aikuinen");
            else if (age > 65)
                Console.WriteLine("seniori");
        }

        /// <summary>
        /// Assignment 5
        /// Tee ohjelma, joka näyttää annetun sekuntimäärän tunteina, minuutteina ja sekunteina. 
        /// Aikamääre sekuntteina kysytään käyttäjältä.
        /// Anna sekunnit > 3661 [Enter]
        /// Antamasi sekunttiaika voidaan ilmaista muodossa: 1 tunti 1 minuutti 1 sekuntti
        /// </summary>
        private static void assignment5()
        {
            Console.WriteLine("Anna sekunnit >");

            int seconds = 0;

            bool parse = int.TryParse(Console.ReadLine(), out seconds);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi sekunnit eivät kelpaa, koska sen pitää olla kokonaisluku.");
                Console.WriteLine("Anna sekunnit >");
                parse = int.TryParse(Console.ReadLine(), out seconds);
            }

            string time = TimeSpan.FromSeconds(seconds).ToString();

            string[] times = time.Split(':');

            Console.WriteLine(
                string.Format("Antamasi sekunttiaika voidaan ilmaista muodossa: {0} tuntia {1} minuuttia {2} sekuntia.",
                times[0], times[1], times[2]));
        }

        /// <summary>
        /// Assignment 6
        /// Auton kulutus on 7.02 litraa 100 kilometrin matkalla ja bensan hinta on 1.595 Euroa. 
        /// Tee ohjelma, joka tulostaa ajetulla matkalla (kysytään käyttäjältä) kuluvan bensan määrän 
        /// sekä bensaan menevän rahan määrän.
        /// 
        /// Anna matka > 250 [Enter]
        /// Bensaa kuluu 17,55 litraa, kustannus 27,99 euroa
        /// </summary>
        private static void assignment6()
        {
            Console.WriteLine("Anna matka kilometreinä >");

            int kilometers = 0;

            bool parse = int.TryParse(Console.ReadLine(), out kilometers);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi kilometrit eivät kelpaa, koska niiden pitää olla annettuna kokonaislukuna.");
                Console.WriteLine("Anna matka kilometreinä >");
                parse = int.TryParse(Console.ReadLine(), out kilometers);
            }

            double consuming = 7.02;
            double gazPrice = 1.595;
            double gazConsuming = (double.Parse(kilometers.ToString()) / 100) * consuming;
            double cost = gazConsuming * gazPrice;

            Console.WriteLine(string.Format("Bensaa kuluu {0} litraa, kustannus {0} euroa", gazConsuming, cost));
            
        }

        /// <summary>
        /// Assignment 7
        /// Tee ohjelma, joka näyttää onko annettu vuosi karkausvuosi. Vuosiluku kysytään käyttäjältä.
        /// 4:llä jaolliset on, paitsi täydet vuosisadat. Kuitenkin 400:lla jaolliset vuosisadat ovat
        /// Esim. 1991 -> ei, 1992 -> on, 1900 -> ei, 2000 -> on
        /// Anna vuosi > 1992 [Enter]
        /// Vuosi on karkausvuosi.
        /// </summary>
        private static void assignment7()
        {
            Console.WriteLine("Anna vuosi >");

            int year = 0;

            bool parse = int.TryParse(Console.ReadLine(), out year);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi vuosiluku ei kelpaa, koska sen pitää olla annettuna kokonaislukuna.");
                Console.WriteLine("Anna vuosi >");
                parse = int.TryParse(Console.ReadLine(), out year);
            }

            // Case 1
            // https://msdn.microsoft.com/en-us/library/system.datetime.isleapyear(v=vs.110).aspx

            if (DateTime.IsLeapYear(year))
                Console.WriteLine("Vuosi on karkausvuosi.");
            else
                Console.WriteLine("Vuosi ei ole karkausvuosi.");

            // Case 2
            if (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))
                Console.WriteLine("Vuosi on karkausvuosi.");
            else
                Console.WriteLine("Vuosi ei ole karkausvuosi.");
        }

        /// <summary>
        /// Assignment 8
        /// Tee ohjelma, joka kysyy käyttäjältä 3 kokonaislukua ja tulostaa niistä suurimman.
        /// Anna Luku > 5 [Enter]
        /// Anna Luku > 15 [Enter]
        /// Anna Luku > 7 [Enter]
        /// Suurin luku on 15
        /// </summary>
        private static void assignment8()
        {
            int numb1 = 0;
            int numb2 = 0;
            int numb3 = 0;

            Console.WriteLine("Anna ensimmäinen luku >");

            bool parse = int.TryParse(Console.ReadLine(), out numb1);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa");
                Console.WriteLine("Anna ensimmäinen luku >");
                parse = int.TryParse(Console.ReadLine(), out numb1);
            }

            Console.WriteLine("Anna toinen luku >");

            parse = int.TryParse(Console.ReadLine(), out numb2);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa");
                Console.WriteLine("Anna toinen luku >");
                parse = int.TryParse(Console.ReadLine(), out numb2);
            }

            Console.WriteLine("Anna kolmas luku >");

            parse = int.TryParse(Console.ReadLine(), out numb3);

            while (parse == false)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa");
                Console.WriteLine("Anna kolmas luku >");
                parse = int.TryParse(Console.ReadLine(), out numb3);
            }

            List<int> numbers = new List<int>();
            numbers.Add(numb1);
            numbers.Add(numb2);
            numbers.Add(numb3);

            int greatest = numbers.Max();

            Console.WriteLine(string.Format("Suurin luku on {0}.", greatest));
        }

        /// <summary>
        /// Assignment 9
        /// Tee ohjelma, joka kysyy käyttäjältä lukuja, kunnes hän syöttää luvun 0. 
        /// Ohjelman tulee tulostaa syötettyjen lukujen summa.
        /// Anna Luku > 10 [Enter]
        /// Anna Luku > 20 [Enter]
        /// Anna Luku > 30 [Enter]
        /// Anna Luku > 0 [Enter]
        /// Lukujen summa on 60
        /// </summary>
        private static void assignment9()
        {
            List<int> numbers = new List<int>();
            int number = 0;
            bool zero = false;
            bool parse = false;

            while (zero == false)
            {
                Console.WriteLine("Anna luku >");
                parse = int.TryParse(Console.ReadLine(), out number);

                while (parse == false)
                {
                    Console.WriteLine("Virhe: Antamasi luku ei kelpaa, koska sen pitää olla kokonaisluku.");
                    Console.WriteLine("Anna luku >");
                    parse = int.TryParse(Console.ReadLine(), out number);
                }

                if(number == 0)
                {
                    zero = true;
                }
                else
                {
                    numbers.Add(number);
                    parse = false;
                }              
            }

            int sum = numbers.Sum();

            Console.WriteLine(string.Format("Lukujen summa on {0}", sum));
        }

        /// <summary>
        /// Assignment 10
        /// Tee ohjelma, joka alustaa sovellukseen käyttöö seuraavan taulukon arvot = [1,2,33,44,55,68,77,96,100]. 
        /// Käy sovelluksessa taulukko läpi ja tulosta ruutuun "HEP"-sana aina kun taulukossa oleva luku on parillinen.
        /// </summary>
        private static void assignment10()
        {
            int[] arr = new int[] {
               1,2,33,44,55,68,77,96,100
            };

            foreach (int number in arr)
            {
                if(number % 2 == 0)
                    Console.WriteLine(string.Format("HEP ja luku on {0}", number));
            }
        }

        #endregion Assignments

        static void Main(string[] args)
        {
            int[] assignments = {
                1,2,3,4,5,6,7,8,9,10,11,12
            };

            Console.WriteLine("Tervetuloa käyttämään harjoitukset 1 -ohjelmaa");
            Console.WriteLine("Valitse tehtävä syöttämällä numero:");

            foreach (int numb in assignments)
            {
                Console.WriteLine(string.Format("Tehtävä {0}", numb));
            }

            Console.WriteLine("Anna tehtävän numero: ");

            int assignment = 0;
            bool parse = int.TryParse(Console.ReadLine(), out assignment);
            
            if(parse == false || assignment < 1 || assignment > 12)
            {
                Console.WriteLine("Syötit virheellisen tehtävänumeron.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(string.Format("Käynnistit tehtävän {0}", assignment));

                switch (assignment)
                {
                    case 1:
                        assignment1();
                        break;
                    case 2:
                        assignment2();
                        break;
                    case 3:
                        assignment3();
                        break;
                    case 4:
                        assignment4();
                        break;
                    case 5:
                        assignment5();
                        break;
                    case 6:
                        assignment6();
                        break;
                    case 7:
                        assignment7();
                        break;
                    case 8:
                        assignment8();
                        break;
                    case 9:
                        assignment9();
                        break;
                    case 10:
                        assignment10();
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
