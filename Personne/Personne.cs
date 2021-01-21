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

        //Comparaison (ordre alphabétique) par le nom puis le prénom si les noms sont égaux
        public static int CompareNomPrenom(Personne p, Personne q)
        {
            int compa = p.Nom.CompareTo(q.Nom);
            if (compa == 0)
            {
                compa = p.Prenom.CompareTo(q.Prenom);
            }
            return compa;
        }

        /*Comparaison par ordre alphabétique des adresses
         *Il faut pour cela extraire la ville de l'adresse
         *Si la ville est la même, on compare le numéro de domicile
         */
        public static int CompareVille(Personne p, Personne q)
        {
            string[] a1 = p.Adresse.Split(' ');
            string[] a2 = p.Adresse.Split(' ');
            int compa = a1[a1.Length - 1].CompareTo(a2[a2.Length - 1]);
            if (compa == 0)
            {
                compa = (Convert.ToInt32(a1[0])).CompareTo(Convert.ToInt32(a2[0]));
            }
            return compa;
        }
    }
}