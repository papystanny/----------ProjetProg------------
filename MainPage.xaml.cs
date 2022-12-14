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

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Essai2Projet
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            
            Affichage.ItemsSource = Gestionnaire.getInstance().GetTrajets();
            
        }

        private async void NavigationView_SelectionChanged(Windows.UI.Xaml.Controls.NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var item = (NavigationViewItem)args.SelectedItem;
        //    header.Text = item.Tag.ToString();              // Code pour alle attraper le header indiqué lorsqu'on clique sue un item du Navigattion 

            switch (item.Name)
            {
                case "connexion":
                    //mainFrame.Navigate(typeof(Connexion));
                    Login dialog = new Login();
                    dialog.XamlRoot = maingrid.XamlRoot;
                    dialog.Title = "Login";
                    dialog.DefaultButton = ContentDialogButton.Primary;

                    ContentDialogResult resultat = await dialog.ShowAsync();

                    if (dialog.Ok == true)
                    {
                        if (Gestionnaire.getInstance().Statut.Equals("Client"))
                        {
                            adminHeader.Visibility = Visibility.Collapsed;
                            homeAdministrateur.Visibility = Visibility.Collapsed;
                         
                            homeGestionChauffeurs.Visibility = Visibility.Collapsed;
                            homeGestionClients.Visibility = Visibility.Collapsed;
                            adminHr.Visibility = Visibility.Collapsed;

                            chauffeurHeader.Visibility = Visibility.Collapsed;
                            homeChauffeur.Visibility = Visibility.Collapsed;
                            ajouterChauffeurTrajet.Visibility = Visibility.Collapsed;
                            chauffeurHr.Visibility = Visibility.Collapsed;

                            clientsHeader.Visibility = Visibility.Visible;
                            homeClient.Visibility = Visibility.Visible;

                            connexion.Visibility = Visibility.Collapsed;
                            ajouterCompte.Visibility = Visibility.Collapsed;
                            inviteHeader.Visibility = Visibility.Collapsed;
                            inviteHr.Visibility = Visibility.Collapsed;

                            compte.Visibility = Visibility.Visible;
                            deconexion.Visibility = Visibility.Visible;

                            mainFrame.Navigate(typeof(ReserverTrajets));

                            header.Text = "Bienvenu" + " " + Gestionnaire.getInstance().Nom;
                        }
                        else if (Gestionnaire.getInstance().Statut.Equals("Chauffeur"))
                        {

                            connexion.Visibility = Visibility.Collapsed;
                            ajouterCompte.Visibility = Visibility.Collapsed;
                            inviteHeader.Visibility = Visibility.Collapsed;
                            inviteHr.Visibility = Visibility.Collapsed;


                            adminHeader.Visibility = Visibility.Collapsed;
                            homeAdministrateur.Visibility = Visibility.Collapsed;
                           
                            homeGestionChauffeurs.Visibility = Visibility.Collapsed;
                            homeGestionClients.Visibility = Visibility.Collapsed;
                            adminHr.Visibility = Visibility.Collapsed;

                            chauffeurHeader.Visibility = Visibility.Visible;
                            homeChauffeur.Visibility = Visibility.Visible;
                            ajouterChauffeurTrajet.Visibility = Visibility.Visible;
                            chauffeurHr.Visibility = Visibility.Visible;

                            clientsHeader.Visibility = Visibility.Collapsed;
                            homeClient.Visibility = Visibility.Collapsed;

                            compte.Visibility = Visibility.Visible;
                            deconexion.Visibility = Visibility.Visible;


                          
                            mainFrame.Navigate(typeof(GestionTrajetsChauffeur));

                            header.Text = "Bienvenu" + " " + Gestionnaire.getInstance().Nom;
                        }
                        else if (Gestionnaire.getInstance().Statut.Equals("Administrateur"))
                        {

                            connexion.Visibility = Visibility.Collapsed;
                            ajouterCompte.Visibility = Visibility.Collapsed;
                            inviteHeader.Visibility = Visibility.Collapsed;
                            inviteHr.Visibility = Visibility.Collapsed;


                            adminHeader.Visibility = Visibility.Visible;
                            homeAdministrateur.Visibility = Visibility.Visible;
                           
                            homeGestionChauffeurs.Visibility = Visibility.Visible;
                            homeGestionClients.Visibility = Visibility.Visible;
                            adminHr.Visibility = Visibility.Visible;

                            chauffeurHeader.Visibility = Visibility.Collapsed;
                            homeChauffeur.Visibility = Visibility.Collapsed;
                            ajouterChauffeurTrajet.Visibility = Visibility.Collapsed;
                            chauffeurHr.Visibility = Visibility.Collapsed;

                            clientsHeader.Visibility = Visibility.Collapsed;
                            homeClient.Visibility = Visibility.Collapsed;

                            compte.Visibility = Visibility.Collapsed;
                            deconexion.Visibility = Visibility.Visible;



                            mainFrame.Navigate(typeof(TrajetsCommun));

                            header.Text = "Bienvenu" + " " + Gestionnaire.getInstance().Nom;
                        }

                    }
                    else
                    {
                        ContentDialog x = new ContentDialog();
                        x.XamlRoot = maingrid.XamlRoot;
                        x.Title = " Erreur ";
                        x.CloseButtonText = "OK";
                        x.Content = "Les informations entrées ne sont pas valides";

                        var result = await x.ShowAsync();
                     //   mainFrame.Navigate(typeof(AjoutUser));

                    }
                    break;
                   
                     

                
                case "ajouterCompte":
                    mainFrame.Navigate(typeof(AjoutUser));
                   
                    break;
          
                    
                   ///////////

                case "homeAdministrateur":
                    mainFrame.Navigate(typeof(TrajetsCommun));
                    break;

                case "homeAddTrajets":
                    mainFrame.Navigate(typeof(AjoutTrajet));
                    break;

                case "homeGestionChauffeurs":
                    mainFrame.Navigate(typeof(GestionChauffeur));
                    break;

                case "homeGestionClients":
                    mainFrame.Navigate(typeof(GestionClient));
                    break;


                   /////////////////

                case "homeChauffeur":
                    mainFrame.Navigate(typeof(GestionTrajetsChauffeur));
                    break;

                case "ajouterChauffeurTrajet":
                    mainFrame.Navigate(typeof(AjoutTrajet));
                    break;

                ////////////////
                case "homeClient":
                    mainFrame.Navigate(typeof(ReserverTrajets));
                    break;

                case "compte":
                    mainFrame.Navigate(typeof(compte));
                    break;

                case "deconexion":
                    Gestionnaire.getInstance().Deonnexion();

                    inviteHeader.Visibility = Visibility.Visible;
                    ajouterCompte.Visibility = Visibility.Visible;
                    connexion.Visibility = Visibility.Visible;
                    inviteHr.Visibility = Visibility.Visible;

                    adminHeader.Visibility = Visibility.Collapsed;
                    homeAdministrateur.Visibility = Visibility.Collapsed;
                   
                    homeGestionChauffeurs.Visibility = Visibility.Collapsed;
                    homeGestionClients.Visibility = Visibility.Collapsed;
                    adminHr.Visibility = Visibility.Collapsed;

                    chauffeurHeader.Visibility = Visibility.Collapsed;
                    homeChauffeur.Visibility = Visibility.Collapsed;
                    ajouterChauffeurTrajet.Visibility = Visibility.Collapsed;
                    chauffeurHr.Visibility = Visibility.Collapsed;

                    clientsHeader.Visibility = Visibility.Collapsed;
                    homeClient.Visibility = Visibility.Collapsed;

                    compte.Visibility = Visibility.Collapsed;
                    deconexion.Visibility = Visibility.Collapsed;

                    inviteHeader.Visibility = Visibility.Visible;

                    mainFrame.Navigate(typeof(AjoutUser));
                    header.Text = "Bienvenue Chez vous ! ";
                    break;

                



                default:
                    break;

            }
        }
    }
}
