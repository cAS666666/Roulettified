using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roulettified.Utilities
{
    public class PseudoRandomNumber
    {
        private Random prng = new Random();

        public int getRandomNumber() {
            return prng.Next(Constants.randMin, Constants.randMax);
        }
    }
}