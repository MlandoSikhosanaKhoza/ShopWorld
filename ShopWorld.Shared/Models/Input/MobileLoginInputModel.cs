﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWorld.Shared
{
    public class MobileLoginInputModel
    {
        [Required]
        [MaxLength(14)]
        public string MobileNumber { get; set; }
    }
}
