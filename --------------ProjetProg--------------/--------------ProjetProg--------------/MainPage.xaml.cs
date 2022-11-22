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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ______________ProjetProg______________
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
       public MainPage()
        {         
            this.InitializeComponent();
         //   AffichageTrajet.ItemsSource = Gestionnaire.getInstance().GetTrajets();
        }

        private void NavigationView_SelectionChanged(Windows.UI.Xaml.Controls.NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;
            header.Text = item.Content.ToString();              // Code pour alle attraper le header indiqué lorsqu'on clique sue un item du Navigattion 

            switch (item.Name)
            {
                case "connexion":
                    mainFrame.Navigate(typeof());
                    break;

                case "ajouterCompte":
                    mainFrame.Navigate(typeof());
                    break;

                case "homeAdministrateur":
                    mainFrame.Navigate(typeof());
                    break;

                case "homeClients":
                    mainFrame.Navigate(typeof());
                    break;
                case "homeChauffeur":
                    mainFrame.Navigate(typeof());
                    break;

                default:
                    break;

            }
        }
}
