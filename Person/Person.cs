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
    /// On implémente l'interface IToCSV car on peut convertir une personne en CSV
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
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="last_name">Le nom de la personne</param>
        /// <param name="fist_name">Le prénom de la personne</param>
        /// <param name="address">L'adresse de la personne</param>
        /// <param name="phone_number">Le numéro de téléphone de la personne</param>
        public Person(string last_name, string fist_name, string address, long phone_number)
        {
            LastName = last_name;
            FirstName = fist_name;
            Address = address;
            PhoneNumber = phone_number;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Cette fonction permet aux classes filles d'utiliser l'interface INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName">Le nom de la propriété</param>
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

        #region Méthode CSV
        /// <summary>
        /// Méthode ToCSV() pour une personne
        /// </summary>
        /// <returns>
        /// La personne au format CSV
        /// </returns>
        public virtual string ToCSV()
        {
            return $"{LastName};{FirstName};{Address};{PhoneNumber}";
        }
        #endregion

        #region Méthodes de comparaison
        /// <summary>
        /// Comparaison de deux personnes
        /// Par nom croisant puis prénom si les noms sont égaux
        /// On utilise la méthode CompareTo() sur les string
        /// </summary>
        /// <param name="person1">Première personne</param>
        /// <param name="person2">Deuxième personne</param>
        /// <returns>
        /// -1 si la personne 1 doit être rangée avant la personne 2
        /// 0 si les deux personnes ont le même nom et le même prénom
        /// 1 si la personne 1 doit être rangée après la personne 2
        /// </returns>
        public static int CompareName(Person person1, Person person2)
        {
            int comparison = person1.LastName.CompareTo(person2.LastName);
            if (comparison == 0)
            {
                comparison = person1.FirstName.CompareTo(person2.FirstName);
            }
            return comparison;
        }

        /// <summary>
        /// Comparaison par ordre alphabétique des adresses
        /// Il faut pour cela extraire la ville de l'adresse
        /// Si la ville est la même, on compare le nom de la rue
        /// Si le nom est le même, on compare le numéro
        /// </summary>
        /// <param name="person1">Première personne</param>
        /// <param name="person2">Deuxième personne</param>
        /// <returns>
        /// -1 si la personne 1 doit être rangée avant la personne 2
        /// 0 si les deux personnes ont le même nom et le même prénom
        /// 1 si la personne 1 doit être rangée après la personne 2
        /// </returns>
        public static int CompareTown(Person person1, Person person2)
        {
            string[] a1 = person1.Address.Split(' ');
            string[] a2 = person2.Address.Split(' ');
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
        #endregion
    }
}