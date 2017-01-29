using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset3_tehtava2
{
    public class Pesukone
    {
        #region Fields

        /// <summary>
        /// Lista pesuohjelmia varten
        /// </summary>
        private List<Pesuohjelma> pesuohjelmat = new List<Pesuohjelma>();

        /// <summary>
        /// Valittu pesuohjelma
        /// </summary>
        private Pesuohjelma pesuohjelma = null;

        /// <summary>
        /// Pesukone käynnissä true/false
        /// </summary>
        private bool pesukoneKaynnissa = false;

        /// <summary>
        /// Onko pesuohjelma käynnissä
        /// </summary>
        private bool pesuohjelmaKaynnissa = false;

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Oletus ctor
        /// </summary>
        public Pesukone()
        {
            alustaPesuohjelmat();
        }

        #endregion Constructors

        #region Private methods

        /// <summary>
        /// Alusta pesuohjelmat (tässä mallissa vain 2 erilaista)
        /// </summary>
        private void alustaPesuohjelmat()
        {
            // Alusta pesuohjelma 1
            pesuohjelmat.Add(new Pesuohjelma {
                Tyyppi = 1,
                Nimi = "Tehokas pesu",
                Kesto = 60,
                Linkousnopeus = 1000,
                Lampotila = 40,
                Vesimaara = 20
            });

            // Alusta pesuohjelma 2
            pesuohjelmat.Add(new Pesuohjelma
            {
                Tyyppi = 2,
                Nimi = "Hellävarainen pesu",
                Kesto = 120,
                Linkousnopeus = 500,
                Lampotila = 50,
                Vesimaara = 30
            });
        }

        #endregion Private methods

        #region Public methods

        /// <summary>
        /// Käynnistä pesukone
        /// </summary>
        public void KaynnistaPesukone()
        {
            this.pesukoneKaynnissa = true;
        }

        /// <summary>
        /// Sammuta pesukone
        /// </summary>
        public void SammutaPesukone()
        {
            this.pesukoneKaynnissa = false;
        }

        /// <summary>
        /// Onko pesukone käynnissä?
        /// </summary>
        /// <returns>true/false</returns>
        public bool OnkoPesukoneKaynnissa()
        {
            return this.pesukoneKaynnissa;
        }

        /// <summary>
        /// Näytä pesuohjelmat
        /// </summary>
        /// <returns></returns>
        public List<Pesuohjelma> NaytaPesuohjelmat()
        {
            return this.pesuohjelmat;
        }

        /// <summary>
        /// Valitse pesuohjelma
        /// </summary>
        /// <param name="tyyppi">Pesuohjelman tyyppi</param>
        public void ValitsePesuohjelma(int tyyppi)
        {
            this.pesuohjelma = pesuohjelmat.Where(x => x.Tyyppi == tyyppi)
                .FirstOrDefault();
        }

        /// <summary>
        /// Näytä valittu pesuohjelma
        /// </summary>
        /// <returns></returns>
        public Pesuohjelma NaytaValittuPesuohjelma()
        {
            return this.pesuohjelma;
        }

        /// <summary>
        /// Aloita pesuohjelma
        /// </summary>
        public void AloitaPesuohjelma()
        {
            this.pesuohjelmaKaynnissa = true;
        }

        /// <summary>
        /// Lopeta pesuohjelma
        /// </summary>
        public void LopetaPesuohjelma()
        {
            this.pesuohjelmaKaynnissa = false;
        }

        /// <summary>
        /// Onko pesuohjelma käynissä
        /// </summary>
        /// <returns>true/false</returns>
        public bool OnkoPesuohjelmaKaynnissa()
        {
            return this.pesuohjelmaKaynnissa;
        }

        #endregion Public methods
    }
}
