﻿using System;
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
    public sealed partial class GestionClient : Page
    {
        Boolean validite = true;
        public GestionClient()
        {
            this.InitializeComponent();
            AfficheChauffeur.ItemsSource = Gestionnaire.getInstance().GetClient();
        }

        private void RechercherClient_event(object sender, RoutedEventArgs e)
        {
            if (nomChauffeur.Text == null || nomChauffeur.Text == "")
            {
                erreurNom.Visibility = Visibility.Visible;
                validite = false;
            }
            else
            {
                erreurNom.Visibility = Visibility.Collapsed;
            }

            if (validite)
            {
                AfficheChauffeur.ItemsSource = Gestionnaire.getInstance().rechercherClients(nomChauffeur.Text);
            }
        }

        private void All_event(object sender, RoutedEventArgs e)
        {

            AfficheChauffeur.ItemsSource = Gestionnaire.getInstance().GetClient();

        }
    }
}
