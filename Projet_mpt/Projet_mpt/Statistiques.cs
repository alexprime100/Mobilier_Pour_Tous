using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Projet_mpt
{
    class Statistiques : IStatistiques
    {
        //classe contenant les méthodes du module statistique, les attributs sont les listes et les fichiers nécessaires aux calculs statistiques

        private Personne_morale association;
        private string DonsNonTraites;
        private string Association;
        private string GardeMeuble;
        private string DepotVente;
        private string archive;
        private List<Adherent> ListeAdherents;
        private string Adherents;
        private Listes listes;
        private List<Beneficiaire> ListeBeneficiaires;
        private List<Depot_vente> Liste_Depots_Vente;
        private string ListeDepotsVente;

        public Statistiques(Personne_morale association)
        {
            this.Association = "Association.txt";
            this.GardeMeuble = "Garde_Meuble.txt";
            this.DepotVente = "Depot_vente.txt";
            this.archive = "Archives.txt";
            this.association = association;
            this.ListeDepotsVente = "Liste_depots_vente.txt";
            this.Adherents = "Adherents.txt";
            this.DonsNonTraites = "Dons_non_traites.txt";
            this.listes = new Listes(ListeDepotsVente, Adherents, association);
            this.ListeAdherents = listes.ListeAdherent();
            this.ListeBeneficiaires = listes.ListeBenef();
            this.Liste_Depots_Vente = listes.ListDepotsVente();
        }

        /// <summary>
        /// menu du module
        /// </summary>
        public void Menu()
        {
            bool fin = false;

            do
            {
                int reponse = 0;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("    ------------------------------------");
                    Console.WriteLine("1 : Nombre de proposition de dons recus");
                    Console.WriteLine("2 : Nombre de donateurs, de bénéficiaires");
                    Console.WriteLine("3 : Nombre  de  propositions  de  dons  acceptées  et  ratio  reçues/acceptées  pour  les  objets volumineux");
                    Console.WriteLine("4 : Volume des ventes");
                    Console.WriteLine("5 : Principales catégories d’articles en stock");
                    Console.WriteLine("6 : Moyenne de temps entre la date de réception et la date de retrait des dons dans les zones de stockage");
                    Console.WriteLine("7 : moyenne de prix des objets dans les dépôts-ventes");
                    Console.WriteLine("8 : moyenne d’âge des bénéficiaires");
                    Console.WriteLine("9 : Retour vers Menu");
                    Console.WriteLine("    ------------------------------------");
                    reponse = Convert.ToInt32(Console.ReadLine());

                } while (reponse < 1 || reponse > 9);

                switch (reponse)
                {
                    case 1:
                        int nombredon = nombre_dons_recus();
                        Console.WriteLine("Le nombre de dons recus au total est: "+nombredon);
                        break;
                    case 2:
                        int nbredonateur = Nombre_donateurs();
                        int nbrebenef = NombreBeneficiaires();
                        Console.WriteLine("Le nombre de donateurs est: "+nbredonateur);
                        Console.WriteLine("Le nombre de beneficiaires est: " + nbrebenef);
                        break;
                    case 3:
                        int NbDonsAccepte = NbDonsAcceptes();
                        double ratio = RatioRecusAcceptes();
                        Console.WriteLine("sur les " + NbDonsAccepte + " recus, seulement " + ratio + " ont été accepté");
                        break;
                    case 4:
                        double volume = VolumeVentes();
                        Console.WriteLine("le volume total des ventes est de " + volume + "m3");
                        break;
                    case 5:
                        Console.WriteLine("saisissez le nombre de catégories principales que vous souhaitez voir :");
                        int nombre = Convert.ToInt32(Console.ReadLine());
                        while (nombre < 1)
                        {
                            Console.WriteLine("ressaisissez ce nombre, il doit etre strictement positif");
                            nombre = Convert.ToInt32(Console.ReadLine());
                        }
                        Console.WriteLine("les principales catégories d'articles sont : ");
                        List<string> Categories = CategoriesPrincipales(nombre);
                        Categories.ForEach(x => Console.WriteLine(x));
                        break;
                    case 6:
                        double MoyenneTemps = MoyenneDate();
                        Console.WriteLine("la moyenne ed temps entre la date de réception et la date de retrait des dons dans les zones de stockage est de " + MoyenneTemps + " jours");
                        break;
                    case 7:
                        double PrixMoyen = MoyennePrix();
                        Console.WriteLine("le prix moyen des objets dans les dépots ventes est de " + PrixMoyen + "€");
                        break;
                    case 8:
                        double AgeMoyen = MoyenneAgeBenef();
                        Console.WriteLine("l'âge moyen des bénéficiaires est de " + AgeMoyen + " ans");
                        break;
                    case 9:
                        fin = true;
                        Console.Clear();
                        break;
                }
            }
            while (!fin);
            
        }

        /// <summary>
        /// renvoie la liste principales categories d'objets en stock (le nombre de categories à afficher est saisi par l'utilisateur)
        /// </summary>
        public List<string> CategoriesPrincipales(int nombre)
        {
            List<Objet_volumineux> Liste1 = listes.List_Objet_volumineux(GardeMeuble);
            List<Objet_volumineux> Liste2 = listes.List_Objet_volumineux(DepotVente);
            List<Objet_volumineux> Liste3 = listes.List_Objet_volumineux(Association);
            List<List<Objet_volumineux>> Listes = new List<List<Objet_volumineux>>();
            Listes.Add(Liste1);
            Listes.Add(Liste2);
            Listes.Add(Liste3);
            List<Objet_volumineux> Liste = listes.Addition(Listes);

            List<string> Categories = new List<string>();
            foreach(Objet_volumineux objet in Liste)
            {
                if (!Doublon(objet.Type_materiel, Categories))
                {
                    Categories.Add(objet.Type_materiel);
                }
            }
            List<int> CoeffCategories = new List<int>();
            for (int i = 0; i < Categories.Count; i++)
            {
                CoeffCategories.Add(0);
                foreach(Objet_volumineux objet in Liste)
                {
                    if (objet.Type_materiel == Categories[i])
                    {
                        CoeffCategories[i]++;
                    }
                }
            }
            int indiceMin = -1;
            while (Categories.Count > nombre)
            {
                indiceMin = IndideMin(CoeffCategories);
                CoeffCategories.RemoveAt(indiceMin);
                Categories.RemoveAt(indiceMin);
            }
            return Categories;
        }

        /// <summary>
        /// renvoie l'indice du minimum d'une liste d'entiers positifs
        /// </summary>
        public int IndideMin(List<int> tab)
        {
            int min = 0;
            int indicemin = 0;
            for (int i = 0; i < tab.Count; i++)
            {
                if (min > tab[i])
                {
                    min = tab[i];
                    indicemin = i;
                }
            }
            return indicemin;
        }

        /// <summary>
        /// vérifie si un élément existe déjà dans une liste
        /// </summary>
        public bool Doublon(string Mot, List<string> Liste)
        {
            foreach(string mot in Liste)
            {
                if (Mot == mot)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// renvoie le volume total des ventes
        /// </summary>
        /// <returns></returns>
        public double VolumeVentes()
        {
            List<Objet_volumineux> Liste = listes.List_Objet_volumineux(archive);
            double volume = 0;
            foreach(Objet_volumineux objet in Liste)
            {
                volume += objet.Volume;
            }
            return volume;
        }

        /// <summary>
        /// renvoie le nombre de dons reçus
        /// </summary>
        public int nombre_dons_recus()
        {
            List<string> Liste1 = File.ReadAllLines(Association).ToList();
            List<string> Liste2 = File.ReadAllLines(GardeMeuble).ToList();
            List<string> Liste3 = File.ReadAllLines(DepotVente).ToList();
            List<string> Liste4 = File.ReadAllLines(DonsNonTraites).ToList();
            List<string> Liste5 = File.ReadAllLines(archive).ToList();

            int somme = Liste1.Count() + Liste2.Count() + Liste3.Count() + Liste4.Count() + Liste5.Count();
            return somme;
        }

        /// <summary>
        /// renvoie le nombre de donateurs qui est égal au nombre d'adhérents
        /// </summary>
        public int Nombre_donateurs()
        {
            int nombre = ListeAdherents.Count();
            return nombre;
        }

        /// <summary>
        /// renvoie le nombre de bénéficiaires
        /// </summary>
        /// <returns></returns>
        public int NombreBeneficiaires()
        {
            int nombre = ListeBeneficiaires.Count();
            return nombre;
        }

        /// <summary>
        /// renvoie le prix moyen des objets en depot vente
        /// </summary>
        /// <returns></returns>
        public double MoyennePrix()
        {
            List<Objet_volumineux> Liste = listes.List_Objet_volumineux(DepotVente);
            double moyenne = 0;
            foreach(Objet_volumineux obj in Liste)
            {
                moyenne += obj.Montant;
            }
            moyenne /= Liste.Count();
            return moyenne;
        }

        /// <summary>
        /// renvoie la durée moyenne (en jours) entre les dates de depot et d'enlevement/vente d'un objet
        /// </summary>
        public double MoyenneDate()
        {
            double moyenne = 0;
            List<Objet_volumineux> Liste = listes.List_Objet_volumineux(archive);
            for (int i = 0; i < Liste.Count; i++)
            {
                if (!Liste[i].Accepte)
                {
                    Liste.RemoveAt(i);
                    i--;
                }
            }
            foreach(Objet_volumineux objet in Liste)
            {
                string[] mots = objet.Tostring1().Split(";");
                DateTime DateEnlevement = Convert.ToDateTime(mots[mots.Length]);
                DateTime DateDepot = Convert.ToDateTime(mots[mots.Length - 2]);
                moyenne += Convert.ToDouble(DateEnlevement - DateDepot);
            }
            moyenne /= Liste.Count;
            return moyenne;
        }

        /// <summary>
        /// renvoie l'age moyen des béneficiaires
        /// </summary>
        /// <returns></returns>
        public double MoyenneAgeBenef()
        {
            List<Beneficiaire> Liste = listes.ListeBenef();
            double moyenne = 0;
            foreach(Beneficiaire benef in Liste)
            {
                moyenne += Convert.ToDouble(DateTime.Now - benef.Date_naissance) / 365;
            }
            moyenne /= Liste.Count;
            return moyenne;
        }

        /// <summary>
        /// renvoie le nombre total de dons acceptes
        /// </summary>
        public int NbDonsAcceptes()
        {
            List<Objet_volumineux> Liste1 = listes.List_Objet_volumineux(archive);
            List<Objet_volumineux> Liste2 = listes.List_Objet_volumineux(GardeMeuble);
            List<Objet_volumineux> Liste3 = listes.List_Objet_volumineux(DepotVente);
            List<Objet_volumineux> Liste4 = listes.List_Objet_volumineux(Association);
            List<List<Objet_volumineux>> Listes = new List<List<Objet_volumineux>>();
            Listes.Add(Liste1);
            Listes.Add(Liste2);
            Listes.Add(Liste3);
            Listes.Add(Liste4);
            List<Objet_volumineux> Liste = listes.Addition(Listes);

            for (int i = 0; i < Liste.Count; i++)
            {
                if(!Liste[i].Accepte || Liste[i].Volume == 0)
                {
                    Liste.RemoveAt(i);
                }
            }
            return Liste.Count;
        }

        /// <summary>
        /// renvoie le ratio de dons recus/acceptes
        /// </summary>
        /// <returns></returns>
        public double RatioRecusAcceptes()
        {
            double total = nombre_dons_recus();
            double ratio = 100 * NbDonsAcceptes() / total;
            return ratio;
        }
    }
}
