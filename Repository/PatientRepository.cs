using Microsoft.Data.SqlClient;
using Project_Kaveri.Contracts;
using Project_Kaveri.Models;
using System.Data;

namespace Project_Kaveri.Repository
{
    public class PatientRepository:IPatientContract
    {
        private readonly IConfiguration _configuration;
        public PatientRepository(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public void CreatePatient(Patient patient)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var command = new SqlCommand("InsertPatient", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Gender", patient.Gender);
            command.Parameters.AddWithValue("@Address", patient.Address);
            command.Parameters.AddWithValue("@City", patient.City);
            command.Parameters.AddWithValue("@PhoneNo", patient.PhoneNo);
            command.Parameters.AddWithValue("@DataCreatedOn", DateTime.UtcNow);


            command.ExecuteNonQuery();
        }
    
        public IEnumerable<Patient> ReadAllPatient()
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            using var command = new SqlCommand("ReadAllPatient", connection);
            command.CommandType = CommandType.StoredProcedure;
            using var reader = command.ExecuteReader();
            var patients = new List<Patient>();
            while (reader.Read())
            {
                patients.Add(new Patient
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                    Age =  reader.GetInt32(reader.GetOrdinal("Age")),
                    Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                    PhoneNo = reader.GetInt64(reader.GetOrdinal("PhoneNo")),
                    City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),

                });
            }
            return patients;
        }

        public Patient FindPatientById(int Id)
        {
            Patient patient = null;

            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                using (var command = new SqlCommand("FindPatientById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patient = new Patient
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader.GetString(reader.GetOrdinal("Name")),
                                Age = reader.GetInt32(reader.GetOrdinal("Age")),
                                Gender = reader.IsDBNull(reader.GetOrdinal("Gender")) ? null : reader.GetString(reader.GetOrdinal("Gender")),
                                PhoneNo = reader.GetInt64(reader.GetOrdinal("PhoneNo")),
                                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? null : reader.GetString(reader.GetOrdinal("Address")),
                                City = reader.IsDBNull(reader.GetOrdinal("City")) ? null : reader.GetString(reader.GetOrdinal("City")),

                            };
                        }
                    }
                }
            }

            return patient;
        }
        
        public void UpdatePatient(Patient patient)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var command = new SqlCommand("UpdatePatient", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", patient.Id);
            command.Parameters.AddWithValue("@Name", patient.Name);
            command.Parameters.AddWithValue("@Age", patient.Age);
            command.Parameters.AddWithValue("@Gender", patient.Gender);
            command.Parameters.AddWithValue("@Address", patient.Address);
            command.Parameters.AddWithValue("@City", patient.City);
            command.Parameters.AddWithValue("@PhoneNo", patient.PhoneNo);
            command.Parameters.AddWithValue("@DataModifiedOn", DateTime.UtcNow);


            command.ExecuteNonQuery();
        }
        
        public void DeletePatient(int Id)
                {
                    using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                    connection.Open();
                    var command = new SqlCommand("DeletePatient", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
                
    }
}
