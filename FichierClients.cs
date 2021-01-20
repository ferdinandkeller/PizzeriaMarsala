using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class FichierClients : ISupprime, IRechercheIndex
    {
        private List<Client> clients;

        public FichierClients()
        {
            this.clients = null;
        }

        public FichierClients(List<Client> clients)
        {
            this.clients = clients;
        }

        public List<Client> Clients
        {
            get { return this.clients; }
        }

        public override string ToString()
        {
            string s = "";
            if(this.clients!=null&& this.clients.Count != 0)
            {
                this.clients.ForEach(x => s += x.ToString() + "\n");
            }
            return s;
        }

        public int RechercheIndex(string nom, string prenom, string adresse)
        {
            Client r = new Client(nom, prenom, adresse, 0,default);
            int i = this.clients.FindIndex(x=>x.Egalite((Personne)r));
            return i;
        }

        public void Supprime(Personne c)
        {
            int i= this.clients.FindIndex(x => x.Egalite((Personne)c));
            this.clients.RemoveAt(i);
        }

        public void Supprime(string nom, string prenom, string adresse)
        {
            int i = RechercheIndex(nom, prenom, adresse);
            this.clients.RemoveAt(i);
        }
    }
}
