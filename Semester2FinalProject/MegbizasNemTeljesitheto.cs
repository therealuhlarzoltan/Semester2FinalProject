using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class MegbizasNemTeljesitheto : Exception
    {
        public Megbizas Megbizas { get; }
        public MegbizasNemTeljesitheto(Megbizas megbizas) : base()
        {
            Megbizas = megbizas;
        }
    }
}
