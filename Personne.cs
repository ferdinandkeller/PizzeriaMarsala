using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /*
     * Cette classe est abstraite, l'objet Personne n'étant pas utilisé dans l'exercice
     * On implémente l'interface IEquatable afin de vérifier si deux personnes sont les même
     * On implémente l'interface IComparable afin de pouvoir trier un tableau de personne par ordre alphabétique
     */
    abstract class Personne: IEquatable<Personne>, IComparable<Personne>
    {

        protected string Nom { get; private set; }
        protected string Prenom { get; private set; }
        protected string Adresse { get; private set; }
        protected long NumeroTel { get; private set; }

        public Personne(string nom, string prenom, string adresse, long numero_tel)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            NumeroTel = numero_tel;
        }

        public override string ToString()
        {
            return $"{Nom} {Prenom} ({NumeroTel}) : habite au {Adresse}";
        }

        public bool Equals(Personne personne)
        {
            return Nom == personne.Nom && Prenom == personne.Prenom;
        }

        public int CompareTo(Personne personne)
        {
            int resultat_comparaison = Nom.CompareTo(personne.Nom);
            if (resultat_comparaison == 0)
            {
                resultat_comparaison = Prenom.CompareTo(personne.Prenom);
            }
            return resultat_comparaison;
        }
    }
}