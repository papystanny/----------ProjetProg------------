using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
//Verifier les injextions sql pour chaque methode
namespace Essai2Projet
{
    class Gestionnaire
    {
        MySqlConnection con;

        ObservableCollection<Trajets> listeTrajets;
        ObservableCollection<Utilisateur> listeUtilisateurs;
        ObservableCollection<string> listeVilleComprises = new ObservableCollection<String>() { "Bastican", "La Tuque", "Louisville", "Maskinongé", "Mékinac" , "Saint-Tite", "Shawinigam", "Trois-Rivières"};

        // string nomUser;
        bool connexion = false;
        bool dispo = false;
        bool dialog = false;
        bool reussiteTrajet = false;
        string nom, prenom, adresse, email, num, statut;
        int id;

        static Gestionnaire gestion = null;
       
        public string Nom { get => nom;  }
        public string Prenom { get => prenom; }
        
        public string Num { get => num;  }
        public string Statut { get => statut; set => statut = value; }
        public int Id{ get => id;  }
        public string Adresse { get => adresse;  }
        public string Email { get => email; }




        // static GestionMaison gestionMAISON = null;

        // ---------------------------------------------------  Authentification dans la base de données.------------------------------------ 
        // -----------------------------------------------------    Cacher les groupes du navigationItem selon les status de connecté -------
        public Gestionnaire()
        {
            this.con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2022_420326ri_eq19;Uid=2171397;Pwd=2171397;");
            listeTrajets = new ObservableCollection<Trajets>();
            listeUtilisateurs = new ObservableCollection<Utilisateur>();

            // --------------------------------------------- A vérifier // Code pour la variable de session de l'utilisateur ( id et statut )  -------------------
            // Idéee du concepteur : Mettre static les cases dans lesquelles le user va rentrer les données, ensuite fairele nomdu Textboc.text et le stocker dans des variables dans le singleton.   

        }

        public static Gestionnaire getInstance()
        {
            if (gestion == null)
                gestion = new Gestionnaire();

            return gestion;

         }

        //Fait ajouter les villes
        public ObservableCollection<String> AjouterVille(string ville)
        {
            listeVilleComprises.Add(ville);
            return listeVilleComprises;
        }

        //Fait afficher les villes 
        public ObservableCollection<String> AfficheViller()
        {
            return listeVilleComprises;
        }

