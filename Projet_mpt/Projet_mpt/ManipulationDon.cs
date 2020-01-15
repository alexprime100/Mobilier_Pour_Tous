using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Projet_mpt
{
    class ManipulationDon 
    {
        //classe contenant toutes les méthodes et sous-méthodes permettant de manipuler des dons
        //les attributs sont tous des listes et des fichiers dont les appels dans les méthodes sont très récurrents
        private Personne_morale association;
        private string DonsNonTraites;
        private string Association;
        private string GardeMeuble;
        private string DepotVente;
        private string archive;
        private List<Adherent> ListeAdherents;
        private string Adherents;
        private string Beneficiaires;
        private Listes listes;
        private List<Beneficiaire> ListeBeneficiaires;
        private List<Depot_vente> Liste_Depots_Vente;
        private string ListeDepotsVente;

        public ManipulationDon(Personne_morale association)
        {
            this.Association = "Association.txt";
            this.GardeMeuble = "Garde_Meuble.txt";
            this.DepotVente = "Depot_vente.txt";
            this.archive = "Archives.txt";
            this.association = association;
            this.ListeDepotsVente = "Liste_depots_vente.txt";
            this.Adherents = "Adherents.txt";
            this.Beneficiaires = "Beneficiaires.txt";
            this.DonsNonTraites = "Dons_non_traites.txt";
            this.listes = new Listes(ListeDepotsVente,Adherents, association);
            this.ListeAdherents = listes.ListeAdherent();
            this.ListeBeneficiaires = listes.ListeBenef();
            this.Liste_Depots_Vente = listes.ListDepotsVente();
        }
        /// <summary>
        /// demande à un adhérent de s'identifier
        /// </summary>
        public Adherent Utilisateur()
        {
            int retour = -1;
            int ID;
            Adherent user = null;
            while (retour == -1)
            {
                int compteur = 0;
                Console.WriteLine("Quel est votre identifiant d'adhérents?");
                ID = Convert.ToInt32(Console.ReadLine());
                while (compteur < ListeAdherents.Count)
                {
                    if (ListeAdherents[compteur].Identifiant == ID)//on trouve la ligne dont l'identifiant est le même
                    {
                        user = new Adherent(ListeAdherents[compteur].Identifiant, ListeAdherents[compteur].Nom, ListeAdherents[compteur].Prenom, ListeAdherents[compteur].Coordonnes, ListeAdherents[compteur].Tel, "Association");
                        compteur = ListeAdherents.Count;
                        retour++;
                    }
                    compteur++;
                }
            }
            return user;
        }
        
        /// <summary>
        /// affiche les caractéristiques d'un don
        /// </summary>
        public void Affichage_don(Objet_volumineux don) //Affichage du don cree
        {
            Console.WriteLine("Affichage du don cree: ");

            for (int i = 0; i < don.Tostring().Length; i++)
            {
                Console.WriteLine(don.Tostring()[i]);
            }
        }

        /// <summary>
        /// Permet d'écrire dans une ligne fichier
        /// </summary>
        public void Ecrire(string ligne, string fichier)
        {
            
            StreamWriter ecrire = new StreamWriter(fichier, true);
            ecrire.WriteLine(ligne);
            ecrire.Close();
        }

        /// <summary>
        /// Création d'un don, puis stockage dans DonNonTraité
        /// </summary>
        public Objet_volumineux Creation_don()
        {
            //crée un don et le stocke dans le fichier DonsNontraites
            Adherent user = Utilisateur();
            Console.WriteLine("Creation d'un don:");
            
            Console.WriteLine("Veuillez saisir le Type de matériel du don:");
            string type_materiel = Console.ReadLine();

            Console.WriteLine("Veuillez saisir la reference:");
            int ref_objet = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Description relative au don:");
            string description = Console.ReadLine();

            int volumineux = -1;
            double hauteur = 0;
            double longueur = 0;
            double largeur = 0;
            Console.WriteLine("L'objet est-il volumineux ?");
            while (volumineux < 0 || volumineux > 1)
            {
                Console.WriteLine("tapez 0 pour non");
                Console.WriteLine("tapez 1 pour oui");
                volumineux = Convert.ToInt32(Console.ReadLine());
            }

            if (volumineux == 1)
            {
                while (hauteur <= 0)
                {
                    Console.WriteLine("saisissez la hauteur");
                    hauteur = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("saisissez la longueur");
                    longueur = Convert.ToDouble(Console.ReadLine());

                    Console.WriteLine("saisissez la largeur");
                    largeur = Convert.ToDouble(Console.ReadLine());
                }
            }
            DateTime date_reception = DateTime.Now;
            Objet_volumineux objet = new Objet_volumineux(hauteur, largeur, longueur, date_reception, type_materiel, ref_objet, user.Nom, user.Tel, user.Coordonnes, description, false, -1, association, 0);

            //Recapitulatif du don
            Console.WriteLine(" ");
            Console.WriteLine("Récapitulatif du don: ");
            Console.WriteLine(" ");
            Console.WriteLine("   --------------------  ");
            Affichage_don(objet);
            Console.WriteLine("   --------------------  ");
            
            Console.WriteLine("Votre don va être stocké dans une base de données en attendant sa validation par l'association");
            Ecrire(objet.Tostring1(), DonsNonTraites);

            return objet;
        }
        /// <summary>
        /// permet d'extraire une ligne d'un fichier connaissant son 1er mot
        /// </summary>
        public string ExtraireString(string mot, string fichier)
        {      
            StreamReader lecteur = new StreamReader(fichier);
            string ligne = "";
            string[] mots;
            bool arret = false;
            while (lecteur.Peek() > 0 && !arret)
            {
                ligne = lecteur.ReadLine();
                mots = ligne.Split(";");
                if (mot == mots[0])
                {
                    arret = true;
                }
            }
            //on ferme le lecteur
            lecteur.Close();
            return ligne;
        }

        /// <summary>
        /// permet de créer un nouveau dépot vente et l'inscrit dans le fichier listedepotsvente
        /// </summary>
        public Depot_vente creation_depot_vente()
        {
            Console.WriteLine("Création d'un nouveau dépot-vente");
            Console.WriteLine("saisissez son identifiant");
            int IdentifiantDPV = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("saisissez son nom");
            string NomDPV = Console.ReadLine();
            Console.WriteLine("saisissez son adresse");
            string AdresseDPV = Console.ReadLine();
            Console.WriteLine("saisissez son numéro de téléphone");
            string TelDPV = Console.ReadLine();
            Depot_vente depot = new Depot_vente(IdentifiantDPV, NomDPV, "", AdresseDPV, TelDPV, "Depot-vente", DateTime.Now, DateTime.Now, 0);
            string LigneDPV = depot.Tostring2();
            Ecrire(LigneDPV, ListeDepotsVente);
            Liste_Depots_Vente = listes.ListDepotsVente();
            return depot;
        }

        /// <summary>
        /// permet à un adhérent de choisir dans quel dépot vente stocker unn objet
        /// </summary>
        public Depot_vente ChoixDepotVente()
        {
            
            if (Liste_Depots_Vente.Count == 0)
            {
                //s'il n'y a aucun dépot vente référencé par l'association, l'appli en crée automatiquement un
                Depot_vente depot = creation_depot_vente();
                return depot;
            }
            else
            {
                //sinon l'utilisateur en choisis un existant ou peut quand même en créer un nouveau
                string lareponse = "";
                Console.WriteLine("voulez-vous stocker l'objet dans un depot existant");
                while (lareponse != "oui" && lareponse != "non")
                {
                    Console.WriteLine("répondez oui ou non");
                    lareponse = Console.ReadLine();
                }
                if (lareponse == "oui")
                {
                    //affichage de la liste des dépots et création d'une liste contenant tout les identifiants des dépots ventes connus. Cela permettra de vérifier si un identifiant saisi existe
                    List<int> NumListe_Depots_Vente = new List<int>();
                    foreach (Depot_vente DPV in Liste_Depots_Vente)
                    {
                        Console.WriteLine(DPV.Tostring2());
                        NumListe_Depots_Vente.Add(DPV.Identifiant);
                    }
                    Console.WriteLine("Saisir l'identifiant du depot Vente");
                    int ID = Convert.ToInt32(Console.ReadLine());

                    //vérification de l'existence
                    int existence = NumListe_Depots_Vente.IndexOf(ID);  //renvoie l'indice dans la liste ou -1 si l'id n'existe pas
                    while (existence == -1)
                    {
                        Console.WriteLine("identifiant introuvable ressaisissez-le");
                        ID = Convert.ToInt32(Console.ReadLine());
                        existence = NumListe_Depots_Vente.IndexOf(ID);
                    }
                    string LigneDPV = ExtraireString(Convert.ToString(ID), ListeDepotsVente);
                    string[] mots = LigneDPV.Split(";");
                    Depot_vente depot = new Depot_vente(ID, mots[1], "", mots[2], mots[3], "Depot-vente", DateTime.Now, DateTime.Now, 0);
                    return depot;
                }
                else
                {
                    Depot_vente depot = creation_depot_vente();
                    return depot;
                }
            }
        }

        /// <summary>
        /// supprime une ligne d'un fichier en connaissant son numéro de ligne dans le fichier
        /// </summary>
        public void SupprimerLigne(string fichier, int ligne)
        {       
            List<String> lines = File.ReadAllLines(fichier).ToList();
            lines.RemoveAt(ligne);
            File.Delete(fichier);
            File.WriteAllLines(fichier, lines.ToArray());
        }

        /// <summary>
        /// permet de modifier une ligne d'un fichier en connaissant son numéro de ligne
        /// </summary>
        public void ModifierLigne(string fichier, int ligne, string LigneModifiee)
        {            
            List<String> lines = File.ReadAllLines(fichier).ToList();
            lines[ligne] = LigneModifiee;
            File.Delete(fichier);
            File.WriteAllLines(fichier, lines.ToArray());
        }

        /// <summary>
        /// renvoie le numéro de la ligne dont le 1er mot est le mot passé en paramètre
        /// </summary>
        public int chercher(string mot, string fichier)
        {         
            StreamReader lecteur = new StreamReader(fichier);
            string ligne = "";
            string[] mots;
            bool arret = false;
            int compteur = 0;
            int max = 0;
            while (lecteur.Peek() > 0)   //détermination du nombre de lignes total
            {
                ligne = lecteur.ReadLine();
                max++;
            }
            lecteur.Close();
            lecteur = new StreamReader(fichier);
            while (lecteur.Peek() > 0 && !arret)
            {
                ligne = lecteur.ReadLine();
                mots = ligne.Split(";");
                if (mot == mots[0])
                {
                    arret = true;
                }
                else
                {
                    compteur++;
                }
            }
            lecteur.Close();
            if (compteur == max)
            {
                compteur = -1;
            }
            return compteur;
        }


        /// <summary>
        ///permet de supprimer une ligne d'un fichier et de la réécrire dans un autre fichier
        /// </summary>
        public void Couper_coller(string from_fichier, string to_fichier, string reference)
        {
            
            string ligneDon1 = ExtraireString(reference, from_fichier);
            Ecrire(ligneDon1, to_fichier);
            int NumLigne1 = chercher(reference, from_fichier);
            SupprimerLigne(from_fichier, NumLigne1);
        }



        /// <summary>
        ///permet de rechercher un bénéficiaire par identifiant,par nom ou par numero de téléphone
        /// </summary>
        public Beneficiaire RechercheBeneficiaire()
        {
            
            Console.WriteLine("Quelle type de recherche voulez-vous effectuer ?");
            Console.WriteLine("tapez 1 pour une recherche par identifiant");
            Console.WriteLine("tapez 2 pour une recherche par nom");
            Console.WriteLine("tapez 3 pour une recherche par numéro de téléphone");
            int reponse = 0;
            
            
            while (reponse < 1 || reponse > 3)
            {
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                Console.WriteLine("saisissez l'identifiant du bénéficiaire");
                List<int> NumListe = new List<int>();
                foreach (Beneficiaire benefi in ListeBeneficiaires)
                {
                    NumListe.Add(benefi.Identifiant);
                }
                int ID = Convert.ToInt32(Console.ReadLine());
                int indice = NumListe.IndexOf(ID);
                while (indice == -1)
                {
                    Console.WriteLine("identifiant introuvable, ressaisissez-le");
                    ID = Convert.ToInt32(Console.ReadLine());
                    indice = NumListe.IndexOf(ID);
                }
                Beneficiaire benef = this.ListeBeneficiaires.Find(x => x.Identifiant == ID);
                return benef;
            }
            else
            {
                if (reponse == 2)
                {
                    Console.WriteLine("saisissez le nom du bénéficiaire");
                    List<string> NomListe = new List<string>();
                    foreach(Beneficiaire benefi in ListeBeneficiaires)
                    {
                        NomListe.Add(benefi.Nom);
                    }
                    string nom = Console.ReadLine();
                    int indice = NomListe.IndexOf(nom);
                    while (indice ==-1)
                    {
                        Console.WriteLine("nom introuvable, ressaisissez-le");
                        nom = Console.ReadLine();
                        indice = NomListe.IndexOf(nom);
                    }
                    Beneficiaire benef = this.ListeBeneficiaires.Find(x => x.Nom == nom);
                    return benef;
                }
                else
                {
                    Console.WriteLine("saisissez le numéro de téléphone du bénéficiaire");
                    List<string> TelListe = new List<string>();
                    foreach(Beneficiaire benefi in ListeBeneficiaires)
                    {
                        TelListe.Add(benefi.Tel);
                        Console.WriteLine(benefi.Tel);
                    }
                    string NumTel = Console.ReadLine();
                    int indice = TelListe.IndexOf(NumTel);
                    while (indice == -1)
                    {
                        Console.WriteLine("Telephone introuvable, ressaisissez-le");
                        NumTel = Console.ReadLine();
                        indice = TelListe.IndexOf(NumTel);
                    }
                    Beneficiaire benef = this.ListeBeneficiaires.Find(x => x.Tel == NumTel);
                    return benef;
                }
            }
        }

        /// <summary>
        /// permet de transférer un don non traité dans un dépot vente
        /// </summary>
        public void TransfertDepotVente(string[] mots)
        {          
            //choix du dépot de vente
            Depot_vente dpv = ChoixDepotVente();

            //création du montant
            Console.WriteLine("saisissez le prix de vente de l'objet");
            double montant = Convert.ToDouble(Console.ReadLine());
            mots[9] = Convert.ToString(montant);
            dpv.Solde += montant;

            //création de la ligne à écrire dans le fichier depot-vente
            mots[10] = "Depot-vente";

            string LigneDon2 = "";
            for (int i = 0; i < mots.Length; i++)
            {
                LigneDon2 += mots[i];
                LigneDon2 += ";";
            }

            DateTime DateDepot = DateTime.Now;

            //Console.WriteLine("Saisissez la date de vente de l'objet au format jj//mm/aaaa");
            DateTime DateVente = DateDepot;
            DateVente.AddMonths(1);
            
            LigneDon2 += DateDepot + ";" + DateVente;
            dpv.DateDepot = DateDepot;
            dpv.DateVente = DateVente;

            //transfert vers le fichier depot-vente
            LigneDon2 += ";" +dpv.Tostring3();
            Ecrire(LigneDon2, DepotVente);
            int NumLigne = chercher(mots[0], DonsNonTraites);
            SupprimerLigne(DonsNonTraites, NumLigne);

            //actualisation du solde du dépot-vente
            string LigneDepot = dpv.Tostring2();
            //ModifierLigne(ListeDepotsVente, )
        }

        /// <summary>
        /// permet de transférer un don non traité au garde meuble
        /// </summary>
        public void TransfertGardeMeuble(string[] mots)
        {
            //recherche du numéro du bénéficiaire
            Beneficiaire benef = RechercheBeneficiaire();
            int ID = benef.Identifiant;
            mots[8] = Convert.ToString(ID);
            //Personne_morale gdm = new Garde_meuble(user.Identifiant, user.Nom, user.Prenom, user.Coordonnes, user.Tel, "Garde-meuble", date, ListeBeneficiaires[index]);
            mots[10] = "Garde-meuble";
            string LigneDon2 = "";
            for (int i = 0; i < mots.Length; i++)
            {
                LigneDon2 += mots[i];
                LigneDon2 += ";";
            }
            DateTime DateDepot = DateTime.Now;
            DateTime DateEnlevement = DateDepot.AddMonths(1);
            LigneDon2 += DateDepot + ";" + DateEnlevement;

            Ecrire(LigneDon2, GardeMeuble);
            int NumLigne = chercher(mots[0], DonsNonTraites);
            SupprimerLigne(DonsNonTraites, NumLigne);
            //Le don a été transféré de DonNonTraites vers GardeMeuble.
        }


        /// <summary>
        /// permet  à un adhérent d'accepter (ou refuser) un don non traité et de le stocker au garde meuble, ou dans un dépot vente, ou dans l'association (ou dans les archives pour un don refusé)
        /// </summary>
        public void accepterDon()
        { 
            Adherent user = Utilisateur();
            Console.WriteLine("voici la liste des dons non traités");

            //afficher le fichier des dons non traités de l'association
            List<String> lines = File.ReadAllLines(DonsNonTraites).ToList();
            foreach (string don in lines)
            {
                Console.WriteLine(don);
            }

            Console.WriteLine("voulez-vous accepter un don ?");
            string reponse = "";
            while (reponse != "oui" && reponse != "non")
            {
                Console.WriteLine("répondez par oui ou par non");
                reponse = Console.ReadLine();
            }

            while (reponse == "oui")
            {
                //choisir le don

                Console.WriteLine("saisissez le numéro de référence du don à saisir");
                string NumRef = Console.ReadLine();
                string don = ExtraireString(NumRef, DonsNonTraites);
                string[] mots = don.Split(";");
                DateTime date = Convert.ToDateTime(mots[1]);
                string Type = mots[2];
                string NomDonateur = mots[3];
                string NumTel = mots[4];
                string adresse = mots[5];
                string description = mots[6];
                mots[7] = Convert.ToString(true);
                bool accepte = Convert.ToBoolean(mots[7]);

                //demander où stocker le don
                int lieu = 0;
                Console.WriteLine("où voulez-vous stocker le don ?");
                while (lieu < 1 || lieu > 3)
                {
                    Console.WriteLine("tapez 1 pour le garde-meuble");
                    Console.WriteLine("tapez 2 pour le dépot-vente");
                    Console.WriteLine("tapez 3 pour l'association");
                    lieu = Convert.ToInt32(Console.ReadLine());
                }
                DateTime DateDepot = DateTime.Now;

                if (lieu == 1)
                {
                    TransfertGardeMeuble(mots);
                }

                if (lieu == 2)
                {
                    TransfertDepotVente(mots);
                }
                if (lieu == 3)
                {
                    Couper_coller(DonsNonTraites, Association, NumRef);
                    string ligne = ExtraireString(NumRef, Association);
                    string[] mots2 = ligne.Split(";");
                    mots2[7] = Convert.ToString(true);
                    ligne = "";
                    for (int i = 0; i < mots2.Length -1; i++)
                    {
                        ligne += mots[i] + ";";
                    }
                    ligne += mots2[mots2.Length - 1];
                    int Numligne = chercher(NumRef, Association);
                    ModifierLigne(Association, Numligne, ligne);
                }
                reponse = "";
                Console.WriteLine("voulez-vous accepter un autre don ?");
                while (reponse != "oui" && reponse != "non")
                {
                    Console.WriteLine("répondez par oui ou par non");
                    reponse = Console.ReadLine();
                }
            }
            Console.WriteLine("voulez-vous refuser un don");
            string reponse2 = "";
            while (reponse2 != "oui" && reponse2 != "non")
            {
                Console.WriteLine("répondez par oui ou par non");
                reponse2 = Console.ReadLine();
            }
            while (reponse2 == "oui")
            {
                Console.WriteLine("saisissez le numéro de référence du don à refuser");
                string NumRef = Console.ReadLine();
                string don = ExtraireString(NumRef, DonsNonTraites);
                string[] mots = don.Split(";");
                mots[7] = Convert.ToString(false);
                string LigneDon2 = "";

                for (int i = 0; i < mots.Length; i++)
                {
                    LigneDon2 += mots[i];
                    LigneDon2 += ";";
                }
                LigneDon2 += DateTime.Now;
                Ecrire(LigneDon2, archive);
                int NumLigne = chercher(mots[0], DonsNonTraites);
                SupprimerLigne(DonsNonTraites, NumLigne);
                reponse2 = "";
                Console.WriteLine("voulez-vous refuser un autre don ?");
                while (reponse2 != "oui" && reponse2 != "non")
                {
                    Console.WriteLine("répondez par oui ou par non");
                    reponse2 = Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// permet à un adhérent de transférer un don à un bénéficiaire, cela enregistre alors le don dans les archives
        /// </summary>
        public void TransfertDonBeneficiaire()
        {         
            Adherent user = Utilisateur();
            Console.WriteLine("où se trouve le don à transférer ?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2 && reponse != 3)
            {
                Console.WriteLine("tapez 1 pour le garde-meuble");
                Console.WriteLine("tapez 2 pour le depot-vente");
                Console.WriteLine("tapez 3 pour l'association");
                reponse = Convert.ToInt32( Console.ReadLine());
            }
            if (reponse == 1)
            {
                List<Objet_volumineux> Liste = listes.List_Objet_volumineux(GardeMeuble);
                Liste.ForEach(x => Console.WriteLine(x.Tostring1()));     //affiche la liste des dons
                Console.WriteLine("Saisissez le numéro de référence du don que vous voulez récupérer");
                //création d'une liste contenant uniquement les identifiants des objets
                
                List<int> NumListe = new List<int>();
                foreach(Objet_volumineux obj in Liste)
                {
                    NumListe.Add(obj.Ref_objet);
                }
                int numero = Convert.ToInt32(Console.ReadLine());
                int indice = NumListe.IndexOf(numero);    //la fonction Indexof permet de vérifier si un élément existe et renvoie dans ce cas son indice dans la liste (renvoie -1 sinon)
                while (indice == -1)
                {
                    Console.WriteLine("objet introuvable, ressaisissez son numéro de référence");
                    numero = Convert.ToInt32(Console.ReadLine());
                    indice = NumListe.IndexOf(numero);
                }
                
                string objet = ExtraireString(Convert.ToString(numero), GardeMeuble);
                string[] mots = objet.Split(";");
                mots[mots.Length - 1] = Convert.ToString( DateTime.Now);
                objet = "";
                for (int i = 0; i < mots.Length-1;i++)
                {
                    objet += mots[i] + ";";
                }
                objet += mots[mots.Length - 1];
                int NumLigne = chercher(Convert.ToString(numero), GardeMeuble);
                Ecrire(objet, archive);
                SupprimerLigne(GardeMeuble, NumLigne);
            }
            if (reponse == 2)
            {
                //stockage de tous les dépots ventes dans une liste
                Liste_Depots_Vente.ForEach(x => Console.WriteLine(x.Tostring2()));
                List<int> NumListe_Depots_Vente = new List<int>();
                foreach(Depot_vente dpv in Liste_Depots_Vente)
                {
                    NumListe_Depots_Vente.Add(dpv.Identifiant);
                }

                //recherche du dépot vente
                Console.WriteLine("saisissez l'identifiant du dépot de vente où se trouve l'objet que vous souhaitez acheter");
                int numero = Convert.ToInt32(Console.ReadLine());

                int indice = NumListe_Depots_Vente.IndexOf(numero);
                while (indice == -1)
                {
                    Console.WriteLine("depot introuvable, ressaisissez son identifiant");
                    numero = Convert.ToInt32(Console.ReadLine());
                    indice = NumListe_Depots_Vente.IndexOf(numero);
                }

                //stockage de tous les objets du dépot vente sélectionné dans une liste
                List<Objet_volumineux> Liste = listes.List_Objet_volumineux(DepotVente);
                

                for (int i = 0; i < Liste.Count; i++)
                {
                    Console.WriteLine("ID : " + Liste[i].LieuStockage.Identifiant);
                    if (Liste[i].LieuStockage.Identifiant != numero)
                    {
                        Liste.RemoveAt(i);
                        i--;
                    }
                }
                List<int> NumListe = new List<int>();
                foreach (Objet_volumineux obj in Liste)
                {
                    NumListe.Add(obj.Ref_objet);
                }

                //recherche de l'objet à acheter
                Console.WriteLine("voici les objets disponibles :");
                Liste.ForEach(x => Console.WriteLine(x.Tostring1()));
                Console.WriteLine("saisissez l'identifiant de l'objet que vous souhaitez acheter");
                int ID = Convert.ToInt32(Console.ReadLine());
                indice = NumListe.IndexOf(ID);
                while (indice==-1)
                {
                    Console.WriteLine("objet introuvable, ressaisissez son numéro de référence");
                    ID = Convert.ToInt32(Console.ReadLine());
                    indice = NumListe.IndexOf(ID);
                }

                //actualisation de la date de vente et archivage
                string objet = ExtraireString(Convert.ToString(ID), DepotVente);
                string[] mots = objet.Split(";");
                mots[16] = Convert.ToString(DateTime.Now);
                string objet2 = "";
                for (int i = 0; i < mots.Length-1;i++)
                {
                    objet2 += mots[i];
                    objet2 += ";";
                }
                objet2 += mots[mots.Length - 1];
                Ecrire(objet2, archive);
                int Numligne = chercher(Convert.ToString(ID), DepotVente);
                SupprimerLigne(DepotVente, Numligne);

                //il faut maintenant actualiser le solde du dépot vente
                Numligne = chercher(Convert.ToString(numero), ListeDepotsVente);
                string ligne = ExtraireString(Convert.ToString(numero), ListeDepotsVente);
                mots = ligne.Split(";");
                double solde = Convert.ToDouble(mots[4]) + Liste[indice].Montant;
                mots[4] = Convert.ToString(solde);
                ligne = "";
                for (int i = 0; i < mots.Length-1; i++)
                {
                    ligne += mots[i] + ";";
                }
                ligne += mots[4];
                ModifierLigne(ListeDepotsVente, Numligne, ligne);
            }
            if (reponse == 3)
            {
                List<Objet_volumineux> Liste = listes.List_Objet_volumineux(Association);
                Liste.ForEach(x => Console.WriteLine(x.Tostring1()));     //affiche la liste des dons
                Console.WriteLine("Saisissez le numéro de référence du don que vous voulez récupérer");
                //création d'une liste contenant uniquement les identifiants des objets
                List<int> NumListe = new List<int>();
                foreach (Objet_volumineux obj in Liste)
                {
                    NumListe.Add(obj.Ref_objet);
                }
                int numero = Convert.ToInt32(Console.ReadLine());
                int indice = NumListe.IndexOf(numero);    //la fonction Indexof permet de vérifier si un élément existe et renvoie dans ce cas son indice dans la liste (renvoie -1 sinon)
                while (indice == -1)
                {
                    Console.WriteLine("objet introuvable, ressaisissez son numéro de référence");
                    numero = Convert.ToInt32(Console.ReadLine());
                    indice = NumListe.IndexOf(numero);
                }
                Couper_coller(Association, archive, Convert.ToString(numero));
            }

            //afficher le fichier des dons non traités de l'association

        }


        /// <summary>
        /// permet de modifier les coordonnées d'une personne (adhérente ou bénéficiaire)
        /// </summary>
        public void ModifierPersonne()
        {        
            Console.WriteLine("quel type de personne voulez-vous modifier ?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("tapez 1 pour modifier un adhérent");
                Console.WriteLine("tapez 2 pour modifier un bénéficiaire");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            string fichier;
            if (reponse ==1)
            {
                fichier = Adherents;
                List<Adherent> Liste = listes.ListeAdherent();
                List<int> NumAdherent = new List<int>();
                foreach(Adherent adh in Liste)
                {
                    NumAdherent.Add(adh.Identifiant);
                }
                Liste.ForEach(x => Console.WriteLine(x));
                Console.WriteLine("saisissez l'identifiant de l'adhérent que vous souhaitez modifier");
                int ID = Convert.ToInt32(Console.ReadLine());
                int indice = NumAdherent.IndexOf(ID);
                while (indice == -1)
                {
                    Console.WriteLine("identifiant introuvable, ressaisissez le");
                    ID = Convert.ToInt32(Console.ReadLine());
                    indice = NumAdherent.IndexOf(ID);
                }
                Console.WriteLine("saisissez son nom");
                string nom = Console.ReadLine();
                Console.WriteLine("saisissez son prénom");
                string prenom = Console.ReadLine();
                Console.WriteLine("saisissez son adresse");
                string adreses = Console.ReadLine();
                Console.WriteLine("saisissez son numéro de téléphone");
                string tel = Console.ReadLine();
                Console.WriteLine("saisissez sa fonction");
                string fonction = Console.ReadLine();
                Adherent adhe = new Adherent(ID, nom, prenom, adreses, tel, fonction);
                string ligne = adhe.Tostring2();
                int Numligne = chercher(Convert.ToString(ID), Adherents);
                ModifierLigne(Adherents, Numligne, ligne);
            }
            else
            {
                fichier = Beneficiaires;
                List<Beneficiaire> Liste = listes.ListeBenef();
                List<int> NumBenef = new List<int>();
                foreach(Beneficiaire benef in Liste)
                {
                    NumBenef.Add(benef.Identifiant);
                }
                Liste.ForEach(x => Console.WriteLine(x));
                Console.WriteLine("saisissez l'identifiant du bénéficiaire que vous souhaitez modifier");
                int ID = Convert.ToInt32(Console.ReadLine());
                int indice = NumBenef.IndexOf(ID);
                while (indice == -1)
                {
                    Console.WriteLine("identifiant introuvable, ressaisissez le");
                    ID = Convert.ToInt32(Console.ReadLine());
                    indice = NumBenef.IndexOf(ID);
                }
                Console.WriteLine("saisissez son nom");
                string nom = Console.ReadLine();
                Console.WriteLine("saisissez son prénom");
                string prenom = Console.ReadLine();
                Console.WriteLine("saisissez son adresse");
                string adreses = Console.ReadLine();
                Console.WriteLine("saisissez son numéro de téléphone");
                string tel = Console.ReadLine();
                Console.WriteLine("saisissez sa date de naissance");
                DateTime DateNaissance = Convert.ToDateTime(Console.ReadLine());
                int Numligne = chercher(Convert.ToString(ID), Beneficiaires);
                Beneficiaire benef2 = new Beneficiaire(ID, nom, prenom, adreses, tel, DateNaissance);
                string ligne = benef2.ToString2();
                ModifierLigne(Beneficiaires, Numligne, ligne);
            }
            
        }


        /// <summary>
        /// permet de supprimer une personne de la liste des adhérents ou bénéficiaires
        /// </summary>
        public void Supprimerpersonne()
        {
            Console.WriteLine("quel type de personne voulez-vous modifier ?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("tapez 1 pour modifier un adhérent");
                Console.WriteLine("tapez 2 pour modifier un bénéficiaire");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                List<Adherent> Liste = listes.ListeAdherent();
                List<int> NumAdherent = new List<int>();
                foreach (Adherent adh in Liste)
                {
                    NumAdherent.Add(adh.Identifiant);
                }
                Liste.ForEach(x => Console.WriteLine(x));
                Console.WriteLine("saisissez l'identifiant de l'adhérent que vous souhaitez supprimer");
                int ID = Convert.ToInt32(Console.ReadLine());
                int indice = NumAdherent.IndexOf(ID);
                while (indice == -1)
                {
                    Console.WriteLine("identifiant introuvable, ressaisissez le");
                    ID = Convert.ToInt32(Console.ReadLine());
                    indice = NumAdherent.IndexOf(ID);
                }
                int Numligne = chercher(Convert.ToString(ID), Adherents);
                SupprimerLigne(Adherents, Numligne);
            }
            else
            {
                List<Beneficiaire> Liste = listes.ListeBenef();
                List<int> NumBenef = new List<int>();
                foreach (Beneficiaire benef in Liste)
                {
                    NumBenef.Add(benef.Identifiant);
                }
                Liste.ForEach(x => Console.WriteLine(x));
                Console.WriteLine("saisissez l'identifiant du bénéficiaire que vous souhaitez supprimer");
                int ID = Convert.ToInt32(Console.ReadLine());
                int indice = NumBenef.IndexOf(ID);
                while (indice == -1)
                {
                    Console.WriteLine("identifiant introuvable, ressaisissez le");
                    ID = Convert.ToInt32(Console.ReadLine());
                    indice = NumBenef.IndexOf(ID);
                }
                int Numligne = chercher(Convert.ToString(ID), Beneficiaires);
                SupprimerLigne(Beneficiaires, Numligne);
            }
        }


        /// <summary>
        /// permet d'ajouter une personna adhérente ou bénéficiaire
        /// </summary>
        public void Ajouterpersonne()
        { 
            Console.WriteLine("quel type de personne voulez-vous ajouter ?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("tapez 1 pour modifier un adhérent");
                Console.WriteLine("tapez 2 pour modifier un bénéficiaire");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine("saisissez son identifiant");
            int identifiant = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("saisissez son nom");
            string nom = Console.ReadLine();
            Console.WriteLine("saisissez son prénom");
            string prenom = Console.ReadLine();
            Console.WriteLine("saisissez son adresse");
            string adresse = Console.ReadLine();
            Console.WriteLine("saisissez son numéro de téléphone");
            string tel = Console.ReadLine();
            if (reponse == 1)
            {
                Console.WriteLine("saisissez sa fonction");
                string fonction = Console.ReadLine();
                Adherent adhe = new Adherent(identifiant, nom, prenom, adresse, tel, fonction);
                Ecrire(adhe.Tostring2(), Adherents);
            }
            else
            {
                Console.WriteLine("saisissez sa Date de naissance en jj/mm/aaaa");
                DateTime date = Convert.ToDateTime(Console.ReadLine());
                Beneficiaire benef = new Beneficiaire(identifiant, nom, prenom, adresse, tel, date);
                Ecrire(benef.ToString2(), Beneficiaires);
            }

 
        }

        /// <summary>
        /// Menu du module don
        /// </summary>
        public void MenuDon()
        { 
            bool fin = false;

            do
            {
                int reponse = 0;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("    ------------------------------------");
                    Console.WriteLine("1 : Creation d'un don+Stockage du don");
                    Console.WriteLine("2 : Transfert d'un don à un bénéficiaire");
                    Console.WriteLine("3 : Archiver un don");
                    Console.WriteLine("4 : Retour vers le Menu");
                    Console.WriteLine("    ------------------------------------");
                    reponse = Convert.ToInt32(Console.ReadLine());

                } while (reponse < 1 || reponse > 4);

                switch (reponse)
                {
                    case 1:
                        Creation_don();
                        break;
                    case 2:
                        accepterDon();
                        break;
                    case 3:
                        TransfertDonBeneficiaire();
                        break;
                    case 4:
                        fin = true;
                        Console.Clear();
                        break;
                }
            }
            while (!fin);
        }

        /// <summary>
        /// Menu du module personne
        /// </summary>
        public void MenuPersonne()
        {
            bool fin = false;
            do
            {
                int reponse = 0;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("    ------------------------------------");
                    Console.WriteLine("1 : Afficher les adhérents et les bénéficiaires ");
                    Console.WriteLine("2 : Rechercher un bénéficiaire");
                    Console.WriteLine("3 : Ajouter/Modifier/supprimer une personne");
                    Console.WriteLine("4 : Retour vers le Menu");
                    Console.WriteLine("    ------------------------------------");
                    reponse = Convert.ToInt32(Console.ReadLine());
                } while (reponse < 1 || reponse > 4);

                switch (reponse)
                {
                    case 1:
                        List<Adherent> ListeAdherents = listes.ListeAdherent();
                        List<Beneficiaire> ListeBenef = listes.ListeBenef();
                        Console.WriteLine("liste des adhérents");
                        ListeAdherents.ForEach(x =>Console.WriteLine( x.Affichage()));
                        Console.WriteLine("\nliste des bénéficiaires");
                        ListeBenef.ForEach(x => Console.WriteLine(x.Affichage()));
                        break;
                    case 2:
                        Beneficiaire benef = RechercheBeneficiaire();
                        Console.WriteLine(benef.Affichage());
                        break;
                    case 3:
                        Console.WriteLine("voulez vous ajouter, modifier ou supprimer une personne ?");
                        int reponse2 = 0;
                        while (reponse2 <1 || reponse2 >3)
                        {
                            Console.WriteLine("tapez 1 pour ajouter une personne");
                            Console.WriteLine("tapez 2 pour modifier une personne");
                            Console.WriteLine("tapez 3 pour supprimer une personne");
                            reponse2 = Convert.ToInt32(Console.ReadLine());
                        }
                        switch (reponse2)
                        {
                            case 1:
                                Ajouterpersonne();
                                break;
                            case 2:
                                ModifierPersonne();
                                break;
                            case 3:
                                Supprimerpersonne();
                                break;
                        }
                        break;
                    case 4:
                        fin = true;
                        Console.Clear();
                        break;
                }
            } while (!fin);
        }

    }
}