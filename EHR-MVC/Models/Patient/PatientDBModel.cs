namespace EHR_MVC.Models.Patient
{
    public class PatientDBModel
    {
        public required long PatientId { get; set; }
        public required string IdNo { get; set; }
        public bool Active { get; set; }
        public required string FamilyName { get; set; }
        public required string GivenName { get; set; }
        public required string Telecom { get; set; }
        public required string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public required string Address { get; set; }
    }
}
