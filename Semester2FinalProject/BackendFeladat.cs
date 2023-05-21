using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class BackendFeladat : Feladat, ICloneable
    {
        public BackendFeladat(string megnevezes, DateTime kezdoDatum, int idoIgeny) : base(megnevezes, kezdoDatum, idoIgeny) { }

        // A feladatok másolásához használt metódus
        public object Clone()
        {
            return new BackendFeladat(Megnevezes, KezdoDatum, IdoIgeny);
        }

        
    }
}
