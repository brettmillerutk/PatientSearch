using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PatientSearch.Models;
using PatientSearch.Services;
using Prism.Commands;
using Prism.Mvvm;

namespace PatientSearch.PatientSearchUI
{
    class PatientSearchViewModel : BindableBase
    {
        public IPatientRepository Patients { get; set; }
        public Patient SelectedPatient
        {
            get { return _selectedPatient; }
            set {
                SetProperty(ref _selectedPatient, value);
            }
        }
        public IEnumerable<Patient> FilteredPatients
        {
            get { return _filteredPatients; }
            set { SetProperty(ref _filteredPatients, value); }
        }

        public string SuccessfulQueryResultString
        {
            get { return _successfulQueryResultString; }
            set { SetProperty(ref _successfulQueryResultString, value); }
        }
        public PatientSearchFilters PatientSearchFilters { get; set; }
        public PatientSearchViewLabels PatientSearchViewLabels { get; }
        public DelegateCommand ClearFiltersCommand { get; private set; }
        public DelegateCommand SearchPatientsCommand { get; private set; }

        public PatientSearchViewModel()
        {
            Patients = new MockPatientRepository();
            PatientSearchViewLabels = new PatientSearchViewLabels();
            PatientSearchFilters = new PatientSearchFilters();
            ClearFiltersCommand = new DelegateCommand(ClearSearchFilters);
            SearchPatientsCommand = new DelegateCommand(GetPatients);
            SuccessfulQueryResultString = "";
        }

        private void ClearSearchFilters()
        {
            this.PatientSearchFilters.ClearAllFilters();
            this.FilteredPatients = null;
            this.SuccessfulQueryResultString = "";
        }

        private void GetPatients()
        {
            FilteredPatients = Patients.GetFilteredPatients(PatientSearchFilters);
            SuccessfulQueryResultString = String.Format("Search successful. {0} records returned.", FilteredPatients.Count());
        }

        private IEnumerable<Patient> _filteredPatients;
        private Patient _selectedPatient;
        private string _successfulQueryResultString;

    }
}
