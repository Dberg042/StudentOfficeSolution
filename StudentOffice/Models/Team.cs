using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Xml.Linq;

namespace StudentOffice.Models
{
    public class Team
    {
      
        public int ID { get; set; }

        [Display(Name = "Team")]
        public string FullName
        {
            get
            {
                return "Team " + FirstName + " "+ LastName;
            }
        }

        [Display(Name = "Team")]
        public string FormalName
        {
            get
            {
                return LastName + ", Team " + FirstName;
            }
        }


        [Display(Name = "First Name")]
        [Required(ErrorMessage = "You cannot leave the first name blank.")]
        [StringLength(50, ErrorMessage = "First name cannot be more than 50 characters long.")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "You cannot leave the last name blank.")]
        [StringLength(100, ErrorMessage = "Last name cannot be more than 100 characters long.")]
        public string LastName { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        
        

        //[Display(Name = "Specialties")]
        //public ICollection<TeamSpecialty> TeamSpecialties { get; set; } = new HashSet<TeamSpecialty>();

        //[Display(Name = "Documents")]
        //public ICollection<TeamDocument> TeamDocuments { get; set; } = new HashSet<TeamDocument>();
    }
}
