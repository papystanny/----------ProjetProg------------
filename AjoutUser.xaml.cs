using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Text;


// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Essai2Projet
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class AjoutUser : Page
    {
        Boolean validite = true;
        Boolean cD = false;
        

        public AjoutUser()
        {
            this.InitializeComponent();
        }

        public bool CD { get => cD;  }

        private string genererSHA256(string texte)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texte));

            StringBuilder sb = new StringBuilder();
            foreach (Byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }


        private async  void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nom.Text == null || nom.Text == "")
            {
                erreurNom.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurNom.Visibility = Visibility.Collapsed;
            }


            if (prenom.Text == null || prenom.Text == "")
            {
                erreurPrenom.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurPrenom.Visibility = Visibility.Collapsed;
            }

            if (adresse.Text == null || adresse.Text == "")
            {
                erreurAdresse.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurAdresse.Visibility = Visibility.Collapsed;
            }

            //Demander à fabrice comment faire le regexp
            if (numeroTelephone.Text == null || numeroTelephone.Text == "" /*|| numeroTelephone.Text != System.Text.RegularExpressions.Regex.IsMatch(this.numeroTelephone.Text, @"^[a-zA-Z]+$")*/)
            {
                erreurNumeroTelephone.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurNumeroTelephone.Visibility = Visibility.Collapsed;
            }

            if (email.Text == null || email.Text == "")
            {
                erreurEmail.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurEmail.Visibility = Visibility.Collapsed;
            }

            if (motDePasse.Password == null || motDePasse.Password == "")
            {

                erreurMotDePasse.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurMotDePasse .Visibility = Visibility.Collapsed;
            }

            if (validite)
            {
                Utilisateur c = new Utilisateur()
                {
                    Nom = nom.Text,
                    Prenom = prenom.Text,
                    Adresse = adresse.Text,
                    Numero = numeroTelephone.Text,
                    Email = email.Text,
                    MotDePasse = motDePasse.Password,
                    Statut = statut.SelectedItem.ToString()
                };
               cD=  Gestionnaire.getInstance().insererUser(c);
                if (cD)
                {
                    ContentDialog x = new ContentDialog();
                    x.XamlRoot = cd.XamlRoot;
                    x.Title = " Bravo ! ";
                    x.CloseButtonText = "OK";
                    x.Content = "Le compte a été crée";

                    var result = await x.ShowAsync();
                  
                }
                else
                {
                    ContentDialog x = new ContentDialog();
                    x.XamlRoot = cd.XamlRoot;
                    x.Title = " Erreur ";
                    x.CloseButtonText = "OK";
                    x.Content = "Veuillez réessayer ";

                    var result = await x.ShowAsync();
                }
                nom.Text = "";
                prenom.Text = "";
                adresse.Text = "";
                numeroTelephone.Text = "";
                email.Text = "";
                motDePasse.Password = "";
                statut.Text = "";
               
            }
        }
    }
}
