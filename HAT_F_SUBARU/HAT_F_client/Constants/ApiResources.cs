using System.Web.UI;

namespace HatFClient.Constants
{
    /// <summary>各種APIのリソース名を定義</summary>
    internal static class ApiResources
    {
        /// <summary>橋本総業一貫化APIのリソース名</summary>
        public static class Ikkanka
        {
            /// <summary>ログイン</summary>
            public static readonly string Login = "api/v2/user/login";
        }

        /// <summary>HAT-F一貫化APIのリソース名</summary>
        public static class HatF
        {
            // TODO それぞれXMLコメントを記載する

            public static readonly string BlobStrage = "api/Blob";

            public static readonly string MasterEditEmployees = "api/Client****/";


            public class Login
            {
                /// <summary>ログイン</summary>
                public static readonly string Auth = "api/Login/auth";

                /// <summary>ログインパスワード変更(本人用)</summary>
                public static readonly string LoginPassword = "api/Login/login-password";

                /// <summary>ログインパスワード変更(管理者用)</summary>
                public static readonly string LoginPasswordAdmin = "api/Login/login-password-admin";
            }

            public class Client
            {
                /// <summary>初期化</summary>
                public static readonly string Init = "api/Client/init";

                public static readonly string Search = "api/Client/orders";

                /// <summary>受注情報を取得</summary>
                public static readonly string GetOrder = "api/Client/orders/{0}";

                /// <summary>受注情報を更新</summary>
                public static readonly string UpdateOrder = "api/Client/orders/{0}";

                /// <summary>ヘッダー情報を削除</summary>
                public static readonly string DeleteHeader = "api/Client/orders/{0}/{1}";

                public static readonly string ProductStock = "api/Client/product-stock";
                public static readonly string ProductStockCount = "api/Client/product-stock-count";
                public static readonly string ProductStockSummary = "api/Client/product-stock-summary";
                public static readonly string SearchTorihiki = "api/Client/torihiki";
                public static readonly string SearchKeyman = "api/Client/keyman";
                public static readonly string SearchSupplier = "api/Client/supplier";
                public static readonly string SearchSupplierCategory = "api/Client/supplier-category";
                public static readonly string SearchPostAddress = "api/Client/post-address";
                public static readonly string SearchGenba = "api/Client/genba";
                public static readonly string SearchKoujiten = "api/Client/koujiten";
                public static readonly string SearchProductHat = "api/Client/product-hat";
                public static readonly string SearchProductMaker = "api/Client/product-maker";
                public static readonly string SupplierInvoices = "api/Client/supplier-invoices";
                public static readonly string ProductSuggestion = "api/Client/product-suggestion?keyword={0}";

                /// <summary>受注情報のヘッダー部分を補完する</summary>
                public static readonly string CompleteHeader = "api/Client/complete-header";

                /// <summary>受注情報の明細部分の金額について補完する</summary>
                public static readonly string CompleteDetails = "api/Client/complete-details";

                /// <summary>発注照合</summary>
                public static readonly string OrderCollation = "api/Client/orders/collation";

                /// <summary>受注確定</summary>
                public static readonly string OrderCommit = "api/Client/orders/commit";

                public static readonly string SendableEmployees = "api/Client/sendable-employees";

                /// <summary>仕入金額照合一覧取得</summary>
                public static readonly string PurchaseBillingDetails = "api/Client/purchase-billing-details";

                /// <summary>仕入納品確認一覧取得</summary>
                public static readonly string PurchaseReceivingDetails = "api/Client/purchase-receiving-details";

                public static readonly string TorihikiInvoices = "api/Client/torihiki-invoices";

                /// <summary>売上額訂正一覧取得</summary>
                public static readonly string PurchaseSalesCorrection = "api/Client/purchase-sales-correction";

                /// <summary>返品入力詳細データ取得</summary>
                public static readonly string SalesReturnDetail = "api/Client/view-sales-return-detail";

                /// <summary>売上予定一覧</summary>
                public static readonly string ReadySales = "api/Client/view-ready-sales";

