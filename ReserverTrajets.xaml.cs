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
    public sealed partial class ReserverTrajets : Page
    {
        bool cd = false;
        public ReserverTrajets()
        {
            this.InitializeComponent();
            Affichage.ItemsSource = Gestionnaire.getInstance().GetTrajets();

            if(Gestionnaire.getInstance().nbPLace())
            {
                
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Trajets t = ((FrameworkElement)sender).DataContext as Trajets;
            cd = Gestionnaire.getInstance().reserverTrajets(t.IdTrajet);
            if(cd)
            {

                ContentDialog x = new ContentDialog();
                x.XamlRoot = main.XamlRoot;
                x.Title = " Bravo ! ";
                x.CloseButtonText = "OK";
                x.Content = "Trajet réserver";

                var result = await x.ShowAsync();
            }
            else
            {

                ContentDialog x = new ContentDialog();
                x.XamlRoot = main.XamlRoot;
                x.Title = " Erreur ";
                x.CloseButtonText = "OK";
                x.Content = "Impossible de réserver ce Trajet";

                var result = await x.ShowAsync();
            }
        
        }
    }
}
