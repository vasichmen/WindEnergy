﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy.Lib.Data
{
    public class RawRange : BindingList<RawItem>
    {


        public string FileName { get; set; }
        

        public RawRange() {
           
        }
    
    }
}