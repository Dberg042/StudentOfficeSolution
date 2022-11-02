using System.ComponentModel.DataAnnotations;

namespace StudentOffice.Models
{
    public class Employee : IValidatableObject
    {
        public int ID { get; set; }

        [Display(Name = "Employee")]
        public string FullName
        {
            get
            {
                return FirstName
                    + (string.IsNullOrEmpty(MiddleName) ? " " :
                        (" " + (char?)MiddleName[0] + ". ").ToUpper())
                    + LastName;
            }
        }

        public string Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int? a = today.Year - DOB?.Year
                    - ((today.Month < DOB?.Month || (today.Month == DOB?.Month && today.Day < DOB?.Day) ? 1 : 0));
                return a?.ToString(); /*Note: You could add .PadLeft(3) but spaces disappear in a web page. */
            }
        }

        [Display(Name = "Phone")]
        public string PhoneFormatted
        {
            get
            {
                return "(" + Phone.Substring(0, 3) + ") " + Phone.Substring(3, 3) + "-" + Phone[6..];
            }
        }

        public string InEmployeeRole
        {
            get
            {
                return EmployeeRoleID.HasValue ? "Yes" : "None";
            }
        }
        [Display(Name = "Employee Number")]
        [Required(ErrorMessage = "You cannot leave the Employee number blank.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "The Employee number must be exactly 10 numeric digits.")]
        [StringLength(10)]
        public string EmployeeNumber { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be more than 50 characters long.")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        [Display(Name = "Suggestion Number")]
        [Required(ErrorMessage = "You cannot leave the number of expected vists per year blank.")]
        
        public int SuggestionNumber { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "You must select a Primary Care Physician.")]
        [Display(Name = "Team")]
        public int TeamID { get; set; }

        public Team Team { get; set; }

        [Display(Name = "Employee Role")]
        public int? EmployeeRoleID { get; set; }

        [Display(Name = "Employee Role")]
        public EmployeeRole EmployeeRole { get; set; }
        
        [Display(Name = "Department")]
        public ICollection<EmployeeCondition> EmployeeConditions { get; set; } = new HashSet<EmployeeCondition>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Create a string array containing the one element-the field where our error message should show up.
            //then you pass this to the ValidaitonResult This is only so the mesasge displays beside the field
            //instead of just in the validaiton summary.
            //var field = new[] { "DOB" };

            if (DOB.GetValueOrDefault() > DateTime.Today)
            {
                yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { "DOB" });
            }
        }
    }
}
