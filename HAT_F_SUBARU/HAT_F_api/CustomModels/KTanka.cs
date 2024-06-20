namespace HAT_F_api.CustomModels
{
    public class KTanka
    {
        /// <summary>
        /// 指定価格
        /// </summary>
        public decimal? SpecifiedPrice { get; set; }

        /// <summary>
        /// 掛率
        /// </summary>
        public decimal? RatePercent {  get; set; }

        /// <summary>
        /// 掛率反映価格
        /// </summary>
        public decimal? RatedPrice { get; set; }
    }
}
