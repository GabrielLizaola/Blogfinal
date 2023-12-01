using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string? Username {  get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public bool RememberMe { get; set; }
    }
}
