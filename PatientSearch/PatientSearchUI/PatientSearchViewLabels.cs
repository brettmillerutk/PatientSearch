using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSearch.PatientSearchUI
{
    class PatientSearchViewLabels
    {
        public string PatientSearchButtonText { get; private set; }
        public string PatientSearchClearText { get; private set; }
        public string PatientSelectButtonText { get; private set; }
        public string PatientSelectCancelText { get; private set; }

        public PatientSearchViewLabels()
        {
            PatientSearchButtonText = "Search";
            PatientSearchClearText = "Clear";
            PatientSelectButtonText = "OK";
            PatientSelectCancelText = "Cancel";
        }
    }
}
