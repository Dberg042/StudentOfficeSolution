namespace StudentOffice.Models
{
    public class EmployeeCondition
    {
        public int ConditionID { get; set; }
        public Condition Condition { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee{ get; set; }
    }
}
