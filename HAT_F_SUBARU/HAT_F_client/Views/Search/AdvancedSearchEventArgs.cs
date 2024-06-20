using System;

namespace HatFClient.Views.Search
{
    /// <summary>詳細検索画面のイベント情報</summary>
    public class AdvancedSearchEventArgs : EventArgs
    {
        /// <summary>検索キャンセル</summary>
        public bool Cancel {  get; set; }
    }
}
