using HAT_F_api.Models;

namespace HatFClient.Views.Sales
{
    /// <summary>
    /// <para><see cref="ViewInterestRateFixed"/>の項目に選択列を追加するためのクラス</para>
    /// <para>匿名オブジェクトでなくクラスとして定義しなければ、グリッドにバインドしても編集ができない</para>
    /// </summary>
    internal class CheckableInterestRateFixed : ViewInterestRateFixed
    {
        public bool 選択 { get; set; }
    }
}