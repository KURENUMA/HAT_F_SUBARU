using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class EmployeeUserAssignedRole
    {
        public Employee Employee { get; set; }
        public List<UserAssignedRole> UserAssignedRoles { get; set; }
    }
}
