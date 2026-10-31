using System;
namespace MedicalAppointmentAssistant
{
    public delegate void AppointmentCreatedHandler(string message);
    public class AppointmentManager
    {
        public event AppointmentCreatedHandler AppointmentCreated;
        public void CreateAppointment()
        {
            if (AppointmentCreated != null)
            {
                AppointmentCreated("Appointment booked successfully!");
            }
        }
    }
}