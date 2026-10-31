using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace MedicalAppointmentAssistant
{
    public partial class AppointmentRecords : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAppointments();
            }
        }
        private void LoadAppointments()
        {
            string connectionString =
            ConfigurationManager.ConnectionStrings["MedicalDB"].ConnectionString;
            string query = "SELECT * FROM Appointments ORDER BY AppointmentId DESC";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvAppointments.DataSource = dt;
                    gvAppointments.DataBind();
                }
            }
        }
    }
}