using EHR_MVC.Repositories;
using EHR_MVC.Models;

namespace EHR_MVC.Services
{
    public class PatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public PatientDBModel ConvertViewModelToDBModel(PatientViewModel viewModel)
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
    }
}
