using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava4
{
    public class Vehicle
    {
        #region Properties

        public string Name { get; set; }
        public int Speed { get; set; }
        public int Tyres { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Oletus ctor
        /// </summary>
        public Vehicle()
        {

        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Palauttaa kaikki Vehiclen ominaisuudet yhtenä merkkijonona
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();

            // Reflection
            foreach (var property in this.GetType().GetProperties())
            {
                str.AppendFormat("{0}:{1}", property.Name, property.GetValue(this));
            }

            return str.ToString();
        }

        /// <summary>
        /// Tulostaa Vehiclen ominaisuudet näytölle
        /// </summary>
        public void PrintData()
        {
            Console.WriteLine("Vehiclen ominaisuudet:");

            // Reflection
            foreach (var property in this.GetType().GetProperties())
            {
                Console.WriteLine(string.Format("{0}: {1}", property.Name, property.GetValue(this)));
            }

        }

        #endregion Public methods
    }
}
