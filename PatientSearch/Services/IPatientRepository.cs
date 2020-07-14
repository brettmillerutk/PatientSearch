using PatientSearch.Models;
using PatientSearch.PatientSearchUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSearch.Services
{
    interface IPatientRepository
    {
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> GetFilteredPatients(PatientSearchFilters PatientSearchFilters);
    }
}
