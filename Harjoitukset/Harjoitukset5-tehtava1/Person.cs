using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Harjoitukset5_tehtava1
{
    public class Person
    {
        #region Properties

        public string Name { get; set; }
        public string SocialSecurityId { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Person()
        {

        }

        #endregion Constructors

        #region Public methods

        /// <summary>
        /// Tulosta luokan tiedot
        /// </summary>
        public void PrintData()
        {
            // Reflection
            foreach (var property in this.GetType().GetProperties())
            {
                Console.WriteLine(string.Format("{0}: {1}", property.Name, property.GetValue(this)));
            }
        }

        #endregion Public methods
    }
}
