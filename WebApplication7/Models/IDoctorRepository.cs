namespace WebApplication7.Models
{
    public class IDoctorRepository : IRepository<Doctor>
    {
        void Guncelle(Doctor doctorInterface);
        void Kaydet();
    }
}
