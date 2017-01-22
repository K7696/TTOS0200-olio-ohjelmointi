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
        /// </summary>
        private static void assignment3()
        {

        }

        /// <summary>
        /// Tehtävä 4
        /// </summary>
        private static void assignment4()
        {

        }

        /// <summary>
        /// Tehtävä 5
        /// </summary>
        private static void assignment5()
        {

        }

        /// <summary>
        /// Tehtävä 6
        /// </summary>
        private static void assignment6()
        {

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
