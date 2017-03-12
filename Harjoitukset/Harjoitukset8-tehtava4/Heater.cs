using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset8_tehtava4
{
    /// <summary>
    /// Toteuttaa Kiukaan toiminta. Kiukaan lämpötilaa sekä sen kosteuden arvoja pitää voida muuttaa. 
    /// Lämpötilan arvot on rajattava välille 0-120.00 ja kosteuden arvot välille 0-100.0. 
    /// Toteuta Kiuas-luokka ja erillinen käyttöliittymä.
    /// </summary>
    public class Heater
    {
        #region Properties

        private double temperature { get; set; }
        private double humidity { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Heater()
        {

        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Set temperature
        /// Temperature can be 0 - 120
        /// </summary>
        /// <param name="value">Temperature</param>
        public void SetTemperature(double value)
        {
            if(value >= 0 && value <= 120)
            {
                temperature = value;
            }
            else
            {
                throw new Exception("Info: Temperature is out of the range. Set it to 0 - 120.");
            }
        }

        /// <summary>
        /// Get temperature
        /// </summary>
        /// <returns></returns>
        public double GetTemperature()
        {
            return temperature;
        }

        /// <summary>
        /// Set humidity
        /// Humidity can be 0 - 100
        /// </summary>
        /// <param name="value">Humidity</param>
        public void SetHumidity(double value)
        {
            if (value >= 0 && value <= 100)
            {
                humidity = value;
            }
            else
            {
                throw new Exception("Info: Humidity is out of the range. Set it to 0 - 100.");
            }
        }

        /// <summary>
        /// Get humidity
        /// </summary>
        /// <returns></returns>
        public double GetHumidity()
        {
            return humidity;
        }

        #endregion Public methods
    }
}
