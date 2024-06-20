using HAT_F_api.StateCodes;
#if NETSTANDARD2_0_OR_GREATER
using System.Linq;
#endif

namespace HAT_F_api.CustomModels
{
    public class FosJyuchuPages
    {
        /// <summary>対象ページインデックス</summary>
        public int TargetPage {  get; set; }

        /// <summary>ページ情報</summary>
        public List<FosJyuchuPage> Pages { get; set; }

        /// <summary>有効なページ</summary>
        public IEnumerable<FosJyuchuPage> AlivePages
            => Pages.Where(p => p.FosJyuchuH.DelFlg != DelFlg.Deleted);
    }
}
