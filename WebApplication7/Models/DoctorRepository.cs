using WebApplication7.Utility;

namespace WebApplication7.Models
{
    public class DoctorRepository : Repository<Doctor>
    {
        private ApplicationDbContext _applicationDbContext;
        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
            _applicationDbContext = context;
        }

        public void Guncelle(Doctor doctorInterface)
        {
            _applicationDbContext.Update(doctorInterface);
        }

        public void Kaydet()
        {
            _applicationDbContext.SaveChanges();
        }

    }
}
