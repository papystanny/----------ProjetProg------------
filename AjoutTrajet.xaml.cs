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
    public sealed partial class AjoutTrajet : Page
    {
        Boolean validite = true;
        public AjoutTrajet()
        {
            this.InitializeComponent();
            villeA.ItemsSource = Gestionnaire.getInstance().AfficheViller();
            villeD.ItemsSource = Gestionnaire.getInstance().AfficheViller();
            nomChauffeur.ItemsSource = Gestionnaire.getInstance().GetNomChauffeurs();
    /*        date.Date.Value = DateTime.Now;
            heureD.Time = TimeSpan.FromDays;          */

        }

        private void AddTrajets_event(object sender, RoutedEventArgs e)
        {
          if(heureD.Time>heureA.Time)
            {
                validite = false;
                erreurHeure.Visibility = Visibility.Visible;
            }
          else
                erreurHeure.Visibility = Visibility.Collapsed;


        if (villeA.Text.Equals(villeD.Text))
            {
                validite = false;
                erreurVille.Visibility = Visibility.Visible;
            }
        else
                erreurVille.Visibility = Visibility.Collapsed;

            if (validite)
            {
                Trajets c = new Trajets()
                {
                    VilleDepart = villeD.SelectedItem.ToString(),
                    VilleArrive = villeA.SelectedItem.ToString(),
                    HeureDepart = heureD.Time.ToString(),
                    HeureArrive = heureA.Time.ToString(),
                    Arret = arret.SelectedItem.ToString(),
                    NomChauffeur = nomChauffeur.SelectedItem.ToString(),
                    Date = date.Date.Value.ToString("yyyy-MM-dd"),
                    TypeVehicule=typeVehicule.SelectedItem.ToString()
                };
                Gestionnaire.getInstance().insererTrajets(c);
                
               
            }



        }
    }
}
