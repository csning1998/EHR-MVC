using EHR_MVC.Repositories;
using EHR_MVC.Models.Patient;

namespace EHR_MVC.Services
{
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public PatientDBModel ConvertPatientViewModel2DBModel(PatientViewModel viewModel)
        {
            return new PatientDBModel
            {
                PatientId = viewModel.PatientId,
                IdNo = viewModel.IdNo,
                Active = viewModel.Active,
                FamilyName = viewModel.FamilyName,
                GivenName = viewModel.GivenName,
                Telecom = viewModel.Telecom,
                Gender = viewModel.Gender,
                Birthday = viewModel.Birthday,
                Address = viewModel.Address
            };
        }
        public async Task<long> InsertPatientAsync(PatientDBModel patient)
        {
            return await _patientRepository.InsertPatientAsync(patient);
        }

        public async Task<bool> UpdatePatientAsync(PatientDBModel patient)
        {
            return await _patientRepository.UpdatePatientAsync(patient);
        }


        public PatientViewModel ConvertPatientDBModel2ViewModel(PatientDBModel dbModel) {
            return new PatientViewModel()
            {
                PatientId = dbModel.PatientId,
                IdNo = dbModel.IdNo,
                Active = dbModel.Active,
                FamilyName = dbModel.FamilyName,
                GivenName = dbModel.GivenName,
                Telecom = dbModel.Telecom,
                Gender = dbModel.Gender,
                Birthday = dbModel.Birthday,
                Address = dbModel.Address,
            };
        }
    }
}
