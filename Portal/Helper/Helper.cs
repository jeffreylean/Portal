using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Portal.Models;

namespace Portal.Helper
{
    public class Helper
    {
        private readonly UserManager<User> _userManager;
        public Helper(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
    }  
}
