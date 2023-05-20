using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A feladat által nem meghatározott kivétel arra az esetre lett létrehozva, ha a bemeneti txt fájl tartalma nem megfelelő
    internal class HibasMegbizasKivetel : Exception
    {
        public HibasMegbizasKivetel() : base() { }
    }
}
