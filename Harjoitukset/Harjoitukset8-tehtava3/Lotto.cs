using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harjoitukset8_tehtava3
{
    public class Lotto
    {
        #region Properties
        /// <summary>
        /// Example: int randomNumber = random.Next(0, 1000);
        /// </summary>
        private Random randomizer = new Random();

        // Luokan sisäiset propertyt
        private readonly int lotteryStart = 1;
        private readonly int lotteryEnd = 39;
        private readonly int vikingLotteryStart = 1;
        private readonly int vikingLotteryEnd = 49;

        // Luokan julkiset propertyt
        public readonly int lotteryCharacterCount = 7;
        public readonly int vikingLotteryCharacterCount = 6;

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Lotto()
        {
            // Alusta tarvittavat 
            randomizer = new Random();
        }

        #endregion // Constructors

        #region Private methods

        /// <summary>
        /// Generoi lottorivi
        /// </summary>
        /// <param name="lotteryCharacterCount">6 = Viking-lotto, 7 = Tavallinen lotto</param>
        /// <param name="lotteryStart">Mistä numerosta aloitetaan arpominen</param>
        /// <param name="lotteryEnd">Mihin numeroon lopetetaan arpominen</param>
        /// <returns>Lottorivi</returns>
        private List<int> _getLotteryRow(int lotteryCharacterCount, int lotteryStart, int lotteryEnd)
        {
            List<int> lotteryRow = new List<int>();
            int number = 0;

            for (int i = 0; i < lotteryCharacterCount; i++)
            {
                // Tee niin kauan kuin lottorivi sisältää arvotun numeron
                do
                {
                    number = randomizer.Next(lotteryStart, lotteryEnd);
                } while (lotteryRow.Contains(number));

                // Lisätään lottoriviin uusi numero
                lotteryRow.Add(number);
            }

            return lotteryRow;
        }

        #endregion // Private methods

        #region Public methods

        /// <summary>
        /// Palauta loton rivi
        /// </summary>
        /// <param name="type">1 = Tavallinen lotto, 2 = Viking-lotto</param>
        /// <return>Lottorivi</return>
        public List<int> GenerateLottoRow(int type)
        {
            if (type == 1)
                return _getLotteryRow(lotteryCharacterCount, lotteryStart, lotteryEnd);
            else if (type == 2)
                return _getLotteryRow(vikingLotteryCharacterCount, vikingLotteryStart, vikingLotteryEnd);

            return null;
        }

        #endregion // Public methods
    }
}
