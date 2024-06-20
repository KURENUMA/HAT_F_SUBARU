using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class EmployeeDept
    {
        public Employee Employee { get; set; }
        public List<DeptMst> DeptMsts { get; set; }
    }
}
