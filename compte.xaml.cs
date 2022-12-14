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
    public sealed partial class compte : Page
    {
        public compte()
        {
            this.InitializeComponent();
            nom.Text =  Gestionnaire.getInstance().Nom;
            prenom.Text = prenom.Text +  Gestionnaire.getInstance().Prenom;
            num.Text = num.Text + Gestionnaire.getInstance().Num;
            adresse.Text = adresse.Text + Gestionnaire.getInstance().Adresse;
            email.Text = email.Text +  Gestionnaire.getInstance().Email;
        }
    }
}
