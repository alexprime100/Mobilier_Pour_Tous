using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    class Tri
    {
        //classe contenant les méthodes du module tri
        private Personne_morale association;       
        private List<Beneficiaire> ListeBeneficiaires;
        private string archive;
        private string Association;
        private string GardeMeuble;
        private string DepotVente;
        private Listes listes;

        public Tri(Personne_morale association)
        {
            this.Association = "Association.txt";
            this.archive = "Archives.txt";
            this.GardeMeuble = "Garde_Meuble.txt";
            this.DepotVente = "Depot_vente.txt";
            this.association = association;
            this.listes = new Listes(association);
            ListeBeneficiaires = listes.ListeBenef();
            
        }

        public List<Objet_volumineux> List_Objet_volumineux(string fichier)
        {
            return listes.List_Objet_volumineux(fichier);
        }

        
        

        public void Affichage(List<Objet_volumineux> Objets)
        {
            foreach(Objet_volumineux Obj in Objets)
            {
                Console.WriteLine(Obj);
            }
        }

        /// <summary>
        /// Permet d'additioner plusieurs listes en une seule liste
        /// </summary>
        public List<Objet_volumineux> Addition(List<List<Objet_volumineux>> Liste)
        {
            List<Objet_volumineux> retour = new List<Objet_volumineux>();
            foreach (List<Objet_volumineux> liste in Liste)
            {
                foreach (Objet_volumineux objet in liste)
                {
                    retour.Add(objet);
                }
            }
            return retour;
        }

        /// <summary>
        /// Permet de trier les dons qui ont été refusés
        /// </summary>
        public List<Objet_volumineux> TriDonsRefuse()
        {
            List<Objet_volumineux> tab = List_Objet_volumineux(archive);
            for (int i = 0; i < tab.Count; i++)
            {
                if (tab[i].Accepte)
                {
                    tab.RemoveAt(i);
                    i--;
                }
            }
            tab.Sort();
            return tab;
        }

        /// <summary>
        /// Permet de trier les dons en traitement 
        /// </summary>
        public List<Objet_volumineux> TriDonsTraitement()
        {
            List<Objet_volumineux> ListeAsso = List_Objet_volumineux(this.Association);
            for (int i = 0; i < ListeAsso.Count; i++)
            {
                if (!ListeAsso[i].Accepte)
                {
                    ListeAsso.RemoveAt(i);
                    i--;
                }
            }
            List<List<Objet_volumineux>> tab = new List<List<Objet_volumineux>>();
            tab.Add(List_Objet_volumineux(this.GardeMeuble));
            tab.Add(List_Objet_volumineux(this.DepotVente));
            tab.Add(ListeAsso);
            List<Objet_volumineux> Liste = Addition(tab);
            Console.WriteLine("voulez-vous trier les dons par numéro de référence ou par nom de donataire ?");
            int reponse = -1;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("répondez 1 pour le numéro de référence ou 2 pour le nom de donataire");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                Liste.Sort(Objet_volumineux.ComparaisonRef);
            }
            else
            {
                Liste.Sort(Objet_volumineux.ComparaisonNom);
            }

            return Liste;
        }

        /// <summary>
        /// Permet de trier les dons Vendus donnés
        /// </summary>
        public List<Objet_volumineux> TriDonsVendusDonnes()
        {
            List<Objet_volumineux> Liste = List_Objet_volumineux(this.archive);
            for (int i = 0; i < Liste.Count; i++)
            {
                if (!Liste[i].Accepte)
                {
                    Liste.RemoveAt(i);
                    i--;
                }
            }
            Console.WriteLine("voulez-vous trier les dons par mois ou par numéro de bénéficiaire ?");
            int reponse = -1;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("répondez 1 pour un tri par mois ou 2 pour un tri par numéro de bénéficiaire");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                Liste.Sort(Objet_volumineux.ComparaisonMois);
            }
            else
            {
                Liste.Sort(Objet_volumineux.ComparaisonNumBenef);
            }
            return Liste;
        }

        /// <summary>
        /// Permet de trier les dons stockés
        /// </summary>
        public List<Objet_volumineux> TriDonsStockes()
        {
            List <List<Objet_volumineux>> liste = new List<List<Objet_volumineux>>();
            liste.Add(List_Objet_volumineux(this.GardeMeuble));
            liste.Add(List_Objet_volumineux(this.DepotVente));
            liste.Add(List_Objet_volumineux(this.Association));
            List<Objet_volumineux> Liste = Addition(liste);
            Console.WriteLine("souhaitez-vous trier par entrepot ou description?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("répondez 1 pour le tri par entrepot");
                Console.WriteLine("répondez 2 pour le tri par description");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                Liste.Sort(Objet_volumineux.ComparaisonEntrepot);
            }
            else
            {
                Liste.Sort(Objet_volumineux.ComparaisonDescription);
            }
            return Liste;
        }

        /// <summary>
        /// tri les objets par entrepots ou par volume
        /// </summary>
        /// <returns></returns>
        public List<Objet_volumineux> TriEntrepotVolume()
        {
            List<List<Objet_volumineux>> liste = new List<List<Objet_volumineux>>();
            liste.Add(List_Objet_volumineux(this.GardeMeuble));
            liste.Add(List_Objet_volumineux(this.DepotVente));
            liste.Add(List_Objet_volumineux(this.Association));
            List<Objet_volumineux> Liste = Addition(liste);
            for (int i = 0; i < Liste.Count; i++)
            {
                if (Liste[i].Volume == 0)
                {
                    Liste.RemoveAt(i);
                    i--;
                }
            }
            Console.WriteLine("Voulez-vous trier par entrepot ou par volume ?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("répondez 1 pour le tri par entrepot");
                Console.WriteLine("répondez 2 pour le tri par volume");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                Liste.Sort(Objet_volumineux.ComparaisonEntrepot);
            }
            else
            {
                Liste.Sort(Objet_volumineux.ComparaisonVolume);
            }
            return Liste;
        }

        /// <summary>
        /// tri les objets par depots ventes ou par prix
        /// </summary>
        /// <returns></returns>
        public List<Objet_volumineux> DepotVente_Prix()
        {
            List<Objet_volumineux> Liste = List_Objet_volumineux(this.DepotVente);
            Console.WriteLine("voulez-vous trier par Depot vente ou par prix ?");
            int reponse = 0;
            while (reponse != 1 && reponse != 2)
            {
                Console.WriteLine("répondez 1 pour le tri par Depot-Vente");
                Console.WriteLine("répondez 2 pour le tri par prix");
                reponse = Convert.ToInt32(Console.ReadLine());
            }
            if (reponse == 1)
            {
                Liste.Sort(Objet_volumineux.ComparaisonDepotVente);
            }
            else
            {
                Liste.Sort(Objet_volumineux.ComparaisonPrix);
            }
            return Liste;
        }

        /// <summary>
        /// Menu déroulant de Tri 
        /// </summary>
        public void MenuTri()
        {
            bool arret = false;
            do
            {
                int reponse = 0;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("    ------------------------------------");
                    Console.WriteLine("1 : Trier les dons refusés par date");
                    Console.WriteLine("2 : trier les dons en traitement par identifiant ou par nom de donataire");
                    Console.WriteLine("3 : Trier les dons vendus/donnés par mois ou par numéro de bénéficiaire");
                    Console.WriteLine("4 : Trier les dons stockés par entrepot ou par description");
                    Console.WriteLine("5 : Trier les dons par volume ou par entrepot");
                    Console.WriteLine("6 : Trier les dons par prix ou par depot vente");
                    Console.WriteLine("7 : retour au menu");
                    Console.WriteLine("    ------------------------------------");
                    reponse = Convert.ToInt32(Console.ReadLine());
                } while (reponse < 1 || reponse > 7);
                List<Objet_volumineux> Liste = new List<Objet_volumineux>();
                switch (reponse)
                {
                    case 1:
                        Liste = TriDonsRefuse();
                        break;
                    case 2:
                        Liste = TriDonsTraitement();
                        break;
                    case 3:
                        Liste = TriDonsVendusDonnes();
                        break;
                    case 4:
                        Liste = TriDonsStockes();
                        break;
                    case 5:
                        Liste = TriEntrepotVolume();
                        break;
                    case 6:
                        Liste = DepotVente_Prix();
                        break;
                    case 7:
                        arret = true;
                        Console.Clear();
                        break;
                    
                }
                Liste.ForEach(x => Console.WriteLine(x.Tostring1()));

            } while (!arret);
        }
    }
}


