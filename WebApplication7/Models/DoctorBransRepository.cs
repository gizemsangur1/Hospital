using WebApplication7.Utility;

namespace WebApplication7.Models
{
    public class DoctorBransRepository : Repository<DoctorBrans>, IDoctorBransRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public DoctorBransRepository(ApplicationDbContext context) : base(context)
        {
            _applicationDbContext = context;
        }

        public void Guncelle(DoctorBrans doctorBransInterface)
        {
            _applicationDbContext.Update(doctorBransInterface);
        }

        public void Kaydet()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
