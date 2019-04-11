﻿using System.Collections.Generic;

namespace POS_SP.Models
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; }
    }
}