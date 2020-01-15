using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Adherent : Personne
    {
        string fonction;

        public Adherent(int identifiant, string nom, string prenom, string coordonnees, string tel, string fonction) : base(identifiant, nom, prenom, coordonnees, tel)
        {
            this.fonction = fonction;
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

        /// <summary>
        /// Méthode ToString permet de stocker un adhérent dans un fichier
        /// </summary>
        public string Tostring2()
        {
            string retour = this.identifiant + ";" + this.nom + ";" + this.prenom + ";" + this.coordonnees + ";" + this.tel + ";" + this.fonction;
            return retour;
        }

        /// <summary>
        /// Affichage à la Console d'un Adhérent
        /// </summary>
        public string Affichage()
        {
            return "Identifiant: " + Identifiant + ", nom: " + Nom + ", prenom: " + Prenom + ", coordonnés: " + Coordonnes + ", telephone: " + Tel + ", Fonction: " + fonction;
        }
    }
}

