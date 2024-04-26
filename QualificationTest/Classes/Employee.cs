namespace QualificationTest
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeLogin { get; set; }
        public string EmployeePassword { get; set; }


        public Employee() { }

        public Employee(int employeeID, string employeeName, string employeeLogin, string employeePassword)
        {
            EmployeeID = employeeID;
            EmployeeName = employeeName;
            EmployeeLogin = employeeLogin;
            EmployeePassword = employeePassword;
        }
    }
}

