using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test2.Pages.Etudiants
{
    public class IndexModel : PageModel
    {

        public List<etudiantInfo> listEtudiants = new List<etudiantInfo>();
        public void OnGet()
        {
            try
            {
                //the url of the database
                string connectionString = "Data Source=.\\sqlexpressver;Initial Catalog=test2;Integrated Security=True";
                // create sql connection
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();
                    // create the sql query that will allow to read the data from the table
                    string sql = "SELECT * FROM etudiants";
                    // create sql command : execute the sql query
                    using(SqlCommand command = new SqlCommand(sql , connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                etudiantInfo etudiant = new etudiantInfo();
                                etudiant.id = "" + reader.GetInt32(0);
                                etudiant.mat = "" + reader.GetInt32(1);
                                etudiant.nom=reader.GetString(2);   
                                etudiant.prenom=reader.GetString(3);
                                etudiant.email= reader.GetString(4);
                                etudiant.datenaiss = reader.GetDateTime(5).ToString("d/M/yyyy");
                                etudiant.spec=reader.GetString(6);

                                listEtudiants.Add(etudiant);
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {

                Console.WriteLine("exception : "+ex.ToString());
            }
        }
    }
    public class etudiantInfo
    {
        public string id;
        public string mat;
        public string nom;
        public string prenom;
        public string email;
        public string datenaiss;
        public string spec;
    }
}
