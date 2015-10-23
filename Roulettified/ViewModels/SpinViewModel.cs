using Roulettified.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Roulettified.ViewModels
{
    public class SpinViewModel
    {
        [Required]
        [Range(Constants.randMin, Constants.randMax)]
        public int choice { get; set; }
    }
}