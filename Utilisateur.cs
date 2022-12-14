using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Essai2Projet
{
    internal class Utilisateur
    {
        string nom;
        string prenom;
        string motDePasse;
        string adresse;
        string numero;
        string email;
        string statut;
        int nombre;

        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string MotDePasse { get => motDePasse; set => motDePasse = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string Numero { get => numero; set => numero = value; }
        public string Email { get => email; set => email = value; }
        public string Statut { get => statut; set => statut = value; }
        public int Nombre { get => nombre; set => nombre = value; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
        this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public override string ToString()
        {
            return nom + " " + prenom + " " + adresse + " " + numero + " " + email + " " + statut;
        }
    }
}
