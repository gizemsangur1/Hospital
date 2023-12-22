
namespace WebApplication7.Models
{
    public interface IDoctorBransRepository : IRepository<DoctorBrans>
    {
        void Guncelle(DoctorBrans doctorBransInterface);

        void Kaydet();
    }
}
