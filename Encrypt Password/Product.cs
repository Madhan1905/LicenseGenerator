﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Encrypt_Password
{
    public class Product
    {
        [PrimaryKey]
        public String Barcode { get; set; }
        public String Name { get; set; }
        public String PrintName { get; set; }
        public String SellingPrice { get; set; }
        public String Cost { get; set; }
        public String MRP { get; set; }

    }
}
