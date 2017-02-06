using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harjoitukset4_tehtava1
{
    public class Elevator
    {
        #region Fields

        private int minFloor = 1;
        private int maxFloor = 5;
        private int floor = 0;
        private bool run = true;

        #endregion Fields

        #region Properties

        public int Floor {
            get
            {
                return floor;
            }
            set
            {
                if (value < minFloor)
                {
                    Console.WriteLine("Floor is too small!");
                }
                else if(value > maxFloor)
                {
                    Console.WriteLine("Floor is too big!");
                }
                else {
                    floor = value;
                    moveElevator();
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default ctor
        /// </summary>
        public Elevator()
        {

        }

        #endregion Constructors

        #region Private methods

        private void moveElevator()
        {
            Console.WriteLine(string.Format("Elevator is now in floor: {0}", floor));
        }

        #endregion Private methods

        #region Public methods

        public void AskFloor()
        {
            Console.WriteLine("Give a new floor number (1-5)");
        }

        #endregion Public methods
    }
}
