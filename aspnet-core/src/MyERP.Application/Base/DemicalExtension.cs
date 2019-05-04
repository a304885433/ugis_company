using System;
using System.Collections.Generic;
using System.Text;

namespace MyERP.Base
{
   public static class DemicalExtension
    {
        public static decimal Round(this decimal val)
        {
            return Math.Round(val, 2);
        }
    }
}
