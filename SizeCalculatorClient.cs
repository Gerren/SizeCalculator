﻿using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SizeCalculator
{
    public class SizeCalculatorClient : LibraryClient
    {
        public override bool IsInstalled => true;

        public override void Open()
        {
            throw new NotImplementedException();
        }
    }
}