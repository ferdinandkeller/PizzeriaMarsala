using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe héritant des classes génériques ObservableCollection<T> et IList<T> où T est une classe implémantant l'interface IToCSV
    /// ObservableCollection<T> : liste d'objets de classe T permettant le binding entre les différents objets et l'iterface graphique
    /// IList<T> : liste d'objets sur lesquels on peu appliquer les méthodes des listes
    /// IToCSV: on peut mettre l'enesmble de la liste sous la forme d'un tableau CSV
    /// On objet de cette classe est par ailleurs triable (il suffit de donner une méthode de tri)
    /// </summary>
    /// <typeparam name="T">
    /// Le type des éléments de la liste 
    /// Implémente l'interface IToCSV
    /// </typeparam>
    public class SortableObservableCollection<T> : ObservableCollection<T>, IList<T> where T : class, IToCSV
    {
        #region Delegate

        /// <summary>
        /// Délégation : définition d'une comparaison entre deux éléments de type T
        /// </summary>
        /// <param name="el1">Element 1</param>
        /// <param name="el2">Element 2</param>
        /// <remarque>
        /// Pour chaque instance de la classe SortableObservableCollection<T>, la classe T possède a moins une méthode satisfaisant la délégation
        /// </remarque>
        /// <returns>
        /// -1,0 ou 1
        /// </returns>
        public delegate int ComparisonFunction(T el1, T el2);

        /// <summary>
        /// Délégation : définition d'une fonction qui vérifie si un élément de la liste est celui que l'on cherche
        /// </summary>
        /// <param name="el">L'élément cherché</param>
        /// <returns>true ou false</returns>
        public delegate bool ValidationFunction(T el);
        #endregion

        #region Méthodes

        /// <summary>
        /// Fonction de tri par insertion qui prend en entrée notre fonction de comparaison (delegate ComparaisonFunction)
        /// </summary>
        /// <param name="comparison_function">Méthode satisfaisant la délégation ComparaisonFunction</param>
        public void Sort(ComparisonFunction comparison_function)
        {
            for (int i = 1; i < this.Count; i++)
            {
                T val = this[i];
                int j = i - 1;
                while (j >= 0 && comparison_function(val, this[j]) < 0)
                {
                    this[j + 1] = this[j];
                    j--;
                }
                this[j + 1] = val;
            }
        }


        /// <summary>
        /// Recherche un élément dans la liste 
        /// </summary>
        /// <param name="validation_function">Méthode satisfaisant la délégation ValidationFunction</param>
        /// <returns>L'élément de la liste satisfaisant le critère de recherche donné par la méthode</returns>
        public T Find(ValidationFunction validation_function)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (validation_function(this[i]))
                {
                    return this[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Convertit un objet de la classe SortableObservableCollection en une List
        /// </summary>
        /// <returns>L'élément converti</returns>
        public List<T> ToList()
        {
            return (List<T>)this.Items;
        }

        /// <summary>
        /// Méthode de classe
        /// Permet de tester si une instance est vide ou nulle
        /// </summary>
        /// <param name="s">l'instance</param>
        /// <returns>true (non null & non vide) ou false</returns>
        public static bool NotEmpty(SortableObservableCollection<T> s)
        {
            return (s != null && s.Count != 0);
        }

        /// <summary>
        /// Enregitrement d'une instance dans un fichier .txt
        /// </summary>
        /// <param name="nomFichier">Le fichier de sauvegarde</param>
        public void SaveInFileTXT(string nomFichier)
        {
            List<T> liste = this.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToString()));
            }
            sw.Close();
        }

        /// <summary>
        /// Enregistrement d'une instance dans un fichier .csv
        /// </summary>
        /// <param name="nomFichier">Le fichier de sauvegarde</param>
        public void SaveInFileCSV(string nomFichier)
        {
            List<T> liste = this.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToCSV())); //Possible car T: class, IToCSV
            }
            sw.Close();
        }
        #endregion

    }
}
