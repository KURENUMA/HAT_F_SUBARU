using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class ClientInit
    {
        public IEnumerable<OptionData> DivBins { get; set; }
        public IEnumerable<OptionData> DivDenpyo { get; set; }
        public IEnumerable<OptionData> DivEmployee { get; set; }
        //public IEnumerable<OptionData> DivGenba { get; set; }
        public IEnumerable<OptionData> DivHachus { get; set; }
        //public IEnumerable<OptionData> DivKmans { get; set; }
        //public IEnumerable<OptionData> DivKoujitens { get; set; }
        public IEnumerable<OptionData> DivNohins { get; set; }
        //public IEnumerable<OptionData> DivOpsSyohins { get; set; }

        [Obsolete("仕入先マスタ(SUPPLIER_MST)のデータ量は多くなるためClientInitから削除予定です。別途、適切な量で取得してください。")]
        public IEnumerable<OptionData> DivShiresakis { get; set; }

        public IEnumerable<WhMst> DivSokos { get; set; }
        //public IEnumerable<OptionData> DivTokuis { get; set; }
        public IEnumerable<OptionData> DivUnchins { get; set; }
        public IEnumerable<OptionData> DivUriages { get; set; }
        public IEnumerable<DivTaxRate> DivTaxRates { get; set; }
    }
}
