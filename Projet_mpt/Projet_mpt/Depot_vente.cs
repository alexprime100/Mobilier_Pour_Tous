using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Depot_vente : Personne_morale
    {
        private DateTime date_depot;
        private DateTime date_vente;
        private double montant;
        private double solde;

        public Depot_vente(int identifiant, string nom, string prenom, string coordonnees, string tel, string type_activite, DateTime date_depot,DateTime date_vente, double montant):base(identifiant, nom, prenom, coordonnees, tel, type_activite)
        {
            this.date_depot = date_depot;
            this.date_vente = date_vente;
            this.montant = montant;
            this.solde = 1000;
        }


        public double Solde
        {
            get { return solde; }
            set { this.solde = value; }
        }

        public DateTime DateDepot
        { 
            get { return date_depot; }
            set { this.date_depot = value; }
        }

        public DateTime DateVente
        {
            get { return DateVente; }
            set { this.date_vente = value; }
        }

        public double Montant
        {
            get { return montant; }
            set { this.montant = value; }
        }

        public override string Tostring1()
        {
            string retour = base.Tostring1() + ";" + this.date_depot + ";" + this.date_vente + ";" + Tostring2();
            return retour;
        }

        public string Tostring2()
        {
            string retour = Tostring3() + ";" + solde;
            return retour;
        }

        public string Tostring3()
        {
            string retour = this.identifiant + ";" + this.nom + ";" + this.coordonnees + ";" + this.tel;
            return retour;
        }
    }
}
