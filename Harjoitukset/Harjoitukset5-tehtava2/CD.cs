using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Harjoitukset5_tehtava2
{
    public class CD
    {
        #region Properties

        /// <summary>
        /// Cd:n artistit
        /// </summary>
        private List<Artisti> artistit { get; set; }

        /// <summary>
        /// Cd:n biisit
        /// </summary>
        private List<Biisi> biisit { get; set; }

        /// <summary>
        /// Cd:n nimi
        /// </summary>
        public string Nimi { get; set; }

        /// <summary>
        /// Milloin levy on julkaistu
        /// </summary>
        public int Vuosi { get; set; }

        /// <summary>
        /// Levyn hinta
        /// </summary>
        public double Hinta { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public CD()
        {
            artistit = new List<Artisti>();
            biisit = new List<Biisi>();
        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Lisää cd:lle artisti
        /// </summary>
        /// <param name="artisti"></param>
        /// <returns></returns>
        public bool LisaaArtisti(Artisti artisti)
        {
            // Tyhjää ei voi lisätä
            if (artisti == null)
                return false;

            artistit.Add(artisti);

            return true;
        }

        /// <summary>
        /// Lisää cd:lle biisi
        /// </summary>
        /// <param name="biisi"></param>
        /// <returns></returns>
        public bool LisaaBiisi(Biisi biisi)
        {
            // Tyhjää ei voi lisätä
            if (biisi == null)
                return false;

            biisit.Add(biisi);

            return true;
        }

        /// <summary>
        /// Tulosta cd:n kaikki tiedot
        /// </summary>
        public void PrintData()
        {
            // Reflection (dynaamiset li
            foreach (var property in this.GetType().GetProperties())
            {
                Console.WriteLine(string.Format("{0}: {1}", property.Name, property.GetValue(this)));
            }
        }

        #endregion Public methods

    }
}
