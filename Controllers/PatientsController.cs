using Microsoft.AspNetCore.Mvc;
using Project_Kaveri.Contracts;
using Project_Kaveri.Models;

namespace Project_Kaveri.Controllers
{
    public class PatientsController : Controller
    {
        private readonly IPatientContract _patientContract;


        public PatientsController(IPatientContract patientContract)
        {

            _patientContract = patientContract;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ReadAllPatients()
        {
            var patients = _patientContract.ReadAllPatient();

            return Json(patients);
        }

        [HttpPost]
        public JsonResult CreatePatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientContract.CreatePatient(patient);
                return Json("Patient details created!");
            }
            else
            {
                return Json("Fill the required field!");
            }
        }

        [HttpGet]
        public JsonResult EditPatient(int Id)
        {
            var patient = _patientContract.FindPatientById(Id);
;

            if (patient == null)
            {
                return Json(null);
            }
            return Json(patient);
        }

        [HttpPost]
        public JsonResult UpdatePatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientContract.UpdatePatient(patient);


                return Json("User Details Updated.");
            }


            return Json("User Validation Failed.");
        }


        [HttpPost]
        public JsonResult DeletePatient(int Id)
        {
            var patient = _patientContract.FindPatientById(Id);

            if (patient != null)
            {
                _patientContract.DeletePatient(Id);
                return Json("User Detail Deleted");
            }
            return Json("User Id Not Found");

        }
    }
}
