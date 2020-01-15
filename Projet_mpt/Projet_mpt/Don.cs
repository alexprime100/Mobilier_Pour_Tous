using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
     class Don
    {
        protected DateTime date_reception;
        protected string type_materiel;
        protected int ref_objet;
        protected string nom_donateur;
        protected string num_tel;
        protected string adresse;
        protected string description;
        protected bool accepte;

        public Don(DateTime date_reception, string type_materiel, int ref_objet, string nom_donateur, string num_tel, string adresse, string description, bool accepte)
        {
            this.date_reception = date_reception;
            this.type_materiel = type_materiel;
            this.ref_objet = ref_objet;
            this.nom_donateur = nom_donateur;
            this.num_tel = num_tel;
            this.adresse = adresse;
            this.description = description;
            this.accepte = accepte;
            //reste chez le donnateur
            //stocké dans le batiment de l'association
            //stocké dans un entrepot ou depot vente
        }

        public DateTime Date_reception
        {
            get{ return this.date_reception; }
        }

        public string Type_materiel
        {
            get { return this.type_materiel; }
        }

        public int Ref_objet
        {
            get { return this.ref_objet; }
        }

        public string Nom_donateur
        {
            get { return this.nom_donateur; }
        }

        public string Num_tel
        {
            get { return this.num_tel; }
        }

        public string Adresse
        {
            get { return this.adresse; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public bool Accepte
        {
            get { return this.accepte; }
        }       

        public virtual string Tostring1()
        {
            string retour = ref_objet + ";" + date_reception + ";" + type_materiel + ";" +  nom_donateur + ";" + num_tel + ";" + adresse +";"+ description+";"+accepte;
            return retour;
        }

    }
}
