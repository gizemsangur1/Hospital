using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebApplication7.Utility;

namespace WebApplication7.Models
{
    public class DoctorRepository : Repository<Doctor>, IDoctorRepository
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

        public IEnumerable<Doctor> GetAll()
        {
            return _applicationDbContext.Doctors.Include(d => d.DoctorBrans);
        }

    }
}
