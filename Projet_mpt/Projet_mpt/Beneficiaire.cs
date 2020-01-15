using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Beneficiaire : Personne
    {
        DateTime date_naissance;

        public Beneficiaire(int identifiant, string nom, string prenom, string coordonnees, string tel, DateTime date_naissance) :base( identifiant,  nom,  prenom,  coordonnees,tel)
        {
            this.date_naissance = date_naissance;
           
        }

        public DateTime Date_naissance
        {
            get { return this.date_naissance; }
            set { this.date_naissance = value; }
        }
        public string Nom
        {
            get { return this.nom; }
            set { this.nom = value; }
        }
        public string Prenom
        {
            get { return this.prenom; }
            set { this.prenom = value; }
        }
        public string Coordonnes
        {
            get { return this.coordonnees; }
            set { this.coordonnees = value; }
        }
        public string Tel
        {
            get { return this.tel; }
            set { this.tel = value; }
        }
        public int Identifiant
        {
            get { return this.identifiant; }
            set { this.identifiant = value; }
        }

        public string Affichage()
        {
            return "Identifiant: " + Identifiant + ", nom: " + Nom + ", prenom: " + Prenom + ", coordonnés: " + Coordonnes + ", telephone: " + Tel + ", Date de naissance: " + Date_naissance;
        }

        public string ToString2()
        {
            string retour = this.identifiant + ";" + this.nom + ";" + this.prenom + ";" + this.coordonnees + ";" + this.tel + ";" + this.date_naissance;
            return retour;
        }
    }
}
