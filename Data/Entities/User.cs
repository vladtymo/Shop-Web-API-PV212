﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class User : IdentityUser
    {
        // add custom properties
        public DateTime? Birthdate { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<RefreshToken>? RefreshTokens { get; set; }

    }
}
