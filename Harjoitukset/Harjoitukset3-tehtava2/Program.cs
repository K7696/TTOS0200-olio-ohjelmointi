using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // .net core console applicationissa pitää määritellä näin, että 
            // merkistö toimii UTF8
            Console.OutputEncoding = Encoding.UTF8;

            /*
             * Tehtävänäsi on ohjelmoida pesukoneen toiminta. Samoin kuin edellinen tehtävä: 
             * mitä ominaisuuksia ja toimintoja tekisit Pesukone-luokkaan?
             * 
             * Suunnittele Pesukone-luokan ominaisuudet ja toiminnot UML-luokkakaaviona.
             * 
             * Toteuta Pesukone-luokan ohjelmointi sekä pääohjelma, jolla luot olion Pesukone-luokasta. 
             * Säädä pesukone-oliota erilaisilla arvoilla, jätä Console.WriteLine()-tulostuslauseet ohjelmaasi, 
             * jotta pesukone-olion käyttäminen jää näkyville. Toteuta Pesukone-luokkaan muutamia erilaisia 
             * konstruktoreita ja alusta niitä käyttämällä oliota pääohjelmasta käsin.
             */

            // Alusta pesukoneolio
            Pesukone pesukone = new Pesukone();

            // Käynnistä pesukone
            pesukone.KaynnistaPesukone();

            Console.WriteLine("Pesukone on käynnistetty");

            // Lista toiminnoista
            List<string> toiminnot = new List<string>();
            toiminnot.Add("0 - Näytä pesukoneen tila");
            toiminnot.Add("1 - Näytä pesuohjelmat");
            toiminnot.Add("2 - Aseta pesuohjelma");
            toiminnot.Add("3 - Aloita pesuohjelma");
            toiminnot.Add("4 - Lopeta pesuohjelma");
            toiminnot.Add("5 - Sammuta pesukone");

            // Apumuuttujat
            int toiminto = 0;
            int pesuohjelmanTyyppi = 0;
            bool parse = false;

            // Ajetaan niin kauan kuin pesukone on käynnissä
            while (pesukone.OnkoPesukoneKaynnissa())
            {
                Console.WriteLine("Valitse toiminto:");
                foreach (var t in toiminnot)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine("Anna toiminnon numero >");

                parse = int.TryParse(Console.ReadLine(), out toiminto);

                // Tuliko kelpo arvo toiminnolle
                if (parse == false || toiminto < 0 || toiminto > 5)
                {
                    Console.WriteLine("Annoite epäkelvon toimintonumeron. Yritä uudestaan.");
                }
                else
                {
                    switch (toiminto)
                    {
                        case 0:
                            // Näytä pesukoneen tila

                            Console.WriteLine("Pesukoneen tila");

                            if (pesukone.NaytaValittuPesuohjelma() == null)
                                Console.WriteLine("Pesuohjelma: Ei ole valittu.");
                            else
                            {
                                Console.WriteLine(string.Format("Valittu pesuohjelma: {0}-{1}", 
                                    pesukone.NaytaValittuPesuohjelma().Tyyppi, 
                                    pesukone.NaytaValittuPesuohjelma().Nimi));

                                if (pesukone.OnkoPesuohjelmaKaynnissa())
                                    Console.WriteLine("Pesun tila: Pesuohjelma on käynnissä.");
                                else
                                    Console.WriteLine("Pesun tila: Pesuohjelma ei ole käynnissä.");
                            }
                            break;
                        case 1:
                            // Näytä mahdolliset pesuohjelmat

                            Console.WriteLine("Mahdolliset pesuohjelmat:");
                            foreach(var pesuohjelma in pesukone.NaytaPesuohjelmat())
                            {
                                Console.WriteLine(string.Format("{0} - {1}", pesuohjelma.Tyyppi, pesuohjelma.Nimi));
                            }
                            break;
                        case 2:
                            // Valitse pesuohjelma

                            Console.WriteLine("Valitse pesuohjelmat:");
                            foreach (var pesuohjelma in pesukone.NaytaPesuohjelmat())
                            {
                                Console.WriteLine(string.Format("{0} - {1}", pesuohjelma.Tyyppi, pesuohjelma.Nimi));
                            }

                            Console.WriteLine("Anna pesuohjelman numero >");

                            parse = int.TryParse(Console.ReadLine(), out pesuohjelmanTyyppi);

                            // Pyydä niin kauan kunnes tulee kunnollinen arvo
                            while (parse == false)
                            {
                                Console.WriteLine("Yritä uudestaan: Syötit epäkelvon arvon.");
                                parse = int.TryParse(Console.ReadLine(), out pesuohjelmanTyyppi);
                            }

                            // Valitse pesuohjelma
                            pesukone.ValitsePesuohjelma(pesuohjelmanTyyppi);

                            // Tarkasta onnistuiko pesuohjelman valinta
                            if (pesukone.NaytaValittuPesuohjelma() == null)
                                Console.WriteLine("Pesuohjelman valinta epäonnistui :(");
                            else
                                Console.WriteLine("Pesuohjelman valinta onnistui :)");
                            break;
                        case 3:
                            // Aloita pesuohjelma

                            // Jos pesuohjelmaa ei ole valittu -> näytä virhe
                            if (pesukone.NaytaValittuPesuohjelma() == null)
                                Console.WriteLine("Huomio: Valitse ensin pesuohjelma");
                            else
                            {
                                // Jos pesuohjelma on käynnissä, niin ei voi aloittaa uutta 
                                // ennenkuin ajossa oleva on ohi tai lopetettu
                                if (pesukone.OnkoPesuohjelmaKaynnissa())
                                    Console.WriteLine("Huomio: Yksi pesuohjelma on jo käynnissä. Katso pesukoneen tilatoiminto.");
                                else
                                {
                                    pesukone.AloitaPesuohjelma();
                                    Console.WriteLine("Huomio: Pesuohjelma käynnistettiin :)");
                                }
                            }

                            break;
                        case 4:
                            // Lopeta pesuohjelma

                            if (pesukone.OnkoPesuohjelmaKaynnissa() == false)
                                Console.WriteLine("Huomio: Pesuohjelmaa ei ole käynnistetty");
                            else
                            {
                                pesukone.LopetaPesuohjelma();
                                Console.WriteLine("Huomio: Pesuohjelma lopetettiin :)");
                            }
                            break;
                        case 5:
                            // Sammuta pesukone

                            pesukone.SammutaPesukone();
                            break;
                    }

                    Console.WriteLine(Environment.NewLine);
                }
            }

            Console.WriteLine("Pesukone on sammutettu. Paina enteria lopettaaksesi ohjelman suorittamisen >");
            Console.ReadKey();
        }
    }
}
