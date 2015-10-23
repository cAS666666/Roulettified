using System;
using System.Collections;

namespace Roulettified.ViewModels
{
    public class SpinViewResponseModel
    {
        public String status { get; set; }
        public String msg { get; set; }
        public int spins { get; set; }
        public int compChoice { get; set; }
        public ArrayList history { get; set; }
    }
}