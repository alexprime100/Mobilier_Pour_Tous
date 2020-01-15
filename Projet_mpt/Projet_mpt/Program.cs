using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace Projet_mpt
{
    class Program
    {
        public static string Adherents = "Adherents.txt";
        public static string ListeDepotsVente = "Liste_depots_vente.txt";
             
        
        static void MenuPrincipal()
        {
            //création de la personne morale correspondant à l'association, indispensable pour le fonctionnement des méthodes stockées dans les autres classes
            Personne_morale association = new Personne_morale(0, "Mobilier ", "Pour Tous", "20 rue du Baroux 95004","0160529555","association");
            //création des principales listes indispensables
            Listes listes = new Listes(ListeDepotsVente, Adherents, association);
            List<Beneficiaire> ListeBeneficiaires = listes.ListeBenef();
            List<Adherent> ListeAdherents = listes.ListeAdherent();
            List<Depot_vente> Liste_Depots_Vente = listes.ListDepotsVente();
            //déclaration des modules don, tri et statistiques afin d'utiliser leurs méthodes
            ManipulationDon ModuleDon = new ManipulationDon(association);
            Tri ModuleTri = new Tri(association);
            Statistiques ModuleStats = new Statistiques(association);
            bool arret = false;
            while (!arret)
            {
                int reponse = 0;
                do
                {
                    Console.WriteLine();
                    Console.WriteLine("    ------------------------------------");
                    Console.WriteLine("1 : Module personne");
                    Console.WriteLine("2 : Module Don");
                    Console.WriteLine("3 : Module Tri");
                    Console.WriteLine("4 : Module statistiques");
                    Console.WriteLine("5 : fin");
                    Console.WriteLine("    ------------------------------------");

                    reponse = Convert.ToInt32(Console.ReadLine());
                }
                while (reponse < 1 || reponse > 5);

                switch(reponse)
                {
                    case 1:
                        ModuleDon.MenuPersonne();
                        break;
                    case 2:
                        ModuleDon.MenuDon();
                        break;
                    case 3:
                        ModuleTri.MenuTri();
                        break;
                    case 4:
                        ModuleStats.Menu();
                        break;
                    case 5:
                        arret = true;
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            MenuPrincipal();
            
            Console.ReadKey();
        }
    }
}