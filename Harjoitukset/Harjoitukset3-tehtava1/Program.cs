using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // .net core console applicationissa pitää määritellä näin, että 
            // merkistö toimii UTF8
            Console.OutputEncoding = Encoding.UTF8;
            /*
                Tehtävänäsi on ohjelmoida kiukaan toiminta.Kiuas pitää pystyä laittamaan päälle ja pois, 
                sekä sen lämpötilaa että sen antamaa kosteutta pitää pystyä säätämään (arvoja ei ole rajattu).

                Suunnittele Kiuas-luokan ominaisuudet ja toiminnot UML-luokkakaaviona.

                Toteuta Kiuas-luokan ohjelmointi sekä pääohjelma, jolla luot olion Kiuas-luokasta. 
                Säädä kiuas-oliota erilaisilla arvoilla, jätä Console.WriteLine()-tulostuslauseet ohjelmaasi, 
                jotta kiuas-olion käyttäminen jää näkyville.
            */

            // Alustetaan kiuas olio
            Kiuas kiuas = new Kiuas();

            // Käynnistetään kiuas
            kiuas.KaynnistaKiuas();

            List<string> toiminnot = new List<string>();
            toiminnot.Add("0 - Näytä kiukaan tila");
            toiminnot.Add("1 - Aseta lämpötila");
            toiminnot.Add("2 - Näytä lämpötila");
            toiminnot.Add("3 - Aseta kosteus");
            toiminnot.Add("4 - Näytä kosteus");
            toiminnot.Add("5 - Sammuta kiuas");

            int toiminto = 0;
            bool parse = false;

            int lampotila = 0;
            int kosteus = 0;

            Console.WriteLine("Kiuas on käynnistetty");

            // Ajetaan niin kauan kuin kiuas on päällä
            while(kiuas.OnkoKiuasPaalla())
            {
                Console.WriteLine("Valitse toiminto:");
                foreach (var t in toiminnot)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine("Anna toiminnon numero >");

                parse = int.TryParse(Console.ReadLine(), out toiminto);

                // Tuliko kelpo arvo toiminnolle
                if(parse == false || toiminto < 0 || toiminto > 5)
                {
                    Console.WriteLine("Annoite epäkelvon toimintonumeron. Yritä uudestaan.");
                }
                else
                {
                    switch (toiminto)
                    {
                        case 0:
                            Console.WriteLine("Kiukaan tila on:");
                            Console.WriteLine(string.Format("Lämpötila: {0}", kiuas.NaytaLampotila()));
                            Console.WriteLine(string.Format("Kosteus: {0}", kiuas.NaytaKosteus()));
                            break;
                        case 1:
                            // Aseta lämpötila
                            Console.WriteLine("Anna lämpötila >");
                            parse = int.TryParse(Console.ReadLine(), out lampotila);

                            // Pyydä niin kauan kunnes tulee kunnollinen arvo
                            while (parse == false)
                            {
                                Console.WriteLine("Yritä uudestaan: Syötit epäkelvon arvon.");
                                parse = int.TryParse(Console.ReadLine(), out lampotila);
                            }

                            kiuas.AsetaLampotila(lampotila);
                            break;
                        case 2:
                            // Näytä lämpötila
                            Console.WriteLine(string.Format("Kiukaan lämpötila on {0}", kiuas.NaytaLampotila()));
                            break;
                        case 3:
                            // Aseta kosteus
                            Console.WriteLine("Anna kosteus >");
                            parse = int.TryParse(Console.ReadLine(), out kosteus);

                            // Pyydä niin kauan kunnes tulee kunnollinen arvo
                            while (parse == false)
                            {
                                Console.WriteLine("Yritä uudestaan: Syötit epäkelvon arvon.");
                                parse = int.TryParse(Console.ReadLine(), out kosteus);
                            }

                            kiuas.AsetaKosteus(kosteus);
                            break;
                        case 4:
                            // Näytä kosteus
                            Console.WriteLine(string.Format("Kiukaan kosteus on {0}", kiuas.NaytaKosteus()));
                            break;
                        case 5:
                            kiuas.SammutaKiuas();
                            break;
                    }
                    Console.WriteLine(Environment.NewLine);
                }               
            }

            Console.WriteLine("Kiuas on sammutettu. Paina enteria lopettaaksesi ohjelman suorittamisen. >");
            Console.ReadKey();
        }
    }
}
