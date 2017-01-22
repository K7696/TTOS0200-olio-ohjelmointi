using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset2
{
    public class Program
    {
        /// <summary>
        /// Tehtävä 1
        /// Mäkihypyssä käytetään viittä arvostelutuomaria. Kirjoita ohjelma, joka kysyy arvostelupisteet 
        /// yhdelle hypylle ja tulostaa tyylipisteiden summan siten, että summasta on vähennetty pois pienin 
        /// ja suurin tyylipiste.
        /// 
        /// Esimerkkitoiminta:
        /// Anna pisteet > 18 [Enter]
        /// Anna pisteet > 15 [Enter]
        /// Anna pisteet > 20 [Enter]
        /// Anna pisteet > 19 [Enter]
        /// Anna pisteet > 17 [Enter]
        /// Kokonaispisteet ovat 54
        /// </summary>
        private static void assignment1()
        {
            bool ready = false;
            bool parse = false;
            int score = 0;
            List<int> scores = new List<int>();

            // Kysy pisteitä niin kauan kunnes tulee kunnollinen luku viisi kertaa väliltä 0-20
            while (ready == false)
            {
                Console.WriteLine("Anna pisteet >");
                parse = int.TryParse(Console.ReadLine(), out score);

                // Voisin kuvitella, että "validit" pisteet mäkihypyssä ovat väliltä 0 - 20
                while (parse == false || score < 0 || score > 20)
                {
                    Console.WriteLine("Virhe: Antamasi luku ei kelpaa, koska sen pitää olla kokonaisluku väliltä 0-20.");
                    Console.WriteLine("Anna pisteet >");
                    parse = int.TryParse(Console.ReadLine(), out score);
                }

                // Lisää "validit" pisteet listaan
                scores.Add(score);

                // Jos listassa on 5 tuomarin pisteet, niin lopeta looppi
                if (scores.Count() == 5)
                {
                    ready = true;
                }
            }

            // Järjestä lista suurimmasta pienimpään
            var orderedScores = scores.OrderByDescending(x => x).ToList();

            // Poista pienin arvo
            orderedScores.Remove(orderedScores.Last());

            // Poista suurin arvo
            orderedScores.Remove(orderedScores.First());

            // Laske setti yhteen
            int sum = orderedScores.Sum();

            // Printtaa setti ulos
            Console.WriteLine(string.Format("Kokonaispisteet ovat {0}", sum));
        }

        /// <summary>
        /// Tehtävä 2
        /// Kirjoita ohjelma, joka pyytää käyttäjältä opiskelijoiden arvosanat 0-5 ohjelmointi-opintojaksosta 
        /// (voit itse päättää lopetusehdon). Tulosta arvosanajakauma käyttäen tähtimerkkejä seuraavasti:
        /// 
        /// Arvosanajakauma: 
        /// 
        /// 0:
        /// 1:****
        /// 2:**
        /// 3:******
        /// 4:*****
        /// 5:**
        /// </summary>
        private static void assignment2()
        {
            // Kysytään käyttäjältä opiskelijoiden lukumäärä
            Console.WriteLine("Anna opiskelijoiden lukumäärä >");
            int students = 0;
            bool parse = int.TryParse(Console.ReadLine(), out students);

            // Kysy niin kauan kunnes tulee kunnollinen arvo, joka on suurempi kuin nolla
            while (parse == false || students < 1)
            {
                Console.WriteLine("Virhe: Antamasi luku ei kelpaa, koska sen pitää olla kokonaisluku ja arvon suurempi kuin nolla.");
                Console.WriteLine("Anna opiskelijoiden lukumäärä >");
                parse = int.TryParse(Console.ReadLine(), out students);
            }

            // Sitten kyselemään arvosanoja
            int index = 0;
            List<int> scores = new List<int>();
            int score = 0;

            while (index < students)
            {
                Console.WriteLine(string.Format("Anna arvosana opiskelijalle {0} >", index+1));
                parse = int.TryParse(Console.ReadLine(), out score);

                // Kysy niin kauan kunnes tulee kunnollinen arvo väliltä 0-5
                while (parse == false || score < 0 || score > 5)
                {
                    Console.WriteLine("Virhe: Antamasi luku ei kelpaa, koska sen pitää olla kokonaisluku väliltä 0-5.");
                    Console.WriteLine(string.Format("Anna arvosana opiskelijalle {0} >", index+1));
                    parse = int.TryParse(Console.ReadLine(), out score);
                }

                // Lisää listaan "validi" arvosana
                scores.Add(score);
                index++;
            }

            // Arvosanaväli 0 - 5
            int count = 0;
            StringBuilder sbStars = new StringBuilder();
            sbStars.AppendFormat("Arvosanajakauma: {0}{1}", Environment.NewLine, Environment.NewLine);
            string stars = "";

            for (int i = 0; i <= 5; i++)
            {
                // Hae listasta count, että montako osuu kullekin numerolle
                count = scores.Where(x => x == i).Count();

                // Lisää tähdet
                for (int j = 0; j < count; j++)
                {
                    stars += "*";
                }

                // Lisää arvosanajakaumarivi, jossa i on arvosana, stars on jakauma per arvosana
                sbStars.AppendFormat("{0}: {1}{2}", i, stars, Environment.NewLine);

                // Tyhjennä tähdet seuraavaa arvosanajakaumaa varten
                stars = "";
            }

            // Printtaa koko setti kerrallaan ulos
            Console.WriteLine(sbStars.ToString());
        }

        /// <summary>
        /// Tehtävä 3
        /// Tee ohjelma, joka tulostaa seuraavanlaisen kuvion. Kysy käyttäjältä puunkorkeus käytä 
        /// juurena kaksi korkeuden yksikkö.
        /// </summary>
        private static void assignment3()
        {
            // Kysytään käyttäjältä puunkorkeus
            Console.WriteLine("Anna puunkorkeus parittomana kokonaislukuna >");
            int levels = 0;
            bool parse = int.TryParse(Console.ReadLine(), out levels);

            // Kysy niin kauan kunnes tulee kunnollinen arvo, joka on suurempi kuin nolla
            // Tämä ei toimi parillisella luvulla, pitää olla pariton koska tähdet ei muuten osu keskelle.
            while (parse == false || levels < 1 || levels % 2 == 0)
            {
                Console.WriteLine("Virhe: Antamasi puunkorkeus ei kelpaa, koska sen pitää olla pariton kokonaisluku ja arvon suurempi kuin nolla.");
                Console.WriteLine("Anna puunkorkeus parittomana kokonaislukuna >");
                parse = int.TryParse(Console.ReadLine(), out levels);
            }

            // Koska rivillä kasvaa aina kahdella tähtien määrä, niin levels + 2
            int totalLevels = levels + 2;

            // Laita luvut taulukkoon, jotta saadaan keskikohta selville
            int[] array = new int[totalLevels];
            for (int i = 0; i < totalLevels; i++)
            {
                array[i] = i + 1;
            }

            // Laske taulukon keskikohta
            int startingPoint = array[array.Length / 2];

            // Alussa tähtiä on vain 1
            int stars = 1;
            string starsStr = "";

            // Tänne sijoitetaan koko setti, mikä printataan kerralla ulos
            StringBuilder sbLevels = new StringBuilder();

            int index = 0;

            // Tää on kyllä ihan kauhean näköstä, mutta tuntuu toimivan...
            while (index < totalLevels)
            {
                for (int i = 0; i < levels; i++)
                {
                    if (i + 1 == startingPoint)
                    {
                        for (int j = 0; j < stars; j++)
                        {
                            starsStr += "*";
                        }

                        // Lisää rivi puuhun
                        sbLevels.AppendFormat("{0}{1}", starsStr, Environment.NewLine);
                    }
                    else
                    {
                        // Lisää tyhjä niin kauan kunnes startingPoint löytyy
                        starsStr += " ";
                    }
                }

                // Lisää 2 tähteä seuraava riviä varten
                stars += 2;

                // Tyhjennä temp-muuttuja
                starsStr = "";

                // Vähennä aloituskohtaa -1
                startingPoint--;

                // Kasvata indexia
                index++;

                // Jos seuraava kierros on toiseksi viimeinen kierros
                // Aseta starting point keskikohtaan
                // Jotta saadaan yksi tähti keskelle
                if ((index+2) == array.Length)
                {
                    startingPoint = array[array.Length / 2];
                    stars = 1;
                }
                // Jos seuraava kierros on viimeinen kierros
                // Aseta starting point keskikohtaan
                // Jotta saadaan yksi tähti keskelle
                else if ((index+1) == array.Length)
                {
                    startingPoint = array[array.Length / 2];
                    stars = 1;
                }
            }

            // Tulosta setti
            Console.WriteLine(sbLevels.ToString());
        }

        /// <summary>
        /// Tehtävä 4
        /// Tee ohjelma, joka arpoo satunnaisluvun väliltä 0-100. Tämän jälkeen ohjelman käyttäjää kehoitetaan arvaaman arvottu luku. 
        /// Ohjelman tulee antaa vihje arvauksen jälkeen onko arvottu luku pienempi vai suurempi. Tämän jälkeen vihjeitä toistetaan 
        /// kunnes käyttäjä arvaa oikean luvun. Tulosta lopuksi arvausten määrä näytölle.
        /// 
        /// Esimerkkitoiminta:
        /// 
        /// Arvaa luku > 50 [Enter]
        /// Luku on suurempi
        /// Arvaa luku > 75 [Enter]
        /// Luku on pienempi
        /// Arvaa luku > 66 [Enter]
        /// Hienoa, arvasit luvun 3 kerralla.
        /// </summary>
        private static void assignment4()
        {
            // Arvo luku väliltä 0-100
            Random random = new Random();
            int randomNumber = random.Next(0, 100);

            bool correct = false;
            bool parse = false;
            int number = 0;
            int guesses = 0;

            // Kysy niin kauan kunnes oikea luku tulee
            while (correct == false)
            {
                Console.WriteLine("Arvaa luku >");
                parse = int.TryParse(Console.ReadLine(), out number);

                // Kysy niin kauan kunnes tulee kunnollinen arvo väliltä 0-100
                while (parse == false || number < 0 || number > 100)
                {
                    Console.WriteLine("Virhe: Antamasi luku ei kelpaa, koska sen pitää olla kokonaisluku väliltä 0-100.");
                    Console.WriteLine("Arvaa luku >");
                    parse = int.TryParse(Console.ReadLine(), out number);
                }

                // Kasvata arvausten määrää
                guesses++;

                // Onko luku sama kuin random
                if(number == randomNumber)
                {
                    correct = true;
                }
                else if(number < randomNumber)
                {
                    Console.WriteLine("Arvaamasi luku on pienempi kuin muistissa oleva luku. Yritä uudestaan.");
                }
                else if (number > randomNumber)
                {
                    Console.WriteLine("Arvaamasi luku on suurempi kuin muistissa oleva luku. Yritä uudestaan.");
                }
            }

            Console.WriteLine(string.Format("Hienoa, arvasit luvun {0} kerralla.", guesses));
        }

        /// <summary>
        /// Tehtävä 5
        /// Tee ohjelma, joka lajittelee kahdessa kokonaislukutaulukossa olevat alkiot suurusjärjestykseen kolmanteen kokonaislukutaulukkoon. 
        /// Tulosta lopuksi lajitellun taulukon sisältö.
        /// 
        /// Esimerkkitoiminta:
        /// 
        /// Luvut taulukossa 1 : 10,20,30,40,50
        /// Luvut taulukossa 2 : 5,15,25,35,45
        /// Luvut yhdistetyssä taulukossa : 5,10,15,20,25,30,35,40,45,50
        /// </summary>
        private static void assignment5()
        {
            // Taulukko 1
            int[] array1 = new int[] {
                10,20,30,40,50
            };

            // Taulukko 2
            int[] array2 = new int[] {
                5,15,25,35,45
            };

            // Case 1 LINQ
            int[] array3 = array1.Union(array2).ToArray();

            // Sorttaa taulukon sisältö
            Array.Sort(array3);

            Console.WriteLine(string.Format("Luvut yhdistetyssä taulukossa ovat: {0}", string.Join(",", array3)));
        }

        /// <summary>
        /// Tehtävä 6
        /// Tee ohjelma, joka kysyy käyttäjältä merkkijonon (lause). 
        /// Sovelluksen tulee ilmoittaa käyttäjälle oliko annettu merkkijono palidromi.
        /// </summary>
        private static void assignment6()
        {
            bool ready = false;

            // Ajetaan niin kauan kunnes käyttäjä haluaa lopettaa tehtävän ajamisen
            while (ready == false)
            {
                Console.WriteLine("Ohjelma tarkastaa onko sana palindromi.");
                Console.WriteLine("Anna sana >");

                string str = Console.ReadLine();

                // Kysytään niin kauan kuin käyttäjä yrittää antaa tyhjää tai yhtä merkkiä, 
                // jolloin ei ole järkeä tarkastaa palindromia
                while (string.IsNullOrEmpty(str) || str.Length < 2)
                {
                    Console.WriteLine("Virhe: Palindromia ei voitu tarkastaa, koska annoit tyhjän sanan.");
                    Console.WriteLine("Anna sana >");
                    str = Console.ReadLine();
                }

                // Case 1 LINQ
                bool palindrome = str.SequenceEqual(str.Reverse());

                if (palindrome)
                {
                    Console.WriteLine("Sana oli palindromi");
                }
                else
                {
                    Console.WriteLine("Sana ei ollut palindromi");
                }

                Console.WriteLine("Testaa uudestaan palindromia syöttämällä k tai e (lopettaa tehtävän ajamisen) " + Environment.NewLine + "ja painamalla enteria >");
                string action = Console.ReadLine();

                while (action.ToLower().Equals("e") == false && action.ToLower().Equals("k") == false)
                {
                    Console.WriteLine("Arvaa uudestaan palindromia k tai e (lopettaa tehtävän ajamisen) " + Environment.NewLine + "ja painamalla enteria >");
                    action = Console.ReadLine();
                }

                // Jos oli e, niin lopetetaan tehtävän ajaminen
                if (action.ToLower().Equals("e"))
                {
                    ready = true;
                }
            }
        }

        /// <summary>
        /// Tehtävä 7
        /// </summary>
        private static void assignment7()
        {

        }


        public static void Main(string[] args)
        {
            // .net core console applicationissa pitää määritellä näin, että 
            // merkistö toimii UTF8
            Console.OutputEncoding = Encoding.UTF8;

            int[] assignments = {
                1,2,3,4,5,6,7
            };

            string action = "";

            Console.WriteLine("Tervetuloa käyttämään harjoitukset2 -ohjelmaa");

            bool run = true;

            while (run)
            {
                Console.WriteLine("Valitse tehtävä syöttämällä numero:");

                foreach (int numb in assignments)
                {
                    Console.WriteLine(string.Format("Tehtävä {0}", numb));
                }

                Console.WriteLine("Anna tehtävän numero: ");

                int assignment = 0;
                bool parse = int.TryParse(Console.ReadLine(), out assignment);

                if (parse == false || assignment < 1 || assignment > 7)
                {
                    Console.WriteLine("Syötit virheellisen tehtävänumeron.");
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
                    }
                }

                Console.WriteLine(Environment.NewLine + "Tehtävän ajo loppui.");
                Console.WriteLine("Palaa ohjelman alkuun syöttämällä k tai e (lopettaa ohjelman ajamisen) " + Environment.NewLine + "ja painamalla enteria >");

                action = Console.ReadLine();

                while (action.ToLower().Equals("e") == false && action.ToLower().Equals("k") == false)
                {
                    Console.WriteLine("Palaa ohjelman alkuun syöttämällä k tai e (lopettaa ohjelman ajamisen) " + Environment.NewLine + "ja painamalla enteria >");
                    action = Console.ReadLine();
                }

                if (action.ToLower().Equals("e"))
                {
                    run = false;
                }
            }


        }
    }
}
