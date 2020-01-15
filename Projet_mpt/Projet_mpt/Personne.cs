using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    abstract class Personne
    {
        //Classe abstract,elle n'est jamais instanciée
        protected int identifiant;
        protected string nom;
        protected string prenom;
        protected string coordonnees;
        protected string tel;

        public Personne(int identifiant, string nom, string prenom, string coordonnees, string tel)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.prenom = prenom;
            this.coordonnees = coordonnees;
            this.tel = tel;
        }


    }
}