        // Pouvoir  rechercher trajet 
        public ObservableCollection<Trajets> rechercheTrajets(DateTime date)
        {

            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select idTrajet , villeDepart, villeArrivee, heuresDepart, heuresArrivee, Day, prixPlace ,nbPlace, typeVehicule, montant, montantS, revenuBrut  FROM Trajet , societe_spt   WHERE Day = '" + date + "'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("Day"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),

                        PrixPlace = r.GetInt32("prixPlace"),
                        NbPlace = r.GetInt32("nbPlace"),
                        TypeVehicule = r.GetString("typeVehicule"),
                        IdTrajet = r.GetInt32("idTrajet"),
                        Montant = r.GetInt32("montant"),
                        MontantS = r.GetInt32("montantS"),
                        RevenuTotal = r.GetInt32("revenuBrut"),


                    };
                    listeTrajets.Add(c);
                    return listeTrajets;
                }
                r.Close(); con.Close(); return listeTrajets;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }



        // Pouvoir  rechercher Periode Trajet 
        public ObservableCollection<Trajets> rechercherPeriode(DateTime date1 , DateTime date2)
        { 

            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nomChauffeur, villeDepart, villeArrivee, heuresDepart, heuresArrivee, Day, montant  FROM Trajet t INNER JOIN Chauffeur c on t.idChauffeur = c.idChauffeur  WHERE Day >=  '" + date1 + "' AND  Day <= '" + date2 + "'" ;
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("Day"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),
                        NomChauffeur = r.GetString("nomChauffeur"),
                  
                    };
                    listeTrajets.Add(c);
                    
                }
                r.Close(); con.Close(); return listeTrajets;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }

        // Pouvoir  rechercher chauffeur 
        public ObservableCollection<Utilisateur> rechercheChauffeurs(string name)
        {
            con.Close();
            listeUtilisateurs.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nomChauffeur, prenomChauffeur, adresse, email, noTelephone FROM chauffeur WHERE nomChauffeur Like '"  + name + "'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Utilisateur c = new Utilisateur()
                    {
                        Nom = r.GetString("nomChauffeur"),
                        Prenom = r.GetString("prenomChauffeur"),
                        Adresse = r.GetString("adresse"),
                        Email = r.GetString("email"),
                        Numero = r.GetString("noTelephone")
                    };
                    listeUtilisateurs.Add(c);
                    return listeUtilisateurs;
                }
                r.Close(); con.Close(); return listeUtilisateurs ;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }

        // Pouvoir  rechercher client 
        public ObservableCollection<Utilisateur> rechercherClients(string name)
        {
            con.Close();
            listeUtilisateurs.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select nomClient, prenomClient, adresse, email, noTelephone FROM client WHERE nomClient Like '" + name + "'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Utilisateur c = new Utilisateur()
                    {
                        Nom = r.GetString("nomClient"),
                        Prenom = r.GetString("prenomClient"),
                        Adresse = r.GetString("adresse"),
                        Email = r.GetString("email"),
                        Numero = r.GetString("noTelephone")
                    };
                    listeUtilisateurs.Add(c);
                    return listeUtilisateurs;
                }
                r.Close(); con.Close(); return listeUtilisateurs;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }

        //Pouvoir  afficher le nom du chauffeur 
        public ObservableCollection<Utilisateur> GetNomChauffeurs()
        {
            con.Close();
            listeUtilisateurs.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = " Select nomChauffeur FROM chauffeur ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Utilisateur c = new Utilisateur()
                    {
                        Nom = r.GetString("nomChauffeur")                        
                    };
                    listeUtilisateurs.Add(c);
                    
                }
                r.Close(); con.Close(); return listeUtilisateurs;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }


        //Pouvoir  afficher le client 
        public ObservableCollection<Utilisateur> GetClient()
        {
            con.Close();
            listeUtilisateurs.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = " Select nomClient, prenomClient, adresse, email, noTelephone FROM client ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Utilisateur c = new Utilisateur()
                    {
                        Nom = r.GetString("nomClient"),
                        Prenom = r.GetString("prenomClient"),
                        Adresse = r.GetString("adresse"),
                        Email = r.GetString("email"),
                        Numero = r.GetString("noTelephone")
                    };
                    listeUtilisateurs.Add(c);
                }
                r.Close(); con.Close(); return listeUtilisateurs;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }


        //Pouvoir  afficher chauffeur 
        public ObservableCollection<Utilisateur> GetChauffeurs()
        {
            con.Close();
            listeUtilisateurs.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = " Select nomChauffeur, prenomChauffeur, adresse, email, noTelephone FROM chauffeur ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    Utilisateur c = new Utilisateur()
                    {
                        Nom = r.GetString("nomChauffeur"),
                        Prenom = r.GetString("prenomChauffeur"),
                        Adresse = r.GetString("adresse"),
                        Email = r.GetString("email"),
                        Numero = r.GetString("noTelephone")
                    };
                    listeUtilisateurs.Add(c);                   
                }
                r.Close(); con.Close(); return listeUtilisateurs;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }


        //Faire affiche les trajets
        public ObservableCollection<Trajets> GetTrajets()
        {
            con.Close();
            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select idTrajet ,nomChauffeur, villeDepart, villeArrivee, heuresDepart, heuresArrivee, Day, prixPlace ,nbPlace, typeVehicule   FROM Trajet t INNER JOIN Chauffeur c on t.idChauffeur = c.idChauffeur WHERE DAY >=  CURDATE()    ;";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            try
            {
                while (r.Read())
                {
                    //lvListe.Items.Add( "\n " + r[1].ToString());
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("Day"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),
                        NomChauffeur = r.GetString("nomChauffeur"),
                        PrixPlace =  r.GetInt32("prixPlace"),
                        NbPlace = r.GetInt32("nbPlace"),
                        TypeVehicule = r.GetString("typeVehicule"),
                        IdTrajet = r.GetInt32("idTrajet")
                    };

                    listeTrajets.Add(c);                    
                }
                r.Close(); con.Close();
                return listeTrajets;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }        
        }




        //Faire affiche les trajets de l'administrateur
        public ObservableCollection<Trajets> GetTrajetsAdmin()
        {
            con.Close();
            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select idTrajet , villeDepart, villeArrivee, heuresDepart, heuresArrivee, Day, prixPlace ,nbPlace, typeVehicule, montant, montantS, revenuBrut FROM trajet  INNER JOIN societe_spt ss on trajet.Xx = ss.noSociete;    ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    //lvListe.Items.Add( "\n " + r[1].ToString());
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("Day"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),
                        
                        PrixPlace = r.GetInt32("prixPlace"),
                        NbPlace = r.GetInt32("nbPlace"),
                        TypeVehicule = r.GetString("typeVehicule"),
                        IdTrajet = r.GetInt32("idTrajet"),
                        Montant= r.GetInt32("montant"),
                        MontantS = r.GetInt32("montantS"),
                        RevenuTotal = r.GetInt32("revenuBrut"),


                    };

                    listeTrajets.Add(c);
                }
                r.Close(); con.Close();
                return listeTrajets;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }


        //Faire affiche les trajets D'UN CHAUFFEUR
        public ObservableCollection<Trajets> GetTrajetsChauffeur()
        {
            con.Close();
            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "  SELECT villeDepart, villeArrivee, heuresDepart, heuresArrivee, arret, Day, nbPlace, nbClient, typeVehicule, prixPlace, montant, montantS  from trajet  WHERE idChauffeur = @id  AND DAY<CURDATE(); ";
            commande.Parameters.AddWithValue("@id", id);
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    //lvListe.Items.Add( "\n " + r[1].ToString());
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("Day"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),
                        Montant = r.GetInt32("montant"),
                        MontantS = r.GetInt32("montantS"),
                        PrixPlace = r.GetInt32("prixPlace"),
                        NbPlace = r.GetInt32("nbPlace"),
                        NbClient = r.GetInt32("nbClient"),
                        TypeVehicule = r.GetString("typeVehicule"),
                     


                    };

                    listeTrajets.Add(c);
                }
                r.Close(); con.Close();
                return listeTrajets;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }

        //Faire affiche les noms des clients
        public ObservableCollection<Trajets> GetNomClients()
        {
            con.Close();
            listeTrajets.Clear();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "  SELECT villeDepart, villeArrivee, heuresDepart, heuresArrivee, arret, Day, nbPlace, typeVehicule, prixPlace from trajet WHERE idChauffeur = @id  AND DAY<CURDATE(); ";
            commande.Parameters.AddWithValue("@id", id);
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            try
            {
                while (r.Read())
                {
                    //lvListe.Items.Add( "\n " + r[1].ToString());
                    Trajets c = new Trajets()
                    {
                        Date = r.GetString("Day"),
                        HeureDepart = r.GetString("heuresDepart"),
                        HeureArrive = r.GetString("heuresArrivee"),
                        VilleDepart = r.GetString("villeDepart"),
                        VilleArrive = r.GetString("villeArrivee"),

                        PrixPlace = r.GetInt32("prixPlace"),
                        NbPlace = r.GetInt32("nbPlace"),
                        TypeVehicule = r.GetString("typeVehicule"),

                    };

                    listeTrajets.Add(c);
                }
                r.Close(); con.Close();
                return listeTrajets;
            }
            catch (MySqlException ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
                return null;
            }
        }

        // Pouvoir inserer les utilisateurs
        public bool  insererUser(Utilisateur c)
        {
            con.Close();
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;

                if (c.Statut.Equals("Client"))
                    commande.CommandText = "insert into client (nomClient, prenomClient, noTelephone, adresse, motDePasse, email)  values( @nom, @prenom, @noTelephone, @adresse, @motDePasse, @email) ";
                else
                    commande.CommandText = "insert into chauffeur  (nomChauffeur, prenomChauffeur, noTelephone, adresse, motDePasse, email)  values( @nom, @prenom, @noTelephone, @adresse, @motDePasse, @email) ";
                commande.Parameters.AddWithValue("@nom", c.Nom);
                commande.Parameters.AddWithValue("@prenom", c.Prenom);
                commande.Parameters.AddWithValue("@noTelephone", c.Numero);
                commande.Parameters.AddWithValue("@adresse", c.Adresse);
                commande.Parameters.AddWithValue("@motDePasse", genererSHA256(c.MotDePasse));
                commande.Parameters.AddWithValue("@email", c.Email);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();
                if (i > 0)
                {
                    return dispo = true;
                }

                return dispo = false;
                con.Close();
                
            }
            catch (MySqlException ex)
            {
                return dispo = false;
                //message d'erreur
            }
        }

        // Pouvoir  reserver les trajets 
        public bool reserverTrajets(int numTrajet)
        {
            try
            {
                con.Close();
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;        
                commande.CommandText = "insert into relationtrajetclient  (idClient, idTrajet)  values( @idClient, @idTrajet )";
                commande.Parameters.AddWithValue("@idClient", id);
                commande.Parameters.AddWithValue("@idTrajet", numTrajet);

                con.Open();
                commande.Prepare();
               int i = commande.ExecuteNonQuery();
                if(i>0)
                {
                    reussiteTrajet = true;
                    return reussiteTrajet;
                }
                return dispo = false;
                con.Close();
            }
            catch (MySqlException ex)
            {
                return dispo = false;
                //message d'erreur
            }
        }


        // Pouvoir inserer les Trajets
        public void insererTrajets (Trajets c)
        {
            try
            {
                con.Close();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

                if (c.TypeVehicule.Equals("Vus"))
                {
                    commande.CommandText = "insert into trajet  (villeDepart, villeArrivee, heuresDepart,  heuresArrivee, arret, Day, nbPlace, typeVehicule, prixPlace, idChauffeur)" +
                                         "  values( @villeDepart, @villeArrive, @heuresDepart, @heuresArrivee, @arret, @Day, @nbPlace, @typeVehicule, @prixplace," +
                                         " (SELECT idchauffeur FROM chauffeur WHERE nomChauffeur LIKE '@nomChauffeur')) ";
                    commande.Parameters.AddWithValue("@villeDepart", c.VilleArrive);
                    commande.Parameters.AddWithValue("@villeArrive", c.VilleDepart);
                    commande.Parameters.AddWithValue("@heuresDepart", c.HeureDepart);
                    commande.Parameters.AddWithValue("@heuresArrivee", c.HeureArrive);
                    commande.Parameters.AddWithValue("@arret", c.Arret);
                    commande.Parameters.AddWithValue("@Day", c.Date);      
                    commande.Parameters.AddWithValue("@nbPlace",5 );
                    commande.Parameters.AddWithValue("@typeVehicule", c.TypeVehicule);
                    commande.Parameters.AddWithValue("@prixplace", 15);
                    commande.Parameters.AddWithValue("@idChauffeur", c.NomChauffeur);
                }
                else
                {
                    commande.CommandText = "insert into trajet  (villeDepart, villeArrivee, heuresDepart,  heuresArrivee, arret, Day, nbPlace, typeVehicule, prixPlace, idChauffeur)" +
                                                        "  values( @villeDepart, @villeArrive, @heuresDepart, @heuresArrivee, @arret, @Day , @nbPlace, @typeVehicule, @prixplace," +
                                                        " (SELECT idchauffeur FROM chauffeur WHERE nomChauffeur LIKE '@nomChauffeur') ";
                    commande.Parameters.AddWithValue("@villeDepart", c.VilleArrive);
                    commande.Parameters.AddWithValue("@villeArrive", c.VilleDepart);
                    commande.Parameters.AddWithValue("@heuresDepart", c.HeureDepart);
                    commande.Parameters.AddWithValue("@heuresArrivee", c.HeureArrive);
                    commande.Parameters.AddWithValue("@arret", c.Arret);
                    commande.Parameters.AddWithValue("@Day", c.Date);                 
                    commande.Parameters.AddWithValue("@nbPlace", 3);
                    commande.Parameters.AddWithValue("@typeVehicule", c.TypeVehicule);
                    commande.Parameters.AddWithValue("@prixplace", 10);
                    commande.Parameters.AddWithValue("@idChauffeur", c.NomChauffeur);
                }
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

        //utiliser pour se déconnecter 
        public void Deonnexion()
        {
            if(connexion)
            {
                connexion = false;
                id = 0; nom = ""; prenom = ""; adresse = ""; email = ""; num = ""; statut = "";
            }
        }


        //utiliser la string de connexion 
        public bool Connexion(Utilisateur c)
        {
            con.Close();
            try
            {

                if (c.Statut.Equals("Client"))
                {
                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = " SELECT idClient, nomClient, prenomClient, adresse, email, noTelephone  FROM client WHERE email = @email AND motDePasse = @motDePasse";
                    commande.Parameters.AddWithValue("@email", c.Email);
                    commande.Parameters.AddWithValue("@motDePasse", genererSHA256(c.MotDePasse));
                    con.Open();
                    MySqlDataReader r = commande.ExecuteReader();
                    if (r.Read())
                    {
                        connexion = true;
                        nom = r.GetString("nomClient");
                        prenom = r.GetString("prenomClient");
                        adresse = r.GetString("adresse");
                        email = r.GetString("email");
                        num = r.GetString("noTelephone");
                        statut = c.Statut;
                        id = r.GetInt32("idClient");
                    }
                    else
                    {
                        connexion = false;
                    }
                }
                  else if (c.Statut.Equals("Administrateur"))
                {
                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = " SELECT nomSociete  FROM societe_spt WHERE email = @email AND motDePasse = @motDePasse";
                    commande.Parameters.AddWithValue("@email", c.Email);
                    commande.Parameters.AddWithValue("@motDePasse",c.MotDePasse);
                    con.Open();
                    MySqlDataReader r = commande.ExecuteReader();
                    if (r.Read())
                    {
                        connexion = true;
                        nom = "Administrateur";
                        statut = c.Statut;
                    }
                    else
                    {
                        connexion = false;
                    }
                }
                else 
                {
                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = "  SELECT idChauffeur, nomChauffeur, prenomChauffeur, adresse, email, noTelephone  FROM chauffeur WHERE email = @email AND motDePasse = @motDePasse";
                    commande.Parameters.AddWithValue("@email", c.Email);
                    commande.Parameters.AddWithValue("@motDePasse", genererSHA256(c.MotDePasse));
                    con.Open();
                    MySqlDataReader r = commande.ExecuteReader();
                    if (r.Read())
                    {
                        connexion = true;
                        nom = r.GetString("nomChauffeur");
                        prenom = r.GetString("prenomChauffeur");
                        statut = c.Statut;
                        adresse = r.GetString("adresse");
                        email = r.GetString("email");
                        num = r.GetString("noTelephone");
                        statut = c.Statut;
                        id = r.GetInt32("idChauffeur");

                    }
                    else
                    {
                        connexion = false;
                    }
                }
            }

            catch (MySqlException ex)
            {
                //message d'erreur
                connexion = false;
            }

            return connexion;
        }


        // méthode d'encryptage
        private string genererSHA256(string texte)
        {
            var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texte));

            StringBuilder sb = new StringBuilder();
            foreach (Byte b in bytes)
                sb.Append(b.ToString("x2"));

            return sb.ToString();
        }




        //utiliser nombe de place 
        public bool nbPLace()
        {
            con.Close();
            try
            {

                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = " SELECT nbPlace FROM trajet";
                    con.Open();
                    MySqlDataReader r = commande.ExecuteReader();
                    if (r.Read())
                    {
                     int nbPlace = r.GetInt32("nbPlace");
                    if(nbPlace>0)
                    {
                        dispo = true;
                        return dispo;
                    }
                    else
                    {
                        dispo = false;
                        return dispo;
                    }
                }
    
            }

            catch (MySqlException ex)
            {
                //message d'erreur
              
            }

            return connexion;
        }



    }
}
