using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette classe sert à représenter un commis
    /// Elle hérite de Personne car tout commis est une personne
    /// </summary>
    /// <attributs>
    /// State: état d'un commis (surplace, enconge)
    /// HiringDate: date d'embauche
    /// ManagedCommandNumber: nombre de commandes gérées
    /// </attributs>
    public class Worker : Person
    {
        #region Attributs
        private WorkerState _CurrentWorkerState;
        public WorkerState CurrentWorkerState
        {
            get => _CurrentWorkerState;
            set
            {
                _CurrentWorkerState = value;
                NotifyPropertyChanged("State");
            }
        }
        public DateTime HiringDate { get; private set; }
        private int _ManagedCommandNumber;
        public int ManagedCommandNumber
        {
            get => _ManagedCommandNumber;
            set
            {
                _ManagedCommandNumber = value;
                NotifyPropertyChanged("ManagedCommandNumber");
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur -> Création d'un commis
        /// </summary>
        /// <param name="state">Etat du commis</param>
        /// <param name="hiring_date">Date d'embauche</param>
        public Worker(string last_name, string first_name, string address, long phone_number, WorkerState state, DateTime hiring_date)
            : base(last_name, first_name, address, phone_number)
        {
            this.CurrentWorkerState = state;
            HiringDate = hiring_date;
            ManagedCommandNumber = 0; // Le commis vient d'être embauché
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// ToString() de la classe fille en utilisant la méthode de la classe mère
        /// </summary>
        /// <returns>
        /// NomDeFamille Prénom [NuméroDeTéléphone] : Adresse
        /// Presence : Etat | Date d'embauche : DateEmbauche
        /// </returns>
        public override string ToString()
        {
            return base.ToString() + $"\nPresence : {nameof(CurrentWorkerState)} | Date d'embauche : {HiringDate.ToShortDateString()}";
        }

        /// <summary>
        /// ToCSV() de la classe fille en utilisant la méthode de la classe mère
        /// </summary>
        /// <returns>
        /// NomDeFamille;Prénom;Adresse;NuméroDeTéléphone;Etat;DateEmbauche
        /// </returns>
        public override string ToCSV()
        {
            return base.ToCSV() + $";{CurrentWorkerState};{HiringDate.ToShortDateString()}";
        }

        /// <summary>
        /// Créer un commis depuis une ligne de fichier CSV
        /// </summary>
        /// <param name="worker">La ligne de fichier qui représente un commis</param>
        /// <returns>Le commis créé</returns>

        public static Worker CSVToWorker(String worker)
        {
            String[] infos = worker.Split(';');
            return new Worker(infos[0], infos[1], infos[2], long.Parse(infos[3]), (WorkerState)Enum.Parse(typeof(WorkerState), infos[4]), DateTime.Parse(infos[5]));
        }

        /// <summary>
        /// Comparaison de deux commis en fonction du nombre de commandes gérées
        /// </summary>
        /// <param name="a">Travailleur a</param>
        /// <param name="b">Travailleur b</param>
        /// <returns>
        /// -1 si le nombre de commandes gérées du commis a est inférieur à celui du commis b
        /// 0 si les deux nombres sont égaux
        /// 1 sinon
        /// </returns>
        public static int CompareManagedOrderNumber(Worker a, Worker b)
        {
            return a.ManagedCommandNumber.CompareTo(b.ManagedCommandNumber);
        }
        #endregion
    }
}
