using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset5_tehtava2
{
    public class Biisi
    {
        #region Properties

        /// <summary>
        /// Biisin numero
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Biisin nimi
        /// </summary>
        public string Nimi { get; set; }

        /// <summary>
        /// Biisin kesto minuutteina
        /// </summary>
        public int Kesto { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Biisi()
        {

        }

        #endregion Constructors
    }
}
