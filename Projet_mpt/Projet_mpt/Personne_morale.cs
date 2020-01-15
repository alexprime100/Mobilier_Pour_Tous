using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Personne_morale : Personne
    {
        protected string type_activite;

        public Personne_morale(int identifiant, string nom, string prenom, string coordonnees, string tel, string type_activite): base( identifiant,  nom,  prenom,  coordonnees,  tel)
        {
            this.type_activite = type_activite;
        }

        public int Identifiant
        {
            get { return identifiant; }
        }

        public string Nom    
        {
            get { return nom; }
        }

        public string Prenom
        {
            get { return prenom; }
        }

        public string Coordonnees
        {
            get { return coordonnees; }
        }
        
        public string Tel
        {
            get { return tel; }
        }

        public string Type_activite
        {
            get { return type_activite; }
        }

        public virtual string Tostring1()
        {
            string retour = "";
            return retour;
        }

        public string Activite()
        {
            return type_activite;
        }
    }
}
