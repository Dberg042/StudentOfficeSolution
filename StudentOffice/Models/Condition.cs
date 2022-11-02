using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace StudentOffice.Models
{
    public class Condition
    {
        public int ID { get; set; }

        [Display(Name = "Employee Condition")]
        [Required(ErrorMessage = "You cannot leave the name of the condition blank.")]
        [StringLength(50, ErrorMessage = "Too Big!")]
        public string ConditionName { get; set; }

        [Display(Name = "Department")]
        public ICollection<EmployeeCondition> EmployeeConditions { get; set; } = new HashSet<EmployeeCondition>();
    }
}
