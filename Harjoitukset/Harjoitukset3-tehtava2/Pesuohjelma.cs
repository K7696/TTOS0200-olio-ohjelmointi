using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava2
{
    public class Pesuohjelma
    {
        #region Fields

        #endregion Fields

        #region Properties

        /// <summary>
        /// Pesuohjelman tyyppi
        /// </summary>
        public int Tyyppi { get; set; }

        /// <summary>
        /// Pesuohjelman nimi
        /// </summary>
        public string Nimi { get; set; }

        /// <summary>
        /// Pesuohjelman kesto sekunteina (ei ole järkeä tässä laittaa tunnin ohjelmaa)
        /// </summary>
        public int Kesto { get; set; }

        /// <summary>
        /// Pesuohjelman linkousnopeus
        /// </summary>
        public int Linkousnopeus { get; set; }

        /// <summary>
        /// Pesuohjelman käyttämä vesimäärä
        /// </summary>
        public int Vesimaara { get; set; }

        /// <summary>
        /// Pesuohjelman veden lämpötila
        /// </summary>
        public int Lampotila { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Oletus ctor
        /// </summary>
        public Pesuohjelma()
        {

        }

        #endregion Constructors

        #region Public methods



        #endregion Public methods
    }
}
