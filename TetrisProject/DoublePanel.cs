﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class DoublePanel : System.Windows.Forms.Panel
    {
        public DoublePanel()
        {
            DoubleBuffered = true;
        }
    }
}
