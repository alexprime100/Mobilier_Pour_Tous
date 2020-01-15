using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Listes
    {
        //cette classe implémente des méthodes permettant de créer toutes les listes récurrentes du programme, telles que la liste des bénéficiaires ou la liste des objets stockés dans un entrepot
        private string ListeDepotsVente;
        private string Adherents;
        private Personne_morale association;

        public Listes(string listeDepotVente,string Adherents, Personne_morale Asso)
        {
            this.ListeDepotsVente = listeDepotVente;
            this.Adherents = Adherents;
            this.association = Asso;
        }

        public Listes( Personne_morale Asso)
        {
            this.association = Asso;
        }

        /// <summary>
        /// fusionne plusieurs listes stockées dans une liste de listes
        /// </summary>
        public List<Objet_volumineux> Addition(List<List<Objet_volumineux>> Listes)
        {
            List < Objet_volumineux > Liste = new List<Objet_volumineux>();
            foreach(List<Objet_volumineux> liste in Listes)
            {
                foreach(Objet_volumineux obj in liste)
                {
                    Liste.Add(obj);
                }
            }
            return Liste;
        }

        /// <summary>
        /// renvoie un bénéficiaire (sous forme d'instance, grâce à son ID)
        /// </summary>
        public Beneficiaire ExtraireBenef(int NumBenef)
        {
            foreach (Beneficiaire benef in ListeBenef())
            {
                if (benef.Identifiant == NumBenef)
                {
                    return benef;
                }
            }
            Console.WriteLine("aucun bénéficiaire ne possède cet identifiant. Ressaisissez l'identifiant");
            NumBenef = Convert.ToInt32(Console.ReadLine());
            return ExtraireBenef(NumBenef);
        }

        /// <summary>
        /// renvoie la liste des objets d'un fichier
        /// </summary>
        public List<Objet_volumineux> List_Objet_volumineux(string fichier)//crée une liste d'objet Volumineux à partir d'un fichier
        {
            List<String> lines = File.ReadAllLines(fichier).ToList();
            string[] mots;
            List<Objet_volumineux> ListeDon = new List<Objet_volumineux>();
            for (int i = 0; i < lines.Count; i++)
            {
                mots = lines[i].Split(";");
                DateTime date_reception = Convert.ToDateTime(mots[1]);
                int ref_objet = Convert.ToInt32(mots[0]);
                string typeMateriel = mots[2];
                string NomDonateur = mots[3];
                string NumTel = mots[4];
                string adresse = mots[5];
                string description = mots[6];
                bool accepte = Convert.ToBoolean(mots[7]);
                int NumBeneficiaire = Convert.ToInt32(mots[8]);
                double montant = Convert.ToDouble(mots[9]);
                string TypeActivite = mots[10];
                double hauteur = Convert.ToDouble(mots[11]);
                double longueur = Convert.ToDouble(mots[12]);
                double largeur = Convert.ToDouble(mots[13]);
                double volume = Convert.ToDouble(mots[14]);
                

                if (mots[10] == "Garde-meuble")
                {
                    DateTime DateDepot = Convert.ToDateTime(mots[15]);
                    DateTime DateVente = DateDepot;
                    DateVente.AddMonths(1);
                    Beneficiaire le_benef = ExtraireBenef(NumBeneficiaire);
                    Personne_morale gdm = new Garde_meuble(association.Identifiant, association.Nom, association.Prenom, association.Coordonnees, association.Tel, TypeActivite, DateDepot, DateVente, le_benef);
                    Objet_volumineux un_don = new Objet_volumineux(hauteur, longueur, largeur, date_reception, typeMateriel, ref_objet, NomDonateur, NumTel, adresse, description, accepte, NumBeneficiaire, gdm, montant);
                    ListeDon.Add(un_don);
                }
                if (mots[10] == "Depot-vente")
                {
                    DateTime DateDepot = Convert.ToDateTime(mots[15]);
                    DateTime DateVente = DateDepot;
                    DateVente.AddMonths(1);
                    int IDdpv = Convert.ToInt32(mots[17]);
                    Personne_morale gdm = new Depot_vente(IDdpv, "", "", "", "", TypeActivite, DateDepot, DateVente, montant);
                    Objet_volumineux un_don = new Objet_volumineux(hauteur, longueur, largeur, date_reception, typeMateriel, ref_objet, NomDonateur, NumTel, adresse, description, accepte, NumBeneficiaire, gdm, montant);
                    ListeDon.Add(un_don);
                }
                if (mots[10] == "association")
                {

                    Objet_volumineux un_don = new Objet_volumineux(hauteur, longueur, largeur, date_reception, typeMateriel, ref_objet, NomDonateur, NumTel, adresse, description, accepte, NumBeneficiaire, association, montant);
                    ListeDon.Add(un_don);
                }
            }
            return ListeDon;
        }

        /// <summary>
        /// renvoie la liste des bénéficiaires
        /// </summary>
        /// <returns></returns>
        public List<Beneficiaire> ListeBenef()
        {
            List<Beneficiaire> liste_beneficiaires = new List<Beneficiaire>();
            StreamReader st = new StreamReader("Beneficiaires.txt");
            string line = "";
            while (st.Peek() > 0)
            {
                line = st.ReadLine();
                string[] mots = line.Split(';');
                string date = mots[5];
                DateTime naisssance = Convert.ToDateTime(date);
                Beneficiaire individu = new Beneficiaire(Convert.ToInt32(mots[0]),mots[1], mots[4], mots[2], mots[3], naisssance);
                liste_beneficiaires.Add(individu);
            }
            st.Close();
            return liste_beneficiaires;
        }

        /// <summary>
        /// renvoie la liste des adhérents
        /// </summary>
        /// <returns></returns>
        public List<Adherent> ListeAdherent()
        {
            List<Adherent> liste_adherents = new List<Adherent>();
            StreamReader st2 = new StreamReader(Adherents);
            string ligne = "";
            while (st2.Peek() > 0)
            {
                ligne = st2.ReadLine();
                string[] mot = ligne.Split(';');
                Adherent individu = new Adherent(Convert.ToInt32(mot[0]), mot[1], mot[4], mot[2], mot[3], mot[5]);

                liste_adherents.Add(individu);
            }
            st2.Close();
            return liste_adherents;
        }

        /// <summary>
        /// renvoie la liste des dépots ventes
        /// </summary>
        /// <returns></returns>
        public List<Depot_vente> ListDepotsVente()
        {
            List<Depot_vente> Liste = new List<Depot_vente>();
            StreamReader lire = new StreamReader(ListeDepotsVente);
            string ligne = "";
            while (lire.Peek() > 0)
            {
                ligne = lire.ReadLine();
                //Console.WriteLine(ligne);
                string[] mots = ligne.Split(";");
                Depot_vente depot = new Depot_vente(Convert.ToInt32(mots[0]), mots[1], "", mots[2], mots[3], "Depot-vente", DateTime.Now, DateTime.Now, Convert.ToDouble(mots[4]));
                Liste.Add(depot);
            }
            lire.Close();
            return Liste;
        }
    }
}
