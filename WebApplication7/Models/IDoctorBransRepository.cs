namespace WebApplication7.Models
{
    public class IDoctorBransRepository : IRepository<DoctorBrans>
    {
        void Guncelle(DoctorBrans doctorBransInterface);
        void Kaydet();
    }
}
