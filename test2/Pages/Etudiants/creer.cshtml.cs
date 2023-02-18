using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace test2.Pages.Etudiants
{
    public class creerModel : PageModel
    {
        public etudiantInfo etudiant= new etudiantInfo() ;
        public string errorMsg = "";
        public string successMsg = "";

        public void OnGet()
        {
        }
        public void OnPost()
        {
            etudiant.mat = Request.Form["mat"];
            etudiant.nom = Request.Form["nom"];
            etudiant.prenom = Request.Form["prenom"];
            etudiant.email = Request.Form["email"];
            etudiant.datenaiss = Request.Form["datenaiss"];
            etudiant.spec = Request.Form["spec"];

            if(etudiant.mat.Length==0 || etudiant.nom.Length==0 || etudiant.prenom.Length==0
                ||etudiant.email.Length==0||etudiant.datenaiss.Length==0 ||etudiant.spec.Length==0
                )
            {
                errorMsg = " Tous les champs sont obligatoires !";
                return;
            }
            //save new student
            try
            {
                string connectionString = "Data Source=.\\sqlexpressver;Initial Catalog=test2;Integrated Security=True";
                // create sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();
                    // create the sql query that will allow to read the data from the table
                    string sql = "INSERT INTO etudiants " +
                                   "(mat , nom , prenom , email , datenaiss , spec)VALUES" +
                                   "(@mat , @nom , @prenom ,@email , @datenaiss ,@spec)";
                    // create sql command : execute the sql query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@mat", etudiant.mat);
                        command.Parameters.AddWithValue("@nom", etudiant.nom);
                        command.Parameters.AddWithValue("@prenom", etudiant.prenom);
                        command.Parameters.AddWithValue("@email", etudiant.email);
                        command.Parameters.AddWithValue("@datenaiss", etudiant.datenaiss);
                        command.Parameters.AddWithValue("@spec", etudiant.spec);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {

                errorMsg = ex.Message;
                return;
            }
            etudiant.mat = ""; etudiant.nom = ""; etudiant.prenom = "";
            etudiant.email = ""; etudiant.datenaiss = ""; etudiant.spec = "";
            successMsg = " Nouveau etudiant enregistre !";

            Response.Redirect("/Etudiants/Index");
        }

    }
}
