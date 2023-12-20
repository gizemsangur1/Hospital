using WebApplication7.Utility;

namespace WebApplication7.Models
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
            _applicationDbContext = context;
        }

        public void Guncelle(Appointment appointmentInterface)
        {
            _applicationDbContext.Update(appointmentInterface);
        }

        public void Kaydet()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
