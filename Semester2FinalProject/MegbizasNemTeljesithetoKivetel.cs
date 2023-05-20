using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A feladat által meghatározott kivétel arra az esetre ha nem teljesíthető a megbízás
    internal class MegbizasNemTeljesithetoKivetel : Exception
    {
        public MegbizasNemTeljesithetoKivetel() : base() {}
    }
}
