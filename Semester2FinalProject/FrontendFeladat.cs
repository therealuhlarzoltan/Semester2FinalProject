using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal class FrontendFeladat : Feladat, ICloneable
    {
        public FrontendFeladat(string megnevezes, DateTime kezdoDatum, int idoIgeny) : base(megnevezes, kezdoDatum, idoIgeny) { }

        public object Clone()
        {
            return new FrontendFeladat(Megnevezes, KezdoDatum, IdoIgeny);
        }
    }
}
