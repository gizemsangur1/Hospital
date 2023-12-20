using WebApplication7.Utility;

namespace WebApplication7.Models
{
    public class DoctorBransRepository : Repository<DoctorBrans>, IDoctorBransRepository
    {
        private ApplicationDbContext _applicatinDbContext;
        public DoctorBransRepository(ApplicationDbContext context) : base(context)
        {
            _applicatinDbContext = context;
        }

        public void Guncelle(DoctorBrans doctorBransInterface)
        {
            _applicatinDbContext.Update(doctorBransInterface);
        }

        public void Kaydet()
        {
            _applicatinDbContext.SaveChanges();
        }
    }
}
