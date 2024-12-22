namespace EHR_MVC.Models.Patient
{
    public class PatientDBModel
    {
        public long PatientId { get; set; }
        public string IdNo { get; set; }
        public bool Active { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Telecom { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PreferredLanguage { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactRelationship { get; set; }
        public string EmergencyContactPhone { get; set; }
    }

}
