using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Objet_volumineux : Objet_Vendu_Donne, IComparable<Objet_volumineux>
    {
        private double hauteur;
        private double largeur;
        private double longueur;
        private double volume;

        public Objet_volumineux(double hauteur, double largeur, double longueur, DateTime date_reception, string type_materiel, int ref_objet, string nom_donateur, string num_tel, string adresse, string description, bool accepte, int beneficiaire, Personne_morale lieu_stockage, double montant):base(date_reception, type_materiel,ref_objet, nom_donateur, num_tel, adresse, description, beneficiaire,  lieu_stockage,  montant, accepte)
        {
            this.hauteur = hauteur;
            this.largeur = largeur;
            this.longueur = longueur;
            this.volume = hauteur * largeur * longueur;
        }

        public double Hauteur
        {
            get { return hauteur; }
            set { this.hauteur = value; }
        }

        public double Largeur
        {
            get { return largeur; }
            set { this.largeur = value; }
        }

        public double Longueur
        {
            get { return longueur; }
            set { this.longueur = value; }
        }

        public double Volume
        {
            get { return volume; }
            set { this.volume = value; }
        }

        //méthode CompareTo permettant d'appliquer la méthode Liste.Sort() dans le module Tri
        public int CompareTo(Objet_volumineux obj)
        {
            return this.date_reception.CompareTo(obj.date_reception);
        }

        //les délégates permettent d'utiliser la méthode Sort() mais avec un autre critère que celui défini dans CompareTo
        public static Comparison<Objet_volumineux> ComparaisonRef = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Ref_objet.CompareTo(obj2.Ref_objet);
        };

        public static Comparison<Objet_volumineux> ComparaisonDescription = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Description.CompareTo(obj2.Description);
        };

        public static Comparison<Objet_volumineux> ComparaisonNom = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Nom_donateur.CompareTo(obj2.Nom_donateur);
        };

        public static Comparison<Objet_volumineux> ComparaisonMois = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Date_reception.Month.CompareTo(obj2.Date_reception.Month);
        };

        public static Comparison<Objet_volumineux> ComparaisonNumBenef = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Beneficiaire.CompareTo(obj2.Beneficiaire);
        };

        public static Comparison<Objet_volumineux> ComparaisonVolume = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Volume.CompareTo(obj2.Volume);
        };

        public static Comparison<Objet_volumineux> ComparaisonPrix = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.Montant.CompareTo(obj2.Montant);
        };

        public static Comparison<Objet_volumineux> ComparaisonEntrepot = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.LieuStockage.Type_activite.CompareTo(obj2.LieuStockage.Type_activite);
        };

        public static Comparison<Objet_volumineux> ComparaisonDepotVente = delegate (Objet_volumineux obj1, Objet_volumineux obj2)
        {
            return obj1.LieuStockage.Identifiant.CompareTo(obj2.LieuStockage.Identifiant);
        };

        public string[] Tostring()
        {
            string[] retour = new string[10];
        
            retour[0] = "La date de reception du don: " + date_reception;
            retour[1] = "Le type: " + type_materiel;
            retour[2] = "La reference: " + ref_objet;
            retour[3] = "Le donateur: " + nom_donateur;
            retour[4] = "Numero: " + num_tel;
            retour[5] = "Adresse: " + adresse;
            retour[6] = "Description relative au don: " + description;
            retour[7] = "";
            retour[8] = "";
            retour[9] = "";
            if (Volume != 0)
            {
                retour[7] = "La hauteur du collie est de :" + hauteur;
                retour[8] = "La largeur du collie est de :" + largeur;
                retour[9] = "La longueur du collie est de :" + longueur;
            }
            return retour;
        }

        public override string Tostring1()
        {
            string retour = base.Tostring1() + ";" + Hauteur + ";" + Longueur + ";" + Largeur + ";" + Volume + LieuStockage.Tostring1();
            return retour;
        }

    }
    
}
