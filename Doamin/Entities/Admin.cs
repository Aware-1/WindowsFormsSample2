﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsLimit { get; set; }
    }

}
