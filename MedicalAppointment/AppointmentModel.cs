using System;
namespace MedicalAppointmentAssistant
{
    // NOTE: renamed from "Appointment" to "AppointmentModel" because the Web Form
    // class is also called "Appointment" (same namespace = compile error).
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentType { get; set; }
        public string Description { get; set; }
        public string AICategory { get; set; }
    }
}