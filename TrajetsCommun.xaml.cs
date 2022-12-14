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
    public sealed partial class TrajetsCommun : Page
    {
        public TrajetsCommun()
        {
            this.InitializeComponent();
            Affiche.ItemsSource = Gestionnaire.getInstance().GetTrajetsAdmin();
            villeComprise.ItemsSource = Gestionnaire.getInstance().AfficheViller();

        }



        private void RechercheTrajet_event(object sender, RoutedEventArgs e)
        {
            Affiche.ItemsSource = Gestionnaire.getInstance().rechercheTrajets(Convert.ToDateTime(date.Date.Value.ToString("yyyy-MM-dd")));
        }

        private void TousLesTrajets_event(object sender, RoutedEventArgs e)
        {
            Affiche.ItemsSource = Gestionnaire.getInstance().GetTrajetsAdmin();
        }

        private void AjouterVille_event(object sender, RoutedEventArgs e)
        {
            Gestionnaire.getInstance().AjouterVille(nomVilleAjout.Text);
            nomVilleAjout.Text = "";
        }

        private async void Period_event(object sender, RoutedEventArgs e)
        {
            Affiche.ItemsSource = Gestionnaire.getInstance().rechercherPeriode((Convert.ToDateTime(dateDebut.Date.Value.ToString("yyyy-MM-dd"))) , (Convert.ToDateTime(dateFin.Date.Value.ToString("yyyy-MM-dd"))));
        }

        private async void  Csv_event(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileSavePicker();



            picker.SuggestedFileName = "test";
            picker.FileTypeChoices.Add("Fichier csv", new List<string>() { ".csv" });

            //crée le fichier
            Windows.Storage.StorageFile monFichier = await picker.PickSaveFileAsync();

            List<Trajets> liste = Gestionnaire.getInstance().GetTrajets().ToList();


            //écrit dans le fichier chacune des lignes du tableau
            //une boucle sera faite sur la collection et prendra chacun des objets de celle-ci
            await Windows.Storage.FileIO.WriteLinesAsync(monFichier, liste.ConvertAll(x => x.ToString()), Windows.Storage.Streams.UnicodeEncoding.Utf8);

        }
    }
}
