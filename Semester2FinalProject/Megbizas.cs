using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class Megbizas
    {
        public string Megbizo { get; }
        public string Megnevezes { get; }

        public IFeladat[] Feladatok;

        public Megbizas(string megbizo, IFeladat[] feladatok)
        {
            Megbizo = megbizo;
            Feladatok = feladatok;
        }
    }
}
