using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentOffice.Models
{
    public class EmployeeRole
    {
        public int ID { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "You cannot leave the name of the Roles blank.")]
        [StringLength(100, ErrorMessage = "Roles name cannot be more than 100 characters long.")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "None")]
        public string RoleName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
