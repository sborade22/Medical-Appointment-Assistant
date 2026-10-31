using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace MedicalAppointmentAssistant
{
    public partial class Appointment : System.Web.UI.Page
    {
        private string eventMessage = "";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(
                new System.Web.UI.PageAsyncTask(BookAppointmentAsync)
            );
        }

        // BOOK APPOINTMENT

        private async Task BookAppointmentAsync()
        {
            try
            {
                string patientName = txtPatientName.Text.Trim();
                string description = txtDescription.Text.Trim();

                // Patient Name Validation

                if (patientName == "")
                {
                    ShowMessage("Please enter patient name.", true);
                    return;
                }

                // Age Validation

                int age;

                if (!int.TryParse(txtAge.Text.Trim(), out age))
                {
                    ShowMessage("Please enter valid age.", true);
                    return;
                }

                if (age <= 0 || age > 120)
                {
                    ShowMessage("Age must be between 1 and 120.", true);
                    return;
                }

                // Gender Validation

                if (ddlGender.SelectedValue == "")
                {
                    ShowMessage("Please select gender.", true);
                    return;
                }

                // Doctor Validation

                if (ddlDoctor.SelectedValue == "")
                {
                    ShowMessage("Please select doctor.", true);
                    return;
                }

                // Date Validation

                DateTime appointmentDate;

                if (!DateTime.TryParse(
                    txtDate.Text.Trim(),
                    out appointmentDate))
                {
                    ShowMessage("Please select valid appointment date.", true);
                    return;
                }

                if (appointmentDate.Date < DateTime.Today)
                {
                    ShowMessage("Past appointment date is not allowed.", true);
                    return;
                }

                // Appointment Type Validation

                if (ddlType.SelectedValue == "")
                {
                    ShowMessage("Please select appointment type.", true);
                    return;
                }

                // Description Validation

                if (description == "")
                {
                    ShowMessage("Please enter problem description.", true);
                    return;
                }

                if (description.Length > 500)
                {
                    ShowMessage("Description must be within 500 characters.", true);
                    return;
                }

                // AI RECOMMENDATION

                string aiCategory = await CategorizeUsingAI(description);

                // CREATE APPOINTMENT OBJECT

                AppointmentModel appointment =
                    new AppointmentModel();

                appointment.PatientName = patientName;
                appointment.Age = age;
                appointment.Gender = ddlGender.SelectedValue;
                appointment.DoctorName = ddlDoctor.SelectedValue;
                appointment.AppointmentDate = appointmentDate;
                appointment.AppointmentType = ddlType.SelectedValue;
                appointment.Description = description;
                appointment.AICategory = aiCategory;

                // SAVE APPOINTMENT IN DATABASE

                SaveAppointment(appointment);

                // EVENT HANDLING

                AppointmentManager manager =
                    new AppointmentManager();

                manager.AppointmentCreated +=
                    AppointmentCreatedMessage;

                manager.CreateAppointment();

                // SUCCESS MESSAGE

                ShowMessage(
                    "Appointment booked successfully! AI Recommendation: "
                    + aiCategory,
                    false
                );

                ClearForm();
            }
            catch (Exception ex)
            {
                ShowMessage("Error: " + ex.Message, true);
            }
        }

        // OLLAMA LOCAL AI INTEGRATION

        private async Task<string> CategorizeUsingAI(string description)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(120);

                string prompt = @"
You are a medical appointment assistant.

Patient Problem:
" + description + @"

Suggest the most appropriate medical department.

Give a short general recommendation.

Do not diagnose diseases.
Do not prescribe medicines.
Do not suggest treatments.

If the patient describes emergency symptoms,
advise immediate emergency medical care.

Mention that your suggestion is not a medical diagnosis.

Keep your answer short and clear.
";

                var requestData = new
                {
                    model = "llama3.2:1b",
                    prompt = prompt,
                    stream = false
                };

                JavaScriptSerializer serializer =
                    new JavaScriptSerializer();

                string json = serializer.Serialize(requestData);

                StringContent content = new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json"
                );

                HttpResponseMessage response =
                    await client.PostAsync(
                        "http://localhost:11434/api/generate",
                        content
                    );

                response.EnsureSuccessStatusCode();

                string result =
                    await response.Content.ReadAsStringAsync();

                Dictionary<string, object> data =
                    serializer.Deserialize<Dictionary<string, object>>(
                        result
                    );

                return data["response"].ToString().Trim();
            }
        }

        // SAVE APPOINTMENT

        private void SaveAppointment(
            AppointmentModel appointment)
        {
            string connectionString =
                ConfigurationManager
                .ConnectionStrings["MedicalDB"]
                .ConnectionString;

            string query = @"
INSERT INTO Appointments
(
    PatientName,
    Age,
    Gender,
    DoctorName,
    AppointmentDate,
    AppointmentType,
    Description,
    AICategory
)
VALUES
(
    @PatientName,
    @Age,
    @Gender,
    @DoctorName,
    @AppointmentDate,
    @AppointmentType,
    @Description,
    @AICategory
)";

            using (SqlConnection con =
                new SqlConnection(connectionString))
            {
                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue(
                        "@PatientName",
                        appointment.PatientName);

                    cmd.Parameters.AddWithValue(
                        "@Age",
                        appointment.Age);

                    cmd.Parameters.AddWithValue(
                        "@Gender",
                        appointment.Gender);

                    cmd.Parameters.AddWithValue(
                        "@DoctorName",
                        appointment.DoctorName);

                    cmd.Parameters.AddWithValue(
                        "@AppointmentDate",
                        appointment.AppointmentDate);

                    cmd.Parameters.AddWithValue(
                        "@AppointmentType",
                        appointment.AppointmentType);

                    cmd.Parameters.AddWithValue(
                        "@Description",
                        appointment.Description);

                    cmd.Parameters.AddWithValue(
                        "@AICategory",
                        appointment.AICategory);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // EVENT HANDLER

        private void AppointmentCreatedMessage(string message)
        {
            eventMessage = message;
        }

        // DISPLAY MESSAGE

        private void ShowMessage(string text, bool isError)
        {
            lblMessage.Text = Server.HtmlEncode(text);

            lblMessage.Style["color"] =
                isError ? "#c0392b" : "#1e8449";
        }

        // CLEAR FORM

        private void ClearForm()
        {
            txtPatientName.Text = "";
            txtAge.Text = "";

            ddlGender.SelectedIndex = 0;
            ddlDoctor.SelectedIndex = 0;

            txtDate.Text = "";

            ddlType.SelectedIndex = 0;

            txtDescription.Text = "";
        }
    }
}
