using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Essai2Projet
{
    internal  class Trajets 
    {
        string date;
        string heureDepart;
        string heureArrive;
        string villeDepart;
        string villeArrive;
        string nomChauffeur;
        string arret;
        string typeVehicule;
        int montant;
        int montantS;
        int prixPlace;
        int nbPlace;
        int idTrajet;
        int nbClient;
        int revenuTotal;

        public string Date { get => date; set => date = value; }
        public string HeureDepart { get => heureDepart; set => heureDepart = value; }
        public string HeureArrive { get => heureArrive; set => heureArrive = value; }
        public string VilleDepart { get => villeDepart; set => villeDepart = value; }
        public string VilleArrive { get => villeArrive; set => villeArrive = value; }
        public string NomChauffeur { get => nomChauffeur; set => nomChauffeur = value; }
        public int Montant { get => montant; set => montant = value; }
        public string Arret { get => arret; set => arret = value; }
        public string TypeVehicule { get => typeVehicule; set => typeVehicule = value; }
        public int NbPlace { get => nbPlace; set => nbPlace = value; }
        public int IdTrajet { get => idTrajet; set => idTrajet = value; }
        public int NbClient { get => nbClient; set => nbClient = value; }
        public int MontantS { get => montantS; set => montantS = value; }
        public int PrixPlace { get => prixPlace; set => prixPlace = value; }
        public int RevenuTotal { get => revenuTotal; set => revenuTotal = value; }

        public override string ToString()
        {
            return date + ";" + heureDepart + ";" + heureArrive + ";" + villeDepart + ";" + villeArrive + ";" + nomChauffeur + ";" +";" + montant;
        }

    }
}