                /// <summary>売上予定一覧件数</summary>
                public static readonly string ReadySalesCount = "api/Client/view-ready-sales-count";

                /// <summary>売上確定一覧</summary>
                public static readonly string FixedSales = "api/Client/view-fixed-sales";

                /// <summary>売上確定一覧件数</summary>
                public static readonly string FixedSalesCount = "api/Client/view-fixed-sales-count";

                /// <summary>物件詳細</summary>
                public static readonly string ConstructionDetail = "api/Client/view-construction-detail";

                /// <summary>物件詳細更新</summary>
                public static readonly string UpdateConstructionDetail = "api/Client/update-construction-detail";

                public static readonly string DeleteInsertConstructionDetailGrid = "api/Client/delete-insert-construction-detail-gird";

                /// <summary>物件詳細明細一覧</summary>
                public static readonly string ConstructionDetailDetail = "api/Client/construction-detail-detail";

                /// <summary>物件情報追加</summary>
                public static readonly string AddConstructionDetail = "api/Client/add-construction-detail";

                public static readonly string CheckDuplicateConstructionCode = "api/Client/check-duplicate-construction-code";

                /// <summary>物件情報ロック</summary>
                public static readonly string ConstructionLock = "api/Client/construction-lock";

                /// <summary>物件情報アンロック</summary>
                public static readonly string ConstructionUnLock = "api/Client/construction-unlock";

                /// <summary>仕入金額照合ロック</summary>
                public static readonly string AmountCheckLock = "api/Client/amount-check-lock";

                /// <summary>仕入金額照合アンロック</summary>
                public static readonly string AmountCheckUnLock = "api/Client/amount-check-unlock";

                /// <summary>売上額訂正ロック</summary>
                public static readonly string SalesEditLock = "api/Client/sales-edit-lock";

                /// <summary>売上額訂正アンロック</summary>
                public static readonly string SalesEditUnLock = "api/Client/sales-edit-unlock";

                /// <summary>請求詳細</summary>
                public static readonly string InvoiceDetail = "api/Client/view-invoice-detail";

                /// <summary>得意先情報</summary>
                public static readonly string CompanyInfo = "api/Client/company-info";

                /// <summary>口座情報</summary>
                public static readonly string InvoiceBank = "api/Client/invoice-bank";

                /// <summary>入庫予定一覧</summary>
                public static readonly string WarehousingReceivings2 = "api/Client/warehousing-receivings-2";

                /// <summary>入庫予定一覧(件数)</summary>
                public static readonly string WarehousingReceivingsCount2 = "api/Client/warehousing-receivings-count-2";

                /// <summary>入庫予定詳細</summary>
                public static readonly string WarehousingReceivingsDetail = "api/Client/warehousing-receiving-details";

                public static readonly string WarehousingShippings = "api/Client/warehousing-shippings";

                /// <summary>出荷指示一覧</summary>
                public static readonly string WarehousingShippingsGenSearch = "api/Client/warehousing-shippings-2";

                /// <summary>出荷指示一覧(件数)</summary>
                public static readonly string WarehousingShippingsCountGenSearch = "api/Client/warehousing-shippings-count-2";

                public static readonly string WarehousingShippingDetails = "api/Client/warehousing-shipping-details";

                /// <summary>仕入請求一覧</summary>
                public static readonly string SearchViewPurchaseBilling = "api/Search/search-view-purchase-billing";
                /// <summary>仕入請求一覧件数</summary>
                public static readonly string SearchViewPurchaseBillingCount = "api/Search/search-view-purchase-billing-count";
                /// <summary>仕入納品確定一覧</summary>
                public static readonly string SearchViewPurchaseReceiving = "api/Search/search-view-purchase-receiving";
                /// <summary>仕入納品確定一覧件数</summary>
                public static readonly string SearchViewPurchaseReceivingCount = "api/Search/search-view-purchase-receiving-count";
                /// <summary>仕入請求明細（仕入金額照合）</summary>
                public static readonly string SearchViewPurchaseBillingDetail = "api/Search/search-view-purchase-billing-detail";
                /// <summary>仕入請求明細（仕入金額照合）件数</summary>
                public static readonly string SearchViewPurchaseBillingDetailCount = "api/Search/search-view-purchase-billing-detail-count";
                /// <summary>仕入請求明細（仕入金額照合）内容の変更</summary>
                public static readonly string PutPurchaseBillingDetail = "api/Client/purchase-billing-details";

