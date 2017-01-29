using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // .net core console applicationissa pitää määritellä näin, että 
            // merkistö toimii UTF8
            Console.OutputEncoding = Encoding.UTF8;

            /* Luo Vehicle-luokka seuraavien tietojen mukaisesti:
             * 
             * ominaisuudet
                Name:string
                Speed:int
                Tyres:int
                toiminnot
                PrintData(), tulostaa Vehiclen ominaisuudet näytölle
                ToString():string, palauttaa kaikki Vehiclen ominaisuudet yhtenä merkkijonona
                Toteuta Vehicle-luokan ohjelmointi sekä pääohjelma, jolla luot olion Vehicle-luokasta. 
                Muuta olion arvoja ja tulosta olion arvoja konsolille.
             * 
             */

            // Alusta olio
            Vehicle veh = new Vehicle();
            veh.Name = "Datsun 100A";
            veh.Speed = 120;
            veh.Tyres = 4;

            // Printtaa kaikki ominaisuudet ulos
            veh.PrintData();

            // Palauttaa kaikki Vehiclen ominaisuudet yhtenä merkkijonona
            Console.WriteLine(string.Format("Kaikki ominaisuudet merkkijonona: {0}{1}", Environment.NewLine, veh.ToString()));


            Console.ReadKey();

        }
    }
}
