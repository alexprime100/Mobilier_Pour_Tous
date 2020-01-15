using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_mpt
{
    interface IStatistiques
    {
        //interface utilisé dans la classe Statistiques
        double MoyennePrix();

        double MoyenneDate();

        double MoyenneAgeBenef();

        int nombre_dons_recus();

        int Nombre_donateurs();

        int NombreBeneficiaires();

        int NbDonsAcceptes();

        double RatioRecusAcceptes();
    }
}