                /// <summary>売上確定前利率異常一覧</summary>
                public static readonly string InterestRateBeforeFix = "api/Client/view-interest-rate-before-fix";

                /// <summary>売上確定後利率異常一覧</summary>
                public static readonly string InterestRateFixed = "api/Client/view-interest-rate-fixed";

                /// <summary>売上確定前利率異常チェック更新</summary>
                public static readonly string InterestRateCheckBeforeFix = "api/Client/interest-rate-check-before-fix";

                /// <summary>売上確定後利率異常チェック更新</summary>
                public static readonly string InterestRateCheckFixed = "api/Client/interest-rate-check-fixed";

                /// <summary>H注番発番</summary>
                public static readonly string GetNextHatOrderNo = "api/Client/hat-order-no/next";

                /// <summary>受発注一覧</summary>
                public static readonly string Orders = "api/Client/view-orders";

                /// <summary>受発注一覧件数</summary>
                public static readonly string OrdersCount = "api/Client/view-orders-count";

                /// <summary>返品入庫詳細</summary>
                public static readonly string SalesReturnReceiptDetail = "api/Client/view-sales-return-receipt-detail";

                /// <summary>返品入力画面（返品入庫入力完了）</summary>
                public static readonly string SalesRefundDetail = "api/Client/view-sales-refund-detail";

                /// <summary>
                /// <para>与信額チェック</para>
                /// <para>{0} = 得意先コード</para>
                /// </summary>
                public static readonly string CheckCredit = "api/Client/check-credit/{0}";

                /// <summary>
                /// 得意先(取引先)検索
                /// </summary>
                public static readonly string CompanysMst = "api/Client/companys-mst";

                /// <summary>
                /// 出荷先(現場)検索
                /// </summary>
                public static readonly string DestinationMst = "api/Client/destinations-mst";

                /// <summary>納品一覧表（訂正・返品）詳細</summary>
                public static readonly string CorrectionDeliveryDetail = "api/Client/view-correction-delivery-detail";

                /// <summary>売上調整</summary>
                public static readonly string SalesAdjustment = "api/Client/sales-adjustment";
            }

            /// <summary>
            /// 在庫コントローラー
            /// </summary>
            public class Stock
            {
                /// <summary>棚卸情報</summary>
                public static readonly string StockInventories = "api/stock/stock-inventories";

                /// <summary>棚卸 Amazon棚卸反映</summary>
                public static readonly string PutInventoryAmazons = "api/stock/stock-inventory-amazons";

                /// <summary>棚卸情報(ビュー)</summary>
                public static readonly string ViewStockInventories = "api/stock/view-stock-inventories";

                /// <summary>棚卸情報・件数(ビュー)</summary>
                public static readonly string ViewStockInventoriesCount = "api/stock/view-stock-inventories-count";

                /// <summary>棚卸用情報作成</summary>
                public static readonly string StockInventoriesNew = "api/stock/stock-inventories/new";

                //public static readonly string StockRefill = "api/stock/stock-refill/{0}/{1}";
                public static readonly string StockRefill = "api/stock/stock-refill/";
                public static readonly string StockRefillCount = "api/stock/stock-refill-count/";
                public static readonly string PutStockRefill = "api/stock/stock-refill/";

                public static readonly string ViewStockRefill = "api/stock/view-stock-refill/";
                public static readonly string ViewStockRefillCount = "api/stock/view-stock-refill-count/";

                public static readonly string ComSyohinMst = "api/stock/com-syohin-mst/{prodCode}";
            }

            public class Announcement
            {
                public static readonly string View = "api/Announcement/view";
            }

