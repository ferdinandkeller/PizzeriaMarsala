using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    
    /// <summary>
    /// Cette classe est abstraite, l'objet Personne n'étant pas utilisé dans l'exercice
    /// On implémente l'interface IEquatable afin de vérifier si deux personnes sont les même
    /// On implémente l'interface IComparable afin de pouvoir trier un tableau de personne par ordre alphabétique
    /// </summary>
    /// <attributs>
    /// LastName : nom de famille
    /// FirstName : prénom
    /// Address: adresse
    /// PhoneNumber: numéro de téléphone
    /// </attributs>
    public abstract class Person: IToCSV, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;
        
        private string _LastName;
        public string LastName {
            get => _LastName;
            set
            {
                _LastName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LastName"));
            }
        }
        private string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set
            {
                _FirstName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FirstName"));
            }
        }
        private string _Address;
        public string Address
        {
            get => _Address;
            set
            {
                _Address = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Address"));
            }
        }
        private long _PhoneNumber;
        public long PhoneNumber
        {
            get => _PhoneNumber;
            set
            {
                _PhoneNumber = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PhoneNumber"));
            }
        }
        #endregion

        # region Constructeur
        public Person(string last_name, string fist_name, string address, long phone_number)
        {
            LastName = last_name;
            FirstName = fist_name;
            Address = address;
            PhoneNumber = phone_number;
        }
        #endregion

        #region Méthodes
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Méthode ToString() pour une personne
        /// </summary>
        /// <returns>
        /// NomDeFamille Prénom [NuméroDeTéléphone] : Adresse
        /// </returns>
        public override string ToString()
        {
            return $"{LastName} {FirstName} [{PhoneNumber}] : {Address}";
        }

        /// <summary>
        /// Méthode ToCSV() pour une personne
        /// </summary>
        /// <returns>
        /// NomDeFamille;Prénom;Adresse;NuméroDeTéléphone
        /// </returns>
        public virtual string ToCSV()
        {
            return $"{LastName};{FirstName};{Address};{PhoneNumber}";
        }

        /// <summary>
        /// Comparaison de deux personnes
        /// Par nom croisant puis prénom si les noms sont égaux
        /// On utilise la méthode CompareTo() sur les string
        /// </summary>
        /// <param name="p1">Première personne</param>
        /// <param name="p2">Deuxième personne</param>
        /// <returns>
        /// -1 si la personne 1 doit être rangée avant la personne 2
        /// 0 si les deux personnes ont le même nom et le même prénom
        /// 1 si la personne 1 doit être rangée après la personne 2
        /// </returns>
        public static int CompareName(Person p1, Person p2)
        {
            int comparison = p1.LastName.CompareTo(p2.LastName);
            if (comparison == 0)
            {
                comparison = p1.FirstName.CompareTo(p2.FirstName);
            }
            return comparison;
        }

        /// <summary>
        /// Comparaison par ordre alphabétique des adresses
        /// Il faut pour cela extraire la ville de l'adresse
        /// Si la ville est la même, on compare le nom de la rue
        /// Si le nom est le même, on compare le numéro
        /// </summary>
        /// <param name="p1">Première personne</param>
        /// <param name="p2">Deuxième personne</param>
        /// <returns>
        /// -1 si la personne 1 doit être rangée avant la personne 2
        /// 0 si les deux personnes ont le même nom et le même prénom
        /// 1 si la personne 1 doit être rangée après la personne 2
        /// </returns>

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
        #endregion
    }
}