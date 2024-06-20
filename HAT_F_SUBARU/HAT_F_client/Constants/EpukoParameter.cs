using System;

namespace HatFClient.Constants
{
    /// <summary>エプコ取込時に使用するパラメータ</summary>
    internal static class EpukoParameter
    {
        /// <summary>邸コードAs String =As String =>OrderInquiryResponse.受注番号</summary>
        public static readonly string OrderInquiryResponseID = "OrderInquiryResponse.OrderResult.Sellers.ID";
        /// <summary>伝票区分As String =As String =>OrderInquiryResponse.仮受注番号</summary>
        public static readonly string OrderInquiryResponseTempID = "OrderInquiryResponse.OrderResult.Sellers.TempID";
        /// <summary>チームAs String =As String =>OrderInquiryResponse.受注者管理番号</summary>
        public static readonly string OrderInquiryResponseCID = "OrderInquiryResponse.OrderResult.Sellers.CID";
        /// <summary>入荷日As String =As String =>OrderInquiryResponse.指定着納期</summary>
        public static readonly string OrderInquiryResponseRequestedDate = "OrderInquiryResponse.OrderResult.Delivery.RequestedDate";
        /// <summary>送り先郵便番号As String =As String =>OrderInquiryResponse.納入先郵便番号</summary>
        public static readonly string OrderInquiryResponsePostalZone = "OrderInquiryResponse.OrderResult.DeliveryToAddress.PostalZone";
        /// <summary>送り先住所As String =As String =>OrderInquiryResponse.納入先住所１</summary>
        public static readonly string OrderInquiryResponseAddressLine1 = "OrderInquiryResponse.OrderResult.DeliveryToAddress.AddressLine1";
        /// <summary>邸番名称As String =As String =>OrderInquiryResponse.納入先名称</summary>
        public static readonly string OrderInquiryResponseName = "OrderInquiryResponse.OrderResult.DeliveryToAddress.Name";
        /// <summary>便As String =As String =>OrderInquiryResponse.納入条件</summary>
        public static readonly string OrderInquiryResponseTerms = "OrderInquiryResponse.OrderResult.DeliveryToAddress.Terms";
        /// <summary>送り先電話番号As String =As String =>OrderInquiryResponse.連絡先電話番号</summary>
        public static readonly string OrderInquiryResponseConsigneeContactTEL = "OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactTEL";
        /// <summary>宛先As String =As String =>OrderInquiryResponse.荷受人名称</summary>
        public static readonly string OrderInquiryResponseConsigneeContactName = "OrderInquiryResponse.OrderResult.DeliveryToAddress.ConsigneeContactName";
        /// <summary>現場As String =As String =>OrderInquiryResponse.受注者側現場コード</summary>
        public static readonly string OrderInquiryResponseSellersCD = "OrderInquiryResponse.OrderResult.SpotLocation.SellersCD";
        /// <summary>エプコ管理番号As String =As String =>OrderInquiryResponse.発注者側現場コード</summary>
        public static readonly string OrderInquiryResponseBuyersCD = "OrderInquiryResponse.OrderResult.SpotLocation.BuyersCD";
        /// <summary>倉庫As String =As String =>OrderInquiryResponse.引取倉庫コード</summary>
        public static readonly string OrderInquiryResponseWarehouseCD = "OrderInquiryResponse.OrderResult.Receipt.WarehouseCD";
        /// <summary>得意先コードAs String =As String =>OrderInquiryResponse.転売先コード</summary>
        public static readonly string OrderInquiryResponsePartyID = "OrderInquiryResponse.OrderResult.Resale.PartyID";
        /// <summary>得意先名称As String =As String =>OrderInquiryResponse.転売先名称</summary>
        public static readonly string OrderInquiryResponsePartyName = "OrderInquiryResponse.OrderResult.Resale.PartyName";
        /// <summary>数量As String =As String =>OrderInquiryResponse.入力数量</summary>
        public static readonly string OrderInquiryResponseQuantity = "OrderInquiryResponse.OrderResult.OrderLineResult.LineItem.Quantity";
        /// <summary>ＨＡＴ商品コードAs String =As String =>OrderInquiryResponse.商品コード・名称</summary>
        public static readonly string OrderInquiryResponseSellersItemIdentification = "OrderInquiryResponse.OrderResult.OrderLineResult.Item.SellersItemIdentification";
        /// <summary>回答金額As String =As String =>OrderInquiryResponse.指定着納期</summary>
        public static readonly string OrderInquiryResponseDate = "OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.RequestedDelivery.Date";
        /// <summary>売単価As String =As String =>OrderInquiryResponse.回答単価</summary>
        public static readonly string OrderInquiryResponseSellingUnitPrice = "OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingUnitPrice";
        /// <summary>売金額As String =As String =>OrderInquiryResponse.回答金額</summary>
        public static readonly string OrderInquiryResponseSellingPrice = "OrderInquiryResponse.OrderResult.OrderLineResult.LineDelivery.AnswerPrice.SellingPrice";

        /// <summary>回答運賃As String =As String =>OrderInquiryResponse.Fare</summary>
        public static readonly string OrderInquiryResponseFare = "OrderInquiryResponse.OrderResult.Delivery.Fare";
        /// <summary>回答区分As String =As String =>OrderInquiryResponse.Kbn"</summary>
        public static readonly string OrderInquiryResponseKbn = "OrderInquiryResponse.OrderResult.Delivery.Kbn";
        /// <summary>回答発注欄As String =As String =>OrderInquiryResponse.Order</summary>
        public static readonly string OrderInquiryResponseOrder = "OrderInquiryResponse.OrderResult.Delivery.Order";
        /// <summary>回答仕入先As String =As String =>OrderInquiryResponse.Supplier</summary>
        public static readonly string OrderInquiryResponseSupplier = "OrderInquiryResponse.OrderResult.Delivery.Supplier";
        /// <summary>回答依頼As String =As String =>OrderInquiryResponse.Commission</summary>
        public static readonly string OrderInquiryResponseCommission = "OrderInquiryResponse.OrderResult.Delivery.Commission";
        /// <summary>回答注番As String =As String =>OrderInquiryResponse.OrderNumber</summary>
        public static readonly string OrderInquiryResponseOrderNumber = "OrderInquiryResponse.OrderResult.Delivery.OrderNumber";

        /// <summary>メッセージバージョン番号</summary>
        public static readonly string OrderResultVerNo = "1.00";
        /// <summary>発注照会:メッセージID</summary>
        public static readonly string OrderResultInquiryGuId = "COMM020011";
        /// <summary>メッセージ詳細ID</summary>
        public static readonly string OrderResultGudId01 = "01";
        /// <summary>セッションID</summary>
        public static readonly string OrderResultSessID = "0001";
        /// <summary>セッション.シーケンス番号</summary>
        public static readonly string OrderResultSessSeqNO = "1";
        /// <summary>メッセージ継続区別</summary>
        public static readonly string OrderResultMessContinue = "1";
        /// <summary>メッセージ応答種別</summary>
        public static readonly string OrderResultMessResp = "9";
        /// <summary>トランザクションID</summary>
        public static readonly string OrderResultTranID = "1";
        /// <summary>送信処理日付</summary>
        public static readonly string OrderResultTransmitDate = DateTime.Now.ToString("yyyyMMdd");
        /// <summary>受信処理時間</summary>
        public static readonly string OrderResultTransmitTime = DateTime.Now.ToString("HHmm");
    }
}
