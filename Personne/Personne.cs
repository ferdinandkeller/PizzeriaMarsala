using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PizzeriaMarsala
{
    /*
     * Cette classe est abstraite, l'objet Personne n'étant pas utilisé dans l'exercice
     * On implémente l'interface IEquatable afin de vérifier si deux personnes sont les même
     * On implémente l'interface IComparable afin de pouvoir trier un tableau de personne par ordre alphabétique
     */
    abstract class Personne: IToCSV
    {

        public string Nom { get; protected set; }
        public string Prenom { get; protected set; }
        public string Adresse { get; protected set; }
        public long NumeroTel { get; protected set; }

        public Personne(string nom, string prenom, string adresse, long numero_tel)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            NumeroTel = numero_tel;
        }

        public override string ToString()
        {
            return $"{Nom} {Prenom} [{NumeroTel}] : {Adresse}";
        }

        public virtual string ToCSV()
        {
            return $"{Nom};{Prenom};{Adresse};{NumeroTel}";
        }
    }
}