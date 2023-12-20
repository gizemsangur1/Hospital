namespace WebApplication7.Models
{
    public class IAppointmentRepository : IRepository<Appointment>
    {
        void Guncelle(Appointment appointmentInterface);
        void Kaydet();
    }
}
