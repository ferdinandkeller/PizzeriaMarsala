using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    public class Worker : Person
    {
        public WorkerState State { get;private set; }
        private DateTime HiringDate { get; set; }

        public int ManagedCommandNumber { get; set; }

        public Worker(string last_name, string first_name, string address, long phone_number, WorkerState state, DateTime hiring_date)
            : base(last_name, first_name, address, phone_number)
        {
            this.State = state;
            HiringDate = hiring_date;
            ManagedCommandNumber = 0;
        }

        public override string ToString()
        {
            return base.ToString() + $"Presence : {nameof(State)} | Date d'embauche : {HiringDate.ToShortDateString()}";
        }

        public override string ToCSV()
        {
            return base.ToCSV() + $";{State};{HiringDate.ToShortDateString()}";
        }

        public static Worker CSVToWorker(String worker)
        {
            String[] infos = worker.Split(';');
            return new Worker(infos[0], infos[1], infos[2], long.Parse(infos[3]), (WorkerState)Enum.Parse(typeof(WorkerState), infos[4]), DateTime.Parse(infos[5]));
        }

    }
}
