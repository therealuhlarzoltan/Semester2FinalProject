using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A bemeneti adatokból létrehozandó megbízás benne a feladatok tömbjével
    internal class Megbizas
    {
        public string Megbizo { get; }
        public string Megnevezes { get; }

        public IFeladat[] Feladatok;

        public Megbizas(string megbizo, string megnevezes, IFeladat[] feladatok)
        {
            Megbizo = megbizo;
            Megnevezes = megnevezes;
            Feladatok = feladatok;
        }
    }
}