            public class Search
            {
                public static readonly string ViewReadySale = "api/Search/search-view-ready-sale";

                /// <summary>物件一覧</summary>
                public static readonly string ConstructionList = "api/Search/search-view-construction";

                /// <summary>物件一覧件数</summary>
                public static readonly string ConstructionListCount = "api/Search/search-view-construction-count";

                /// <summary>請求一覧</summary>
                public static readonly string InvoiceList = "api/Search/search-view-invoice";

                /// <summary>請求一覧件数</summary>
                public static readonly string InvoiceListCount = "api/Search/search-view-invoice-count";

                /// <summary>請求済一覧</summary>
                public static readonly string InvoiceAmountList = "api/Search/search-view-invoice-amount";

                /// <summary>請求済一覧件数</summary>
                public static readonly string InvoiceAmountListCount = "api/Search/search-view-invoice-amount-count";

                /// <summary>納品一覧表（社内用）</summary>
                public static readonly string InternalDelivery = "api/Search/search-view-internal-delivery";

                /// <summary>納品一覧表（社内用）件数</summary>
                public static readonly string InternalDeliveryCount = "api/Search/search-view-internal-delivery-count";

                /// <summary>仕入請求一覧</summary>
                public static readonly string ViewPurchaseBilling = "api/Search/search-view-purchase-billing";

                /// <summary>仕入納品確定一覧</summary>
                public static readonly string ViewPurchaseReceiving = "api/Search/search-view-purchase-receiving";

                /// <summary>仕入納品確定一覧件数</summary>
                public static readonly string ViewPurchaseReceivingCount = "api/Search/search-view-purchase-receiving-count";

                /// <summary>仕入請求明細（仕入金額照合）件数</summary>
                public static readonly string ViewPurchaseBillingDetailCount = "api/Search/search-view-purchase-billing-detail-count";

                /// <summary>売上訂正一覧</summary>
                public static readonly string SalesCorrection = "api/Search/search-view-sales-correction";

                /// <summary>売上訂正一覧件数</summary>
                public static readonly string SalesCorrectionCount = "api/Search/search-view-sales-correction-count";

                /// <summary>納品一覧表（訂正・返品）</summary>
                public static readonly string CorrectionDelivery = "api/Search/search-view-correction-delivery";

                /// <summary>納品一覧表（訂正・返品）件数</summary>
                public static readonly string CorrectionDeliveryCount = "api/Search/search-view-correction-delivery-count";

                /// <summary>返品入力一覧</summary>
                public static readonly string SalesReturn = "api/Search/search-view-sales-return";

                /// <summary>返品入力一覧件数</summary>
                public static readonly string SalesReturnCount = "api/Search/search-view-sales-return-count";

                /// <summary>返品入庫一覧</summary>
                public static readonly string SalesReturnReceipt = "api/Search/search-view-sales-return-receipt";

                /// <summary>返品入庫一覧件数</summary>
                public static readonly string SalesReturnReceiptCount = "api/Search/search-view-sales-return-receipt-count";

                /// <summary>仕入請求明細（仕入金額照合）</summary>
                public static readonly string ViewPurchaseBillingDetail = "api/Search/search-view-purchase-billing-detail";
            }

            public class Approval
            {
                /// <summary>承認状態を取得</summary>
                public static readonly string Get = "api/Approval/{0}";

                /// <summary>返品関連のすべての承認状態を取得</summary>
                public static readonly string ReturnApprovalSuites = "api/Approval/return/{0}";

                /// <summary>新規承認</summary>
                public static readonly string Put = "api/Approval/{0}";

                /// <summary>承認更新</summary>
                public static readonly string Update = "api/Approval/{0}/{1}";
            }

            /// <summary>マスター編集</summary>
            public class MasterEditor
            {
                // TODO: API側をマスタ編集に移動する
                /// <summary>ユーザ権限ごとの社員マスタ取得</summary>
                public static readonly string EmloyeeUserAssignedRole = "api/Client/employee-user-assigned-role";

