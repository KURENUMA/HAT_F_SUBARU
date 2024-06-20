using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class KTankaSaleEx : KTankaSale
    {
        public string Cd5 { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? ENetPrice { get; set; }
    }
}
