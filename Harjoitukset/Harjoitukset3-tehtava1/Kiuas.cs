using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava1
{
    public class Kiuas
    {
        #region Fields

        /// <summary>
        /// Onko kiuas päällä - true / false
        /// </summary>
        private bool kiuasPaalla = false;

        /// <summary>
        /// Lämpötila
        /// </summary>
        private int lampotila = 0;

        /// <summary>
        /// Kosteus
        /// </summary>
        private int kosteus = 0;

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Oletus ctor
        /// </summary>
        public Kiuas()
        {

        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Aseta kiuas päälle
        /// </summary>
        public void KaynnistaKiuas()
        {
            this.kiuasPaalla = true;
        }

        /// <summary>
        /// Sammuta kiuas
        /// </summary>
        public void SammutaKiuas()
        {
            this.kiuasPaalla = false;
        }

        /// <summary>
        /// Tarkasta onko kiuas päällä
        /// </summary>
        /// <returns>true/false</returns>
        public bool OnkoKiuasPaalla()
        {
            return this.kiuasPaalla;
        }

        /// <summary>
        /// Aseta lämpötila
        /// </summary>
        /// <param name="value"></param>
        public void AsetaLampotila(int value)
        {
            this.lampotila = value;
        }

        /// <summary>
        /// Näytä kiukaan lämpötila
        /// </summary>
        /// <returns></returns>
        public int NaytaLampotila()
        {
            return this.lampotila;
        }

        /// <summary>
        /// Aseta kosteus
        /// </summary>
        /// <param name="value"></param>
        public void AsetaKosteus(int value)
        {
            this.kosteus = value;
        }

        /// <summary>
        /// Näytä kosteus
        /// </summary>
        public int NaytaKosteus()
        {
            return this.kosteus;
        }

        #endregion Public methods
    }
}
