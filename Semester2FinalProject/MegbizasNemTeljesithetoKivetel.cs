using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class MegbizasNemTeljesithetoKivetel : Exception
    {
        public Megbizas Megbizas { get; }
        public MegbizasNemTeljesithetoKivetel(Megbizas megbizas) : base()
        {
            Megbizas = megbizas;
        }
    }
}
