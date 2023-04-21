using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    internal abstract class Feladat : IFeladat
    {
        public string Megnevezes { get; }
        public DateTime KezdoDatum { get; }
        public int IdoIgeny { get; }

        public Feladat(string megnevezes, DateTime kezdoDatum, int idoIgeny)
        {
            Megnevezes = megnevezes;
            KezdoDatum = kezdoDatum;
            IdoIgeny = idoIgeny;
        }

        public bool KepesElvegezni(Beosztott beosztott)
        {
            if (beosztott.MunkaOrak >= IdoIgeny)
            {
                return true;
            }

            return false;

        }

    }
}
