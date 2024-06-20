using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    /// <summary>現場検索の結果</summary>
    public class SearchGenbaResult
    {
        /// <summary>顧客マスタ</summary>
        public CustomersMst Customer { get; set; }

        /// <summary>現場マスタ</summary>
        public DestinationsMst Destination { get; set; }
    }
}