using System.ComponentModel;

namespace HatFClient.Common
{
    // TODO API側にも同じ定義があるのでアセンブリ共有したい
    /// <summary>承認結果</summary>
    /// <remarks><see cref="DescriptionAttribute"/>は<see cref="EnumUtil.GetDescription(System.Enum)"/>を使用して取得してください</remarks>
    public enum ApprovalResult
    {
        /// <summary>申請</summary>
        [Description("申請")]
        Request = 0,

        /// <summary>差し戻し</summary>
        [Description("差し戻し")]
        Reject = 1,

        /// <summary>承認</summary>
        [Description("承認")]
        Approve = 2,

        /// <summary>最終承認</summary>
        [Description("最終承認")]
        FinalApprove = 3
    }
}
