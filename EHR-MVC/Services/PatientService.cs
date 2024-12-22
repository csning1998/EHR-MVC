using EHR_MVC.Repositories;
using EHR_MVC.Models.Patient;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace EHR_MVC.Services
{
    public class PatientService(PatientRepository patientRepository)
    {
        private readonly PatientRepository _patientRepository = patientRepository;

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
                Address = viewModel.Address,
                Email = viewModel.Email,
                PostalCode = viewModel.PostalCode,
                Country = viewModel.Country,
                PreferredLanguage = viewModel.PreferredLanguage,
                EmergencyContactName = viewModel.EmergencyContactName,
                EmergencyContactRelationship = viewModel.EmergencyContactRelationship,
                EmergencyContactPhone = viewModel.EmergencyContactPhone
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
                Email = dbModel.Email,
                PostalCode = dbModel.PostalCode,
                Country = dbModel.Country,
                PreferredLanguage = dbModel.PreferredLanguage,
                EmergencyContactName = dbModel.EmergencyContactName,
                EmergencyContactRelationship = dbModel.EmergencyContactRelationship,
                EmergencyContactPhone = dbModel.EmergencyContactPhone
            };
        }
    }

    public class FhirService
    {
        public string ConvertPatientToFhirJson(PatientDBModel patient)
        {
            var fhirPatient = new Patient
            {
                Id = patient.PatientId.ToString(),
                Active = patient.Active,
                Name =
                [
                    new HumanName
                    {
                        Use = HumanName.NameUse.Official,
                        Family = patient.FamilyName ?? string.Empty,
                        Given = !string.IsNullOrEmpty(patient.GivenName) ? new[] { patient.GivenName } : null
                    }
                ],
                Gender = patient.Gender.Equals("m", StringComparison.CurrentCultureIgnoreCase) ? AdministrativeGender.Male : AdministrativeGender.Female,
                BirthDate = patient.Birthday.ToString("yyyy-MM-dd"),
                Address =
                [
                    new() {
                        Line = !string.IsNullOrEmpty(patient.Address) ? new[] { patient.Address } : null,
                        PostalCode = patient.PostalCode,
                        Country = patient.Country
                    }
                ],
                Telecom =
                [
                    new(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Mobile, patient.Telecom),
                    new(ContactPoint.ContactPointSystem.Email, ContactPoint.ContactPointUse.Home, patient.Email)
                ],
                Contact =
                [
                    new() {
                        Name = new HumanName { Family = patient.EmergencyContactName ?? string.Empty },
                        Telecom =
                        [
                            new(ContactPoint.ContactPointSystem.Phone, ContactPoint.ContactPointUse.Mobile, patient.EmergencyContactPhone ?? string.Empty)
                        ],
                        Relationship =
                        [
                            new("http://terminology.hl7.org/CodeSystem/v2-0131", "E", "Emergency")
                        ]
                    }
                ]
            };

            var jsonSerializer = new FhirJsonSerializer();
            return jsonSerializer.SerializeToString(fhirPatient);
        }
    }
}
