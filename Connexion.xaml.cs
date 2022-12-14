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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace Essai2Projet
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class Connexion : Page
    {
        Boolean validite = true;
        public Connexion()
        {
            this.InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
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
                    Nom = nom.Text,
                    Statut = statut.SelectedItem.ToString(),
                    MotDePasse = motDePasse.Password
                };
                Gestionnaire.getInstance().Connexion(c);
                nom.Text ="";
                motDePasse.Password ="";

            }
        }
    }
}
