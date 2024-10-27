namespace EHR_MVC.Models
{
    public class PatientViewModel
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
    }
}
