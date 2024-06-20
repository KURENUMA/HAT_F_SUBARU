using HAT_F_api.Models;

namespace HAT_F_api.CustomModels
{
    public class FosJyuchuPage
    {
        /// <summary>新規ページ</summary>
        public bool IsNew { get; set; }
        
        /// <summary>コンストラクタ</summary>
        /// <param name="isNew">新規ページ</param>
        public FosJyuchuPage(bool isNew = false)
        {
            IsNew = isNew;
        }

        public FosJyuchuH FosJyuchuH { get; set; }
        public List<FosJyuchuD> FosJyuchuDs { get; set; }
    }
}
