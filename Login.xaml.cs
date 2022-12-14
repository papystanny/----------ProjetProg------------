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
using System.Security.Cryptography;

// Pour plus d'informations sur le modèle d'élément Boîte de dialogue de contenu, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Essai2Projet
{
    public sealed partial class Login : ContentDialog
    {
        bool ok = false;
        Boolean validite = true;

        public bool Ok { get => ok; }

        public Login()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
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
                erreurMotDePasse.Visibility = Visibility.Collapsed;
            }

            if (validite)
            {
                Utilisateur c = new Utilisateur()
                {
                    Email = email.Text,
                    Statut = statut.SelectedItem.ToString(),
                    MotDePasse = motDePasse.Password
                };
                ok = Gestionnaire.getInstance().Connexion(c);
              
              


            }

        }

      
        private void ContentDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ok = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