                // TODO: API側をマスタ編集に移動する
                /// <summary>社員マスタ検索</summary>
                public static readonly string Emloyee = "api/Client/employee";

                // TODO: API側をマスタ編集に移動する
                /// <summary>ユーザー割当権限検索</summary>
                public static readonly string UserAssignedRole = "api/Client/user-assigned-role";

                /// <summary>汎用マスター編集</summary>
                public static readonly string MasterTableList = "api/MasterEditor/master-tables";

                /// <summary>汎用マスター編集</summary>
                public static readonly string MasterTable = "api/MasterEditor/master-table/{0}";

                /// <summary>得意先(取引先)汎用検索</summary>
                public static readonly string CompanysMstGensearch = "api/MasterEditor/companys-mst-gensearch";

                /// <summary>得意先(取引先)汎用検索・件数</summary>
                public static readonly string CompanysMstCountGensearch = "api/MasterEditor/companys-mst-count-gensearch";

                /// <summary>仕入先汎用検索</summary>
                public static readonly string SupplierMstGensearch = "api/MasterEditor/supplier-mst-gensearch";

                /// <summary>仕入先汎用検索・件数</summary>
                public static readonly string SupplierMstCountGensearch = "api/MasterEditor/supplier-mst-count-gensearch";

                /// <summary>
                /// <para>仕入先情報取得/更新</para>
                /// <para>{0} = 仕入先コード</para>
                /// <para>{1} = 仕入先枝番</para>
                /// </summary>
                public static readonly string Supplier = "api/MasterEditor/supplier/{0}/{1}";

                /// <summary>
                /// <para>得意先情報取得/更新</para>
                /// <para>{0} = 得意先コード</para>
                /// </summary>
                public static readonly string CompanysMst = "api/MasterEditor/companys-mst/{0}";

                /// <summary>顧客(工事店)汎用検索</summary>
                public static readonly string CustomersMstGensearch = "api/MasterEditor/customers-mst-gensearch";

                /// <summary>顧客(工事店)汎用検索</summary>
                public static readonly string CustomersMstCountGensearch = "api/MasterEditor/customers-mst-count-gensearch";

                /// <summary>顧客(工事店)</summary>
                public static readonly string CustomersMst = "api/MasterEditor/customers-mst/{0}/{1}";

                /// <summary>出荷先(現場)汎用検索</summary>
                public static readonly string DestinationsMstGensearch = "api/MasterEditor/destinations-mst-gensearch";

                /// <summary>出荷先(現場)汎用検索・件数</summary>
                public static readonly string DestinationsMstCountGensearch = "api/MasterEditor/destinations-mst-count-gensearch";

                /// <summary>出荷先</summary>
                public static readonly string DestinationsMst = "api/MasterEditor/destinations-mst/{0}/{1}/{2}";

                /// <summary>社員汎用検索</summary>
                public static readonly string EmployeesGensearch = "api/MasterEditor/employees-gensearch";

                /// <summary>社員汎用検索・件数</summary>
                public static readonly string EmployeesCountGensearch = "api/MasterEditor/employees-count-gensearch";

                /// <summary>役職別既定権限</summary>
                public static readonly string TitleDefaultRole = "api/MasterEditor/title-default-role";

                /// <summary>権限</summary>
                public static readonly string DivUserRole = "api/MasterEditor/div-user-role";

                /// <summary>部門</summary>
                public static readonly string DeptMst = "api/MasterEditor/dept-mst";

                /// <summary>社員割当権限</summary>
                public static readonly string EmployeeUserAssignedRole = "api/MasterEditor/employee-user-assigned-role";
            }

            /// <summary>仕入関連</summary>
            public class Purchase
            {
                /// <summary>仕入取込データ</summary>
                public static readonly string PuImport = "api/Purchase/pu-import";
            }
        }

        /// <summary>エプコ</summary>
        public static class Epuko
        {
            /// <summary>エプコ取込検索</summary>
            public static readonly string ReturnEpukoOrder = "ReturnEpukoOrder";
        }
    }
}