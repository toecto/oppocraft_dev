﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OppoCraft
{
    public class Task
    {
        public virtual bool Tick()
        {
            return false;
        }
    }
}
