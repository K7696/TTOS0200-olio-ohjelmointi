using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset5_tehtava2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // .net core console applicationissa pitää määritellä näin, että 
            // merkistö toimii UTF8
            Console.OutputEncoding = Encoding.UTF8;

            /*
             * - Toteuta CD-luokka, joka sisältää CD-levylle yleisesti kuuluvia ominaisuuksia: artisti, hinta ja biisit. 
             * - Biisien lukumäärä ei saa olla rajattu eli käytä apuna jotain dynaamista tietorakennetta. 
             * - Biisien kannalta kannattaa toteuttaa Biisi-luokka, jossa on käytössä yleisesti biisille kuuluvia 
             * ominaisuuksia: nimi ja pituus. 
             * - Ohjelmoi suunnittelemasi CD-luokka. 
             * - Toteuta pääohjelmassa CD-olio. 
             * - Tiedot voit keksiä itse, niitä ei tarvitse kysyä käyttäjältä. 
             * - Tulosta ruudulle CD:n tietoja. 
             * Pohjalle voit ottaa harjoituksissa 3 tehdyn vastaavan sovelluksen.
             */

            bool lisays = false;

            // Alusta cd ja täytä sille tietoja
            CD cd = new CD();
            cd.Hinta = 20.50;
            cd.Vuosi = 2017;
            cd.Nimi = "Teknologia rokkaa - Bill Gates / Steve Jobs";

            // Alusta artistit
            Artisti art1 = new Artisti();
            art1.Genre = "Pop";
            art1.Nimi = "Bill Gates";

            Artisti art2 = new Artisti();
            art2.Genre = "Rock";
            art2.Nimi = "Steve Jobs";

            // Lisää artistit cd:lle
            lisays = cd.LisaaArtisti(art1);
            lisays = cd.LisaaArtisti(art2);

            // Alusta biisit
            Biisi biisi1 = new Biisi();
            biisi1.Numero = 1;
            biisi1.Nimi = "Koodaus on ihanaa";
            biisi1.Kesto = 5;

            Biisi biisi2 = new Biisi();
            biisi2.Numero = 2;
            biisi2.Nimi = "Koodarin elämää";
            biisi2.Kesto = 3;

            Biisi biisi3 = new Biisi();
            biisi3.Numero = 3;
            biisi3.Nimi = "Bugin tarina";
            biisi3.Kesto = 4;

            // Lisää biisit cd:lle
            lisays = cd.LisaaBiisi(biisi1);
            lisays = cd.LisaaBiisi(biisi2);
            lisays = cd.LisaaBiisi(biisi3);

            // Tulosta cd:n kaikki tiedot
            cd.PrintData();

            Console.ReadKey();
        }
    }
}
