﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doamin.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
    }
}
