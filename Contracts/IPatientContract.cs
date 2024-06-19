using Project_Kaveri.Models;

namespace Project_Kaveri.Contracts
{
    public interface IPatientContract
    {

        public void CreatePatient(Patient patient);

        public IEnumerable<Patient> ReadAllPatient();
        public Patient FindPatientById(int Id);
        public void DeletePatient(int Id);

        public void UpdatePatient(Patient patient);




    }
}
