using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semester2FinalProject
{
    // A feladat által meghatározott interfész amit minden feladat megvalósít
    internal interface IFeladat
    {
        DateTime KezdoDatum { get; }
        int IdoIgeny { get; }
    }
}
