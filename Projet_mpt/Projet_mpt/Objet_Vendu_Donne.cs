using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Objet_Vendu_Donne : Don
    {
         private int beneficiaire;
         private Personne_morale lieu_stockage;
         private double montant;

        public Objet_Vendu_Donne(DateTime date_reception, string type_materiel, int ref_objet, string nom_donateur, string num_tel, string adresse, string description, int beneficiaire, Personne_morale lieu_stockage, double montant, bool accepte) : base(date_reception,type_materiel,ref_objet,nom_donateur,num_tel,adresse,description, accepte)
        {
            this.beneficiaire = beneficiaire;
            this.lieu_stockage = lieu_stockage;
            this.montant = montant;
        }

        public int Beneficiaire
        {
            get { return beneficiaire; }
            set { this.beneficiaire = value; }
        }

        public Personne_morale LieuStockage
        {
            get { return lieu_stockage; }
            set { this.lieu_stockage = value; }
        }

        public double Montant
        {
            get { return montant; }
            set { this.montant = value; }
        }

        public override string Tostring1()
        {
            string retour = base.Tostring1() + ";" + beneficiaire + ";" + montant + ";" + this.lieu_stockage.Activite() ;
            return retour;
        }

    }
}
