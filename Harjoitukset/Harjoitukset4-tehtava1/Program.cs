using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset4_tehtava1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             * Tehtävänäsi on ohjelmoida Dynamon hissin kerroksen ohjaukseen liittyvä säädin. 
             * Toteutetun ohjelman tulee pystyä kysymään käyttäjältä haluttu kerros ja siirtämään 
             * hissi haluttuun kerrokseen (tässä tapauksessa ohjelma kertoo käyttäjälle missä 
             * kerroksessa hissi on). Muista, että Dynamon hissi voi olla vain kerroksissa 1-5. 
             * Käytä Hissi-luokassa get- ja set-aksessoreita suojamaan olion tila.Tehtävänäsi on 
             * ohjelmoida Dynamon hissin kerroksen ohjaukseen liittyvä säädin. Toteutetun ohjelman 
             * tulee pystyä kysymään käyttäjältä haluttu kerros ja siirtämään hissi haluttuun 
             * kerrokseen (tässä tapauksessa ohjelma kertoo käyttäjälle missä kerroksessa hissi on). 
             * Muista, että Dynamon hissi voi olla vain kerroksissa 1-5. Käytä Hissi-luokassa get- ja 
             * set-aksessoreita suojamaan olion tila.
             */

            // Luo instanssi
            Elevator elevator = new Elevator();

            int floor = 0;
            bool canContinue = false;

            while (canContinue == false)
            {
                elevator.AskFloor();
                canContinue = int.TryParse(Console.ReadLine(), out floor);

                elevator.Floor = floor;
                canContinue = false;
            }

            Console.ReadKey();
        }
    }
}
