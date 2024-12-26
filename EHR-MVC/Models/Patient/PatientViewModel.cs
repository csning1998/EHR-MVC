using System.ComponentModel.DataAnnotations;

namespace EHR_MVC.Models.Patient
{
    public class PatientViewModel
    {
        public long PatientId { get; set; }

        [Required]
        [RegularExpression(@"^([A-Z][1-2][0-9]{8}|[A-Z]{2}[0-9]{8})$", ErrorMessage = "Invalid ID Number format.")]
        public required string IdNo { get; set; }
        public bool Active { get; set; }

        [Required(ErrorMessage = "Family Name is required.")]
        [StringLength(50, ErrorMessage = "Family Name cannot exceed 50 characters.")]
        public required string FamilyName { get; set; }

        [Required(ErrorMessage = "Given Name is required.")]
        [StringLength(50, ErrorMessage = "Given Name cannot exceed 50 characters.")]
        public required string GivenName { get; set; }

        [Required(ErrorMessage = "Telecom is required.")]
        [RegularExpression(@"^09\d{8}$", ErrorMessage = "Invalid Telecom Number.")]
        public required string Telecom { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression(@"^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'.")]
        public required string Gender { get; set; }

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public required string Address { get; set; }

        public string? Email { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? PreferredLanguage { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactRelationship { get; set; }
        public string? EmergencyContactPhone { get; set; }
    }
}
