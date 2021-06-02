using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }   
        public string Password { get; set; }
        public string retrunUrl { get; set; }
    }
}
