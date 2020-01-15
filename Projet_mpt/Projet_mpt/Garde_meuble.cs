using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Garde_meuble : Personne_morale
    {
        private DateTime date_depot;
        private DateTime date_enlevement;
        private Beneficiaire beneficiaire; //Personne recuperant le don


        public Garde_meuble(int identifiant, string nom, string prenom, string coordonnees, string tel, string type_activite, DateTime DateDepot, DateTime DateEnlevement, Beneficiaire benef) :base(identifiant, nom, prenom, coordonnees, tel, type_activite)
        {
            this.date_depot = DateDepot;
            this.date_enlevement = DateEnlevement;
            this.beneficiaire = benef;
        }

        public DateTime DateDepot
        {
            get { return date_depot; }
        }

        public DateTime DateEnlevement
        {
            get { return date_enlevement; }
            set { this.date_enlevement = value; }
        }

        public Beneficiaire Beneficaire1
        {
            get { return beneficiaire; }
        }

        public override string Tostring1()
        {
            string retour = base.Tostring1() + ";" + this.date_depot + ";" + this.date_enlevement;
            return retour;
        }

    }
}
