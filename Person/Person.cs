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
    public abstract class Person: IToCSV
    {

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public long PhoneNumber { get; set; }

        public Person(string last_name, string fist_name, string address, long phone_number)
        {
            LastName = last_name;
            FirstName = fist_name;
            Address = address;
            PhoneNumber = phone_number;
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} [{PhoneNumber}] : {Address}";
        }

        public virtual string ToCSV()
        {
            return $"{LastName};{FirstName};{Address};{PhoneNumber}";
        }

        // Comparaison (ordre alphabétique) par le nom puis le prénom si les noms sont égaux
        public static int CompareName(Person p1, Person p2)
        {
            int comparison = p1.LastName.CompareTo(p2.LastName);
            if (comparison == 0)
            {
                comparison = p1.FirstName.CompareTo(p2.FirstName);
            }
            return comparison;
        }

        /*
         * Comparaison par ordre alphabétique des adresses
         * Il faut pour cela extraire la ville de l'adresse
         * Si la ville est la même, on compare le numéro de domicile
         */
        public static int CompareTown(Person p1, Person p2)
        {
            string[] a1 = p1.Address.Split(' ');
            string[] a2 = p2.Address.Split(' ');
            int comparison = a1[a1.Length - 1].CompareTo(a2[a2.Length - 1]);
            if (comparison == 0)
            {
                comparison = a1[a1.Length - 2].CompareTo(a2[a2.Length - 2]);
                if (comparison == 0)
                {
                    comparison = (Convert.ToInt32(a1[0])).CompareTo(Convert.ToInt32(a2[0]));
                }
                
            }
            return comparison;
        }
    }
}