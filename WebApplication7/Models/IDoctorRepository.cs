using System.Collections;

namespace WebApplication7.Models
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        void Guncelle(Doctor doctorInterface);
        void Kaydet();

        IEnumerable<Doctor> GetAll();
    }
}
