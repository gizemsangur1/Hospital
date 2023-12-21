namespace WebApplication7.Models
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        void Guncelle(Appointment appointmentInterface);
        void Kaydet();
    }
}
