using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Harjoitustyo
{
    public class Customer : Person
    {
        #region Properties

        public int Id { get; set; }

        public Address InvoicingAddress { get; set; }

        #endregion Properties
    }
}
