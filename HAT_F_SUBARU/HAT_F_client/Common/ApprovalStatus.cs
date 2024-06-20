using System.ComponentModel;

namespace HatFClient.Common
{
    // TODO API側にも同じ定義があるのでアセンブリ共有したい
    /// <summary>承認状況</summary>
    /// <remarks><see cref="DescriptionAttribute"/>は<see cref="EnumUtil.GetDescription(System.Enum)"/>を使用して取得してください</remarks>
    public enum ApprovalStatus
    {
        /// <summary>申請中</summary>
        [Description("申請中")]
        Request = 0,

        /// <summary>承認中</summary>
        [Description("承認中")]
        Approve = 1,

        /// <summary>承認済</summary>
        [Description("承認済")]
        FinalApprove = 9
    }
}
