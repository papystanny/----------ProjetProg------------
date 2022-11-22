using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Essai________Prog________
{
    class Gestionnaire
    {
        MySqlConnection con;

        ObservableCollection<Trajets> listeTrajets;
        ObservableCollection<Utilisateur> listeUtilisateurs;

        static Gestionnaire gestion = null;
        // static GestionMaison gestionMAISON = null;

        // ---------------------------------------------------  Authentification dans la base de données.------------------------------------ 
        // -----------------------------------------------------    Cacher les groupes du navigationItem selon les status de connecté -------
        public Gestionnaire()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq19;Uid=2171397;Pwd=2171397;");
            listeTrajets = new ObservableCollection<Trajets>();

            // --------------------------------------------- A vérifier // Code pour la variable de session de l'utilisateur ( id et statut )  -------------------
            // Idéee du concepteur : Mettre static les cases dans lesquelles le user va rentrer les données, ensuite fairele nomdu Textboc.text et le stocker dans des variables dans le singleton.   
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "";
                con.Open();
                commande.Prepare();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (MySqlException ex)
            {
                //message d'erreur
            }
        }

        public static Gestionnaire getInstance()
        {
            if (gestion == null)
                gestion = new Gestionnaire();

            return gestion;

        }

        public ObservableCollection<Trajets> GetTrajets()
        {
            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nomChauffeur, villeDepart, villeArrivee, heuresDepart, heuresArrivee, date, montant  FROM Trajet t INNER JOIN Chauffeur c on t.idChauffeur = c.idChauffeur;    ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();



            try
            {
                while (r.Read())
                {
                    //lvListe.Items.Add( "\n " + r[1].ToString());
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("date"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),
                        NomChauffeur = r.GetString("nomChauffeur"),
                        Montant = r.GetDouble("montant")

                    };

                    listeTrajets.Add(c);
                }
                r.Close(); con.Close();
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return listeTrajets;
        }


    }
}
