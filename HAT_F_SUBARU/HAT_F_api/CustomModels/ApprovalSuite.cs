using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class ApprovalSuite
    {
        public Approval Approval { get; set; }
        public List<ApprovalProcedureEx> ApprovalProcedures { get; set; } = new List<ApprovalProcedureEx>();

        public int LatestApprovalResult { get; set; }

        public class ApprovalProcedureEx : ApprovalProcedure
        {
            public string EmpName { get; set; }
        }
    }
}
