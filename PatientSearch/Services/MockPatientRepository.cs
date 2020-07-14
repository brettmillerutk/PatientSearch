using PatientSearch.Models;
using PatientSearch.PatientSearchUI;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PatientSearch.Services
{
    class MockPatientRepository : BindableBase, IPatientRepository
    {
        public List<Patient> Patients
        {
            get { return _patients; }
            set { SetProperty(ref _patients, value); }
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return Patients;
        }

        public IEnumerable<Patient> GetFilteredPatients(PatientSearchFilters PatientSearchFilters)
        {
            //return Patients.Where(p => p.LastName=="Miller");
            IQueryable<Patient> queryableData = Patients.AsQueryable<Patient>();

            //var newtest= PatientSearchFilters.BuildFilterExpression(queryableData);

            IQueryable<Patient> results = queryableData.Provider.CreateQuery<Patient>(PatientSearchFilters.BuildFilterExpression(queryableData));

            return (IEnumerable<Patient>)results;

        }

        private List<Patient> _patients = new List<Patient>()
        {
            new Patient(){FirstName="Brett", LastName="Miller", DOB= new DateTime(1985,02,08,0,0,0), MrNumber="12345"},
            new Patient(){FirstName="Meghan", LastName="Miller", DOB= new DateTime(1984,03,14,0,0,0), MrNumber="11111"},
            new Patient(){FirstName="Greg", LastName="McDonough", DOB= new DateTime(1985,07,09,0,0,0), MrNumber="22222"},
            new Patient(){FirstName="Peggy", LastName="Day", DOB= new DateTime(2013,01,01,0,0,0), MrNumber="33333"},
            new Patient(){FirstName="Bert", LastName="Cooper", DOB= new DateTime(2018,10,20,0,0,0), MrNumber="abc123"},
            new Patient(){FirstName="Jon", LastName="Miller", DOB= new DateTime(1957,12,10,0,0,0), MrNumber="papaw"}
        };
    }
}
