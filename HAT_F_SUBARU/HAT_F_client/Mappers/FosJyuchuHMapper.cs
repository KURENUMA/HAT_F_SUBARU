using HAT_F_api.CustomModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace HatFClient.Mappers
{
    public class FosJyuchuHMapper {
        public static FosJyuchuHMapper _instance;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static FosJyuchuHMapper GetInstance() {
            if (_instance == null) {
                _instance = new FosJyuchuHMapper();
            }
            return _instance;
        }
        
        public DataTable FosJyuchuPage2DataTable(FosJyuchuPage fosJyuchuPage)
        {
            DataTable headerTable = new DataTable();
            InitHeaderColumns(headerTable);


            return headerTable;
        }
        private void InitHeaderColumns(DataTable dt) {
            dt.Columns.Add("SaveKey", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("OrderNo", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("OrderState", typeof(string));      // 《ラベルなし》「発注前」(FosJyuchuH)string
            dt.Columns.Add("DenNo", typeof(string));           // 伝No(FosJyuchuH)string
            dt.Columns.Add("DenSort", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("DenState", typeof(string));        // 《ラベルなし》「発注前」(FosJyuchuH)string
            dt.Columns.Add("DenSortInt", typeof(int));         //《画面対応なし》整列用に追加
            dt.Columns.Add("DenFlg", typeof(string));          // 伝区(FosJyuchuH)string
            dt.Columns.Add("EstimateNo", typeof(string));      // 見積番号(FosJyuchuH)string
            dt.Columns.Add("EstCoNo", typeof(string));         // 見積番号(FosJyuchuH)string
            dt.Columns.Add("TeamCd", typeof(string));          // 販課(FosJyuchuH)string
            dt.Columns.Add("TantoCd", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("TantoName", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Jyu2", typeof(string));            // 受発注者(FosJyuchuH)string
            dt.Columns.Add("Jyu2Cd", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Jyu2Id", typeof(int));             // 20240228 ADD
            dt.Columns.Add("Nyu2", typeof(string));            // 入力者(FosJyuchuH)string
            dt.Columns.Add("Nyu2Cd", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Nyu2Id", typeof(int));             // 20240228 ADD
            dt.Columns.Add("HatOrderNo", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("CustOrderno", typeof(string));     // 客注(FosJyuchuH)string
            dt.Columns.Add("TokuiCd", typeof(string));         // 得意先(FosJyuchuH)string
            dt.Columns.Add("TokuiName", typeof(string));       // 20240228 ADD
            dt.Columns.Add("KmanCd", typeof(string));          // 担(FosJyuchuH)string
            dt.Columns.Add("KmanName", typeof(string));        // 20240228 ADD
            dt.Columns.Add("GenbaCd", typeof(string));         // 現場(FosJyuchuH)string
            dt.Columns.Add("Nouki", typeof(DateTime));         // 納日(FosJyuchuH)DateTime
            dt.Columns.Add("HatNyukabi", typeof(DateTime));    //《画面対応なし》入荷日(FosJyuchuH)DateTime
            dt.Columns.Add("BukkenExp", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Sale1Flag", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Kessai", typeof(string));          // 決済(FosJyuchuH)string
            dt.Columns.Add("Raikan", typeof(string));          // 来勘(FosJyuchuH)string
            dt.Columns.Add("KoujitenCd", typeof(string));      // 工事店(FosJyuchuH)string
            dt.Columns.Add("KoujitenName", typeof(string));    // 20240228 ADD
            dt.Columns.Add("SokoCd", typeof(string));          // 倉庫(FosJyuchuH)string
            dt.Columns.Add("SokoName", typeof(string));        // 20240228 ADD
            dt.Columns.Add("NoteHouse", typeof(string));       // 社内備考(FosJyuchuH)string
            dt.Columns.Add("OrderFlag", typeof(string));       // 受区(FosJyuchuH)string
            dt.Columns.Add("RecYmd", typeof(DateTime));        //《画面対応なし》(FosJyuchuH)DateTime
            dt.Columns.Add("ShiresakiCd", typeof(string));     // 仕入(FosJyuchuH)string
            dt.Columns.Add("ShiresakiName", typeof(string));   // 20240228 ADD
            dt.Columns.Add("Hkbn", typeof(string));            // 発注(FosJyuchuH)string
            dt.Columns.Add("Sirainm", typeof(string));         // 依頼(FosJyuchuH)string
            dt.Columns.Add("Sfax", typeof(string));            // FAX(FosJyuchuH)string
            dt.Columns.Add("SmailAdd", typeof(string));        //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("OrderMemo1", typeof(string));      // 発注時メモ(FosJyuchuH)string
            dt.Columns.Add("OrderMemo2", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("OrderMemo3", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Nohin", typeof(string));           // 区分(FosJyuchuH)string
            dt.Columns.Add("Unchin", typeof(string));          // 運賃(FosJyuchuH)string
            dt.Columns.Add("Bincd", typeof(string));           // 扱便(FosJyuchuH)string
            dt.Columns.Add("Binname", typeof(string));         // 扱便(FosJyuchuH)string
            dt.Columns.Add("OkuriFlag", typeof(string));       // 送元(FosJyuchuH)string
            dt.Columns.Add("ShireKa", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("Dseq", typeof(string));            // 内部No.(FosJyuchuH)string
            dt.Columns.Add("OrderDenNo", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("MakerDenNo", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("IpAdd", typeof(string));           //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("InpDate", typeof(DateTime));       //《画面対応なし》(FosJyuchuH)DateTime
            dt.Columns.Add("UpdDate", typeof(DateTime));       //《画面対応なし》(FosJyuchuH)DateTime
            dt.Columns.Add("AnswerName", typeof(string));      // 回答者(FosJyuchuH)string
            dt.Columns.Add("DelFlg", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("AnswerConfirmFlg", typeof(string));//《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("SansyoDseq", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("ReqBiko", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("OkuriCd", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("Account", typeof(string));         //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("SetFlg", typeof(string));          //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("KohinPrintFlg", typeof(string));   //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("TelRenrakuFlg", typeof(string));   // 電話連絡済(FosJyuchuH)string
            //dt.Columns.Add("HopeOrderNo", typeof(string));     //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("RenrakuJikou", typeof(string));    //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("UkeshoFlg", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("EpukoKanriNo", typeof(string));    //《画面対応なし》(FosJyuchuH)string
            dt.Columns.Add("SupplierType", typeof(decimal));    // 20240228 ADD

            dt.Columns.Add("RecvGenbaCd", typeof(string));     // 《画面対応なし》(FosJyuchuHRecv)string
            dt.Columns.Add("RecvName1", typeof(string));       // 宛先1(FosJyuchuHRecv)string
            dt.Columns.Add("RecvName2", typeof(string));       // 宛先2(FosJyuchuHRecv)string
            dt.Columns.Add("RecvTel", typeof(string));         // TEL(FosJyuchuHRecv)string
            dt.Columns.Add("RecvPostcode", typeof(string));    // 〒(FosJyuchuHRecv)string
            dt.Columns.Add("RecvAdd1", typeof(string));        // 住所1(FosJyuchuHRecv)string
            dt.Columns.Add("RecvAdd2", typeof(string));        // 住所2(FosJyuchuHRecv)string
            dt.Columns.Add("RecvAdd3", typeof(string));        // 住所3(FosJyuchuHRecv)string

            dt.Columns.Add("OpsOrderNo", typeof(string));      // OPSNo.(FosJyuchuHOp)string
            dt.Columns.Add("OpsRecYmd", typeof(DateTime));     // 《画面対応なし》(FosJyuchuHOp)DateTime
            dt.Columns.Add("OpsHachuAdr", typeof(string));     // 《画面対応なし》(FosJyuchuHOp)string
            dt.Columns.Add("OpsBin", typeof(string));          // 《画面対応なし》(FosJyuchuHOp)string
            dt.Columns.Add("OpsHachuName", typeof(string));    // 《画面対応なし》(FosJyuchuHOp)string

            /* 20240228 ADD */
            dt.Columns.Add("GOrderNo", typeof(string));        /// 《GLASS.受注データ.受注番号》
            dt.Columns.Add("GOrderDate", typeof(DateTime));   /// 《GLASS.受注データ.受注日;
            dt.Columns.Add("GStartDate", typeof(DateTime));    /// 《GLASS.受注データ.部門開始日》
            dt.Columns.Add("GCustCode", typeof(string));       /// 《GLASS.受注データ.顧客コード》
            dt.Columns.Add("GCustSubNo", typeof(string));      /// 《GLASS.受注データ.顧客枝番》
            dt.Columns.Add("GOrderAmnt", typeof(long));        /// 《GLASS.受注データ.受注金額合計》
            dt.Columns.Add("GCmpTax", typeof(long));           /// 《GLASS.受注データ.消費税金額》
            dt.Columns.Add("CreateDate", typeof(DateTime));    /// 《GLASS.受注データ.作成日時》
            dt.Columns.Add("Creator", typeof(int));            /// 《GLASS.受注データ.作成者》
            dt.Columns.Add("UpdateDate", typeof(DateTime));    /// 《GLASS.受注データ.更新日時》
            dt.Columns.Add("Updater", typeof(int));            /// 《GLASS.受注データ.更新者》

            //dt.Columns.Add("SyobunCdChk", typeof(string));     //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("Accountid", typeof(string));       //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("Deliverycd", typeof(string));      //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("Wholesaler1", typeof(string));     //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("Wholesaler2", typeof(string));     //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("NegotiationNo", typeof(string));   //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("SpecifieddemandCd", typeof(string));//《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("DemandcategoryCd", typeof(string));//《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("SpotlocationSellersCd", typeof(string));//《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("WarehouseCd", typeof(string));     //《画面対応なし》(FosJyuchuH)string
            //dt.Columns.Add("BuyerscontactId", typeof(string)); //《画面対応なし》(FosJyuchuH)string

            //dt.Columns.Add("IchuFlg", typeof(string));         // 《画面対応なし》(FosJyuchuHIchu)string
        }
    }


}
