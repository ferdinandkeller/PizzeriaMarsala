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
    /// CurrentWorkerState: état d'un commis (SurPlace, EnConges)
    /// HiringDate: la date d'embauche
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
                NotifyPropertyChanged("CurrentWorkerState");
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
        /// Constructeur principal
        /// </summary>
        /// <param name="last_name">Nom</param>
        /// <param name="first_name">Prénom</param>
        /// <param name="address">Adresse</param>
        /// <param name="phone_number">Numéro de téléphone</param>
        /// <param name="current_worker_state">Etat du commis</param>
        /// <param name="hiring_date">Date d'embauche</param>
        public Worker(string last_name, string first_name, string address, long phone_number, WorkerState current_worker_state, DateTime hiring_date)
            : base(last_name, first_name, address, phone_number)
        {
            this.CurrentWorkerState = current_worker_state;
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

        #region Méthodes CSV
        /// <summary>
        /// ToCSV() de la classe fille en utilisant la méthode de la classe mère
        /// </summary>
        /// <returns>
        /// Le commis au format CSV
        /// </returns>
        public override string ToCSV()
        {
            return $"{base.ToCSV()};{CurrentWorkerState};{HiringDate.ToShortDateString()}";
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
        #endregion

        #region Méthode de comparaison
        /// <summary>
        /// Comparaison de deux commis en fonction du nombre de commandes gérées
        /// </summary>
        /// <param name="worker1">Le premier commis</param>
        /// <param name="worker2">Le second commis</param>
        /// <returns>
        /// -1 si le nombre de commandes gérées du commis a est inférieur à celui du commis b
        /// 0 si les deux nombres sont égaux
        /// 1 sinon
        /// </returns>
        public static int CompareManagedOrderNumber(Worker worker1, Worker worker2)
        {
            return worker1.ManagedCommandNumber.CompareTo(worker2.ManagedCommandNumber);
        }
        #endregion
        #endregion
    }
}
