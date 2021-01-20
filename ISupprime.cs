using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    interface ISupprime
    {
        void Supprime(Personne p);
        void Supprime(string nom, string prenom, string adresse);
    }
}
