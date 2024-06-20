using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Models;

public partial class HatFContext : DbContext
{
    public HatFContext()
    {
    }

    public HatFContext(DbContextOptions<HatFContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AmounCheckLock> AmounCheckLocks { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Approval> Approvals { get; set; }

    public virtual DbSet<ApprovalProcedure> ApprovalProcedures { get; set; }

    public virtual DbSet<ApprovalTarget> ApprovalTargets { get; set; }

    public virtual DbSet<BankAcutMst> BankAcutMsts { get; set; }

    public virtual DbSet<ComSyohinMst> ComSyohinMsts { get; set; }

    public virtual DbSet<CompanysMst> CompanysMsts { get; set; }

    public virtual DbSet<Construction> Constructions { get; set; }

    public virtual DbSet<ConstructionDetail> ConstructionDetails { get; set; }

    public virtual DbSet<ConstructionLock> ConstructionLocks { get; set; }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<CustomersMst> CustomersMsts { get; set; }

    public virtual DbSet<DeptMst> DeptMsts { get; set; }

    public virtual DbSet<DestinationsMst> DestinationsMsts { get; set; }

    public virtual DbSet<DivAuth> DivAuths { get; set; }

    public virtual DbSet<DivBin> DivBins { get; set; }

    public virtual DbSet<DivDelivery> DivDeliveries { get; set; }

    public virtual DbSet<DivFare> DivFares { get; set; }

    public virtual DbSet<DivInvoiceIssue> DivInvoiceIssues { get; set; }

    public virtual DbSet<DivOrder> DivOrders { get; set; }

    public virtual DbSet<DivSlip> DivSlips { get; set; }

    public virtual DbSet<DivStockLocation> DivStockLocations { get; set; }

    public virtual DbSet<DivTaxRate> DivTaxRates { get; set; }

    public virtual DbSet<DivUriage> DivUriages { get; set; }

    public virtual DbSet<DivUserRole> DivUserRoles { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<FosJyuchuD> FosJyuchuDs { get; set; }

    public virtual DbSet<FosJyuchuH> FosJyuchuHs { get; set; }

    public virtual DbSet<HatOrderNoSequence> HatOrderNoSequences { get; set; }

    public virtual DbSet<ImpFosJyuchuD> ImpFosJyuchuDs { get; set; }

    public virtual DbSet<ImpKeymanm2> ImpKeymanm2s { get; set; }

    public virtual DbSet<ImpKojixm> ImpKojixms { get; set; }

    public virtual DbSet<ImpSyobunm> ImpSyobunms { get; set; }

    public virtual DbSet<ImpTeamm> ImpTeamms { get; set; }

    public virtual DbSet<Imp取引先マスタ> Imp取引先マスタs { get; set; }

    public virtual DbSet<Imp現場マスタ> Imp現場マスタs { get; set; }

    public virtual DbSet<Imp社員マスタ> Imp社員マスタs { get; set; }

    public virtual DbSet<InterestRateCheckBeforeFix> InterestRateCheckBeforeFixes { get; set; }

    public virtual DbSet<InterestRateCheckFixed> InterestRateCheckFixeds { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }

    public virtual DbSet<KTankaPurchase> KTankaPurchases { get; set; }

    public virtual DbSet<KTankaSale> KTankaSales { get; set; }

    public virtual DbSet<PayeeMst> PayeeMsts { get; set; }

    public virtual DbSet<PostAddress> PostAddresses { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductSupplier> ProductSuppliers { get; set; }

    public virtual DbSet<Pu> Pus { get; set; }

    public virtual DbSet<PuBilling> PuBillings { get; set; }

    public virtual DbSet<PuDetail> PuDetails { get; set; }

    public virtual DbSet<PuImport> PuImports { get; set; }

    public virtual DbSet<ReturningProduct> ReturningProducts { get; set; }

    public virtual DbSet<ReturningProductsDetail> ReturningProductsDetails { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<SalesAdjustment> SalesAdjustments { get; set; }

    public virtual DbSet<SalesDetail> SalesDetails { get; set; }

    public virtual DbSet<SalesEditLock> SalesEditLocks { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<StockHistory> StockHistories { get; set; }

    public virtual DbSet<StockInventory> StockInventories { get; set; }

    public virtual DbSet<StockLocation> StockLocations { get; set; }

    public virtual DbSet<StockRefill> StockRefills { get; set; }

    public virtual DbSet<StockRefillOrder> StockRefillOrders { get; set; }

    public virtual DbSet<StockRefillOrderDetail> StockRefillOrderDetails { get; set; }

    public virtual DbSet<StockReserve> StockReserves { get; set; }

    public virtual DbSet<SupplierMst> SupplierMsts { get; set; }

    public virtual DbSet<TitleDefaultRole> TitleDefaultRoles { get; set; }

    public virtual DbSet<TotoContract> TotoContracts { get; set; }

    public virtual DbSet<UserAssignedRole> UserAssignedRoles { get; set; }

    public virtual DbSet<UserAuthentication> UserAuthentications { get; set; }

    public virtual DbSet<ViewConstruction> ViewConstructions { get; set; }

    public virtual DbSet<ViewConstructionDetail> ViewConstructionDetails { get; set; }

    public virtual DbSet<ViewCorrectionDelivery> ViewCorrectionDeliveries { get; set; }

    public virtual DbSet<ViewCorrectionDeliveryDetail> ViewCorrectionDeliveryDetails { get; set; }

    public virtual DbSet<ViewFixedSale> ViewFixedSales { get; set; }

    public virtual DbSet<ViewInterestRateBeforeFix> ViewInterestRateBeforeFixes { get; set; }

    public virtual DbSet<ViewInterestRateFixed> ViewInterestRateFixeds { get; set; }

    public virtual DbSet<ViewInternalDelivery> ViewInternalDeliveries { get; set; }

    public virtual DbSet<ViewInvoice> ViewInvoices { get; set; }

    public virtual DbSet<ViewInvoiceBatch> ViewInvoiceBatches { get; set; }

    public virtual DbSet<ViewInvoiceDetail> ViewInvoiceDetails { get; set; }

    public virtual DbSet<ViewInvoicedAmount> ViewInvoicedAmounts { get; set; }

    public virtual DbSet<ViewJyuchuSale> ViewJyuchuSales { get; set; }

    public virtual DbSet<ViewOrder> ViewOrders { get; set; }

    public virtual DbSet<ViewProductStock> ViewProductStocks { get; set; }

    public virtual DbSet<ViewPurchaseBilling> ViewPurchaseBillings { get; set; }

    public virtual DbSet<ViewPurchaseBillingDetail> ViewPurchaseBillingDetails { get; set; }

    public virtual DbSet<ViewPurchaseReceiving> ViewPurchaseReceivings { get; set; }

    public virtual DbSet<ViewPurchaseReceivingDetail> ViewPurchaseReceivingDetails { get; set; }

    public virtual DbSet<ViewPurchaseSalesCorrection> ViewPurchaseSalesCorrections { get; set; }

    public virtual DbSet<ViewReadySale> ViewReadySales { get; set; }

    public virtual DbSet<ViewReadySalesBatch> ViewReadySalesBatches { get; set; }

    public virtual DbSet<ViewReturnReceipt> ViewReturnReceipts { get; set; }

    public virtual DbSet<ViewSalesAdjustment> ViewSalesAdjustments { get; set; }

    public virtual DbSet<ViewSalesCorrection> ViewSalesCorrections { get; set; }

    public virtual DbSet<ViewSalesCorrectionDetail> ViewSalesCorrectionDetails { get; set; }

    public virtual DbSet<ViewSalesRefundDetail> ViewSalesRefundDetails { get; set; }

    public virtual DbSet<ViewSalesReturn> ViewSalesReturns { get; set; }

    public virtual DbSet<ViewSalesReturnDetail> ViewSalesReturnDetails { get; set; }

    public virtual DbSet<ViewSalesReturnReceipt> ViewSalesReturnReceipts { get; set; }

    public virtual DbSet<ViewSalesReturnReceiptDetail> ViewSalesReturnReceiptDetails { get; set; }

    public virtual DbSet<ViewStockInventory> ViewStockInventories { get; set; }

    public virtual DbSet<ViewStockRefill> ViewStockRefills { get; set; }

    public virtual DbSet<ViewWarehousingReceiving> ViewWarehousingReceivings { get; set; }

    public virtual DbSet<ViewWarehousingReceivingDetail> ViewWarehousingReceivingDetails { get; set; }

    public virtual DbSet<ViewWarehousingShipping> ViewWarehousingShippings { get; set; }

    public virtual DbSet<ViewWarehousingShippingDetail> ViewWarehousingShippingDetails { get; set; }

    public virtual DbSet<Warehousing> Warehousings { get; set; }

    public virtual DbSet<WhMst> WhMsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DBConnectString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Japanese_CI_AS");

        modelBuilder.Entity<AmounCheckLock>(entity =>
        {
            entity.HasKey(e => e.HatOrderNo).HasName("AMOUN_CHECK_LOCK_PKC");

            entity.ToTable("AMOUN_CHECK_LOCK", tb => tb.HasComment("仕入金額照合ロック"));

            entity.Property(e => e.HatOrderNo)
                .HasMaxLength(10)
                .HasComment("HAT注文番号")
                .HasColumnName("HAT_ORDER_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EditStartDatetime)
                .HasComment("編集開始日時")
                .HasColumnType("datetime")
                .HasColumnName("EDIT_START_DATETIME");
            entity.Property(e => e.EditorEmpId)
                .HasComment("編集者")
                .HasColumnName("EDITOR_EMP_ID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementsId).HasName("PK__ANNOUNCE__62AAD96870080F89");

            entity.ToTable("ANNOUNCEMENTS");

            entity.Property(e => e.AnnouncementsId).HasColumnName("ANNOUNCEMENTS_ID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("CONTENT");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("DELETED");
            entity.Property(e => e.Displayed)
                .HasDefaultValueSql("((1))")
                .HasColumnName("DISPLAYED");
            entity.Property(e => e.EndDate)
                .HasColumnType("date")
                .HasColumnName("END_DATE");
            entity.Property(e => e.ImportanceLevel).HasColumnName("IMPORTANCE_LEVEL");
            entity.Property(e => e.StartDate)
                .HasColumnType("date")
                .HasColumnName("START_DATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Approval>(entity =>
        {
            entity.HasKey(e => e.ApprovalId).HasName("APPROVAL_PKC");

            entity.ToTable("APPROVAL", tb => tb.HasComment("承認データ:申請者が申請：新規レコード作成\r\n承認者のアクション：承認状態を更新"));

            entity.Property(e => e.ApprovalId)
                .HasMaxLength(20)
                .HasComment("承認要求番号")
                .HasColumnName("APPROVAL_ID");
            entity.Property(e => e.ApprovalStatus)
                .HasComment("承認状態,0:申請中 1:承認中 9:承認済")
                .HasColumnName("APPROVAL_STATUS");
            entity.Property(e => e.ApprovalTargetId)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("承認対象ID")
                .HasColumnName("APPROVAL_TARGET_ID");
            entity.Property(e => e.ApprovalType)
                .IsRequired()
                .HasMaxLength(2)
                .HasComment("承認種別:仕入売上訂正/返品入力/返品入庫")
                .HasColumnName("APPROVAL_TYPE");
            entity.Property(e => e.Approver1EmpId)
                .HasComment("承認者1:社員ID")
                .HasColumnName("APPROVER1_EMP_ID");
            entity.Property(e => e.Approver2EmpId)
                .HasComment("承認者2:社員ID")
                .HasColumnName("APPROVER2_EMP_ID");
            entity.Property(e => e.CreateDate)
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.FinalApproverEmpId)
                .HasComment("最終承認者:社員ID")
                .HasColumnName("FINAL_APPROVER_EMP_ID");
            entity.Property(e => e.RequestorEmpId)
                .HasComment("申請者:社員ID")
                .HasColumnName("REQUESTOR_EMP_ID");
            entity.Property(e => e.UpdateDate)
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ApprovalProcedure>(entity =>
        {
            entity.HasKey(e => e.ApprovalProcedureId).HasName("APPROVAL_PROCEDURE_PKC");

            entity.ToTable("APPROVAL_PROCEDURE", tb => tb.HasComment("承認処理"));

            entity.Property(e => e.ApprovalProcedureId)
                .HasMaxLength(20)
                .HasComment("承認処理ID")
                .HasColumnName("APPROVAL_PROCEDURE_ID");
            entity.Property(e => e.ApprovalComment)
                .IsRequired()
                .HasMaxLength(1000)
                .HasComment("承認コメント")
                .HasColumnName("APPROVAL_COMMENT");
            entity.Property(e => e.ApprovalDate)
                .HasComment("登録日")
                .HasColumnType("datetime")
                .HasColumnName("APPROVAL_DATE");
            entity.Property(e => e.ApprovalId)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("承認要求番号")
                .HasColumnName("APPROVAL_ID");
            entity.Property(e => e.ApprovalResult)
                .HasComment("承認動作,0:申請 1:差し戻し 2:承認済 3:最終承認済")
                .HasColumnName("APPROVAL_RESULT");
            entity.Property(e => e.CreateDate)
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EmpId)
                .HasComment("社員ID")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.UpdateDate)
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ApprovalTarget>(entity =>
        {
            entity.HasKey(e => new { e.ApprovalTargetId, e.ApprovalTargetSub }).HasName("APPROVAL_TARGET_PKC");

            entity.ToTable("APPROVAL_TARGET", tb => tb.HasComment("承認対象★"));

            entity.Property(e => e.ApprovalTargetId)
                .HasMaxLength(20)
                .HasComment("承認対象ID")
                .HasColumnName("APPROVAL_TARGET_ID");
            entity.Property(e => e.ApprovalTargetSub)
                .HasComment("承認対象枝番")
                .HasColumnName("APPROVAL_TARGET_SUB");
            entity.Property(e => e.CompCode)
                .HasMaxLength(8)
                .HasComment("取引先コード")
                .HasColumnName("COMP_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.PoPrice)
                .HasComment("仕入単価（M単価）")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PO_PRICE");
            entity.Property(e => e.PuNo)
                .HasMaxLength(30)
                .HasComment("仕入番号")
                .HasColumnName("PU_NO");
            entity.Property(e => e.PuQuantity)
                .HasComment("仕入数量（M数量）")
                .HasColumnName("PU_QUANTITY");
            entity.Property(e => e.PuRowNo)
                .HasComment("仕入行番号")
                .HasColumnName("PU_ROW_NO");
            entity.Property(e => e.Quantity)
                .HasComment("売上数量")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.RowNo)
                .HasComment("売上行番号")
                .HasColumnName("ROW_NO");
            entity.Property(e => e.SalesNo)
                .HasMaxLength(10)
                .HasComment("売上番号")
                .HasColumnName("SALES_NO");
            entity.Property(e => e.SupCode)
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.SupSubNo)
                .HasComment("仕入先枝番")
                .HasColumnName("SUP_SUB_NO");
            entity.Property(e => e.Unitprice)
                .HasComment("販売単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("UNITPRICE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<BankAcutMst>(entity =>
        {
            entity.HasKey(e => e.BankAcutCode).HasName("BANK_ACUT_MST_PKC");

            entity.ToTable("BANK_ACUT_MST", tb => tb.HasComment("入金口座マスタ"));

            entity.Property(e => e.BankAcutCode)
                .HasMaxLength(8)
                .HasComment("入金口座コード")
                .HasColumnName("BANK_ACUT_CODE");
            entity.Property(e => e.ABankBlncCode)
                .HasMaxLength(3)
                .HasComment("全銀協支店コード")
                .HasColumnName("A_BANK_BLNC_CODE");
            entity.Property(e => e.ABankCode)
                .HasMaxLength(4)
                .HasComment("全銀協銀行コード")
                .HasColumnName("A_BANK_CODE");
            entity.Property(e => e.ActName)
                .HasMaxLength(20)
                .HasComment("口座名義人")
                .HasColumnName("ACT_NAME");
            entity.Property(e => e.ApplEndDate)
                .HasDefaultValueSql("('2100/12/31')")
                .HasComment("適用終了日")
                .HasColumnType("datetime")
                .HasColumnName("APPL_END_DATE");
            entity.Property(e => e.ApplStartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("適用開始日")
                .HasColumnType("datetime")
                .HasColumnName("APPL_START_DATE");
            entity.Property(e => e.BankActType)
                .HasMaxLength(1)
                .HasComment("銀行口座種別,O:普通 C:当座")
                .HasColumnName("BANK_ACT_TYPE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasMaxLength(12)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeptCode)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("部門コード")
                .HasColumnName("DEPT_CODE");
            entity.Property(e => e.ReciveActName)
                .HasMaxLength(30)
                .HasComment("入金口座名")
                .HasColumnName("RECIVE_ACT_NAME");
            entity.Property(e => e.ReciveActNo)
                .HasMaxLength(12)
                .HasComment("入金口座番号,銀行:7桁 郵便局:12桁")
                .HasColumnName("RECIVE_ACT_NO");
            entity.Property(e => e.ReciveBankActType)
                .HasMaxLength(1)
                .HasComment("入金口座区分,B:銀行 P:郵便局")
                .HasColumnName("RECIVE_BANK_ACT_TYPE");
            entity.Property(e => e.StartActName)
                .HasMaxLength(30)
                .HasComment("適用開始後入金口座名")
                .HasColumnName("START_ACT_NAME");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("部門開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatePgm)
                .HasMaxLength(50)
                .HasComment("更新プログラム名")
                .HasColumnName("UPDATE_PGM");
            entity.Property(e => e.UpdatePlgDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("プログラム更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_PLG_DATE");
            entity.Property(e => e.Updater)
                .HasMaxLength(12)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ComSyohinMst>(entity =>
        {
            entity.HasKey(e => e.HatSyohin);

            entity.ToTable("COM_SYOHIN_MST");

            entity.HasIndex(e => e.SelName, "IX_COM_SYOHIN_MST_SEL_NAME");

            entity.Property(e => e.HatSyohin)
                .HasMaxLength(50)
                .HasColumnName("HAT_SYOHIN");
            entity.Property(e => e.AbcHyouka)
                .HasMaxLength(1)
                .HasColumnName("ABC_HYOUKA");
            entity.Property(e => e.AcBuncd)
                .HasMaxLength(4)
                .HasColumnName("AC_BUNCD");
            entity.Property(e => e.AcBunnm1)
                .HasMaxLength(15)
                .HasColumnName("AC_BUNNM1");
            entity.Property(e => e.AcBunnm2)
                .HasMaxLength(20)
                .HasColumnName("AC_BUNNM2");
            entity.Property(e => e.AcDsy)
                .HasMaxLength(2)
                .HasColumnName("AC_DSY");
            entity.Property(e => e.AcKanm)
                .HasMaxLength(35)
                .HasColumnName("AC_KANM");
            entity.Property(e => e.AcKvkey)
                .HasMaxLength(38)
                .HasColumnName("AC_KVKEY");
            entity.Property(e => e.AcMkflg1)
                .HasMaxLength(1)
                .HasColumnName("AC_MKFLG1");
            entity.Property(e => e.AcMkflg2)
                .HasMaxLength(1)
                .HasColumnName("AC_MKFLG2");
            entity.Property(e => e.AcNoki)
                .HasMaxLength(2)
                .HasColumnName("AC_NOKI");
            entity.Property(e => e.AcRno)
                .HasMaxLength(1)
                .HasColumnName("AC_RNO");
            entity.Property(e => e.AcSt1)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_1");
            entity.Property(e => e.AcSt10)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_10");
            entity.Property(e => e.AcSt2)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_2");
            entity.Property(e => e.AcSt3)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_3");
            entity.Property(e => e.AcSt4)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_4");
            entity.Property(e => e.AcSt5)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_5");
            entity.Property(e => e.AcSt6)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_6");
            entity.Property(e => e.AcSt7)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_7");
            entity.Property(e => e.AcSt8)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_8");
            entity.Property(e => e.AcSt9)
                .HasMaxLength(1)
                .HasColumnName("AC_ST_9");
            entity.Property(e => e.AcTancd)
                .HasMaxLength(2)
                .HasColumnName("AC_TANCD");
            entity.Property(e => e.AcTis)
                .HasColumnType("date")
                .HasColumnName("AC_TIS");
            entity.Property(e => e.AcVkey)
                .HasMaxLength(38)
                .HasColumnName("AC_VKEY");
            entity.Property(e => e.AcXhat)
                .HasMaxLength(1)
                .HasColumnName("AC_XHAT");
            entity.Property(e => e.AcXkvan)
                .HasMaxLength(1)
                .HasColumnName("AC_XKVAN");
            entity.Property(e => e.AcYobi1)
                .HasMaxLength(2)
                .HasColumnName("AC_YOBI1");
            entity.Property(e => e.Bumoncd)
                .HasMaxLength(10)
                .HasColumnName("BUMONCD");
            entity.Property(e => e.CatalgId)
                .HasMaxLength(10)
                .HasColumnName("CATALG_ID");
            entity.Property(e => e.CatalgNo)
                .HasMaxLength(10)
                .HasColumnName("CATALG_NO");
            entity.Property(e => e.CatalgPage)
                .HasMaxLength(5)
                .HasColumnName("CATALG_PAGE");
            entity.Property(e => e.Cinet)
                .HasMaxLength(40)
                .HasColumnName("CINET");
            entity.Property(e => e.Code10)
                .HasMaxLength(10)
                .HasColumnName("CODE10");
            entity.Property(e => e.ComSyohin)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("COM_SYOHIN");
            entity.Property(e => e.ComSyohinKikaku)
                .HasMaxLength(30)
                .HasColumnName("COM_SYOHIN_KIKAKU");
            entity.Property(e => e.ComSyohinName)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("COM_SYOHIN_NAME");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.DelDate)
                .HasColumnType("date")
                .HasColumnName("DEL_DATE");
            entity.Property(e => e.DelFlg)
                .HasMaxLength(1)
                .HasColumnName("DEL_FLG");
            entity.Property(e => e.DelUseMax)
                .HasColumnType("date")
                .HasColumnName("DEL_USE_MAX");
            entity.Property(e => e.EcopointoKbn)
                .HasMaxLength(1)
                .HasColumnName("ECOPOINTO_KBN");
            entity.Property(e => e.FosFlg)
                .HasMaxLength(1)
                .HasDefaultValueSql("((1))")
                .HasColumnName("FOS_FLG");
            entity.Property(e => e.GCategoryCode)
                .HasMaxLength(8)
                .HasColumnName("G_CATEGORY_CODE");
            entity.Property(e => e.GSerialNo)
                .HasMaxLength(40)
                .HasColumnName("G_SERIAL_NO");
            entity.Property(e => e.GStockManageType).HasColumnName("G_STOCK_MANAGE_TYPE");
            entity.Property(e => e.GStockReserveType).HasColumnName("G_STOCK_RESERVE_TYPE");
            entity.Property(e => e.GSupSubNo).HasColumnName("G_SUP_SUB_NO");
            entity.Property(e => e.GTaxType).HasColumnName("G_TAX_TYPE");
            entity.Property(e => e.GWideUseType).HasColumnName("G_WIDE_USE_TYPE");
            entity.Property(e => e.Groupcd1)
                .HasMaxLength(50)
                .HasColumnName("GROUPCD1");
            entity.Property(e => e.Groupcd2)
                .HasMaxLength(50)
                .HasColumnName("GROUPCD2");
            entity.Property(e => e.Groupcd3)
                .HasMaxLength(50)
                .HasColumnName("GROUPCD3");
            entity.Property(e => e.Groupcd4)
                .HasMaxLength(50)
                .HasColumnName("GROUPCD4");
            entity.Property(e => e.Groupcd5)
                .HasMaxLength(50)
                .HasColumnName("GROUPCD5");
            entity.Property(e => e.HachuKbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("HACHU_KBN");
            entity.Property(e => e.HachuLot)
                .HasColumnType("decimal(4, 0)")
                .HasColumnName("HACHU_LOT");
            entity.Property(e => e.HachuReadtime)
                .HasColumnType("decimal(3, 0)")
                .HasColumnName("HACHU_READTIME");
            entity.Property(e => e.HachuniKbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("HACHUNI_KBN");
            entity.Property(e => e.HanbaiMarume)
                .HasColumnType("decimal(2, 0)")
                .HasColumnName("HANBAI_MARUME");
            entity.Property(e => e.HanbaiTaikei)
                .HasMaxLength(5)
                .HasColumnName("HANBAI_TAIKEI");
            entity.Property(e => e.HanbaitanDate)
                .HasColumnType("date")
                .HasColumnName("HANBAITAN_DATE");
            entity.Property(e => e.HanbaitanNew)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("HANBAITAN_NEW");
            entity.Property(e => e.HanbaitanOld)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("HANBAITAN_OLD");
            entity.Property(e => e.HashimotoPurchase)
                .HasDefaultValueSql("((0))")
                .HasColumnName("HASHIMOTO_PURCHASE");
            entity.Property(e => e.HatSyohinCh)
                .HasMaxLength(50)
                .HasColumnName("HAT_SYOHIN_CH");
            entity.Property(e => e.HinbanCd)
                .HasMaxLength(2)
                .HasColumnName("HINBAN_CD");
            entity.Property(e => e.HinbanShuKbn)
                .HasMaxLength(2)
                .HasColumnName("HINBAN_SHU_KBN");
            entity.Property(e => e.HizaikotanDate)
                .HasColumnType("date")
                .HasColumnName("HIZAIKOTAN_DATE");
            entity.Property(e => e.HizaikotanNew)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("HIZAIKOTAN_NEW");
            entity.Property(e => e.HizaikotanOld)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("HIZAIKOTAN_OLD");
            entity.Property(e => e.HopeBarcode)
                .HasMaxLength(15)
                .HasColumnName("HOPE_BARCODE");
            entity.Property(e => e.HopeCheckFlg)
                .HasMaxLength(1)
                .HasColumnName("HOPE_CHECK_FLG");
            entity.Property(e => e.HopeChu)
                .HasMaxLength(4)
                .HasColumnName("HOPE_CHU");
            entity.Property(e => e.HopeDai)
                .HasMaxLength(4)
                .HasColumnName("HOPE_DAI");
            entity.Property(e => e.HopeDispSeq)
                .HasMaxLength(10)
                .HasColumnName("HOPE_DISP_SEQ");
            entity.Property(e => e.HopeFlg)
                .HasMaxLength(1)
                .HasDefaultValueSql("((0))")
                .HasColumnName("HOPE_FLG");
            entity.Property(e => e.HopeMekarcd)
                .HasMaxLength(7)
                .HasColumnName("HOPE_MEKARCD");
            entity.Property(e => e.HopeSyohin)
                .HasMaxLength(38)
                .HasColumnName("HOPE_SYOHIN");
            entity.Property(e => e.HopeSyohinKikaku)
                .HasMaxLength(20)
                .HasColumnName("HOPE_SYOHIN_KIKAKU");
            entity.Property(e => e.HopeSyohinName)
                .HasMaxLength(40)
                .HasColumnName("HOPE_SYOHIN_NAME");
            entity.Property(e => e.HopeTeibanKbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("HOPE_TEIBAN_KBN");
            entity.Property(e => e.HostTaikei)
                .HasMaxLength(4)
                .HasColumnName("HOST_TAIKEI");
            entity.Property(e => e.Hscd)
                .HasMaxLength(6)
                .HasColumnName("HSCD");
            entity.Property(e => e.InsDate)
                .HasColumnType("datetime")
                .HasColumnName("INS_DATE");
            entity.Property(e => e.InsUserid)
                .HasMaxLength(20)
                .HasColumnName("INS_USERID");
            entity.Property(e => e.IrisuChu)
                .HasColumnType("decimal(5, 0)")
                .HasColumnName("IRISU_CHU");
            entity.Property(e => e.IrisuDai)
                .HasColumnType("decimal(5, 0)")
                .HasColumnName("IRISU_DAI");
            entity.Property(e => e.IrisuSho)
                .HasColumnType("decimal(5, 0)")
                .HasColumnName("IRISU_SHO");
            entity.Property(e => e.Itfcd)
                .HasMaxLength(14)
                .HasColumnName("ITFCD");
            entity.Property(e => e.Jancd)
                .HasMaxLength(13)
                .HasColumnName("JANCD");
            entity.Property(e => e.Jyuryo)
                .HasColumnType("decimal(9, 3)")
                .HasColumnName("JYURYO");
            entity.Property(e => e.JyuryoTani)
                .HasMaxLength(1)
                .HasColumnName("JYURYO_TANI");
            entity.Property(e => e.KakakuKbn)
                .HasMaxLength(1)
                .HasColumnName("KAKAKU_KBN");
            entity.Property(e => e.KeiChubunrui)
                .HasMaxLength(10)
                .HasColumnName("KEI_CHUBUNRUI");
            entity.Property(e => e.KeiDaibunrui)
                .HasMaxLength(10)
                .HasColumnName("KEI_DAIBUNRUI");
            entity.Property(e => e.KeiShobunrui)
                .HasMaxLength(10)
                .HasColumnName("KEI_SHOBUNRUI");
            entity.Property(e => e.KeiyakuBunrui)
                .HasMaxLength(5)
                .HasColumnName("KEIYAKU_BUNRUI");
            entity.Property(e => e.MekarBunrui)
                .HasMaxLength(5)
                .HasColumnName("MEKAR_BUNRUI");
            entity.Property(e => e.MekarHinban)
                .HasMaxLength(50)
                .HasColumnName("MEKAR_HINBAN");
            entity.Property(e => e.MekarName)
                .HasMaxLength(60)
                .HasColumnName("MEKAR_NAME");
            entity.Property(e => e.MekarNameK)
                .HasMaxLength(30)
                .HasColumnName("MEKAR_NAME_K");
            entity.Property(e => e.Mekarcd)
                .IsRequired()
                .HasMaxLength(5)
                .HasColumnName("MEKARCD");
            entity.Property(e => e.MotoHatSyohin)
                .HasMaxLength(50)
                .HasColumnName("MOTO_HAT_SYOHIN");
            entity.Property(e => e.NagamonoFlg)
                .HasMaxLength(1)
                .HasColumnName("NAGAMONO_FLG");
            entity.Property(e => e.OpsChu)
                .HasMaxLength(10)
                .HasColumnName("OPS_CHU");
            entity.Property(e => e.OpsDai)
                .HasMaxLength(10)
                .HasColumnName("OPS_DAI");
            entity.Property(e => e.OpsFlg)
                .HasMaxLength(1)
                .HasColumnName("OPS_FLG");
            entity.Property(e => e.OpsSaidai)
                .HasMaxLength(10)
                .HasColumnName("OPS_SAIDAI");
            entity.Property(e => e.OpsSaisho)
                .HasMaxLength(10)
                .HasColumnName("OPS_SAISHO");
            entity.Property(e => e.OpsSho)
                .HasMaxLength(10)
                .HasColumnName("OPS_SHO");
            entity.Property(e => e.Ordercd)
                .HasMaxLength(13)
                .HasColumnName("ORDERCD");
            entity.Property(e => e.OroshitanDate)
                .HasColumnType("date")
                .HasColumnName("OROSHITAN_DATE");
            entity.Property(e => e.OroshitanNew)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("OROSHITAN_NEW");
            entity.Property(e => e.OroshitanOld)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("OROSHITAN_OLD");
            entity.Property(e => e.OtherUseKbn)
                .HasMaxLength(1)
                .HasColumnName("OTHER_USE_KBN");
            entity.Property(e => e.Saisu)
                .HasColumnType("decimal(9, 3)")
                .HasColumnName("SAISU");
            entity.Property(e => e.SaisuUt)
                .HasColumnType("decimal(12, 3)")
                .HasColumnName("SAISU_UT");
            entity.Property(e => e.SelChubunrui)
                .HasMaxLength(10)
                .HasColumnName("SEL_CHUBUNRUI");
            entity.Property(e => e.SelDaibunrui)
                .HasMaxLength(10)
                .HasColumnName("SEL_DAIBUNRUI");
            entity.Property(e => e.SelName)
                .HasMaxLength(200)
                .HasColumnName("SEL_NAME");
            entity.Property(e => e.SelShobunrui)
                .HasMaxLength(10)
                .HasColumnName("SEL_SHOBUNRUI");
            entity.Property(e => e.SelectHinbanKbn)
                .HasMaxLength(1)
                .HasColumnName("SELECT_HINBAN_KBN");
            entity.Property(e => e.SetAutokumi)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("SET_AUTOKUMI");
            entity.Property(e => e.SetHinban)
                .HasMaxLength(50)
                .HasColumnName("SET_HINBAN");
            entity.Property(e => e.SetKbn)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("SET_KBN");
            entity.Property(e => e.ShiireMarume)
                .HasColumnType("decimal(2, 0)")
                .HasColumnName("SHIIRE_MARUME");
            entity.Property(e => e.ShiireTaikei)
                .HasMaxLength(5)
                .HasColumnName("SHIIRE_TAIKEI");
            entity.Property(e => e.ShiireZeikbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("SHIIRE_ZEIKBN");
            entity.Property(e => e.ShiireZeirank)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("SHIIRE_ZEIRANK");
            entity.Property(e => e.ShiireniKbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("SHIIRENI_KBN");
            entity.Property(e => e.Shiiresaki)
                .HasMaxLength(10)
                .HasColumnName("SHIIRESAKI");
            entity.Property(e => e.Shiiresaki2)
                .HasMaxLength(10)
                .HasColumnName("SHIIRESAKI2");
            entity.Property(e => e.ShiyouNo)
                .HasMaxLength(15)
                .HasColumnName("SHIYOU_NO");
            entity.Property(e => e.SkeiChubunrui)
                .HasMaxLength(10)
                .HasColumnName("SKEI_CHUBUNRUI");
            entity.Property(e => e.SkeiDaibunrui)
                .HasMaxLength(10)
                .HasColumnName("SKEI_DAIBUNRUI");
            entity.Property(e => e.SkeiShobunrui)
                .HasMaxLength(10)
                .HasColumnName("SKEI_SHOBUNRUI");
            entity.Property(e => e.SoapFlg)
                .HasMaxLength(1)
                .HasColumnName("SOAP_FLG");
            entity.Property(e => e.SortNo1)
                .HasMaxLength(50)
                .HasColumnName("SORT_NO1");
            entity.Property(e => e.SortNo2)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("SORT_NO2");
            entity.Property(e => e.SortNo3)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("SORT_NO3");
            entity.Property(e => e.SortNo4)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("SORT_NO4");
            entity.Property(e => e.SortNo5)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("SORT_NO5");
            entity.Property(e => e.SortNo6)
                .HasMaxLength(50)
                .HasColumnName("SORT_NO6");
            entity.Property(e => e.SuryoTani)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnName("SURYO_TANI");
            entity.Property(e => e.SutairuHinban)
                .HasMaxLength(20)
                .HasColumnName("SUTAIRU_HINBAN");
            entity.Property(e => e.SyohinBunrui)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("SYOHIN_BUNRUI");
            entity.Property(e => e.SyohinKikaku)
                .HasMaxLength(20)
                .HasColumnName("SYOHIN_KIKAKU");
            entity.Property(e => e.SyohinName)
                .HasMaxLength(100)
                .HasColumnName("SYOHIN_NAME");
            entity.Property(e => e.SyohinNameK)
                .HasMaxLength(50)
                .HasColumnName("SYOHIN_NAME_K");
            entity.Property(e => e.SyouanKbn)
                .HasMaxLength(1)
                .HasColumnName("SYOUAN_KBN");
            entity.Property(e => e.Takasa)
                .HasColumnType("decimal(7, 0)")
                .HasColumnName("TAKASA");
            entity.Property(e => e.TakasaUt)
                .HasColumnType("decimal(9, 0)")
                .HasColumnName("TAKASA_UT");
            entity.Property(e => e.Tate)
                .HasColumnType("decimal(7, 0)")
                .HasColumnName("TATE");
            entity.Property(e => e.TateUt)
                .HasColumnType("decimal(9, 0)")
                .HasColumnName("TATE_UT");
            entity.Property(e => e.TeikatanDate)
                .HasColumnType("date")
                .HasColumnName("TEIKATAN_DATE");
            entity.Property(e => e.TeikatanNew)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("TEIKATAN_NEW");
            entity.Property(e => e.TeikatanOld)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("TEIKATAN_OLD");
            entity.Property(e => e.TokujyuCd)
                .HasMaxLength(2)
                .HasColumnName("TOKUJYU_CD");
            entity.Property(e => e.Toukeicd1)
                .HasMaxLength(10)
                .HasColumnName("TOUKEICD1");
            entity.Property(e => e.Toukeicd2)
                .HasMaxLength(10)
                .HasColumnName("TOUKEICD2");
            entity.Property(e => e.Toukeicd3)
                .HasMaxLength(10)
                .HasColumnName("TOUKEICD3");
            entity.Property(e => e.Toukeicd4)
                .HasMaxLength(10)
                .HasColumnName("TOUKEICD4");
            entity.Property(e => e.Toukeicd5)
                .HasMaxLength(10)
                .HasColumnName("TOUKEICD5");
            entity.Property(e => e.UpdCnt)
                .HasColumnType("decimal(3, 0)")
                .HasColumnName("UPD_CNT");
            entity.Property(e => e.UpdDate)
                .HasColumnType("datetime")
                .HasColumnName("UPD_DATE");
            entity.Property(e => e.UpdUserid)
                .HasMaxLength(20)
                .HasColumnName("UPD_USERID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
            entity.Property(e => e.UriZeikbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("URI_ZEIKBN");
            entity.Property(e => e.UriZeirank)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("URI_ZEIRANK");
            entity.Property(e => e.UriniKbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("URINI_KBN");
            entity.Property(e => e.Yoko)
                .HasColumnType("decimal(7, 0)")
                .HasColumnName("YOKO");
            entity.Property(e => e.YokoUt)
                .HasColumnType("decimal(9, 0)")
                .HasColumnName("YOKO_UT");
            entity.Property(e => e.YotoCd)
                .HasMaxLength(2)
                .HasColumnName("YOTO_CD");
            entity.Property(e => e.ZaikoKanriKbn)
                .HasColumnType("decimal(1, 0)")
                .HasColumnName("ZAIKO_KANRI_KBN");
            entity.Property(e => e.ZaikotanDate)
                .HasColumnType("date")
                .HasColumnName("ZAIKOTAN_DATE");
            entity.Property(e => e.ZaikotanNew)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("ZAIKOTAN_NEW");
            entity.Property(e => e.ZaikotanOld)
                .HasColumnType("decimal(15, 3)")
                .HasColumnName("ZAIKOTAN_OLD");
            entity.Property(e => e.ZumenNo)
                .HasMaxLength(20)
                .HasColumnName("ZUMEN_NO");
        });

        modelBuilder.Entity<CompanysMst>(entity =>
        {
            entity.HasKey(e => e.CompCode);

            entity.ToTable("COMPANYS_MST", tb => tb.HasComment("取引先マスタ"));

            entity.Property(e => e.CompCode)
                .HasMaxLength(8)
                .HasComment("取引先コード")
                .HasColumnName("COMP_CODE");
            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .HasComment("住所１")
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .HasComment("住所２")
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.Address3)
                .HasMaxLength(40)
                .HasColumnName("ADDRESS3");
            entity.Property(e => e.CompBranchName)
                .HasMaxLength(40)
                .HasColumnName("COMP_BRANCH_NAME");
            entity.Property(e => e.CompGroupCode)
                .HasMaxLength(4)
                .HasComment("取引先グループコード")
                .HasColumnName("COMP_GROUP_CODE");
            entity.Property(e => e.CompKana)
                .HasMaxLength(40)
                .HasComment("取引先名カナ")
                .HasColumnName("COMP_KANA");
            entity.Property(e => e.CompKanaShort)
                .HasMaxLength(40)
                .HasColumnName("COMP_KANA_SHORT");
            entity.Property(e => e.CompName)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("取引先名")
                .HasColumnName("COMP_NAME");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeleteDate)
                .HasColumnType("datetime")
                .HasColumnName("DELETE_DATE");
            entity.Property(e => e.Deleted).HasColumnName("DELETED");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("FAX");
            entity.Property(e => e.Fax2)
                .HasMaxLength(15)
                .HasColumnName("FAX2");
            entity.Property(e => e.InvoiceRegistNumber)
                .HasMaxLength(14)
                .HasComment("インボイス登録番号")
                .HasColumnName("INVOICE_REGIST_NUMBER");
            entity.Property(e => e.MaxCredit)
                .HasComment("与信限度額")
                .HasColumnName("MAX_CREDIT");
            entity.Property(e => e.NoSalesFlg)
                .HasDefaultValueSql("((0))")
                .HasComment("取引禁止フラグ")
                .HasColumnName("NO_SALES_FLG");
            entity.Property(e => e.State)
                .HasMaxLength(4)
                .HasComment("都道府県")
                .HasColumnName("STATE");
            entity.Property(e => e.SupType)
                .HasDefaultValueSql("((0))")
                .HasComment("仕入先区分")
                .HasColumnName("SUP_TYPE");
            entity.Property(e => e.Tel)
                .HasMaxLength(15)
                .HasColumnName("TEL");
            entity.Property(e => e.TempCreditUp)
                .HasDefaultValueSql("((0))")
                .HasComment("与信一時増加枠")
                .HasColumnName("TEMP_CREDIT_UP");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WideUseType)
                .HasComment("雑区分")
                .HasColumnName("WIDE_USE_TYPE");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("郵便番号")
                .HasColumnName("ZIP_CODE");
        });

        modelBuilder.Entity<Construction>(entity =>
        {
            entity.HasKey(e => e.ConstructionCode).HasName("CONSTRUCTION_PKC");

            entity.ToTable("CONSTRUCTION", tb => tb.HasComment("物件情報★:物件情報を格納するテーブル"));

            entity.Property(e => e.ConstructionCode)
                .HasMaxLength(20)
                .HasComment("物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.BalanceSheet)
                .HasComment("BS")
                .HasColumnName("BALANCE_SHEET");
            entity.Property(e => e.BuildingNameEtc)
                .HasMaxLength(50)
                .HasComment("ビル名等:物件王に合わせて項目追加")
                .HasColumnName("BUILDING_NAME_ETC");
            entity.Property(e => e.Comment)
                .HasMaxLength(1024)
                .HasComment("コメント:備考を入力するカラム")
                .HasColumnName("COMMENT");
            entity.Property(e => e.ConstructionKana)
                .HasMaxLength(50)
                .HasComment("物件名フリガナ")
                .HasColumnName("CONSTRUCTION_KANA");
            entity.Property(e => e.ConstructionName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("物件名")
                .HasColumnName("CONSTRUCTION_NAME");
            entity.Property(e => e.ConstructorFax)
                .HasMaxLength(15)
                .HasComment("建設会社FAX")
                .HasColumnName("CONSTRUCTOR_FAX");
            entity.Property(e => e.ConstructorIndustry)
                .HasComment("建設業種,0:大手サブコン:物件王に合わせて項目追加")
                .HasColumnName("CONSTRUCTOR_INDUSTRY");
            entity.Property(e => e.ConstructorName)
                .HasMaxLength(50)
                .HasComment("建設会社名")
                .HasColumnName("CONSTRUCTOR_NAME");
            entity.Property(e => e.ConstructorRepName)
                .HasMaxLength(50)
                .HasComment("建設会社代表者名")
                .HasColumnName("CONSTRUCTOR_REP_NAME");
            entity.Property(e => e.ConstructorTel)
                .HasMaxLength(15)
                .HasComment("建設会社TEL")
                .HasColumnName("CONSTRUCTOR_TEL");
            entity.Property(e => e.ConstructorType)
                .HasComment("建設種別,0:マンション:物件王に合わせて項目追加")
                .HasColumnName("CONSTRUCTOR_TYPE");
            entity.Property(e => e.ConstructtonNotes)
                .HasMaxLength(1024)
                .HasComment("物件備考")
                .HasColumnName("CONSTRUCTTON_NOTES");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EmpId)
                .HasComment("担当社員ID:物件王に合わせて項目追加")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.EstimateSendDate)
                .HasComment("見積送付日:見積書を得意先に提出した日")
                .HasColumnType("datetime")
                .HasColumnName("ESTIMATE_SEND_DATE");
            entity.Property(e => e.InquiryDate)
                .HasComment("引合日:引合を受けた日")
                .HasColumnType("datetime")
                .HasColumnName("INQUIRY_DATE");
            entity.Property(e => e.KmanCd)
                .HasMaxLength(2)
                .HasComment("キーマンCD:物件王に合わせて項目追加")
                .HasColumnName("KMAN_CD");
            entity.Property(e => e.NewInstallCount)
                .HasComment("新設件数")
                .HasColumnName("NEW_INSTALL_COUNT");
            entity.Property(e => e.OrderCompeltedDate)
                .HasComment("受注対応完了日:受注対応が完了した日")
                .HasColumnType("datetime")
                .HasColumnName("ORDER_COMPELTED_DATE");
            entity.Property(e => e.OrderConfidence)
                .HasMaxLength(20)
                .HasComment("受注確度")
                .HasColumnName("ORDER_CONFIDENCE");
            entity.Property(e => e.OrderContractRceiptDate)
                .HasComment("注文請書受領日:注文書とセットで送られてくる")
                .HasColumnType("datetime")
                .HasColumnName("ORDER_CONTRACT_RCEIPT_DATE");
            entity.Property(e => e.OrderContractSendDate)
                .HasComment("注文請書送付日:社印を押して得意先に送付した日")
                .HasColumnType("datetime")
                .HasColumnName("ORDER_CONTRACT_SEND_DATE");
            entity.Property(e => e.OrderRceiptDate)
                .HasComment("注文書受領日:受注日とイコール")
                .HasColumnType("datetime")
                .HasColumnName("ORDER_RCEIPT_DATE");
            entity.Property(e => e.OrderState)
                .HasComment("受注状態,0:引合/1:見積作成/2:見積提出/3:受注済/4:完了")
                .HasColumnName("ORDER_STATE");
            entity.Property(e => e.PropertyAddress)
                .HasMaxLength(50)
                .HasComment("物件住所:NACSS取込で使用")
                .HasColumnName("PROPERTY_ADDRESS");
            entity.Property(e => e.RecommendComment)
                .HasMaxLength(1024)
                .HasComment("推薦備考:推薦物件の備考を入力するカラム")
                .HasColumnName("RECOMMEND_COMMENT");
            entity.Property(e => e.Recommended)
                .HasDefaultValueSql("((0))")
                .HasComment("推薦物件:推薦物件かどうか")
                .HasColumnName("RECOMMENDED");
            entity.Property(e => e.RecvAdd1)
                .HasMaxLength(30)
                .HasComment("現場住所1:一貫化に流すため同じ体系にする")
                .HasColumnName("RECV_ADD1");
            entity.Property(e => e.RecvAdd2)
                .HasMaxLength(30)
                .HasComment("現場住所2:一貫化に流すため同じ体系にする")
                .HasColumnName("RECV_ADD2");
            entity.Property(e => e.RecvAdd3)
                .HasMaxLength(30)
                .HasComment("現場住所3:一貫化に流すため同じ体系にする")
                .HasColumnName("RECV_ADD3");
            entity.Property(e => e.RecvFax)
                .HasMaxLength(15)
                .HasComment("現場FAX")
                .HasColumnName("RECV_FAX");
            entity.Property(e => e.RecvPostcode)
                .HasMaxLength(8)
                .HasComment("現場郵便番号:一貫化の工事店に該当する箇所")
                .HasColumnName("RECV_POSTCODE");
            entity.Property(e => e.RecvTel)
                .HasMaxLength(15)
                .HasComment("現場TEL")
                .HasColumnName("RECV_TEL");
            entity.Property(e => e.RegistorEmpId)
                .HasComment("登録者社員ID")
                .HasColumnName("REGISTOR_EMP_ID");
            entity.Property(e => e.RemovalCount)
                .HasComment("撤去件数")
                .HasColumnName("REMOVAL_COUNT");
            entity.Property(e => e.RenovationCount)
                .HasComment("改造件数")
                .HasColumnName("RENOVATION_COUNT");
            entity.Property(e => e.SalesEvent)
                .HasComment("セール")
                .HasColumnName("SALES_EVENT");
            entity.Property(e => e.SearchKey)
                .HasMaxLength(20)
                .HasComment("検索キー:ユーザー自由項目")
                .HasColumnName("SEARCH_KEY");
            entity.Property(e => e.TeamCd)
                .HasMaxLength(3)
                .HasComment("チームCD:物件王に合わせて項目追加")
                .HasColumnName("TEAM_CD");
            entity.Property(e => e.Thailand)
                .HasComment("タイ")
                .HasColumnName("THAILAND");
            entity.Property(e => e.TokuiCd)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("得意先コード:得意先マスタ (取引先マスタ)の取引先コード")
                .HasColumnName("TOKUI_CD");
            entity.Property(e => e.Uncontracted)
                .HasDefaultValueSql("((0))")
                .HasComment("未契約物件:契約単価が決定する前に受注したかどうか")
                .HasColumnName("UNCONTRACTED");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ConstructionDetail>(entity =>
        {
            entity.HasKey(e => new { e.ConstructionCode, e.Koban }).HasName("CONSTRUCTION_DETAIL_PKC");

            entity.ToTable("CONSTRUCTION_DETAIL", tb => tb.HasComment("物件明細:物件明細テーブル"));

            entity.Property(e => e.ConstructionCode)
                .HasMaxLength(20)
                .HasComment("物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.Koban)
                .HasComment("子番")
                .HasColumnName("KOBAN");
            entity.Property(e => e.AppropState)
                .HasComment("計上ステータス,0:未計上/1:計上済")
                .HasColumnName("APPROP_STATE");
            entity.Property(e => e.Bara)
                .HasComment("バラ数")
                .HasColumnName("BARA");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Nouki)
                .HasComment("納日")
                .HasColumnType("date")
                .HasColumnName("NOUKI");
            entity.Property(e => e.SiiKake)
                .HasComment("掛率(仕入)")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("SII_KAKE");
            entity.Property(e => e.SiiTan)
                .HasComment("仕入単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SII_TAN");
            entity.Property(e => e.Suryo)
                .HasComment("数量")
                .HasColumnName("SURYO");
            entity.Property(e => e.SyohinCd)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("SYOHIN_CD");
            entity.Property(e => e.SyohinName)
                .HasMaxLength(100)
                .HasComment("商品名称")
                .HasColumnName("SYOHIN_NAME");
            entity.Property(e => e.Tani)
                .HasMaxLength(10)
                .HasComment("単位")
                .HasColumnName("TANI");
            entity.Property(e => e.TeiTan)
                .HasComment("定価単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("TEI_TAN");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者")
                .HasColumnName("UPDATER");
            entity.Property(e => e.UriKake)
                .HasComment("掛率(売上)")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("URI_KAKE");
            entity.Property(e => e.UriTan)
                .HasComment("売上単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("URI_TAN");
        });

        modelBuilder.Entity<ConstructionLock>(entity =>
        {
            entity.HasKey(e => e.ConstructionCode).HasName("CONSTRUCTION_LOCK_PKC");

            entity.ToTable("CONSTRUCTION_LOCK", tb => tb.HasComment("物件情報ロック"));

            entity.Property(e => e.ConstructionCode)
                .HasMaxLength(20)
                .HasComment("物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EditStartDatetime)
                .HasComment("編集開始日時")
                .HasColumnType("datetime")
                .HasColumnName("EDIT_START_DATETIME");
            entity.Property(e => e.EditorEmpId)
                .HasComment("編集者")
                .HasColumnName("EDITOR_EMP_ID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Credit>(entity =>
        {
            entity.HasKey(e => e.CreditNo).HasName("CREDIT_PKC");

            entity.ToTable("CREDIT", tb => tb.HasComment("入金データ"));

            entity.Property(e => e.CreditNo)
                .HasMaxLength(10)
                .HasComment("入金番号")
                .HasColumnName("CREDIT_NO");
            entity.Property(e => e.BankAcutCode)
                .HasMaxLength(8)
                .HasComment("入金口座コード")
                .HasColumnName("BANK_ACUT_CODE");
            entity.Property(e => e.CompCode)
                .HasMaxLength(8)
                .HasComment("顧客コード")
                .HasColumnName("COMP_CODE");
            entity.Property(e => e.CompSubNo)
                .HasComment("顧客枝番")
                .HasColumnName("COMP_SUB_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasMaxLength(12)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.CreditDate)
                .HasComment("入金日")
                .HasColumnType("datetime")
                .HasColumnName("CREDIT_DATE");
            entity.Property(e => e.DeptCode)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("部門コード")
                .HasColumnName("DEPT_CODE");
            entity.Property(e => e.PayMethodType)
                .HasDefaultValueSql("((1))")
                .HasComment("支払方法区分,1:振込,2:手形,3:でんさい")
                .HasColumnName("PAY_METHOD_TYPE");
            entity.Property(e => e.Received)
                .HasDefaultValueSql("((0))")
                .HasComment("消込金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("RECEIVED");
            entity.Property(e => e.ReceivedAmnt)
                .HasDefaultValueSql("((0))")
                .HasComment("入金金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("RECEIVED_AMNT");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatePgm)
                .HasMaxLength(50)
                .HasComment("更新プログラム名")
                .HasColumnName("UPDATE_PGM");
            entity.Property(e => e.UpdatePlgDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("プログラム更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_PLG_DATE");
            entity.Property(e => e.Updater)
                .HasMaxLength(12)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<CustomersMst>(entity =>
        {
            entity.HasKey(e => new { e.CustCode, e.CustSubNo }).IsClustered(false);

            entity.ToTable("CUSTOMERS_MST", tb => tb.HasComment("顧客マスタ"));

            entity.Property(e => e.CustCode)
                .HasMaxLength(12)
                .HasComment("顧客コード")
                .HasColumnName("CUST_CODE");
            entity.Property(e => e.CustSubNo)
                .HasComment("顧客枝番")
                .HasColumnName("CUST_SUB_NO");
            entity.Property(e => e.ArCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasComment("請求先コード")
                .HasColumnName("AR_CODE");
            entity.Property(e => e.ArSubNo)
                .HasComment("請求先枝番")
                .HasColumnName("AR_SUB_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.CustAddress1)
                .HasMaxLength(40)
                .HasComment("顧客住所１")
                .HasColumnName("CUST_ADDRESS1");
            entity.Property(e => e.CustAddress2)
                .HasMaxLength(40)
                .HasComment("顧客住所２")
                .HasColumnName("CUST_ADDRESS2");
            entity.Property(e => e.CustAddress3)
                .HasMaxLength(40)
                .HasColumnName("CUST_ADDRESS3");
            entity.Property(e => e.CustArFlag)
                .HasComment("顧客請求区分,1:都度請求,2:締請求")
                .HasColumnName("CUST_AR_FLAG");
            entity.Property(e => e.CustCloseDate1)
                .HasComment("顧客締日１,15:15日締め")
                .HasColumnName("CUST_CLOSE_DATE1");
            entity.Property(e => e.CustCloseDate2)
                .HasComment("顧客締日２,99:末締め")
                .HasColumnName("CUST_CLOSE_DATE2");
            entity.Property(e => e.CustEmail)
                .HasMaxLength(100)
                .HasComment("顧客メールアドレス")
                .HasColumnName("CUST_EMAIL");
            entity.Property(e => e.CustFax)
                .HasMaxLength(15)
                .HasComment("顧客FAX番号")
                .HasColumnName("CUST_FAX");
            entity.Property(e => e.CustKana)
                .HasMaxLength(40)
                .HasComment("顧客名カナ")
                .HasColumnName("CUST_KANA");
            entity.Property(e => e.CustName)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("顧客名")
                .HasColumnName("CUST_NAME");
            entity.Property(e => e.CustPayDates1)
                .HasComment("顧客支払日１,10:10日払い,99：末日")
                .HasColumnName("CUST_PAY_DATES1");
            entity.Property(e => e.CustPayDates2)
                .HasComment("顧客支払日２,10:10日払い,99：末日")
                .HasColumnName("CUST_PAY_DATES2");
            entity.Property(e => e.CustPayMethod1)
                .HasDefaultValueSql("((1))")
                .HasComment("顧客支払方法１,1:振込,2:手形")
                .HasColumnName("CUST_PAY_METHOD1");
            entity.Property(e => e.CustPayMethod2)
                .HasDefaultValueSql("((1))")
                .HasComment("顧客支払方法２,1:振込,2:手形")
                .HasColumnName("CUST_PAY_METHOD2");
            entity.Property(e => e.CustPayMonths1)
                .HasDefaultValueSql("((1))")
                .HasComment("顧客支払月１,0:当月,1:翌月,2:翌々月")
                .HasColumnName("CUST_PAY_MONTHS1");
            entity.Property(e => e.CustPayMonths2)
                .HasDefaultValueSql("((1))")
                .HasComment("顧客支払月２,0:当月,1:翌月,2:翌々月")
                .HasColumnName("CUST_PAY_MONTHS2");
            entity.Property(e => e.CustState)
                .HasMaxLength(4)
                .HasComment("顧客都道府県")
                .HasColumnName("CUST_STATE");
            entity.Property(e => e.CustTel)
                .HasMaxLength(15)
                .HasComment("顧客電話番号")
                .HasColumnName("CUST_TEL");
            entity.Property(e => e.CustType)
                .HasDefaultValueSql("((0))")
                .HasComment("顧客区分")
                .HasColumnName("CUST_TYPE");
            entity.Property(e => e.CustUserDepName)
                .HasMaxLength(40)
                .HasComment("顧客部門名")
                .HasColumnName("CUST_USER_DEP_NAME");
            entity.Property(e => e.CustUserName)
                .HasMaxLength(20)
                .HasComment("顧客担当者名")
                .HasColumnName("CUST_USER_NAME");
            entity.Property(e => e.CustZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("顧客郵便番号")
                .HasColumnName("CUST_ZIP_CODE");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.EmpCode)
                .HasMaxLength(10)
                .HasComment("自社担当者コード")
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.KeymanCode)
                .HasMaxLength(2)
                .HasColumnName("KEYMAN_CODE");
            entity.Property(e => e.KojitenCode)
                .HasMaxLength(6)
                .HasColumnName("KOJITEN_CODE");
            entity.Property(e => e.PayerCode)
                .HasMaxLength(8)
                .HasComment("回収先コード")
                .HasColumnName("PAYER_CODE");
            entity.Property(e => e.PayerSubNo)
                .HasComment("回収先枝番")
                .HasColumnName("PAYER_SUB_NO");
            entity.Property(e => e.St1)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST1");
            entity.Property(e => e.St10)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST10");
            entity.Property(e => e.St2)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST2");
            entity.Property(e => e.St3)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST3");
            entity.Property(e => e.St4)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST4");
            entity.Property(e => e.St5)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST5");
            entity.Property(e => e.St6)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST6");
            entity.Property(e => e.St7)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST7");
            entity.Property(e => e.St8)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST8");
            entity.Property(e => e.St9)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("ST9");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DeptMst>(entity =>
        {
            entity.HasKey(e => new { e.DeptCode, e.StartDate }).HasName("PK_DEPT_MST_0510");

            entity.ToTable("DEPT_MST", tb => tb.HasComment("部門マスタ"));

            entity.Property(e => e.DeptCode)
                .HasMaxLength(6)
                .HasComment("部門コード")
                .HasColumnName("DEPT_CODE");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DepName)
                .HasMaxLength(40)
                .HasComment("部門名")
                .HasColumnName("DEP_NAME");
            entity.Property(e => e.DeptLayer)
                .HasComment("組織階層")
                .HasColumnName("DEPT_LAYER");
            entity.Property(e => e.DeptPath)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("部門パス")
                .HasColumnName("DEPT_PATH");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("('2100/12/31')")
                .HasComment("終了日")
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.SlitYn)
                .HasDefaultValueSql("((1))")
                .HasComment("伝票入力可否,0:不可 1:可能")
                .HasColumnName("SLIT_YN");
            entity.Property(e => e.TeamCd)
                .IsRequired()
                .HasMaxLength(3)
                .HasComment("チームコード")
                .HasColumnName("TEAM_CD");
            entity.Property(e => e.Terminal)
                .HasComment("最下層区分")
                .HasColumnName("TERMINAL");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DestinationsMst>(entity =>
        {
            entity.HasKey(e => new { e.CustCode, e.CustSubNo, e.DistNo });

            entity.ToTable("DESTINATIONS_MST", tb => tb.HasComment("出荷先マスタ"));

            entity.Property(e => e.CustCode)
                .HasMaxLength(12)
                .HasDefaultValueSql("((0))")
                .HasColumnName("CUST_CODE");
            entity.Property(e => e.CustSubNo)
                .HasComment("顧客枝番")
                .HasColumnName("CUST_SUB_NO");
            entity.Property(e => e.DistNo)
                .HasComment("出荷先番号")
                .HasColumnName("DIST_NO");
            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .HasComment("出荷先住所１")
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .HasComment("出荷先住所２")
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.Address3)
                .HasMaxLength(40)
                .HasComment("出荷先住所３")
                .HasColumnName("ADDRESS3");
            entity.Property(e => e.AreaCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComment("地域コード")
                .HasColumnName("AREA_CODE");
            entity.Property(e => e.CompCode)
                .HasMaxLength(8)
                .HasComment("取引先コード")
                .HasColumnName("COMP_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.DestFax)
                .HasMaxLength(40)
                .HasComment("出荷先電話FAX")
                .HasColumnName("DEST_FAX");
            entity.Property(e => e.DestTel)
                .HasMaxLength(40)
                .HasComment("出荷先電話番号")
                .HasColumnName("DEST_TEL");
            entity.Property(e => e.DistName1)
                .HasMaxLength(40)
                .HasComment("出荷先名１")
                .HasColumnName("DIST_NAME1");
            entity.Property(e => e.DistName2)
                .HasMaxLength(40)
                .HasComment("出荷先名２")
                .HasColumnName("DIST_NAME2");
            entity.Property(e => e.GenbaCode)
                .HasMaxLength(3)
                .HasColumnName("GENBA_CODE");
            entity.Property(e => e.KojitenCode)
                .HasMaxLength(6)
                .HasComment("工事店コード")
                .HasColumnName("KOJITEN_CODE");
            entity.Property(e => e.Remarks)
                .HasMaxLength(500)
                .HasComment("備考")
                .HasColumnName("REMARKS");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("出荷先郵便番号")
                .HasColumnName("ZIP_CODE");
        });

        modelBuilder.Entity<DivAuth>(entity =>
        {
            entity.HasKey(e => e.AuthId);

            entity.ToTable("DIV_AUTH");

            entity.Property(e => e.AuthId).HasColumnName("AUTH_ID");
            entity.Property(e => e.AuthCd)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AUTH_CD");
            entity.Property(e => e.AuthDeleted).HasColumnName("AUTH_DELETED");
            entity.Property(e => e.AuthName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AUTH_NAME");
            entity.Property(e => e.AuthPassword)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AUTH_PASSWORD");
        });

        modelBuilder.Entity<DivBin>(entity =>
        {
            entity.HasKey(e => e.BinCd).HasName("PK__DIV_BIN__06AA1BE3B820A7BD");

            entity.ToTable("DIV_BIN", tb => tb.HasComment("便区分"));

            entity.Property(e => e.BinCd)
                .HasMaxLength(5)
                .HasComment("便区分CD")
                .HasColumnName("BIN_CD");
            entity.Property(e => e.BinName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("便区分名")
                .HasColumnName("BIN_NAME");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivDelivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryCd).HasName("PK__DIV_DELI__7D743BCCA29D734E");

            entity.ToTable("DIV_DELIVERY", tb => tb.HasComment("納品区分"));

            entity.Property(e => e.DeliveryCd)
                .HasMaxLength(1)
                .HasComment("納品区分CD")
                .HasColumnName("DELIVERY_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.DeliveryName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("納品区分名")
                .HasColumnName("DELIVERY_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivFare>(entity =>
        {
            entity.HasKey(e => e.FareCd).HasName("PK__DIV_FARE__2FA09D6699F32A93");

            entity.ToTable("DIV_FARE", tb => tb.HasComment("運賃区分"));

            entity.Property(e => e.FareCd)
                .HasMaxLength(1)
                .HasComment("運賃区分CD")
                .HasColumnName("FARE_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.FareName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("運賃区分名")
                .HasColumnName("FARE_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivInvoiceIssue>(entity =>
        {
            entity.HasKey(e => e.InvoiceIssueCd).HasName("PK__DIV_INVO__47F380120650CA6D");

            entity.ToTable("DIV_INVOICE_ISSUE", tb => tb.HasComment("便区分"));

            entity.Property(e => e.InvoiceIssueCd)
                .HasMaxLength(5)
                .HasComment("便区分CD")
                .HasColumnName("INVOICE_ISSUE_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.InvoiceIssueName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("便区分名")
                .HasColumnName("INVOICE_ISSUE_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivOrder>(entity =>
        {
            entity.HasKey(e => e.OrderCd).HasName("PK__DIV_ORDE__460A4B2C327F954B");

            entity.ToTable("DIV_ORDER", tb => tb.HasComment("発注区分"));

            entity.Property(e => e.OrderCd)
                .HasMaxLength(1)
                .HasComment("発注区分CD")
                .HasColumnName("ORDER_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.OrderName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("発注区分名")
                .HasColumnName("ORDER_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivSlip>(entity =>
        {
            entity.HasKey(e => e.SlipCd).HasName("PK__DIV_SLIP__58F2B252AF28E221");

            entity.ToTable("DIV_SLIP", tb => tb.HasComment("伝票区分"));

            entity.Property(e => e.SlipCd)
                .HasMaxLength(2)
                .HasComment("伝票区分CD")
                .HasColumnName("SLIP_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.SlipName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("伝票区分名")
                .HasColumnName("SLIP_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivStockLocation>(entity =>
        {
            entity.HasKey(e => new { e.DivStockWhCode, e.DivStockLocationCode }).HasName("DIV_STOCK_LOCATION_PKC");

            entity.ToTable("DIV_STOCK_LOCATION", tb => tb.HasComment("在庫置場区分★"));

            entity.Property(e => e.DivStockWhCode)
                .HasMaxLength(3)
                .HasComment("在庫置場倉庫CD")
                .HasColumnName("DIV_STOCK_WH_CODE");
            entity.Property(e => e.DivStockLocationCode)
                .HasMaxLength(10)
                .HasComment("在庫置場CD")
                .HasColumnName("DIV_STOCK_LOCATION_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("((0))")
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.DivStockLocationName)
                .HasMaxLength(50)
                .HasComment("在庫置場名")
                .HasColumnName("DIV_STOCK_LOCATION_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivTaxRate>(entity =>
        {
            entity.HasKey(e => e.TaxRateCd).HasName("PK__DIV_TAX___2EA4B1F51F4BDDA0");

            entity.ToTable("DIV_TAX_RATE", tb => tb.HasComment("税率区分"));

            entity.Property(e => e.TaxRateCd)
                .HasMaxLength(1)
                .HasComment("税率区分CD")
                .HasColumnName("TAX_RATE_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.TaxRate)
                .HasComment("税率")
                .HasColumnName("TAX_RATE");
            entity.Property(e => e.TaxRateName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("税率区分名")
                .HasColumnName("TAX_RATE_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<DivUriage>(entity =>
        {
            entity.HasKey(e => e.UriageCd).HasName("PK__DIV_URIA__5442495A88276F3B");

            entity.ToTable("DIV_URIAGE", tb => tb.HasComment("売上区分"));

            entity.Property(e => e.UriageCd)
                .HasMaxLength(5)
                .HasComment("売上区分CD")
                .HasColumnName("URIAGE_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
            entity.Property(e => e.UriageName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("売上区分名")
                .HasColumnName("URIAGE_NAME");
        });

        modelBuilder.Entity<DivUserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleCd).HasName("PK__DIV_USER__A50CEE6840F2EAD4");

            entity.ToTable("DIV_USER_ROLE", tb => tb.HasComment("役割区分"));

            entity.Property(e => e.UserRoleCd)
                .HasMaxLength(2)
                .HasComment("役割CD")
                .HasColumnName("USER_ROLE_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator).HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
            entity.Property(e => e.UserRoleName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("役割区分名")
                .HasColumnName("USER_ROLE_NAME");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).IsClustered(false);

            entity.ToTable("EMPLOYEE", tb => tb.HasComment("社員マスタ"));

            entity.Property(e => e.EmpId).HasColumnName("EMP_ID");
            entity.Property(e => e.ApprovalCode)
                .HasMaxLength(2)
                .HasComment("承認権限コード")
                .HasColumnName("APPROVAL_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Deleted).HasColumnName("DELETED");
            entity.Property(e => e.DeptCode)
                .HasMaxLength(6)
                .HasComment("部門コード")
                .HasColumnName("DEPT_CODE");
            entity.Property(e => e.Email)
                .HasMaxLength(320)
                .HasComment("メールアドレス")
                .HasColumnName("EMAIL");
            entity.Property(e => e.EmpCode)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("社員コード")
                .HasColumnName("EMP_CODE");
            entity.Property(e => e.EmpKana)
                .HasMaxLength(40)
                .HasComment("社員名カナ")
                .HasColumnName("EMP_KANA");
            entity.Property(e => e.EmpName)
                .HasMaxLength(20)
                .HasComment("社員名")
                .HasColumnName("EMP_NAME");
            entity.Property(e => e.EmpTag)
                .HasMaxLength(40)
                .HasColumnName("EMP_TAG");
            entity.Property(e => e.Fax)
                .HasMaxLength(13)
                .HasComment("FAX番号")
                .HasColumnName("FAX");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(8)
                .HasComment("パスワード (不使用)")
                .HasColumnName("LOGIN_PASSWORD");
            entity.Property(e => e.OccuCode)
                .HasMaxLength(20)
                .HasComment("職種コード")
                .HasColumnName("OCCU_CODE");
            entity.Property(e => e.StartDate)
                .HasComment("開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Tel)
                .HasMaxLength(13)
                .HasComment("電話番号")
                .HasColumnName("TEL");
            entity.Property(e => e.TitleCode)
                .HasMaxLength(20)
                .HasComment("役職コード")
                .HasColumnName("TITLE_CODE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<FosJyuchuD>(entity =>
        {
            entity.HasKey(e => new { e.SaveKey, e.DenSort, e.DenNoLine }).HasName("PK__FOS_JYUC__859A8D455D0F14C3");

            entity.ToTable("FOS_JYUCHU_D", tb => tb.HasComment("受注詳細"));

            entity.Property(e => e.SaveKey)
                .HasMaxLength(24)
                .HasComment("《画面対応なし》")
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.DenSort)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.DenNoLine)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("DEN_NO_LINE");
            entity.Property(e => e.AddDetailFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("ADD_DETAIL_FLG");
            entity.Property(e => e.Bara)
                .HasComment("バラ数")
                .HasColumnName("BARA");
            entity.Property(e => e.Chuban)
                .HasMaxLength(15)
                .HasComment("《画面対応なし》")
                .HasColumnName("CHUBAN");
            entity.Property(e => e.Code5)
                .HasMaxLength(5)
                .HasComment("《画面対応なし》")
                .HasColumnName("CODE5");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("《GLASS.受注データ.作成日時》")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("《GLASS.受注データ.作成者》")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DelFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("DEL_FLG");
            entity.Property(e => e.DenNo)
                .HasMaxLength(6)
                .HasComment("《画面対応なし》")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.Dencd)
                .HasComment("《画面対応なし》")
                .HasColumnName("DENCD");
            entity.Property(e => e.Dseq)
                .HasMaxLength(6)
                .HasComment("《画面対応なし》")
                .HasColumnName("DSEQ");
            entity.Property(e => e.EcoFlg)
                .HasMaxLength(2)
                .HasComment("《画面対応なし》")
                .HasColumnName("ECO_FLG");
            entity.Property(e => e.EstCoNo)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("EST_CO_NO");
            entity.Property(e => e.EstimateNo)
                .HasMaxLength(11)
                .HasComment("《画面対応なし》")
                .HasColumnName("ESTIMATE_NO");
            entity.Property(e => e.GCompleteFlg)
                .HasComment("《GLASS.受注データ.完了フラグ》")
                .HasColumnName("G_COMPLETE_FLG");
            entity.Property(e => e.GDeliveredQty)
                .HasComment("《GLASS.受注データ.出荷済数量》")
                .HasColumnName("G_DELIVERED_QTY");
            entity.Property(e => e.GDeliveryOrderQty)
                .HasComment("《GLASS.受注データ.出荷指示数量》")
                .HasColumnName("G_DELIVERY_ORDER_QTY");
            entity.Property(e => e.GDiscount)
                .HasComment("《GLASS.受注データ.値引金額》")
                .HasColumnName("G_DISCOUNT");
            entity.Property(e => e.GOrderNo)
                .HasMaxLength(14)
                .HasComment("《GLASS.受注データ.受注番号》")
                .HasColumnName("G_ORDER_NO");
            entity.Property(e => e.GReserveQty)
                .HasComment("《GLASS.受注データ.引当数量》")
                .HasColumnName("G_RESERVE_QTY");
            entity.Property(e => e.Gauto)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("GAUTO");
            entity.Property(e => e.Hinban)
                .HasMaxLength(50)
                .HasComment("《画面対応なし》")
                .HasColumnName("HINBAN");
            entity.Property(e => e.HopeMeisaiNo)
                .HasMaxLength(4)
                .HasComment("《画面対応なし》")
                .HasColumnName("HOPE_MEISAI_NO");
            entity.Property(e => e.HopeOrderNo)
                .HasMaxLength(14)
                .HasComment("《画面対応なし》")
                .HasColumnName("HOPE_ORDER_NO");
            entity.Property(e => e.InpDate)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("INP_DATE");
            entity.Property(e => e.Kikaku)
                .HasMaxLength(20)
                .HasComment("《画面対応なし》")
                .HasColumnName("KIKAKU");
            entity.Property(e => e.Koban)
                .HasComment("子番")
                .HasColumnName("KOBAN");
            entity.Property(e => e.Lbiko)
                .HasMaxLength(20)
                .HasComment("行備考")
                .HasColumnName("LBIKO");
            entity.Property(e => e.LineNo)
                .HasComment("《画面対応なし》")
                .HasColumnName("LINE_NO");
            entity.Property(e => e.Locat)
                .HasMaxLength(4)
                .HasComment("《画面対応なし》")
                .HasColumnName("LOCAT");
            entity.Property(e => e.MoveFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("MOVE_FLG");
            entity.Property(e => e.Nouki)
                .HasComment("納日")
                .HasColumnType("date")
                .HasColumnName("NOUKI");
            entity.Property(e => e.OpsBara)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_BARA");
            entity.Property(e => e.OpsKikaku)
                .HasMaxLength(20)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_KIKAKU");
            entity.Property(e => e.OpsKonpo)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_KONPO");
            entity.Property(e => e.OpsLineno)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_LINENO");
            entity.Property(e => e.OpsNyukabi)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("OPS_NYUKABI");
            entity.Property(e => e.OpsOrderNo)
                .HasMaxLength(6)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_ORDER_NO");
            entity.Property(e => e.OpsRecYmd)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("OPS_REC_YMD");
            entity.Property(e => e.OpsShukkadt)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("OPS_SHUKKADT");
            entity.Property(e => e.OpsSokocd)
                .HasMaxLength(2)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_SOKOCD");
            entity.Property(e => e.OpsSyohinCd)
                .HasMaxLength(60)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_SYOHIN_CD");
            entity.Property(e => e.OpsTani)
                .HasMaxLength(10)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_TANI");
            entity.Property(e => e.OpsUauto)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_UAUTO");
            entity.Property(e => e.OpsUkigo)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_UKIGO");
            entity.Property(e => e.OpsUritu)
                .HasComment("《画面対応なし》")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("OPS_URITU");
            entity.Property(e => e.OpsUtanka)
                .HasComment("《画面対応なし》")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("OPS_UTANKA");
            entity.Property(e => e.OrderDenLineNo)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_DEN_LINE_NO");
            entity.Property(e => e.OrderDenNo)
                .HasMaxLength(14)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_DEN_NO");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(12)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_NO");
            entity.Property(e => e.OrderNoLine)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_NO_LINE");
            entity.Property(e => e.OrderState)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_STATE");
            entity.Property(e => e.OyahinFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("OYAHIN_FLG");
            entity.Property(e => e.Oyahinb)
                .HasMaxLength(24)
                .HasComment("《画面対応なし》")
                .HasColumnName("OYAHINB");
            entity.Property(e => e.ReqNouki)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("REQ_NOUKI");
            entity.Property(e => e.Sbiko)
                .HasMaxLength(64)
                .HasComment("《画面対応なし》")
                .HasColumnName("SBIKO");
            entity.Property(e => e.ShiresakiCd)
                .HasMaxLength(6)
                .HasComment("仕入")
                .HasColumnName("SHIRESAKI_CD");
            entity.Property(e => e.SiiAnswTan)
                .HasComment("回答単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SII_ANSW_TAN");
            entity.Property(e => e.SiiKake)
                .HasComment("掛率(仕入)")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("SII_KAKE");
            entity.Property(e => e.SiiKigou)
                .HasMaxLength(1)
                .HasComment("仕入記号")
                .HasColumnName("SII_KIGOU");
            entity.Property(e => e.SiiKin)
                .HasComment("《画面対応なし》")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SII_KIN");
            entity.Property(e => e.SiiTan)
                .HasComment("仕入単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SII_TAN");
            entity.Property(e => e.SokoCd)
                .HasMaxLength(2)
                .HasComment("倉庫")
                .HasColumnName("SOKO_CD");
            entity.Property(e => e.Suryo)
                .HasComment("数量")
                .HasColumnName("SURYO");
            entity.Property(e => e.SyobunCd)
                .HasMaxLength(3)
                .HasComment("分類")
                .HasColumnName("SYOBUN_CD");
            entity.Property(e => e.SyohinCd)
                .HasMaxLength(50)
                .HasComment("商品コード・名称")
                .HasColumnName("SYOHIN_CD");
            entity.Property(e => e.SyohinName)
                .HasMaxLength(100)
                .HasComment("商品コード・名称")
                .HasColumnName("SYOHIN_NAME");
            entity.Property(e => e.Tani)
                .HasMaxLength(10)
                .HasComment("単位")
                .HasColumnName("TANI");
            entity.Property(e => e.TaxFlg)
                .HasMaxLength(1)
                .HasComment("消費税")
                .HasColumnName("TAX_FLG");
            entity.Property(e => e.TeiKake)
                .HasComment("《画面対応なし》")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("TEI_KAKE");
            entity.Property(e => e.TeiKigou)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("TEI_KIGOU");
            entity.Property(e => e.TeiKin)
                .HasComment("《画面対応なし》")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("TEI_KIN");
            entity.Property(e => e.TeiTan)
                .HasComment("定価単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("TEI_TAN");
            entity.Property(e => e.Uauto)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("UAUTO");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("《GLASS.受注データ.更新日時》")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("《GLASS.受注データ.更新者》")
                .HasColumnName("UPDATER");
            entity.Property(e => e.UriKake)
                .HasComment("掛率(売上)")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("URI_KAKE");
            entity.Property(e => e.UriKigou)
                .HasMaxLength(1)
                .HasComment("売上記号")
                .HasColumnName("URI_KIGOU");
            entity.Property(e => e.UriKin)
                .HasComment("《画面対応なし》")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("URI_KIN");
            entity.Property(e => e.UriTan)
                .HasComment("売上単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("URI_TAN");
            entity.Property(e => e.UriType)
                .HasMaxLength(6)
                .HasComment("《画面対応なし》")
                .HasColumnName("URI_TYPE");
            entity.Property(e => e.Urikubn)
                .HasMaxLength(2)
                .HasComment("売区")
                .HasColumnName("URIKUBN");
        });

        modelBuilder.Entity<FosJyuchuH>(entity =>
        {
            entity.HasKey(e => new { e.SaveKey, e.DenSort });

            entity.ToTable("FOS_JYUCHU_H", tb => tb.HasComment("受注ヘッダー"));

            entity.Property(e => e.SaveKey)
                .HasMaxLength(24)
                .HasComment("《画面対応なし》")
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.DenSort)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.AnswerConfirmFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("ANSWER_CONFIRM_FLG");
            entity.Property(e => e.AnswerName)
                .HasMaxLength(20)
                .HasComment("回答者")
                .HasColumnName("ANSWER_NAME");
            entity.Property(e => e.ArrivalDate)
                .HasComment("入荷日")
                .HasColumnType("datetime")
                .HasColumnName("ARRIVAL_DATE");
            entity.Property(e => e.Bincd)
                .HasMaxLength(5)
                .HasComment("扱便(CD)")
                .HasColumnName("BINCD");
            entity.Property(e => e.Binname)
                .HasMaxLength(20)
                .HasComment("扱便(名)")
                .HasColumnName("BINNAME");
            entity.Property(e => e.Bukken)
                .HasMaxLength(128)
                .HasComment("《画面対応なし》")
                .HasColumnName("BUKKEN");
            entity.Property(e => e.BukkenExp)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("BUKKEN_EXP");
            entity.Property(e => e.ConstructionCode)
                .HasMaxLength(20)
                .HasComment("物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("《GLASS.受注データ.作成日時》")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("《GLASS.受注データ.作成者》")
                .HasColumnName("CREATOR");
            entity.Property(e => e.CustOrderno)
                .HasMaxLength(20)
                .HasComment("客注")
                .HasColumnName("CUST_ORDERNO");
            entity.Property(e => e.DelFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("DEL_FLG");
            entity.Property(e => e.DenFlg)
                .HasMaxLength(2)
                .HasComment("伝区")
                .HasColumnName("DEN_FLG");
            entity.Property(e => e.DenNo)
                .HasMaxLength(6)
                .HasComment("伝No")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.DenShippingPrinted)
                .HasDefaultValueSql("((0))")
                .HasComment("《ラベルなし》出荷伝票印刷済")
                .HasColumnName("DEN_SHIPPING_PRINTED");
            entity.Property(e => e.DenState)
                .HasMaxLength(1)
                .HasComment("《ラベルなし》「発注前」")
                .HasColumnName("DEN_STATE");
            entity.Property(e => e.DenValidCheckDate)
                .HasComment("伝票チェック日時")
                .HasColumnType("datetime")
                .HasColumnName("DEN_VALID_CHECK_DATE");
            entity.Property(e => e.DenValidCheckState)
                .HasMaxLength(1)
                .HasDefaultValueSql("('0')")
                .HasComment("伝票チェック")
                .HasColumnName("DEN_VALID_CHECK_STATE");
            entity.Property(e => e.Dseq)
                .HasMaxLength(6)
                .HasComment("内部No.")
                .HasColumnName("DSEQ");
            entity.Property(e => e.DueDate)
                .HasComment("納入日")
                .HasColumnType("datetime")
                .HasColumnName("DUE_DATE");
            entity.Property(e => e.EpukoKanriNo)
                .HasMaxLength(20)
                .HasComment("《画面対応なし》 (エプコ管理No)")
                .HasColumnName("EPUKO_KANRI_NO");
            entity.Property(e => e.EstCoNo)
                .HasMaxLength(3)
                .HasComment("見積番号")
                .HasColumnName("EST_CO_NO");
            entity.Property(e => e.EstimateNo)
                .HasMaxLength(11)
                .HasComment("見積番号")
                .HasColumnName("ESTIMATE_NO");
            entity.Property(e => e.GCmpTax)
                .HasComment("《GLASS.受注データ.消費税金額》")
                .HasColumnName("G_CMP_TAX");
            entity.Property(e => e.GCustCode)
                .HasMaxLength(8)
                .HasComment("《GLASS.受注データ.顧客コード》")
                .HasColumnName("G_CUST_CODE");
            entity.Property(e => e.GCustSubNo)
                .HasMaxLength(8)
                .HasComment("《GLASS.受注データ.顧客枝番》")
                .HasColumnName("G_CUST_SUB_NO");
            entity.Property(e => e.GOrderAmnt)
                .HasComment("《GLASS.受注データ.受注金額合計》")
                .HasColumnName("G_ORDER_AMNT");
            entity.Property(e => e.GOrderDate)
                .HasComment("《GLASS.受注データ.受注日》")
                .HasColumnType("datetime")
                .HasColumnName("G_ORDER_DATE");
            entity.Property(e => e.GOrderNo)
                .HasMaxLength(14)
                .HasComment("《GLASS.受注データ.受注番号》")
                .HasColumnName("G_ORDER_NO");
            entity.Property(e => e.GStartDate)
                .HasComment("《GLASS.受注データ.部門開始日》")
                .HasColumnType("datetime")
                .HasColumnName("G_START_DATE");
            entity.Property(e => e.GenbaCd)
                .HasMaxLength(3)
                .HasComment("現場")
                .HasColumnName("GENBA_CD");
            entity.Property(e => e.HatNyukabi)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("HAT_NYUKABI");
            entity.Property(e => e.HatOrderNo)
                .HasMaxLength(10)
                .HasComment("H注番 (HAT-F注文番号)")
                .HasColumnName("HAT_ORDER_NO");
            entity.Property(e => e.Hkbn)
                .HasMaxLength(1)
                .HasComment("発注")
                .HasColumnName("HKBN");
            entity.Property(e => e.InpDate)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("INP_DATE");
            entity.Property(e => e.IpAdd)
                .HasMaxLength(16)
                .HasComment("《画面対応なし》")
                .HasColumnName("IP_ADD");
            entity.Property(e => e.Jyu2)
                .HasMaxLength(2)
                .HasComment("受発注者")
                .HasColumnName("JYU2");
            entity.Property(e => e.Jyu2Cd)
                .HasMaxLength(4)
                .HasComment("《画面対応なし》 (受発注者/社員番号)")
                .HasColumnName("JYU2_CD");
            entity.Property(e => e.Jyu2Id)
                .HasComment("《画面対応なし》 (受発注者/社員ID)")
                .HasColumnName("JYU2_ID");
            entity.Property(e => e.Kessai)
                .HasMaxLength(1)
                .HasComment("決済")
                .HasColumnName("KESSAI");
            entity.Property(e => e.KmanCd)
                .HasMaxLength(2)
                .HasComment("担(CD)(キーマン)")
                .HasColumnName("KMAN_CD");
            entity.Property(e => e.KmanName)
                .HasMaxLength(20)
                .HasComment("担(名)(キーマン)")
                .HasColumnName("KMAN_NAME");
            entity.Property(e => e.KoujitenCd)
                .HasMaxLength(10)
                .HasComment("工事店(CD)")
                .HasColumnName("KOUJITEN_CD");
            entity.Property(e => e.KoujitenName)
                .HasMaxLength(60)
                .HasComment("工事店(名)")
                .HasColumnName("KOUJITEN_NAME");
            entity.Property(e => e.MakerDenNo)
                .HasMaxLength(14)
                .HasComment("《画面対応なし》")
                .HasColumnName("MAKER_DEN_NO");
            entity.Property(e => e.Nohin)
                .HasMaxLength(1)
                .HasComment("区分")
                .HasColumnName("NOHIN");
            entity.Property(e => e.NoteHouse)
                .HasMaxLength(15)
                .HasComment("社内備考")
                .HasColumnName("NOTE_HOUSE");
            entity.Property(e => e.Nouki)
                .HasComment("納日")
                .HasColumnType("date")
                .HasColumnName("NOUKI");
            entity.Property(e => e.Nyu2)
                .HasMaxLength(2)
                .HasComment("入力者")
                .HasColumnName("NYU2");
            entity.Property(e => e.Nyu2Cd)
                .HasMaxLength(4)
                .HasComment("《画面対応なし》 (入力者/社員番号)")
                .HasColumnName("NYU2_CD");
            entity.Property(e => e.Nyu2Id)
                .HasComment("《画面対応なし》 (入力者/社員ID)")
                .HasColumnName("NYU2_ID");
            entity.Property(e => e.OkuriFlag)
                .HasMaxLength(1)
                .HasComment("送元")
                .HasColumnName("OKURI_FLAG");
            entity.Property(e => e.OpsBin)
                .HasMaxLength(5)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_BIN");
            entity.Property(e => e.OpsHachuAdr)
                .HasMaxLength(100)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_HACHU_ADR");
            entity.Property(e => e.OpsHachuName)
                .HasMaxLength(20)
                .HasComment("《画面対応なし》")
                .HasColumnName("OPS_HACHU_NAME");
            entity.Property(e => e.OpsOrderNo)
                .HasMaxLength(6)
                .HasComment("OPSNo.")
                .HasColumnName("OPS_ORDER_NO");
            entity.Property(e => e.OpsRecYmd)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("OPS_REC_YMD");
            entity.Property(e => e.OrderDenNo)
                .HasMaxLength(14)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_DEN_NO");
            entity.Property(e => e.OrderFlag)
                .HasMaxLength(1)
                .HasComment("受区")
                .HasColumnName("ORDER_FLAG");
            entity.Property(e => e.OrderMemo1)
                .HasMaxLength(90)
                .HasComment("発注時メモ")
                .HasColumnName("ORDER_MEMO1");
            entity.Property(e => e.OrderMemo2)
                .HasMaxLength(30)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_MEMO2");
            entity.Property(e => e.OrderMemo3)
                .HasMaxLength(30)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_MEMO3");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(12)
                .HasComment("《画面対応なし》")
                .HasColumnName("ORDER_NO");
            entity.Property(e => e.OrderState)
                .HasMaxLength(1)
                .HasComment("《ラベルなし》「発注前」")
                .HasColumnName("ORDER_STATE");
            entity.Property(e => e.Raikan)
                .HasMaxLength(1)
                .HasComment("来勘")
                .HasColumnName("RAIKAN");
            entity.Property(e => e.RecYmd)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("REC_YMD");
            entity.Property(e => e.RecvAdd1)
                .HasMaxLength(30)
                .HasComment("住所1")
                .HasColumnName("RECV_ADD1");
            entity.Property(e => e.RecvAdd2)
                .HasMaxLength(30)
                .HasComment("住所2")
                .HasColumnName("RECV_ADD2");
            entity.Property(e => e.RecvAdd3)
                .HasMaxLength(30)
                .HasComment("住所3")
                .HasColumnName("RECV_ADD3");
            entity.Property(e => e.RecvGenbaCd)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("RECV_GENBA_CD");
            entity.Property(e => e.RecvName1)
                .HasMaxLength(30)
                .HasComment("宛先1")
                .HasColumnName("RECV_NAME1");
            entity.Property(e => e.RecvName2)
                .HasMaxLength(20)
                .HasComment("宛先2")
                .HasColumnName("RECV_NAME2");
            entity.Property(e => e.RecvPostcode)
                .HasMaxLength(8)
                .HasComment("〒")
                .HasColumnName("RECV_POSTCODE");
            entity.Property(e => e.RecvTel)
                .HasMaxLength(15)
                .HasComment("TEL")
                .HasColumnName("RECV_TEL");
            entity.Property(e => e.ReqBiko)
                .HasMaxLength(200)
                .HasComment("《画面対応なし》")
                .HasColumnName("REQ_BIKO");
            entity.Property(e => e.Sale1Flag)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》")
                .HasColumnName("SALE1_FLAG");
            entity.Property(e => e.SansyoDseq)
                .HasMaxLength(6)
                .HasComment("《画面対応なし》")
                .HasColumnName("SANSYO_DSEQ");
            entity.Property(e => e.Sfax)
                .HasMaxLength(15)
                .HasComment("FAX")
                .HasColumnName("SFAX");
            entity.Property(e => e.ShippedDate)
                .HasComment("出荷日")
                .HasColumnType("datetime")
                .HasColumnName("SHIPPED_DATE");
            entity.Property(e => e.ShireKa)
                .HasMaxLength(3)
                .HasComment("《画面対応なし》")
                .HasColumnName("SHIRE_KA");
            entity.Property(e => e.ShiresakiCd)
                .HasMaxLength(6)
                .HasComment("仕入(CD)")
                .HasColumnName("SHIRESAKI_CD");
            entity.Property(e => e.ShiresakiName)
                .HasMaxLength(60)
                .HasComment("仕入(名)")
                .HasColumnName("SHIRESAKI_NAME");
            entity.Property(e => e.Sirainm)
                .HasMaxLength(20)
                .HasComment("依頼")
                .HasColumnName("SIRAINM");
            entity.Property(e => e.SmailAdd)
                .HasMaxLength(50)
                .HasComment("《画面対応なし》")
                .HasColumnName("SMAIL_ADD");
            entity.Property(e => e.SokoCd)
                .HasMaxLength(2)
                .HasComment("倉庫(CD)")
                .HasColumnName("SOKO_CD");
            entity.Property(e => e.SokoName)
                .HasMaxLength(30)
                .HasComment("倉庫(名)")
                .HasColumnName("SOKO_NAME");
            entity.Property(e => e.SupplierType)
                .HasDefaultValueSql("((0))")
                .HasComment("発注先種別,null/0:未設定 1:橋本本体 2:橋本本体以外")
                .HasColumnName("SUPPLIER_TYPE");
            entity.Property(e => e.TantoCd)
                .HasMaxLength(4)
                .HasComment("《画面対応なし》担当(CD)")
                .HasColumnName("TANTO_CD");
            entity.Property(e => e.TantoName)
                .HasMaxLength(20)
                .HasComment("《画面対応なし》担当(名)")
                .HasColumnName("TANTO_NAME");
            entity.Property(e => e.TeamCd)
                .HasMaxLength(3)
                .HasComment("販課")
                .HasColumnName("TEAM_CD");
            entity.Property(e => e.TelRenrakuFlg)
                .HasMaxLength(1)
                .HasComment("電話連絡済")
                .HasColumnName("TEL_RENRAKU_FLG");
            entity.Property(e => e.TokuiCd)
                .HasMaxLength(6)
                .HasComment("得意先(CD)")
                .HasColumnName("TOKUI_CD");
            entity.Property(e => e.TokuiName)
                .HasMaxLength(60)
                .HasComment("得意先(名)")
                .HasColumnName("TOKUI_NAME");
            entity.Property(e => e.UkeshoFlg)
                .HasMaxLength(1)
                .HasComment("《画面対応なし》 (請書取込フラグ)")
                .HasColumnName("UKESHO_FLG");
            entity.Property(e => e.Unchin)
                .HasMaxLength(1)
                .HasComment("運賃")
                .HasColumnName("UNCHIN");
            entity.Property(e => e.UpdDate)
                .HasComment("《画面対応なし》")
                .HasColumnType("date")
                .HasColumnName("UPD_DATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("《GLASS.受注データ.更新日時》")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("《GLASS.受注データ.更新者》")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WhStatus)
                .HasMaxLength(1)
                .HasDefaultValueSql("('0')")
                .HasComment("倉庫ステータス")
                .HasColumnName("WH_STATUS");
        });

        modelBuilder.Entity<HatOrderNoSequence>(entity =>
        {
            entity.HasKey(e => e.Key).IsClustered(false);

            entity.ToTable("HAT_ORDER_NO_SEQUENCE", tb => tb.HasComment("H注番の連番管理"));

            entity.Property(e => e.Key)
                .HasMaxLength(50)
                .HasComment("H注番のキー")
                .HasColumnName("KEY");
            entity.Property(e => e.Number)
                .HasComment("連番(最大999)")
                .HasColumnName("NUMBER");
        });

        modelBuilder.Entity<ImpFosJyuchuD>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_FOS_JYUCHU_D");

            entity.Property(e => e.AddDetailFlg)
                .HasMaxLength(500)
                .HasColumnName("ADD_DETAIL_FLG");
            entity.Property(e => e.Bara)
                .HasMaxLength(500)
                .HasColumnName("BARA");
            entity.Property(e => e.Chuban)
                .HasMaxLength(500)
                .HasColumnName("CHUBAN");
            entity.Property(e => e.Code5)
                .HasMaxLength(500)
                .HasColumnName("CODE5");
            entity.Property(e => e.DelFlg)
                .HasMaxLength(500)
                .HasColumnName("DEL_FLG");
            entity.Property(e => e.DenNo)
                .HasMaxLength(500)
                .HasColumnName("DEN_NO");
            entity.Property(e => e.DenNoLine)
                .HasMaxLength(500)
                .HasColumnName("DEN_NO_LINE");
            entity.Property(e => e.DenSort)
                .HasMaxLength(500)
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.Dencd)
                .HasMaxLength(500)
                .HasColumnName("DENCD");
            entity.Property(e => e.Dseq)
                .HasMaxLength(500)
                .HasColumnName("DSEQ");
            entity.Property(e => e.EcoFlg)
                .HasMaxLength(500)
                .HasColumnName("ECO_FLG");
            entity.Property(e => e.EstCoNo)
                .HasMaxLength(500)
                .HasColumnName("EST_CO_NO");
            entity.Property(e => e.EstimateNo)
                .HasMaxLength(500)
                .HasColumnName("ESTIMATE_NO");
            entity.Property(e => e.Gauto)
                .HasMaxLength(500)
                .HasColumnName("GAUTO");
            entity.Property(e => e.Hinban)
                .HasMaxLength(500)
                .HasColumnName("HINBAN");
            entity.Property(e => e.HopeMeisaiNo)
                .HasMaxLength(500)
                .HasColumnName("HOPE_MEISAI_NO");
            entity.Property(e => e.HopeOrderNo)
                .HasMaxLength(500)
                .HasColumnName("HOPE_ORDER_NO");
            entity.Property(e => e.InpDate)
                .HasMaxLength(500)
                .HasColumnName("INP_DATE");
            entity.Property(e => e.Kikaku)
                .HasMaxLength(500)
                .HasColumnName("KIKAKU");
            entity.Property(e => e.Lbiko)
                .HasMaxLength(500)
                .HasColumnName("LBIKO");
            entity.Property(e => e.LineNo)
                .HasMaxLength(500)
                .HasColumnName("LINE_NO");
            entity.Property(e => e.Locat)
                .HasMaxLength(500)
                .HasColumnName("LOCAT");
            entity.Property(e => e.MoveFlg)
                .HasMaxLength(500)
                .HasColumnName("MOVE_FLG");
            entity.Property(e => e.Nouki)
                .HasMaxLength(500)
                .HasColumnName("NOUKI");
            entity.Property(e => e.OpsBara)
                .HasMaxLength(500)
                .HasColumnName("OPS_BARA");
            entity.Property(e => e.OpsKikaku)
                .HasMaxLength(500)
                .HasColumnName("OPS_KIKAKU");
            entity.Property(e => e.OpsKonpo)
                .HasMaxLength(500)
                .HasColumnName("OPS_KONPO");
            entity.Property(e => e.OpsLineno)
                .HasMaxLength(500)
                .HasColumnName("OPS_LINENO");
            entity.Property(e => e.OpsNyukabi)
                .HasMaxLength(500)
                .HasColumnName("OPS_NYUKABI");
            entity.Property(e => e.OpsOrderNo)
                .HasMaxLength(500)
                .HasColumnName("OPS_ORDER_NO");
            entity.Property(e => e.OpsRecYmd)
                .HasMaxLength(500)
                .HasColumnName("OPS_REC_YMD");
            entity.Property(e => e.OpsShukkadt)
                .HasMaxLength(500)
                .HasColumnName("OPS_SHUKKADT");
            entity.Property(e => e.OpsSokocd)
                .HasMaxLength(500)
                .HasColumnName("OPS_SOKOCD");
            entity.Property(e => e.OpsSyohinCd)
                .HasMaxLength(500)
                .HasColumnName("OPS_SYOHIN_CD");
            entity.Property(e => e.OpsSyohinName)
                .HasMaxLength(500)
                .HasColumnName("OPS_SYOHIN_NAME");
            entity.Property(e => e.OpsTani)
                .HasMaxLength(500)
                .HasColumnName("OPS_TANI");
            entity.Property(e => e.OpsUauto)
                .HasMaxLength(500)
                .HasColumnName("OPS_UAUTO");
            entity.Property(e => e.OpsUkigo)
                .HasMaxLength(500)
                .HasColumnName("OPS_UKIGO");
            entity.Property(e => e.OpsUritu)
                .HasMaxLength(500)
                .HasColumnName("OPS_URITU");
            entity.Property(e => e.OpsUtanka)
                .HasMaxLength(500)
                .HasColumnName("OPS_UTANKA");
            entity.Property(e => e.OrderDenLineNo)
                .HasMaxLength(500)
                .HasColumnName("ORDER_DEN_LINE_NO");
            entity.Property(e => e.OrderDenNo)
                .HasMaxLength(500)
                .HasColumnName("ORDER_DEN_NO");
            entity.Property(e => e.OrderNo)
                .HasMaxLength(500)
                .HasColumnName("ORDER_NO");
            entity.Property(e => e.OrderNoLine)
                .HasMaxLength(500)
                .HasColumnName("ORDER_NO_LINE");
            entity.Property(e => e.OrderState)
                .HasMaxLength(500)
                .HasColumnName("ORDER_STATE");
            entity.Property(e => e.OyahinFlg)
                .HasMaxLength(500)
                .HasColumnName("OYAHIN_FLG");
            entity.Property(e => e.Oyahinb)
                .HasMaxLength(500)
                .HasColumnName("OYAHINB");
            entity.Property(e => e.ReqNouki)
                .HasMaxLength(500)
                .HasColumnName("REQ_NOUKI");
            entity.Property(e => e.SaveKey)
                .HasMaxLength(500)
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.Sbiko)
                .HasMaxLength(500)
                .HasColumnName("SBIKO");
            entity.Property(e => e.ShiresakiCd)
                .HasMaxLength(500)
                .HasColumnName("SHIRESAKI_CD");
            entity.Property(e => e.SiiAnswTan)
                .HasMaxLength(500)
                .HasColumnName("SII_ANSW_TAN");
            entity.Property(e => e.SiiKake)
                .HasMaxLength(500)
                .HasColumnName("SII_KAKE");
            entity.Property(e => e.SiiKigou)
                .HasMaxLength(500)
                .HasColumnName("SII_KIGOU");
            entity.Property(e => e.SiiKin)
                .HasMaxLength(500)
                .HasColumnName("SII_KIN");
            entity.Property(e => e.SiiTan)
                .HasMaxLength(500)
                .HasColumnName("SII_TAN");
            entity.Property(e => e.SokoCd)
                .HasMaxLength(500)
                .HasColumnName("SOKO_CD");
            entity.Property(e => e.Suryo)
                .HasMaxLength(500)
                .HasColumnName("SURYO");
            entity.Property(e => e.SyobunCd)
                .HasMaxLength(500)
                .HasColumnName("SYOBUN_CD");
            entity.Property(e => e.SyohinCd)
                .HasMaxLength(500)
                .HasColumnName("SYOHIN_CD");
            entity.Property(e => e.SyohinName)
                .HasMaxLength(500)
                .HasColumnName("SYOHIN_NAME");
            entity.Property(e => e.Tani)
                .HasMaxLength(500)
                .HasColumnName("TANI");
            entity.Property(e => e.TaxFlg)
                .HasMaxLength(500)
                .HasColumnName("TAX_FLG");
            entity.Property(e => e.TeiKake)
                .HasMaxLength(500)
                .HasColumnName("TEI_KAKE");
            entity.Property(e => e.TeiKigou)
                .HasMaxLength(500)
                .HasColumnName("TEI_KIGOU");
            entity.Property(e => e.TeiKin)
                .HasMaxLength(500)
                .HasColumnName("TEI_KIN");
            entity.Property(e => e.TeiTan)
                .HasMaxLength(500)
                .HasColumnName("TEI_TAN");
            entity.Property(e => e.Uauto)
                .HasMaxLength(500)
                .HasColumnName("UAUTO");
            entity.Property(e => e.UriKake)
                .HasMaxLength(500)
                .HasColumnName("URI_KAKE");
            entity.Property(e => e.UriKigou)
                .HasMaxLength(500)
                .HasColumnName("URI_KIGOU");
            entity.Property(e => e.UriKin)
                .HasMaxLength(500)
                .HasColumnName("URI_KIN");
            entity.Property(e => e.UriTan)
                .HasMaxLength(500)
                .HasColumnName("URI_TAN");
            entity.Property(e => e.UriType)
                .HasMaxLength(500)
                .HasColumnName("URI_TYPE");
            entity.Property(e => e.Urikubn)
                .HasMaxLength(500)
                .HasColumnName("URIKUBN");
        });

        modelBuilder.Entity<ImpKeymanm2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_KEYMANM_2");

            entity.Property(e => e.Adrs)
                .HasMaxLength(500)
                .HasColumnName("ADRS");
            entity.Property(e => e.AtukaiKingaku)
                .HasMaxLength(500)
                .HasColumnName("ATUKAI_KINGAKU");
            entity.Property(e => e.Biko)
                .HasMaxLength(500)
                .HasColumnName("BIKO");
            entity.Property(e => e.Birthday)
                .HasMaxLength(500)
                .HasColumnName("BIRTHDAY");
            entity.Property(e => e.ChumonMailFlg)
                .HasMaxLength(500)
                .HasColumnName("CHUMON_MAIL_FLG");
            entity.Property(e => e.Fax)
                .HasMaxLength(500)
                .HasColumnName("FAX");
            entity.Property(e => e.Inpdt)
                .HasMaxLength(500)
                .HasColumnName("INPDT");
            entity.Property(e => e.KeitaiMail)
                .HasMaxLength(500)
                .HasColumnName("KEITAI_MAIL");
            entity.Property(e => e.KeitaiTel)
                .HasMaxLength(500)
                .HasColumnName("KEITAI_TEL");
            entity.Property(e => e.KeymanEntryMailFlg)
                .HasMaxLength(500)
                .HasColumnName("KEYMAN_ENTRY_MAIL_FLG");
            entity.Property(e => e.KmanKana)
                .HasMaxLength(500)
                .HasColumnName("KMAN_KANA");
            entity.Property(e => e.Kmancd)
                .HasMaxLength(500)
                .HasColumnName("KMANCD");
            entity.Property(e => e.Kmanid)
                .HasMaxLength(500)
                .HasColumnName("KMANID");
            entity.Property(e => e.Kmannm1)
                .HasMaxLength(500)
                .HasColumnName("KMANNM1");
            entity.Property(e => e.Kmannm2)
                .HasMaxLength(500)
                .HasColumnName("KMANNM2");
            entity.Property(e => e.LoginId)
                .HasMaxLength(500)
                .HasColumnName("LOGIN_ID");
            entity.Property(e => e.Mail)
                .HasMaxLength(500)
                .HasColumnName("MAIL");
            entity.Property(e => e.ManabiyaFlg)
                .HasMaxLength(500)
                .HasColumnName("MANABIYA_FLG");
            entity.Property(e => e.MitsumoriMailFlg)
                .HasMaxLength(500)
                .HasColumnName("MITSUMORI_MAIL_FLG");
            entity.Property(e => e.MtbFlg)
                .HasMaxLength(500)
                .HasColumnName("MTB_FLG");
            entity.Property(e => e.OpsBiko)
                .HasMaxLength(500)
                .HasColumnName("OPS_BIKO");
            entity.Property(e => e.OpsFlg)
                .HasMaxLength(500)
                .HasColumnName("OPS_FLG");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.PointOrderMailFlg)
                .HasMaxLength(500)
                .HasColumnName("POINT_ORDER_MAIL_FLG");
            entity.Property(e => e.Postcd)
                .HasMaxLength(500)
                .HasColumnName("POSTCD");
            entity.Property(e => e.SalesInfoMailFlg)
                .HasMaxLength(500)
                .HasColumnName("SALES_INFO_MAIL_FLG");
            entity.Property(e => e.Seibetsu)
                .HasMaxLength(500)
                .HasColumnName("SEIBETSU");
            entity.Property(e => e.SharepostMailFlg)
                .HasMaxLength(500)
                .HasColumnName("SHAREPOST_MAIL_FLG");
            entity.Property(e => e.Syozoku)
                .HasMaxLength(500)
                .HasColumnName("SYOZOKU");
            entity.Property(e => e.Syumi)
                .HasMaxLength(500)
                .HasColumnName("SYUMI");
            entity.Property(e => e.Tantogyomu)
                .HasMaxLength(500)
                .HasColumnName("TANTOGYOMU");
            entity.Property(e => e.Tel)
                .HasMaxLength(500)
                .HasColumnName("TEL");
            entity.Property(e => e.Tokucd)
                .HasMaxLength(500)
                .HasColumnName("TOKUCD");
            entity.Property(e => e.Tokucho)
                .HasMaxLength(500)
                .HasColumnName("TOKUCHO");
            entity.Property(e => e.UkeshoMailFlg)
                .HasMaxLength(500)
                .HasColumnName("UKESHO_MAIL_FLG");
            entity.Property(e => e.Updt)
                .HasMaxLength(500)
                .HasColumnName("UPDT");
            entity.Property(e => e.WebMiraiFlg)
                .HasMaxLength(500)
                .HasColumnName("WEB_MIRAI_FLG");
            entity.Property(e => e.WebMiraiTeam)
                .HasMaxLength(500)
                .HasColumnName("WEB_MIRAI_TEAM");
            entity.Property(e => e.Yakusyoku)
                .HasMaxLength(500)
                .HasColumnName("YAKUSYOKU");
        });

        modelBuilder.Entity<ImpKojixm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_KOJIXM");

            entity.Property(e => e.Adrs)
                .HasMaxLength(500)
                .HasColumnName("ADRS");
            entity.Property(e => e.Fil1)
                .HasMaxLength(500)
                .HasColumnName("FIL1");
            entity.Property(e => e.KojiAnm)
                .HasMaxLength(500)
                .HasColumnName("KOJI_ANM");
            entity.Property(e => e.KojiNnm)
                .HasMaxLength(500)
                .HasColumnName("KOJI_NNM");
            entity.Property(e => e.Kojicd)
                .HasMaxLength(500)
                .HasColumnName("KOJICD");
            entity.Property(e => e.Postcd)
                .HasMaxLength(500)
                .HasColumnName("POSTCD");
        });

        modelBuilder.Entity<ImpSyobunm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_SYOBUNM");

            entity.Property(e => e.Cd32)
                .HasMaxLength(500)
                .HasColumnName("CD32");
            entity.Property(e => e.Code5)
                .HasMaxLength(500)
                .HasColumnName("CODE5");
            entity.Property(e => e.Inpdt)
                .HasMaxLength(500)
                .HasColumnName("INPDT");
            entity.Property(e => e.Sirecd)
                .HasMaxLength(500)
                .HasColumnName("SIRECD");
            entity.Property(e => e.Syonm1)
                .HasMaxLength(500)
                .HasColumnName("SYONM1");
            entity.Property(e => e.Syonm2)
                .HasMaxLength(500)
                .HasColumnName("SYONM2");
            entity.Property(e => e.Updt)
                .HasMaxLength(500)
                .HasColumnName("UPDT");
        });

        modelBuilder.Entity<ImpTeamm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_TEAMM");

            entity.Property(e => e.Blkcd)
                .HasMaxLength(500)
                .HasColumnName("BLKCD");
            entity.Property(e => e.Bscd)
                .HasMaxLength(500)
                .HasColumnName("BSCD");
            entity.Property(e => e.Bsnm)
                .HasMaxLength(500)
                .HasColumnName("BSNM");
            entity.Property(e => e.Fax)
                .HasMaxLength(500)
                .HasColumnName("FAX");
            entity.Property(e => e.Inpdt)
                .HasMaxLength(500)
                .HasColumnName("INPDT");
            entity.Property(e => e.Shaincd)
                .HasMaxLength(500)
                .HasColumnName("SHAINCD");
            entity.Property(e => e.Teamcd)
                .HasMaxLength(500)
                .HasColumnName("TEAMCD");
            entity.Property(e => e.Teamnm)
                .HasMaxLength(500)
                .HasColumnName("TEAMNM");
            entity.Property(e => e.Tel)
                .HasMaxLength(500)
                .HasColumnName("TEL");
            entity.Property(e => e.Updt)
                .HasMaxLength(500)
                .HasColumnName("UPDT");
        });

        modelBuilder.Entity<Imp取引先マスタ>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_取引先マスタ");

            entity.Property(e => e.AAdrs1K)
                .HasMaxLength(500)
                .HasColumnName("A_ADRS1_K");
            entity.Property(e => e.AAdrs2K)
                .HasMaxLength(500)
                .HasColumnName("A_ADRS2_K");
            entity.Property(e => e.AAdrs3K)
                .HasMaxLength(500)
                .HasColumnName("A_ADRS3_K");
            entity.Property(e => e.AFax)
                .HasMaxLength(500)
                .HasColumnName("A_FAX");
            entity.Property(e => e.AFax2)
                .HasMaxLength(500)
                .HasColumnName("A_FAX2");
            entity.Property(e => e.AKaisyaK)
                .HasMaxLength(500)
                .HasColumnName("A_KAISYA_K");
            entity.Property(e => e.ATel)
                .HasMaxLength(500)
                .HasColumnName("A_TEL");
            entity.Property(e => e.ATensyoK)
                .HasMaxLength(500)
                .HasColumnName("A_TENSYO_K");
            entity.Property(e => e.Chiikicd)
                .HasMaxLength(500)
                .HasColumnName("CHIIKICD");
            entity.Property(e => e.DDate)
                .HasMaxLength(500)
                .HasColumnName("D_DATE");
            entity.Property(e => e.Fil1)
                .HasMaxLength(500)
                .HasColumnName("FIL1");
            entity.Property(e => e.IDate)
                .HasMaxLength(500)
                .HasColumnName("I_DATE");
            entity.Property(e => e.Postcd)
                .HasMaxLength(500)
                .HasColumnName("POSTCD");
            entity.Property(e => e.Ryaku)
                .HasMaxLength(500)
                .HasColumnName("RYAKU");
            entity.Property(e => e.SAdrs1A)
                .HasMaxLength(500)
                .HasColumnName("S_ADRS1_A");
            entity.Property(e => e.SAdrs2A)
                .HasMaxLength(500)
                .HasColumnName("S_ADRS2_A");
            entity.Property(e => e.SAdrs3A)
                .HasMaxLength(500)
                .HasColumnName("S_ADRS3_A");
            entity.Property(e => e.SKaisyaA)
                .HasMaxLength(500)
                .HasColumnName("S_KAISYA_A");
            entity.Property(e => e.SKaisyaK)
                .HasMaxLength(500)
                .HasColumnName("S_KAISYA_K");
            entity.Property(e => e.SPostcdDm)
                .HasMaxLength(500)
                .HasColumnName("S_POSTCD_DM");
            entity.Property(e => e.STensyoA)
                .HasMaxLength(500)
                .HasColumnName("S_TENSYO_A");
            entity.Property(e => e.STensyoK)
                .HasMaxLength(500)
                .HasColumnName("S_TENSYO_K");
            entity.Property(e => e.St1)
                .HasMaxLength(500)
                .HasColumnName("ST1");
            entity.Property(e => e.St10)
                .HasMaxLength(500)
                .HasColumnName("ST10");
            entity.Property(e => e.St2)
                .HasMaxLength(500)
                .HasColumnName("ST2");
            entity.Property(e => e.St3)
                .HasMaxLength(500)
                .HasColumnName("ST3");
            entity.Property(e => e.St4)
                .HasMaxLength(500)
                .HasColumnName("ST4");
            entity.Property(e => e.St5)
                .HasMaxLength(500)
                .HasColumnName("ST5");
            entity.Property(e => e.St6)
                .HasMaxLength(500)
                .HasColumnName("ST6");
            entity.Property(e => e.St7)
                .HasMaxLength(500)
                .HasColumnName("ST7");
            entity.Property(e => e.St8)
                .HasMaxLength(500)
                .HasColumnName("ST8");
            entity.Property(e => e.St9)
                .HasMaxLength(500)
                .HasColumnName("ST9");
            entity.Property(e => e.Syohin)
                .HasMaxLength(500)
                .HasColumnName("SYOHIN");
            entity.Property(e => e.Torihikicd)
                .HasMaxLength(500)
                .HasColumnName("TORIHIKICD");
        });

        modelBuilder.Entity<Imp現場マスタ>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_現場マスタ");

            entity.Property(e => e.Adrs)
                .HasMaxLength(500)
                .HasColumnName("ADRS");
            entity.Property(e => e.Fax)
                .HasMaxLength(500)
                .HasColumnName("FAX");
            entity.Property(e => e.Genbacd)
                .HasMaxLength(500)
                .HasColumnName("GENBACD");
            entity.Property(e => e.Genbanm)
                .HasMaxLength(500)
                .HasColumnName("GENBANM");
            entity.Property(e => e.Inpdt)
                .HasMaxLength(500)
                .HasColumnName("INPDT");
            entity.Property(e => e.OpsUse)
                .HasMaxLength(500)
                .HasColumnName("OPS_USE");
            entity.Property(e => e.Postcd)
                .HasMaxLength(500)
                .HasColumnName("POSTCD");
            entity.Property(e => e.Tel)
                .HasMaxLength(500)
                .HasColumnName("TEL");
            entity.Property(e => e.Tokucd)
                .HasMaxLength(500)
                .HasColumnName("TOKUCD");
            entity.Property(e => e.Updt)
                .HasMaxLength(500)
                .HasColumnName("UPDT");
            entity.Property(e => e.Usbuk)
                .HasMaxLength(500)
                .HasColumnName("USBUK");
        });

        modelBuilder.Entity<Imp社員マスタ>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_imp_社員マスタ");

            entity.Property(e => e.Fax番号)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("FAX番号");
            entity.Property(e => e.パスワード)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.役職)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.承認権限コード)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.社員id連番自動生成)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("社員ID(連番 自動生成)");
            entity.Property(e => e.社員コード)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.社員名)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.社員名カナ)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.社員指定タグ)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.職種コード雇用形態)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("職種コード(雇用形態)");
            entity.Property(e => e.部門コード)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.開始日)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.電話番号)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<InterestRateCheckBeforeFix>(entity =>
        {
            entity.HasKey(e => e.InterestRateCheckBeforeFixId).HasName("INTEREST_RATE_CHECK_BEFORE_FIX_PKC");

            entity.ToTable("INTEREST_RATE_CHECK_BEFORE_FIX", tb => tb.HasComment("売上確定前利率異常チェック"));

            entity.Property(e => e.InterestRateCheckBeforeFixId)
                .HasComment("利率異常チェックID")
                .HasColumnName("INTEREST_RATE_CHECK_BEFORE_FIX_ID");
            entity.Property(e => e.CheckDatetime)
                .HasComment("チェック日時")
                .HasColumnType("datetime")
                .HasColumnName("CHECK_DATETIME");
            entity.Property(e => e.CheckerId)
                .HasComment("チェック者")
                .HasColumnName("CHECKER_ID");
            entity.Property(e => e.CheckerPost)
                .HasMaxLength(20)
                .HasComment("チェック者役職")
                .HasColumnName("CHECKER_POST");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .HasComment("コメント")
                .HasColumnName("COMMENT");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DenNoLine)
                .HasMaxLength(1)
                .HasComment("DEN_NO_LINE")
                .HasColumnName("DEN_NO_LINE");
            entity.Property(e => e.DenSort)
                .HasMaxLength(3)
                .HasComment("DEN_SORT")
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.SaveKey)
                .HasMaxLength(24)
                .HasComment("SAVE_KEY")
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<InterestRateCheckFixed>(entity =>
        {
            entity.HasKey(e => e.InterestRateCheckFixedId).HasName("INTEREST_RATE_CHECK_FIXED_PKC");

            entity.ToTable("INTEREST_RATE_CHECK_FIXED", tb => tb.HasComment("売上確定前利率異常チェック"));

            entity.Property(e => e.InterestRateCheckFixedId)
                .HasComment("利率異常チェックID")
                .HasColumnName("INTEREST_RATE_CHECK_FIXED_ID");
            entity.Property(e => e.CheckDatetime)
                .HasComment("チェック日時")
                .HasColumnType("datetime")
                .HasColumnName("CHECK_DATETIME");
            entity.Property(e => e.CheckerId)
                .HasComment("チェック者")
                .HasColumnName("CHECKER_ID");
            entity.Property(e => e.CheckerPost)
                .HasMaxLength(20)
                .HasComment("チェック者役職")
                .HasColumnName("CHECKER_POST");
            entity.Property(e => e.Comment)
                .HasMaxLength(1000)
                .HasComment("コメント")
                .HasColumnName("COMMENT");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.RowNo)
                .HasComment("売上行番号")
                .HasColumnName("ROW_NO");
            entity.Property(e => e.SalesNo)
                .HasMaxLength(10)
                .HasComment("売上番号")
                .HasColumnName("SALES_NO");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceNo).IsClustered(false);

            entity.ToTable("INVOICE", tb => tb.HasComment("請求データ"));

            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(10)
                .HasComment("請求番号")
                .HasColumnName("INVOICE_NO");
            entity.Property(e => e.CmpTax)
                .HasComment("消費税金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("CMP_TAX");
            entity.Property(e => e.CompCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasComment("取引先コード")
                .HasColumnName("COMP_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.CurrentBalance)
                .HasComment("当月残高")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("CURRENT_BALANCE");
            entity.Property(e => e.CustSubNo)
                .HasComment("顧客枝番")
                .HasColumnName("CUST_SUB_NO");
            entity.Property(e => e.InvoiceReceived)
                .HasDefaultValueSql("((0))")
                .HasComment("請求消込金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("INVOICE_RECEIVED");
            entity.Property(e => e.InvoiceState)
                .HasComment("請求状態: 0:未請求/1:請求書発行済/2:請求書送付済/3:入金済/4:完了")
                .HasColumnName("INVOICE_STATE");
            entity.Property(e => e.InvoicedDate)
                .HasComment("請求日")
                .HasColumnType("datetime")
                .HasColumnName("INVOICED_DATE");
            entity.Property(e => e.LastReceived)
                .HasComment("前回入金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("LAST_RECEIVED");
            entity.Property(e => e.MonthInvoice)
                .HasComment("当月請求額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("MONTH_INVOICE");
            entity.Property(e => e.MonthReceived)
                .HasComment("当月入金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("MONTH_RECEIVED");
            entity.Property(e => e.MonthSales)
                .HasComment("当月売上額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("MONTH_SALES");
            entity.Property(e => e.PreviousInvoice)
                .HasComment("前回請求残高")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PREVIOUS_INVOICE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceNo, e.SalesNo, e.RowNo }).IsClustered(false);

            entity.ToTable("INVOICE_DETAILS", tb => tb.HasComment("請求データ明細"));

            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(10)
                .HasComment("請求番号")
                .HasColumnName("INVOICE_NO");
            entity.Property(e => e.SalesNo)
                .HasMaxLength(10)
                .HasComment("売上番号")
                .HasColumnName("SALES_NO");
            entity.Property(e => e.RowNo)
                .HasComment("売上行番号")
                .HasColumnName("ROW_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<KTankaPurchase>(entity =>
        {
            entity.HasKey(e => new { e.ProdCode, e.SupCode, e.Sign, e.StartDate }).HasName("K_TANKA_PURCHASE_PKC");

            entity.ToTable("K_TANKA_PURCHASE", tb => tb.HasComment("契約単価・仕入"));

            entity.HasIndex(e => e.ChangeApplicationId, "K_TANKA_PURCHASE_CHANGE_APPLICATION_ID");

            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.SupCode)
                .HasMaxLength(8)
                .HasComment("仕入先コード(6桁)")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.Sign)
                .HasMaxLength(1)
                .HasComment("記号")
                .HasColumnName("SIGN");
            entity.Property(e => e.StartDate)
                .HasComment("開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Approved)
                .HasDefaultValueSql("((0))")
                .HasComment("承認済")
                .HasColumnName("APPROVED");
            entity.Property(e => e.ChangeApplicationId)
                .HasMaxLength(20)
                .HasComment("変更申請ID")
                .HasColumnName("CHANGE_APPLICATION_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("('2999-12-31')")
                .HasComment("終了日")
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.Rate)
                .HasComment("掛率")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("RATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<KTankaSale>(entity =>
        {
            entity.HasKey(e => new { e.ProdCode, e.CustCode, e.Sign, e.StartDate }).HasName("K_TANKA_SALES_PKC");

            entity.ToTable("K_TANKA_SALES", tb => tb.HasComment("契約単価・販売"));

            entity.HasIndex(e => e.ChangeApplicationId, "K_TANKA_SALES_CHANGE_APPLICATION_ID");

            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.CustCode)
                .HasMaxLength(8)
                .HasComment("顧客コード(6桁)")
                .HasColumnName("CUST_CODE");
            entity.Property(e => e.Sign)
                .HasMaxLength(1)
                .HasComment("記号")
                .HasColumnName("SIGN");
            entity.Property(e => e.StartDate)
                .HasComment("開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.Approved)
                .HasDefaultValueSql("((0))")
                .HasComment("承認済")
                .HasColumnName("APPROVED");
            entity.Property(e => e.ChangeApplicationId)
                .HasMaxLength(20)
                .HasComment("変更申請ID")
                .HasColumnName("CHANGE_APPLICATION_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("('2999-12-31')")
                .HasComment("終了日")
                .HasColumnType("datetime")
                .HasColumnName("END_DATE");
            entity.Property(e => e.Rate)
                .HasComment("掛率")
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("RATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<PayeeMst>(entity =>
        {
            entity.HasKey(e => e.PayeeCode).HasName("PAYEE_MST_PKC");

            entity.ToTable("PAYEE_MST", tb => tb.HasComment("支払先マスタ★"));

            entity.Property(e => e.PayeeCode)
                .HasMaxLength(8)
                .HasComment("支払先コード, (仕入先CD前4桁と一致）")
                .HasColumnName("PAYEE_CODE");
            entity.Property(e => e.ABankBlncCode)
                .HasMaxLength(3)
                .HasComment("全銀協支店コード★")
                .HasColumnName("A_BANK_BLNC_CODE");
            entity.Property(e => e.ABankCode)
                .HasMaxLength(4)
                .HasComment("全銀協銀行コード★")
                .HasColumnName("A_BANK_CODE");
            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .HasComment("住所１")
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .HasComment("住所２")
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.Address3)
                .HasMaxLength(40)
                .HasComment("住所3★追加")
                .HasColumnName("ADDRESS3");
            entity.Property(e => e.BankActName)
                .HasMaxLength(20)
                .HasComment("銀行口座名義人★")
                .HasColumnName("BANK_ACT_NAME");
            entity.Property(e => e.BankActType)
                .HasMaxLength(1)
                .HasComment("銀行口座種別,O:普通 C:当座★")
                .HasColumnName("BANK_ACT_TYPE");
            entity.Property(e => e.BankBlncName)
                .HasMaxLength(20)
                .HasComment("銀行支店名★")
                .HasColumnName("BANK_BLNC_NAME");
            entity.Property(e => e.BankName)
                .HasMaxLength(20)
                .HasComment("銀行名★")
                .HasColumnName("BANK_NAME");
            entity.Property(e => e.BankNo)
                .HasMaxLength(12)
                .HasComment("銀行口座番号★")
                .HasColumnName("BANK_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeleteDate)
                .HasComment("削除日★追加")
                .HasColumnType("date")
                .HasColumnName("DELETE_DATE");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("((0))")
                .HasComment("削除済★追加")
                .HasColumnName("DELETED");
            entity.Property(e => e.ErmcUserNo)
                .HasMaxLength(9)
                .HasComment("でんさいコード（利用者番号）★:Electronically Recorded Monetary Claims")
                .HasColumnName("ERMC_USER_NO");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasComment("FAX★追加")
                .HasColumnName("FAX");
            entity.Property(e => e.Fax2)
                .HasMaxLength(15)
                .HasComment("FAX2★追加")
                .HasColumnName("FAX2");
            entity.Property(e => e.InvoiceRegistNumber)
                .HasMaxLength(14)
                .HasComment("インボイス登録番号")
                .HasColumnName("INVOICE_REGIST_NUMBER");
            entity.Property(e => e.MaxCredit)
                .HasComment("与信限度額")
                .HasColumnName("MAX_CREDIT");
            entity.Property(e => e.NoSalesFlg)
                .HasDefaultValueSql("((0))")
                .HasComment("取引禁止フラグ")
                .HasColumnName("NO_SALES_FLG");
            entity.Property(e => e.PayMethodType)
                .HasDefaultValueSql("((1))")
                .HasComment("支払方法区分,1:振込,2:手形,3:でんさい:デフォルトの支払方法")
                .HasColumnName("PAY_METHOD_TYPE");
            entity.Property(e => e.PayeeBranchName)
                .HasMaxLength(40)
                .HasComment("支店名(センター名)★")
                .HasColumnName("PAYEE_BRANCH_NAME");
            entity.Property(e => e.PayeeKana)
                .HasMaxLength(40)
                .HasComment("支払先名カナ")
                .HasColumnName("PAYEE_KANA");
            entity.Property(e => e.PayeeKanaShort)
                .HasMaxLength(40)
                .HasComment("支払先名略称★")
                .HasColumnName("PAYEE_KANA_SHORT");
            entity.Property(e => e.PayeeName)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("支払先名")
                .HasColumnName("PAYEE_NAME");
            entity.Property(e => e.State)
                .HasMaxLength(4)
                .HasComment("都道府県")
                .HasColumnName("STATE");
            entity.Property(e => e.SupCloseDate)
                .HasComment("支払先締日,15:15日締め")
                .HasColumnName("SUP_CLOSE_DATE");
            entity.Property(e => e.SupPayDates)
                .HasComment("支払先支払日,10:10日払い,99：末日")
                .HasColumnName("SUP_PAY_DATES");
            entity.Property(e => e.SupPayMonths)
                .HasComment("支払先支払月,0:当月,1:翌月,2:翌々月")
                .HasColumnName("SUP_PAY_MONTHS");
            entity.Property(e => e.Tel)
                .HasMaxLength(15)
                .HasComment("TEL★追加")
                .HasColumnName("TEL");
            entity.Property(e => e.TempCreditUp)
                .HasComment("与信一時増加枠")
                .HasColumnName("TEMP_CREDIT_UP");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WideUseType)
                .HasComment("雑区分")
                .HasColumnName("WIDE_USE_TYPE");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("郵便番号")
                .HasColumnName("ZIP_CODE");
        });

        modelBuilder.Entity<PostAddress>(entity =>
        {
            entity.ToTable("POST_ADDRESS");

            entity.Property(e => e.PostAddressId).HasColumnName("POST_ADDRESS_ID");
            entity.Property(e => e.CyomeTownArea)
                .HasMaxLength(300)
                .HasComment("丁目を有する町域の場合の表示（「1」は該当、「0」は該当せず）")
                .HasColumnName("CYOME_TOWN_AREA");
            entity.Property(e => e.GovernmentCode)
                .HasMaxLength(300)
                .HasComment("全国地方公共団体コード（JIS X0401、X0402）")
                .HasColumnName("GOVERNMENT_CODE");
            entity.Property(e => e.KoazaStreetAddress)
                .HasMaxLength(300)
                .HasComment("小字毎に番地が起番されている町域の表示（「1」は該当、「0」は該当せず）")
                .HasColumnName("KOAZA_STREET_ADDRESS");
            entity.Property(e => e.MultiPostCodeTownArea)
                .HasMaxLength(300)
                .HasComment("一町域が二以上の郵便番号で表される場合の表示（「1」は該当、「0」は該当せず）")
                .HasColumnName("MULTI_POST_CODE_TOWN_AREA");
            entity.Property(e => e.MultiTownAreaPostCode)
                .HasMaxLength(300)
                .HasComment("一つの郵便番号で二以上の町域を表す場合の表示（「1」は該当、「0」は該当せず）")
                .HasColumnName("MULTI_TOWN_AREA_POST_CODE");
            entity.Property(e => e.Municipalities)
                .HasMaxLength(300)
                .HasComment("市区町村名")
                .HasColumnName("MUNICIPALITIES");
            entity.Property(e => e.MunicipalitiesKana)
                .HasMaxLength(300)
                .HasComment("市区町村名（カナ）")
                .HasColumnName("MUNICIPALITIES_KANA");
            entity.Property(e => e.PostCode3)
                .IsRequired()
                .HasMaxLength(300)
                .HasComment("（旧）郵便番号（5桁）")
                .HasColumnName("POST_CODE3");
            entity.Property(e => e.PostCode7)
                .IsRequired()
                .HasMaxLength(300)
                .HasComment("郵便番号（7桁）")
                .HasColumnName("POST_CODE7");
            entity.Property(e => e.Prefectures)
                .HasMaxLength(300)
                .HasComment("都道府県名")
                .HasColumnName("PREFECTURES");
            entity.Property(e => e.PrefecturesKana)
                .HasMaxLength(300)
                .HasComment("都道府県名（カナ）")
                .HasColumnName("PREFECTURES_KANA");
            entity.Property(e => e.TownArea)
                .HasMaxLength(300)
                .HasComment("町域名")
                .HasColumnName("TOWN_AREA");
            entity.Property(e => e.TownAreaKana)
                .HasMaxLength(300)
                .HasComment("町域名")
                .HasColumnName("TOWN_AREA_KANA");
            entity.Property(e => e.UpdateReason)
                .HasMaxLength(300)
                .HasComment("変更理由　（「0」は変更なし、「1」市政・区政・町政・分区・政令指定都市施行、「2」住居表示の実施、「3」区画整理、「4」郵便区調整等、「5」訂正、「6」廃止（廃止データのみ使用））")
                .HasColumnName("UPDATE_REASON");
            entity.Property(e => e.UpdateType)
                .HasMaxLength(300)
                .HasComment("更新の表示（「0」は変更なし、「1」は変更あり、「2」廃止（廃止データのみ使用））")
                .HasColumnName("UPDATE_TYPE");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryCode).IsClustered(false);

            entity.ToTable("PRODUCT_CATEGORY", tb => tb.HasComment("商品分類マスタ"));

            entity.Property(e => e.CategoryCode)
                .HasMaxLength(8)
                .HasComment("商品分類コード")
                .HasColumnName("CATEGORY_CODE");
            entity.Property(e => e.CategoryLayer)
                .HasDefaultValueSql("((0))")
                .HasComment("商品分類階層")
                .HasColumnName("CATEGORY_LAYER");
            entity.Property(e => e.CategoryPath)
                .HasMaxLength(100)
                .HasComment("商品分類パス")
                .HasColumnName("CATEGORY_PATH");
            entity.Property(e => e.Cd32)
                .HasMaxLength(2)
                .HasColumnName("CD32");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.LowestFlug)
                .HasDefaultValueSql("((0))")
                .HasComment("最下層区分")
                .HasColumnName("LOWEST_FLUG");
            entity.Property(e => e.ProdCateName)
                .HasMaxLength(30)
                .HasComment("商品分類名")
                .HasColumnName("PROD_CATE_NAME");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ProductSupplier>(entity =>
        {
            entity.HasKey(e => new { e.CategoryCode, e.SupCode }).IsClustered(false);

            entity.ToTable("PRODUCT_SUPPLIER");

            entity.Property(e => e.CategoryCode)
                .HasMaxLength(8)
                .HasComment("商品分類コード(CODE5)")
                .HasColumnName("CATEGORY_CODE");
            entity.Property(e => e.SupCode)
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater).HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Pu>(entity =>
        {
            entity.HasKey(e => e.PuNo).HasName("PU_PKC");

            entity.ToTable("PU", tb => tb.HasComment("仕入データ"));

            entity.Property(e => e.PuNo)
                .HasMaxLength(30)
                .HasComment("仕入番号:発注番号+仕入先コード+YYYYMM")
                .HasColumnName("PU_NO");
            entity.Property(e => e.CmpTax)
                .HasComment("消費税金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("CMP_TAX");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DenFlg)
                .HasMaxLength(2)
                .HasComment("伝区")
                .HasColumnName("DEN_FLG");
            entity.Property(e => e.DenNo)
                .HasMaxLength(20)
                .HasComment("伝票番号:M伝票番号")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.DeptCode)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("部門コード")
                .HasColumnName("DEPT_CODE");
            entity.Property(e => e.EmpId)
                .HasComment("仕入担当者ID（社員ID）:データ取込を実施した社員？（追加項目）")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.HatOrderNo)
                .HasMaxLength(10)
                .HasComment("HAT-F注文番号")
                .HasColumnName("HAT_ORDER_NO");
            entity.Property(e => e.PoNo)
                .HasMaxLength(10)
                .HasComment("発注番号:受注ヘッダ.HAT_ORDER_NO")
                .HasColumnName("PO_NO");
            entity.Property(e => e.PuAmmount)
                .HasComment("仕入金額合計（M金額）")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PU_AMMOUNT");
            entity.Property(e => e.PuDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("仕入日（納入日）")
                .HasColumnType("datetime")
                .HasColumnName("PU_DATE");
            entity.Property(e => e.PuType)
                .HasDefaultValueSql("((1))")
                .HasComment("仕入区分")
                .HasColumnName("PU_TYPE");
            entity.Property(e => e.SlipComment)
                .HasMaxLength(1000)
                .HasComment("備考")
                .HasColumnName("SLIP_COMMENT");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.SupCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.SupSubNo)
                .HasComment("仕入先枝番")
                .HasColumnName("SUP_SUB_NO");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<PuBilling>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PU_BILLING", tb => tb.HasComment("仕入請求データ:仕入請求取込された情報を格納"));

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.PoRowNo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("発注行番号:受注詳細.CHUBAN")
                .HasColumnName("PO_ROW_NO");
            entity.Property(e => e.ProdCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.ProdName)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("商品名")
                .HasColumnName("PROD_NAME");
            entity.Property(e => e.PuDate)
                .HasComment("仕入日（納入日）")
                .HasColumnType("datetime")
                .HasColumnName("PU_DATE");
            entity.Property(e => e.PuDenNo)
                .HasMaxLength(20)
                .HasComment("仕入先伝票番号")
                .HasColumnName("PU_DEN_NO");
            entity.Property(e => e.PuNo)
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("PU_NO");
            entity.Property(e => e.PuPayYearMonth)
                .HasComment("仕入支払年月")
                .HasColumnType("datetime")
                .HasColumnName("PU_PAY_YEAR_MONTH");
            entity.Property(e => e.PuPrice)
                .HasDefaultValueSql("((0))")
                .HasComment("仕入単価（M単価）")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PU_PRICE");
            entity.Property(e => e.PuQuantity)
                .HasDefaultValueSql("((1))")
                .HasComment("仕入数量（M数量）")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PU_QUANTITY");
            entity.Property(e => e.PuTax)
                .HasComment("消費税率")
                .HasColumnName("PU_TAX");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<PuDetail>(entity =>
        {
            entity.HasKey(e => new { e.PuRowNo, e.PuNo }).HasName("PU_DETAILS_PKC");

            entity.ToTable("PU_DETAILS", tb => tb.HasComment("仕入データ明細"));

            entity.Property(e => e.PuRowNo)
                .HasComment("仕入行番号")
                .HasColumnName("PU_ROW_NO");
            entity.Property(e => e.PuNo)
                .HasMaxLength(30)
                .HasComment("仕入番号:発注番号+仕入先コード+YYYYMM")
                .HasColumnName("PU_NO");
            entity.Property(e => e.ApprovalTargetId)
                .HasMaxLength(20)
                .HasComment("承認対象ID")
                .HasColumnName("APPROVAL_TARGET_ID");
            entity.Property(e => e.CheckStatus)
                .HasComment("照合ステータス(0:未確認/1:編集中/2:確認済/3:未決/4:違算/5:未請求)")
                .HasColumnName("CHECK_STATUS");
            entity.Property(e => e.Chuban)
                .HasMaxLength(15)
                .HasColumnName("CHUBAN");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.OriginalPuNo)
                .HasMaxLength(30)
                .HasComment("元仕入番号")
                .HasColumnName("ORIGINAL_PU_NO");
            entity.Property(e => e.OriginalPuRowNo)
                .HasComment("元仕入行番号")
                .HasColumnName("ORIGINAL_PU_ROW_NO");
            entity.Property(e => e.PoPrice)
                .HasDefaultValueSql("((0))")
                .HasComment("仕入単価（M単価）")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PO_PRICE");
            entity.Property(e => e.PoRowNo)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("発注行番号:発注データ明細.PO_ROW_NO")
                .HasColumnName("PO_ROW_NO");
            entity.Property(e => e.ProdCode)
                .IsRequired()
                .HasMaxLength(50)
                .HasComment("商品コード:（取込データの値を設定？）")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.ProdName)
                .IsRequired()
                .HasMaxLength(100)
                .HasComment("商品名:（取込データの値を設定？）")
                .HasColumnName("PROD_NAME");
            entity.Property(e => e.PuCorrectionType)
                .HasMaxLength(2)
                .HasComment("仕入訂正区分")
                .HasColumnName("PU_CORRECTION_TYPE");
            entity.Property(e => e.PuPayDate)
                .HasComment("支払日")
                .HasColumnType("datetime")
                .HasColumnName("PU_PAY_DATE");
            entity.Property(e => e.PuPayYearMonth)
                .HasComment("仕入支払年月")
                .HasColumnType("datetime")
                .HasColumnName("PU_PAY_YEAR_MONTH");
            entity.Property(e => e.PuQuantity)
                .HasComment("仕入数量（M数量）")
                .HasColumnName("PU_QUANTITY");
            entity.Property(e => e.PuRowDspNo)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("仕入行表示番号（M注番）")
                .HasColumnName("PU_ROW_DSP_NO");
            entity.Property(e => e.TaxRateCd)
                .HasMaxLength(1)
                .HasComment("消費税率区分:B:10%、8:8%、Z:非課税")
                .HasColumnName("TAX_RATE_CD");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード:倉庫入庫時に設定される（直送の場合NULLのため必須を外す）")
                .HasColumnName("WH_CODE");
        });

        modelBuilder.Entity<PuImport>(entity =>
        {
            entity.HasKey(e => e.PuImportNo).HasName("PU_IMPORT_PKC");

            entity.ToTable("PU_IMPORT", tb => tb.HasComment("仕入取込データ★"));

            entity.Property(e => e.PuImportNo)
                .HasMaxLength(30)
                .HasComment("仕入取込番号")
                .HasColumnName("PU_IMPORT_NO");
            entity.Property(e => e.Chuban)
                .HasMaxLength(15)
                .HasComment("F注番")
                .HasColumnName("CHUBAN");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeliveryNo)
                .IsRequired()
                .HasMaxLength(30)
                .HasComment("納品書番号")
                .HasColumnName("DELIVERY_NO");
            entity.Property(e => e.HatOrderNo)
                .HasMaxLength(10)
                .HasComment("HAT-F注文番号")
                .HasColumnName("HAT_ORDER_NO");
            entity.Property(e => e.Koban)
                .HasComment("子番")
                .HasColumnName("KOBAN");
            entity.Property(e => e.Noubi)
                .HasComment("納入日")
                .HasColumnType("date")
                .HasColumnName("NOUBI");
            entity.Property(e => e.PaySupCode)
                .HasMaxLength(8)
                .HasComment("支払先コード")
                .HasColumnName("PAY_SUP_CODE");
            entity.Property(e => e.PoPrice)
                .HasComment("仕入単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PO_PRICE");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.ProdName)
                .HasMaxLength(100)
                .HasComment("商品名")
                .HasColumnName("PROD_NAME");
            entity.Property(e => e.PuKbn)
                .HasMaxLength(5)
                .HasComment("区分:0orNULL:売上")
                .HasColumnName("PU_KBN");
            entity.Property(e => e.PuQuantity)
                .HasComment("仕入数量")
                .HasColumnName("PU_QUANTITY");
            entity.Property(e => e.SupCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.SupSubNo)
                .HasComment("仕入先枝番")
                .HasColumnName("SUP_SUB_NO");
            entity.Property(e => e.TaxFlg)
                .HasMaxLength(5)
                .HasComment("消費税")
                .HasColumnName("TAX_FLG");
            entity.Property(e => e.TaxRate)
                .HasComment("消費税率")
                .HasColumnName("TAX_RATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ReturningProduct>(entity =>
        {
            entity.HasKey(e => e.ReturningProductsId).HasName("RETURNING_PRODUCTS_PKC");

            entity.ToTable("RETURNING_PRODUCTS", tb => tb.HasComment("返品データ★"));

            entity.Property(e => e.ReturningProductsId)
                .HasMaxLength(10)
                .HasComment("返品ID")
                .HasColumnName("RETURNING_PRODUCTS_ID");
            entity.Property(e => e.ApprovalId)
                .HasMaxLength(20)
                .HasComment("承認要求番号")
                .HasColumnName("APPROVAL_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.RefundApprovalId)
                .HasMaxLength(20)
                .HasComment("返金承認要求番号")
                .HasColumnName("REFUND_APPROVAL_ID");
            entity.Property(e => e.ReturnDate)
                .HasComment("返品日時")
                .HasColumnType("datetime")
                .HasColumnName("RETURN_DATE");
            entity.Property(e => e.StockApprovalId)
                .HasMaxLength(20)
                .HasComment("入庫承認要求番号")
                .HasColumnName("STOCK_APPROVAL_ID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<ReturningProductsDetail>(entity =>
        {
            entity.HasKey(e => new { e.ReturningProductsId, e.RowNo }).HasName("RETURNING_PRODUCTS_DETAILS_PKC");

            entity.ToTable("RETURNING_PRODUCTS_DETAILS", tb => tb.HasComment("返品データ明細★"));

            entity.Property(e => e.ReturningProductsId)
                .HasMaxLength(10)
                .HasComment("返品ID")
                .HasColumnName("RETURNING_PRODUCTS_ID");
            entity.Property(e => e.RowNo)
                .HasComment("行番号")
                .HasColumnName("ROW_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DenNo)
                .HasMaxLength(20)
                .HasComment("伝票番号")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.Quantity)
                .HasComment("売上数量")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.RefundUnitPrice)
                .HasDefaultValueSql("((0))")
                .HasComment("返金単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("REFUND_UNIT_PRICE");
            entity.Property(e => e.ReturnQuantity)
                .HasComment("返品数量")
                .HasColumnName("RETURN_QUANTITY");
            entity.Property(e => e.ReturnRequestQuantity)
                .HasComment("返品依頼数量")
                .HasColumnName("RETURN_REQUEST_QUANTITY");
            entity.Property(e => e.SalesCd)
                .HasMaxLength(2)
                .HasComment("売区")
                .HasColumnName("SALES_CD");
            entity.Property(e => e.SalesNo)
                .HasMaxLength(10)
                .HasComment("売上番号")
                .HasColumnName("SALES_NO");
            entity.Property(e => e.SalesRowNo)
                .HasComment("売上行番号")
                .HasColumnName("SALES_ROW_NO");
            entity.Property(e => e.SellUnitPrice)
                .HasDefaultValueSql("((0))")
                .HasComment("販売単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SELL_UNIT_PRICE");
            entity.Property(e => e.StockUnitPrice)
                .HasDefaultValueSql("((0))")
                .HasComment("在庫単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("STOCK_UNIT_PRICE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SalesNo).HasName("SALES_PKC");

            entity.ToTable("SALES", tb => tb.HasComment("売上データ:倉出/マルマ:出荷指示時に登録\r\n直送:仕入請求金額照合時に登録\r\n赤黒訂正時に登録"));

            entity.Property(e => e.SalesNo)
                .HasMaxLength(10)
                .HasComment("売上番号")
                .HasColumnName("SALES_NO");
            entity.Property(e => e.CmpTax)
                .HasComment("消費税合計")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("CMP_TAX");
            entity.Property(e => e.CompCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasComment("取引先コード")
                .HasColumnName("COMP_CODE");
            entity.Property(e => e.ConstructionCode)
                .HasMaxLength(20)
                .HasComment("物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeptCode)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("部門コード")
                .HasColumnName("DEPT_CODE");
            entity.Property(e => e.EmpId)
                .HasComment("社員ID:（追加項目）")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.OrderNo)
                .IsRequired()
                .HasMaxLength(12)
                .HasComment("受注番号:受注ヘッダ.ORDER_NO")
                .HasColumnName("ORDER_NO");
            entity.Property(e => e.OrgnlNo)
                .HasMaxLength(10)
                .HasComment("元伝票番号")
                .HasColumnName("ORGNL_NO");
            entity.Property(e => e.SalesAmnt)
                .HasComment("売上金額合計")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SALES_AMNT");
            entity.Property(e => e.SalesDate)
                .HasComment("売上日,出荷日:(倉出/マルマ:到着予定日,直送:納入日)")
                .HasColumnType("datetime")
                .HasColumnName("SALES_DATE");
            entity.Property(e => e.SalesType)
                .HasDefaultValueSql("((1))")
                .HasComment("売上区分,1:売上 2:売上返品")
                .HasColumnName("SALES_TYPE");
            entity.Property(e => e.SlipComment)
                .HasMaxLength(1000)
                .HasComment("備考")
                .HasColumnName("SLIP_COMMENT");
            entity.Property(e => e.StartDate)
                .HasComment("部門開始日")
                .HasColumnType("datetime")
                .HasColumnName("START_DATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedNo)
                .HasComment("赤黒伝票番号")
                .HasColumnName("UPDATED_NO");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<SalesAdjustment>(entity =>
        {
            entity.HasKey(e => e.SalesAdjustmentNo).HasName("SALES_ADJUSTMENT_PKC");

            entity.ToTable("SALES_ADJUSTMENT", tb => tb.HasComment("売上データ調整"));

            entity.Property(e => e.SalesAdjustmentNo)
                .HasMaxLength(30)
                .HasComment("売上調整番号")
                .HasColumnName("SALES_ADJUSTMENT_NO");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(30)
                .HasComment("勘定科目")
                .HasColumnName("ACCOUNT_TITLE");
            entity.Property(e => e.AdjustmentAmount)
                .HasDefaultValueSql("((0))")
                .HasComment("調整金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("ADJUSTMENT_AMOUNT");
            entity.Property(e => e.AdjustmentCategory)
                .HasComment("調整区分,1:協賛金 2:保険 3:経費 4:商品購入")
                .HasColumnName("ADJUSTMENT_CATEGORY");
            entity.Property(e => e.ApprovalId)
                .HasMaxLength(20)
                .HasColumnName("APPROVAL_ID");
            entity.Property(e => e.ConstructionCode)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasComment("摘要")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.EmpId)
                .HasComment("社員ID")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.InvoicedDate)
                .HasComment("請求日")
                .HasColumnType("datetime")
                .HasColumnName("INVOICED_DATE");
            entity.Property(e => e.TaxFlg)
                .HasMaxLength(1)
                .HasComment("消費税:B:10%,8:8%,Z:非課税")
                .HasColumnName("TAX_FLG");
            entity.Property(e => e.TaxRate)
                .HasComment("消費税率")
                .HasColumnName("TAX_RATE");
            entity.Property(e => e.TokuiCd)
                .IsRequired()
                .HasMaxLength(6)
                .HasComment("得意先コード")
                .HasColumnName("TOKUI_CD");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<SalesDetail>(entity =>
        {
            entity.HasKey(e => new { e.SalesNo, e.RowNo }).HasName("SALES_DETAILS_PKC");

            entity.ToTable("SALES_DETAILS", tb => tb.HasComment("売上データ明細"));

            entity.Property(e => e.SalesNo)
                .HasMaxLength(10)
                .HasComment("売上番号")
                .HasColumnName("SALES_NO");
            entity.Property(e => e.RowNo)
                .HasComment("売上行番号")
                .HasColumnName("ROW_NO");
            entity.Property(e => e.AccountTitle)
                .HasMaxLength(30)
                .HasComment("勘定科目")
                .HasColumnName("ACCOUNT_TITLE");
            entity.Property(e => e.ApprovalTargetId)
                .HasMaxLength(20)
                .HasComment("承認対象ID")
                .HasColumnName("APPROVAL_TARGET_ID");
            entity.Property(e => e.AutoJournalDate)
                .HasComment("自動仕訳処理日")
                .HasColumnType("datetime")
                .HasColumnName("AUTO_JOURNAL_DATE");
            entity.Property(e => e.Chuban)
                .HasMaxLength(15)
                .HasComment("H注番")
                .HasColumnName("CHUBAN");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeliveredQty)
                .HasDefaultValueSql("((0))")
                .HasComment("出荷数量")
                .HasColumnName("DELIVERED_QTY");
            entity.Property(e => e.DenFlg)
                .HasMaxLength(2)
                .HasComment("伝区")
                .HasColumnName("DEN_FLG");
            entity.Property(e => e.DenNo)
                .HasMaxLength(20)
                .HasComment("伝票番号")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.Discount)
                .HasComment("値引金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("DISCOUNT");
            entity.Property(e => e.HatOrderNo)
                .HasMaxLength(10)
                .HasComment("HAT注文番号")
                .HasColumnName("HAT_ORDER_NO");
            entity.Property(e => e.InvoiceDelayType)
                .HasComment("請求遅延区分")
                .HasColumnName("INVOICE_DELAY_TYPE");
            entity.Property(e => e.InvoiceNo)
                .HasMaxLength(10)
                .HasComment("請求番号")
                .HasColumnName("INVOICE_NO");
            entity.Property(e => e.InvoicedDate)
                .HasComment("請求日")
                .HasColumnType("datetime")
                .HasColumnName("INVOICED_DATE");
            entity.Property(e => e.Item)
                .HasMaxLength(30)
                .HasComment("品目")
                .HasColumnName("ITEM");
            entity.Property(e => e.OriginalRowNo)
                .HasComment("元売上行番号")
                .HasColumnName("ORIGINAL_ROW_NO");
            entity.Property(e => e.OriginalSalesNo)
                .HasMaxLength(10)
                .HasComment("元売上番号")
                .HasColumnName("ORIGINAL_SALES_NO");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.ProdName)
                .HasMaxLength(100)
                .HasComment("商品名")
                .HasColumnName("PROD_NAME");
            entity.Property(e => e.Quantity)
                .HasComment("売上数量")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.SalesCd)
                .HasMaxLength(2)
                .HasComment("売区")
                .HasColumnName("SALES_CD");
            entity.Property(e => e.SalesCorrectionType)
                .HasMaxLength(2)
                .HasComment("売上訂正区分")
                .HasColumnName("SALES_CORRECTION_TYPE");
            entity.Property(e => e.Summary)
                .HasMaxLength(30)
                .HasComment("摘要")
                .HasColumnName("SUMMARY");
            entity.Property(e => e.Unitprice)
                .HasComment("販売単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("UNITPRICE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<SalesEditLock>(entity =>
        {
            entity.HasKey(e => e.HatOrderNo).HasName("SALES_EDIT_LOCK_PKC");

            entity.ToTable("SALES_EDIT_LOCK", tb => tb.HasComment("売上額訂正ロック"));

            entity.Property(e => e.HatOrderNo)
                .HasMaxLength(10)
                .HasComment("HAT注文番号")
                .HasColumnName("HAT_ORDER_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者")
                .HasColumnName("CREATOR");
            entity.Property(e => e.EditStartDatetime)
                .HasComment("編集開始日時")
                .HasColumnType("datetime")
                .HasColumnName("EDIT_START_DATETIME");
            entity.Property(e => e.EditorEmpId)
                .HasComment("編集者")
                .HasColumnName("EDITOR_EMP_ID");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.WhCode, e.ProdCode, e.RotNo, e.StockType, e.QualityType }).IsClustered(false);

            entity.ToTable("STOCK", tb => tb.HasComment("在庫データ"));

            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.RotNo)
                .HasMaxLength(20)
                .HasComment("ロット番号")
                .HasColumnName("ROT_NO");
            entity.Property(e => e.StockType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('1')")
                .HasComment("在庫区分,1:自社在庫 2:預り在庫")
                .HasColumnName("STOCK_TYPE");
            entity.Property(e => e.QualityType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('G')")
                .HasComment("良品区分,G:良品 F:不良品 U:未検品")
                .HasColumnName("QUALITY_TYPE");
            entity.Property(e => e.Actual)
                .HasDefaultValueSql("((1))")
                .HasComment("実在庫数")
                .HasColumnName("ACTUAL");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.LastDeliveryDate)
                .HasComment("最終出荷日")
                .HasColumnType("datetime")
                .HasColumnName("LAST_DELIVERY_DATE");
            entity.Property(e => e.RowVer)
                .IsRequired()
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasComment("楽観的排他ロック用行バージョン,(スキャフォールドでTimestamp属性は付与されません)")
                .HasColumnName("ROW_VER");
            entity.Property(e => e.StockRank)
                .HasMaxLength(1)
                .HasComment("ランク")
                .HasColumnName("STOCK_RANK");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.Valid)
                .HasDefaultValueSql("((1))")
                .HasComment("有効在庫数")
                .HasColumnName("VALID");
        });

        modelBuilder.Entity<StockHistory>(entity =>
        {
            entity.HasKey(e => new { e.WhCode, e.ProdCode, e.RotNo, e.StockType, e.QualityType, e.LastDeliveryDate }).IsClustered(false);

            entity.ToTable("STOCK_HISTORY");

            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.RotNo)
                .HasMaxLength(20)
                .HasComment("ロット番号")
                .HasColumnName("ROT_NO");
            entity.Property(e => e.StockType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('1')")
                .HasComment("在庫区分,1:自社在庫 2:預り在庫")
                .HasColumnName("STOCK_TYPE");
            entity.Property(e => e.QualityType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('G')")
                .HasComment("良品区分,G:良品 F:不良品 U:未検品")
                .HasColumnName("QUALITY_TYPE");
            entity.Property(e => e.LastDeliveryDate)
                .HasComment("最終出荷日")
                .HasColumnType("datetime")
                .HasColumnName("LAST_DELIVERY_DATE");
            entity.Property(e => e.Actual)
                .HasDefaultValueSql("((1))")
                .HasComment("実在庫数")
                .HasColumnName("ACTUAL");
            entity.Property(e => e.ActualBara).HasColumnName("ACTUAL_BARA");
            entity.Property(e => e.BeginningPeriod)
                .HasDefaultValueSql("((0))")
                .HasColumnName("BEGINNING_PERIOD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.StockRank)
                .HasMaxLength(1)
                .HasComment("ランク")
                .HasColumnName("STOCK_RANK");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.Valid)
                .HasDefaultValueSql("((1))")
                .HasComment("有効在庫数")
                .HasColumnName("VALID");
            entity.Property(e => e.ValidBara).HasColumnName("VALID_BARA");
        });

        modelBuilder.Entity<StockInventory>(entity =>
        {
            entity.HasKey(e => new { e.WhCode, e.ProdCode, e.RotNo, e.StockType, e.QualityType, e.InventoryYearmonth }).HasName("STOCK_INVENTORY_PKC");

            entity.ToTable("STOCK_INVENTORY", tb => tb.HasComment("在庫データ_棚卸★"));

            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.RotNo)
                .HasMaxLength(20)
                .HasComment("ロット番号,★削除予定")
                .HasColumnName("ROT_NO");
            entity.Property(e => e.StockType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('1')")
                .HasComment("在庫区分,1:自社在庫 2:預り在庫 (マルマ)")
                .HasColumnName("STOCK_TYPE");
            entity.Property(e => e.QualityType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('G')")
                .HasComment("良品区分,G:良品 F:不良品 U:未検品")
                .HasColumnName("QUALITY_TYPE");
            entity.Property(e => e.InventoryYearmonth)
                .HasComment("棚卸年月,yyyyMM01で保存")
                .HasColumnType("datetime")
                .HasColumnName("INVENTORY_YEARMONTH");
            entity.Property(e => e.Actual)
                .HasComment("実在庫数")
                .HasColumnName("ACTUAL");
            entity.Property(e => e.ActualBara)
                .HasComment("実在庫数 (バラ)")
                .HasColumnName("ACTUAL_BARA");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Managed)
                .HasComment("管理在庫数")
                .HasColumnName("MANAGED");
            entity.Property(e => e.ManagedBara)
                .HasComment("管理在庫数 (バラ)")
                .HasColumnName("MANAGED_BARA");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasComment("状況,「連携済」等の対応コード")
                .HasColumnName("STATUS");
            entity.Property(e => e.StockNo)
                .HasComment("棚卸NO,倉庫+区分で連番")
                .HasColumnName("STOCK_NO");
            entity.Property(e => e.StockRank)
                .HasMaxLength(1)
                .HasComment("ランク")
                .HasColumnName("STOCK_RANK");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<StockLocation>(entity =>
        {
            entity.HasKey(e => new { e.WhCode, e.ProdCode }).HasName("STOCK_LOCATION_PKC");

            entity.ToTable("STOCK_LOCATION", tb => tb.HasComment("在庫置場★"));

            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.StockLocationCode)
                .HasMaxLength(10)
                .HasComment("在庫置場コード")
                .HasColumnName("STOCK_LOCATION_CODE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<StockRefill>(entity =>
        {
            entity.HasKey(e => new { e.WhCode, e.ProdCode }).HasName("STOCK_REFILL_PKC");

            entity.ToTable("STOCK_REFILL", tb => tb.HasComment("在庫補充★"));

            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.OrderQuantity)
                .HasComment("発注個数")
                .HasColumnName("ORDER_QUANTITY");
            entity.Property(e => e.StockThreshold)
                .HasComment("在庫閾値,在庫数が下回ったら発注候補にします")
                .HasColumnName("STOCK_THRESHOLD");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<StockRefillOrder>(entity =>
        {
            entity.HasKey(e => e.StockRefillOrderId).HasName("STOCK_REFILL_ORDER_PKC");

            entity.ToTable("STOCK_REFILL_ORDER", tb => tb.HasComment("在庫補充発注★"));

            entity.Property(e => e.StockRefillOrderId)
                .ValueGeneratedNever()
                .HasComment("在庫補充発注ID")
                .HasColumnName("STOCK_REFILL_ORDER_ID");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.OrderDatetime)
                .HasComment("発注日時")
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATETIME");
            entity.Property(e => e.SupCode)
                .IsRequired()
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WhCode)
                .IsRequired()
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
        });

        modelBuilder.Entity<StockRefillOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.StockRefillOrderId, e.ProdCode }).HasName("STOCK_REFILL_ORDER_DETAIL_PKC");

            entity.ToTable("STOCK_REFILL_ORDER_DETAIL", tb => tb.HasComment("在庫補充発注詳細★"));

            entity.Property(e => e.StockRefillOrderId)
                .HasComment("在庫補充発注ID")
                .HasColumnName("STOCK_REFILL_ORDER_ID");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.OrderQuantity)
                .HasComment("発注個数")
                .HasColumnName("ORDER_QUANTITY");
            entity.Property(e => e.OrderUnitPrice)
                .HasComment("発注単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("ORDER_UNIT_PRICE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<StockReserve>(entity =>
        {
            entity.HasKey(e => e.StockReserveId).HasName("STOCK_RESERVE_PKC");

            entity.ToTable("STOCK_RESERVE", tb => tb.HasComment("在庫引当★"));

            entity.Property(e => e.StockReserveId)
                .HasComment("在庫引当ID,自動採番")
                .HasColumnName("STOCK_RESERVE_ID");
            entity.Property(e => e.CreateDate)
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DenNo)
                .HasMaxLength(6)
                .HasComment("伝票番号")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.Quantity)
                .HasComment("数量")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.QuantityBara)
                .HasComment("数量 (バラ)")
                .HasColumnName("QUANTITY_BARA");
            entity.Property(e => e.StockReserveDatetime)
                .HasComment("在庫引当日時")
                .HasColumnType("datetime")
                .HasColumnName("STOCK_RESERVE_DATETIME");
            entity.Property(e => e.StockType)
                .HasMaxLength(1)
                .HasComment("在庫区分,1:自社在庫 2:預り在庫 (マルマ)")
                .HasColumnName("STOCK_TYPE");
            entity.Property(e => e.UpdateDate)
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WarehousingCause)
                .HasComment("入出庫理由,0:通常 1:棚卸し調整")
                .HasColumnName("WAREHOUSING_CAUSE");
            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
        });

        modelBuilder.Entity<SupplierMst>(entity =>
        {
            entity.HasKey(e => new { e.SupCode, e.SupSubNo })
                .HasName("PK_SUPPLIER_MST_0509")
                .IsClustered(false);

            entity.ToTable("SUPPLIER_MST", tb => tb.HasComment("仕入先マスタ"));

            entity.Property(e => e.SupCode)
                .HasMaxLength(8)
                .HasComment("仕入先コード")
                .HasColumnName("SUP_CODE");
            entity.Property(e => e.SupSubNo)
                .HasComment("仕入先枝番")
                .HasColumnName("SUP_SUB_NO");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.PayMethodType)
                .HasDefaultValueSql("((1))")
                .HasComment("支払方法区分,1:振込,2:手形")
                .HasColumnName("PAY_METHOD_TYPE");
            entity.Property(e => e.SupAddress1)
                .HasMaxLength(40)
                .HasComment("仕入先住所１")
                .HasColumnName("SUP_ADDRESS1");
            entity.Property(e => e.SupAddress2)
                .HasMaxLength(40)
                .HasComment("仕入先住所２")
                .HasColumnName("SUP_ADDRESS2");
            entity.Property(e => e.SupCloseDate)
                .HasComment("仕入先締日,15:15日締め")
                .HasColumnName("SUP_CLOSE_DATE");
            entity.Property(e => e.SupDepName)
                .HasMaxLength(40)
                .HasComment("仕入先部門名")
                .HasColumnName("SUP_DEP_NAME");
            entity.Property(e => e.SupEmail)
                .HasMaxLength(100)
                .HasComment("仕入先メールアドレス")
                .HasColumnName("SUP_EMAIL");
            entity.Property(e => e.SupEmpName)
                .HasMaxLength(20)
                .HasComment("仕入先担当者名")
                .HasColumnName("SUP_EMP_NAME");
            entity.Property(e => e.SupFax)
                .HasMaxLength(13)
                .HasComment("仕入先FAX番号")
                .HasColumnName("SUP_FAX");
            entity.Property(e => e.SupKana)
                .HasMaxLength(40)
                .HasComment("仕入先名カナ")
                .HasColumnName("SUP_KANA");
            entity.Property(e => e.SupName)
                .IsRequired()
                .HasMaxLength(40)
                .HasComment("仕入先名")
                .HasColumnName("SUP_NAME");
            entity.Property(e => e.SupPayDates)
                .HasComment("仕入先支払日,10:10日払い,99：末日")
                .HasColumnName("SUP_PAY_DATES");
            entity.Property(e => e.SupPayMonths)
                .HasDefaultValueSql("((1))")
                .HasComment("仕入先支払月,0:当月,1:翌月,2:翌々月")
                .HasColumnName("SUP_PAY_MONTHS");
            entity.Property(e => e.SupState)
                .HasMaxLength(4)
                .HasComment("仕入先都道府県")
                .HasColumnName("SUP_STATE");
            entity.Property(e => e.SupTel)
                .HasMaxLength(13)
                .HasComment("仕入先電話番号")
                .HasColumnName("SUP_TEL");
            entity.Property(e => e.SupZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("仕入先郵便番号")
                .HasColumnName("SUP_ZIP_CODE");
            entity.Property(e => e.SupplierType)
                .HasComment("発注先種別,null/0:未設定 1:橋本本体 2:橋本本体以外")
                .HasColumnName("SUPPLIER_TYPE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<TitleDefaultRole>(entity =>
        {
            entity.HasKey(e => new { e.TitleCode, e.UserRoleCd }).HasName("DEFAULT_TITLE_ROLE_PKC");

            entity.ToTable("TITLE_DEFAULT_ROLE", tb => tb.HasComment("役職既定役割 (ロール)"));

            entity.Property(e => e.TitleCode)
                .HasMaxLength(20)
                .HasComment("役職コード")
                .HasColumnName("TITLE_CODE");
            entity.Property(e => e.UserRoleCd)
                .HasMaxLength(2)
                .HasComment("役割CD")
                .HasColumnName("USER_ROLE_CD");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("CREATE_DATE")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("CREATOR")
                .HasColumnName("CREATOR");
            entity.Property(e => e.Deleted)
                .HasDefaultValueSql("((0))")
                .HasComment("削除済")
                .HasColumnName("DELETED");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("UPDATE_DATE")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("UPDATER")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<TotoContract>(entity =>
        {
            entity.HasKey(e => e.OrderNo).HasName("TOTO_CONTRACT_PKC");

            entity.ToTable("TOTO_CONTRACT", tb => tb.HasComment("TOTO請書情報:読み込んだTOTO請書情報を格納するテーブル"));

            entity.Property(e => e.OrderNo)
                .HasMaxLength(10)
                .HasComment("注番:各請書の項目毎に一意になる項目")
                .HasColumnName("ORDER_NO");
            entity.Property(e => e.AppropState)
                .HasComment("計上ステータス,0:未計上/1:計上済:他のステータスがあるかは要確認")
                .HasColumnName("APPROP_STATE");
            entity.Property(e => e.ArrangementDate)
                .HasComment("手配:TOTO請書項目に存在したため念のため追加")
                .HasColumnType("date")
                .HasColumnName("ARRANGEMENT_DATE");
            entity.Property(e => e.BaseDate)
                .HasComment("拠点:TOTO請書項目に存在したため念のため追加")
                .HasColumnType("date")
                .HasColumnName("BASE_DATE");
            entity.Property(e => e.ConstructionCode)
                .IsRequired()
                .HasMaxLength(20)
                .HasComment("物件コード:取込した対象の物件コード")
                .HasColumnName("CONSTRUCTION_CODE");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(1024)
                .HasComment("届け先:直送の住所")
                .HasColumnName("DELIVERY_ADDRESS");
            entity.Property(e => e.DeliveryDate)
                .HasComment("納期")
                .HasColumnType("date")
                .HasColumnName("DELIVERY_DATE");
            entity.Property(e => e.Digit5Code)
                .HasMaxLength(6)
                .HasComment("5桁コード")
                .HasColumnName("DIGIT5_CODE");
            entity.Property(e => e.InternalNo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("内部番号:請書に記載されている6桁のコード")
                .HasColumnName("INTERNAL_NO");
            entity.Property(e => e.LineNo)
                .HasMaxLength(10)
                .HasComment("行番号:1～6までしかないはず")
                .HasColumnName("LINE_NO");
            entity.Property(e => e.ProductCode)
                .HasMaxLength(3)
                .HasComment("商品コード")
                .HasColumnName("PRODUCT_CODE");
            entity.Property(e => e.ProductName)
                .HasMaxLength(24)
                .HasComment("商品名")
                .HasColumnName("PRODUCT_NAME");
            entity.Property(e => e.PurchaseMargin)
                .HasComment("仕入利率")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PURCHASE_MARGIN");
            entity.Property(e => e.PurchasePrice)
                .HasComment("仕入単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("PURCHASE_PRICE");
            entity.Property(e => e.Quantity)
                .HasComment("数量")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.RecipientAddress)
                .HasMaxLength(1024)
                .HasComment("宛先:直送の宛先")
                .HasColumnName("RECIPIENT_ADDRESS");
            entity.Property(e => e.RecvNo)
                .IsRequired()
                .HasMaxLength(10)
                .HasComment("受番:請書に記載されている7桁のコード")
                .HasColumnName("RECV_NO");
            entity.Property(e => e.SellingPrice)
                .HasComment("売単価")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("SELLING_PRICE");
            entity.Property(e => e.SiteName)
                .HasMaxLength(50)
                .HasComment("現場名:物件に登録する現場とイコールになる")
                .HasColumnName("SITE_NAME");
            entity.Property(e => e.TotalPurchaseAmount)
                .HasComment("仕入合計金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("TOTAL_PURCHASE_AMOUNT");
            entity.Property(e => e.TotalSalesAmount)
                .HasComment("売合計金額")
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("TOTAL_SALES_AMOUNT");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者")
                .HasColumnName("UPDATER");
        });

        modelBuilder.Entity<UserAssignedRole>(entity =>
        {
            entity.HasKey(e => new { e.EmpId, e.UserRoleCd }).HasName("PK__USER_ASS__8CBB34C077B9F79A");

            entity.ToTable("USER_ASSIGNED_ROLE", tb => tb.HasComment("ユーザー割当役割"));

            entity.Property(e => e.EmpId)
                .HasComment("社員ID")
                .HasColumnName("EMP_ID");
            entity.Property(e => e.UserRoleCd)
                .HasMaxLength(2)
                .HasComment("役割コード")
                .HasColumnName("USER_ROLE_CD");
        });

        modelBuilder.Entity<UserAuthentication>(entity =>
        {
            entity.HasKey(e => e.EmpId);

            entity.ToTable("USER_AUTHENTICATION");

            entity.Property(e => e.EmpId)
                .ValueGeneratedNever()
                .HasColumnName("EMP_ID");
            entity.Property(e => e.LoginPassword)
                .HasMaxLength(500)
                .HasColumnName("LOGIN_PASSWORD");
        });

        modelBuilder.Entity<ViewConstruction>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_CONSTRUCTION");

            entity.Property(e => e.備考).HasMaxLength(1024);
            entity.Property(e => e.建設会社名).HasMaxLength(50);
            entity.Property(e => e.引合日).HasColumnType("datetime");
            entity.Property(e => e.得意先).HasMaxLength(40);
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(6);
            entity.Property(e => e.検索キー).HasMaxLength(20);
            entity.Property(e => e.物件コード)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.物件名)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.現場住所1).HasMaxLength(30);
            entity.Property(e => e.現場住所2).HasMaxLength(30);
            entity.Property(e => e.現場住所3).HasMaxLength(30);
            entity.Property(e => e.現場郵便番号).HasMaxLength(8);
            entity.Property(e => e.登録者).HasMaxLength(20);
        });

        modelBuilder.Entity<ViewConstructionDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_CONSTRUCTION_DETAIL");

            entity.Property(e => e.Bs).HasColumnName("BS");
            entity.Property(e => e.キーマンcd)
                .HasMaxLength(2)
                .HasColumnName("キーマンCD");
            entity.Property(e => e.コメント).HasMaxLength(1024);
            entity.Property(e => e.チームcd)
                .HasMaxLength(3)
                .HasColumnName("チームCD");
            entity.Property(e => e.ビル名等).HasMaxLength(50);
            entity.Property(e => e.受注対応完了日).HasColumnType("datetime");
            entity.Property(e => e.受注確度).HasMaxLength(20);
            entity.Property(e => e.建設会社fax)
                .HasMaxLength(15)
                .HasColumnName("建設会社FAX");
            entity.Property(e => e.建設会社tel)
                .HasMaxLength(15)
                .HasColumnName("建設会社TEL");
            entity.Property(e => e.建設会社代表者名).HasMaxLength(50);
            entity.Property(e => e.建設会社名).HasMaxLength(50);
            entity.Property(e => e.引合日).HasColumnType("datetime");
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(6);
            entity.Property(e => e.得意先住所)
                .IsRequired()
                .HasMaxLength(120);
            entity.Property(e => e.得意先名).HasMaxLength(40);
            entity.Property(e => e.担当社員id).HasColumnName("担当社員ID");
            entity.Property(e => e.検索キー).HasMaxLength(20);
            entity.Property(e => e.注文書受領日).HasColumnType("datetime");
            entity.Property(e => e.注文請書受領日).HasColumnType("datetime");
            entity.Property(e => e.注文請書送付日).HasColumnType("datetime");
            entity.Property(e => e.物件コード)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.物件住所).HasMaxLength(50);
            entity.Property(e => e.物件備考).HasMaxLength(1024);
            entity.Property(e => e.物件名)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.物件名フリガナ).HasMaxLength(50);
            entity.Property(e => e.現場fax)
                .HasMaxLength(15)
                .HasColumnName("現場FAX");
            entity.Property(e => e.現場tel)
                .HasMaxLength(15)
                .HasColumnName("現場TEL");
            entity.Property(e => e.現場住所1).HasMaxLength(30);
            entity.Property(e => e.現場住所2).HasMaxLength(30);
            entity.Property(e => e.現場住所3).HasMaxLength(30);
            entity.Property(e => e.現場郵便番号).HasMaxLength(8);
            entity.Property(e => e.登録者社員id).HasColumnName("登録者社員ID");
            entity.Property(e => e.見積送付日).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewCorrectionDelivery>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_CORRECTION_DELIVERY");

            entity.Property(e => e.売上金額).HasColumnType("decimal(22, 2)");
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.得意先名).HasMaxLength(40);
            entity.Property(e => e.訂正日).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewCorrectionDeliveryDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_CORRECTION_DELIVERY_DETAIL");

            entity.Property(e => e.元売上単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.元売上番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.得意先名).HasMaxLength(40);
            entity.Property(e => e.訂正単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.訂正日).HasColumnType("datetime");
            entity.Property(e => e.訂正申請者id).HasColumnName("訂正申請者ID");
            entity.Property(e => e.訂正申請者名).HasMaxLength(20);
            entity.Property(e => e.訂正番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.訂正種別)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.部門コード)
                .IsRequired()
                .HasMaxLength(6);
            entity.Property(e => e.部門名).HasMaxLength(40);
        });

        modelBuilder.Entity<ViewFixedSale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_FIXED_SALES");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.営業担当者名).HasMaxLength(20);
            entity.Property(e => e.得意先).HasMaxLength(60);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.納期).HasColumnType("date");
        });

        modelBuilder.Entity<ViewInterestRateBeforeFix>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INTEREST_RATE_BEFORE_FIX");

            entity.Property(e => e.DenNoLine)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnName("DEN_NO_LINE");
            entity.Property(e => e.DenSort)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.SaveKey)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.コメント).HasMaxLength(1000);
            entity.Property(e => e.コメント役職).HasMaxLength(20);
            entity.Property(e => e.コメント者).HasMaxLength(20);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.仕入単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入掛率).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.仕入記号).HasMaxLength(1);
            entity.Property(e => e.仕入額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.伝票区分).HasMaxLength(2);
            entity.Property(e => e.伝票区分名).HasMaxLength(50);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.利率).HasColumnType("decimal(25, 14)");
            entity.Property(e => e.商品コード).HasMaxLength(24);
            entity.Property(e => e.商品名).HasMaxLength(30);
            entity.Property(e => e.営業担当者名).HasMaxLength(20);
            entity.Property(e => e.売上単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売上掛率).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.売上記号).HasMaxLength(1);
            entity.Property(e => e.売上額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.定価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.得意先).HasMaxLength(60);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewInterestRateFixed>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INTEREST_RATE_FIXED");

            entity.Property(e => e.コメント).HasMaxLength(1000);
            entity.Property(e => e.コメント役職).HasMaxLength(20);
            entity.Property(e => e.コメント者).HasMaxLength(20);
            entity.Property(e => e.仕入単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入額).HasColumnType("decimal(17, 2)");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.利率).HasColumnType("decimal(26, 14)");
            entity.Property(e => e.取引先コード).HasMaxLength(8);
            entity.Property(e => e.取引先名).HasMaxLength(40);
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.売上番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.摘要).HasMaxLength(30);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.販売単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.販売額).HasColumnType("decimal(22, 2)");
        });

        modelBuilder.Entity<ViewInternalDelivery>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INTERNAL_DELIVERY");

            entity.Property(e => e.仕入単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入額).HasColumnType("decimal(17, 2)");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.利率).HasColumnType("decimal(25, 14)");
            entity.Property(e => e.取引先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.取引先名).HasMaxLength(40);
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.販売単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.販売額).HasColumnType("decimal(22, 2)");
        });

        modelBuilder.Entity<ViewInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INVOICE");

            entity.Property(e => e.備考).HasMaxLength(1000);
            entity.Property(e => e.売上日).HasColumnType("datetime");
            entity.Property(e => e.売上金額合計).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.得意先名)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.担当).HasMaxLength(20);
            entity.Property(e => e.消費税合計).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewInvoiceBatch>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INVOICE_BATCH");

            entity.Property(e => e.前回入金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.前回請求残高).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売上単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売上日).HasColumnType("datetime");
            entity.Property(e => e.売上番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.請求先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.顧客コード)
                .IsRequired()
                .HasMaxLength(12);
        });

        modelBuilder.Entity<ViewInvoiceDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INVOICE_DETAIL");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.前回入金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.前回請求残高).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(50);
            entity.Property(e => e.当月売上額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.当月請求額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.得意先名).HasMaxLength(40);
            entity.Property(e => e.消費税金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.請求日).HasColumnType("datetime");
            entity.Property(e => e.請求番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.販売単価).HasColumnType("decimal(11, 2)");
        });

        modelBuilder.Entity<ViewInvoicedAmount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_INVOICED_AMOUNT");

            entity.Property(e => e.当月入金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.得意先コード).HasMaxLength(8);
            entity.Property(e => e.得意先名).HasMaxLength(40);
            entity.Property(e => e.担当).HasMaxLength(20);
            entity.Property(e => e.消費税金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.請求日).HasColumnType("datetime");
            entity.Property(e => e.請求番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.請求額).HasColumnType("decimal(11, 2)");
        });

        modelBuilder.Entity<ViewJyuchuSale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_JYUCHU_SALE");

            entity.Property(e => e.取引先コード).HasMaxLength(8);
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.商品コード).HasMaxLength(24);
            entity.Property(e => e.商品名).HasMaxLength(30);
            entity.Property(e => e.社員コード).HasMaxLength(4);
            entity.Property(e => e.請求日).HasColumnType("datetime");
            entity.Property(e => e.販売単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.部門コード).HasMaxLength(6);
            entity.Property(e => e.部門開始日).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewOrder>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_ORDERS");

            entity.Property(e => e.DenSort)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.SaveKey)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.伝票区分).HasMaxLength(53);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入力者).HasMaxLength(20);
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.受発注者).HasMaxLength(20);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.得意先名).HasMaxLength(60);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.発注状態)
                .IsRequired()
                .HasMaxLength(14)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewProductStock>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_PRODUCT_STOCK");

            entity.Property(e => e.データ種別)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.入出庫日時).HasColumnType("datetime");
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品分類コード).HasMaxLength(10);
            entity.Property(e => e.商品分類名)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.商品名).HasMaxLength(50);
            entity.Property(e => e.商品名フル).HasMaxLength(100);
        });

        modelBuilder.Entity<ViewPurchaseBilling>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_PURCHASE_BILLING");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.ステータス)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.仕入支払年月日).HasColumnType("datetime");
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入力日).HasColumnType("datetime");
            entity.Property(e => e.入力者).HasMaxLength(20);
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.営業担当).HasMaxLength(20);
            entity.Property(e => e.得意先名).HasMaxLength(60);
            entity.Property(e => e.得意先営業担当).HasMaxLength(20);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.発注金額合計).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.納期).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPurchaseBillingDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_PURCHASE_BILLING_DETAIL");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.Hページ番号)
                .IsRequired()
                .HasMaxLength(3);
            entity.Property(e => e.H単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.H子番)
                .IsRequired()
                .HasMaxLength(4);
            entity.Property(e => e.H注番).HasMaxLength(15);
            entity.Property(e => e.H納日).HasColumnType("date");
            entity.Property(e => e.H行番号)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.H金額).HasColumnType("decimal(22, 2)");
            entity.Property(e => e.M伝票番号).HasMaxLength(20);
            entity.Property(e => e.M単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.M注番).HasMaxLength(30);
            entity.Property(e => e.M納日).HasColumnType("datetime");
            entity.Property(e => e.M金額).HasColumnType("decimal(17, 2)");
            entity.Property(e => e.仕入先).HasMaxLength(60);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入担当者id).HasColumnName("仕入担当者ID");
            entity.Property(e => e.仕入支払年月日).HasColumnType("datetime");
            entity.Property(e => e.仕入番号).HasMaxLength(30);
            entity.Property(e => e.伝区).HasMaxLength(2);
            entity.Property(e => e.伝票区分).HasMaxLength(2);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.得意先).HasMaxLength(60);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.部門コード).HasMaxLength(6);
        });

        modelBuilder.Entity<ViewPurchaseReceiving>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_PURCHASE_RECEIVING");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.ステータス)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入力日).HasColumnType("datetime");
            entity.Property(e => e.入力者).HasMaxLength(20);
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.営業担当).HasMaxLength(20);
            entity.Property(e => e.得意先名).HasMaxLength(60);
            entity.Property(e => e.得意先営業担当).HasMaxLength(20);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.発注金額合計).HasColumnType("decimal(38, 2)");
            entity.Property(e => e.納期).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPurchaseReceivingDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_PURCHASE_RECEIVING_DETAIL");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.Hページ番号)
                .IsRequired()
                .HasMaxLength(3);
            entity.Property(e => e.H単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.H注番).HasMaxLength(15);
            entity.Property(e => e.H納日).HasColumnType("date");
            entity.Property(e => e.H行番号)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.H金額).HasColumnType("decimal(22, 2)");
            entity.Property(e => e.M伝票番号).HasMaxLength(20);
            entity.Property(e => e.M単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.M注番).HasMaxLength(30);
            entity.Property(e => e.M納日).HasColumnType("datetime");
            entity.Property(e => e.M金額).HasColumnType("decimal(17, 2)");
            entity.Property(e => e.仕入先).HasMaxLength(60);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入番号).HasMaxLength(30);
            entity.Property(e => e.伝区).HasMaxLength(2);
            entity.Property(e => e.伝票区分).HasMaxLength(2);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.得意先).HasMaxLength(60);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewPurchaseSalesCorrection>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_PURCHASE_SALES_CORRECTION");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.H単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.H注番).HasMaxLength(15);
            entity.Property(e => e.H納日).HasColumnType("datetime");
            entity.Property(e => e.H金額).HasColumnType("decimal(22, 2)");
            entity.Property(e => e.M単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.M金額).HasColumnType("decimal(17, 2)");
            entity.Property(e => e.仕入先)
                .IsRequired()
                .HasMaxLength(40);
            entity.Property(e => e.仕入先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.仕入番号)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.伝票区分).HasMaxLength(2);
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.商品コード)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.商品名)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.売上番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.変更後h単価)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("変更後H単価");
            entity.Property(e => e.変更後h数量).HasColumnName("変更後H数量");
            entity.Property(e => e.変更後h金額).HasColumnName("変更後H金額");
            entity.Property(e => e.変更後m単価)
                .HasColumnType("decimal(11, 2)")
                .HasColumnName("変更後M単価");
            entity.Property(e => e.変更後m数量).HasColumnName("変更後M数量");
            entity.Property(e => e.変更後m金額).HasColumnName("変更後M金額");
            entity.Property(e => e.変更後仕入先).HasMaxLength(8);
            entity.Property(e => e.変更後得意先).HasMaxLength(8);
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(8);
            entity.Property(e => e.承認対象id)
                .HasMaxLength(20)
                .HasColumnName("承認対象ID");
            entity.Property(e => e.承認要求番号).HasMaxLength(20);
        });

        modelBuilder.Entity<ViewReadySale>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_READY_SALES");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.仕入単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入掛率).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.仕入記号).HasMaxLength(1);
            entity.Property(e => e.仕入額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.伝票区分).HasMaxLength(2);
            entity.Property(e => e.伝票区分名).HasMaxLength(50);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入荷日).HasColumnType("datetime");
            entity.Property(e => e.利率).HasColumnType("decimal(26, 14)");
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.営業担当者名).HasMaxLength(20);
            entity.Property(e => e.売上単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売上掛率).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.売上記号).HasMaxLength(1);
            entity.Property(e => e.売上額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.定価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.得意先).HasMaxLength(60);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.発注先種別名)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.納期).HasColumnType("date");
        });

        modelBuilder.Entity<ViewReadySalesBatch>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_READY_SALES_BATCH");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.H注番).HasMaxLength(15);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(60);
            entity.Property(e => e.仕入単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入掛率).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.仕入記号).HasMaxLength(1);
            entity.Property(e => e.仕入額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.伝票区分).HasMaxLength(2);
            entity.Property(e => e.伝票区分名).HasMaxLength(50);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入荷日).HasColumnType("datetime");
            entity.Property(e => e.利率).HasColumnType("decimal(25, 14)");
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.営業担当者名).HasMaxLength(20);
            entity.Property(e => e.売上単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売上掛率).HasColumnType("decimal(9, 2)");
            entity.Property(e => e.売上記号).HasMaxLength(1);
            entity.Property(e => e.売上額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.定価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.得意先).HasMaxLength(60);
            entity.Property(e => e.得意先コード).HasMaxLength(6);
            entity.Property(e => e.明細DenNoLine)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnName("明細_DEN_NO_LINE");
            entity.Property(e => e.明細DenSort)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("明細_DEN_SORT");
            entity.Property(e => e.明細SaveKey)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("明細_SAVE_KEY");
            entity.Property(e => e.物件コード).HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.発注先種別名)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.社員id).HasColumnName("社員ID");
            entity.Property(e => e.納期).HasColumnType("datetime");
            entity.Property(e => e.部門コード).HasMaxLength(6);
            entity.Property(e => e.部門開始日).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewReturnReceipt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_RETURN_RECEIPT");

            entity.Property(e => e.Hat注番)
                .HasMaxLength(10)
                .HasColumnName("HAT注番");
            entity.Property(e => e.H納品単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入先名).HasMaxLength(40);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.商品コード).HasMaxLength(24);
            entity.Property(e => e.商品名).HasMaxLength(30);
            entity.Property(e => e.発送元)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewSalesAdjustment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_ADJUSTMENT");

            entity.Property(e => e.勘定科目).HasMaxLength(30);
            entity.Property(e => e.売上調整番号)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.得意先コード)
                .IsRequired()
                .HasMaxLength(6);
            entity.Property(e => e.得意先名).HasMaxLength(40);
            entity.Property(e => e.承認要求番号).HasMaxLength(20);
            entity.Property(e => e.摘要).HasMaxLength(100);
            entity.Property(e => e.消費税).HasMaxLength(1);
            entity.Property(e => e.物件コード)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.物件名).HasMaxLength(50);
            entity.Property(e => e.調整金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.請求日).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewSalesCorrection>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_CORRECTION");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.ステータス)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.仕入先コード).HasMaxLength(6);
            entity.Property(e => e.仕入先名).HasMaxLength(40);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.受注番号).HasMaxLength(12);
            entity.Property(e => e.営業担当者名).HasMaxLength(20);
        });

        modelBuilder.Entity<ViewSalesCorrectionDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_CORRECTION_DETAIL");

            entity.Property(e => e.H納品金額).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.仕入先伝票)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.商品コード).HasMaxLength(24);
            entity.Property(e => e.商品名).HasMaxLength(30);
        });

        modelBuilder.Entity<ViewSalesRefundDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_REFUND_DETAIL");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.在庫単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売区).HasMaxLength(2);
            entity.Property(e => e.売区名)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.返品id)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("返品ID");
            entity.Property(e => e.返金単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.返金承認要求番号).HasMaxLength(20);
        });

        modelBuilder.Entity<ViewSalesReturn>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_RETURN");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.承認ステータス).HasMaxLength(9);
            entity.Property(e => e.承認要求番号).HasMaxLength(20);
        });

        modelBuilder.Entity<ViewSalesReturnDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_RETURN_DETAIL");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.在庫単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売上番号)
                .IsRequired()
                .HasMaxLength(10);
            entity.Property(e => e.売区).HasMaxLength(2);
            entity.Property(e => e.売区名)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.承認要求番号).HasMaxLength(20);
            entity.Property(e => e.返品id)
                .HasMaxLength(10)
                .HasColumnName("返品ID");
        });

        modelBuilder.Entity<ViewSalesReturnReceipt>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_RETURN_RECEIPT");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.入庫承認要求番号).HasMaxLength(20);
        });

        modelBuilder.Entity<ViewSalesReturnReceiptDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_SALES_RETURN_RECEIPT_DETAIL");

            entity.Property(e => e.Hat注文番号)
                .HasMaxLength(10)
                .HasColumnName("HAT注文番号");
            entity.Property(e => e.伝票番号).HasMaxLength(20);
            entity.Property(e => e.入庫承認要求番号).HasMaxLength(20);
            entity.Property(e => e.単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.商品コード).HasMaxLength(50);
            entity.Property(e => e.商品名).HasMaxLength(100);
            entity.Property(e => e.在庫単価).HasColumnType("decimal(11, 2)");
            entity.Property(e => e.売区).HasMaxLength(2);
            entity.Property(e => e.売区名)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.返品id)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("返品ID");
        });

        modelBuilder.Entity<ViewStockInventory>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_STOCK_INVENTORY");

            entity.Property(e => e.ランク).HasMaxLength(1);
            entity.Property(e => e.倉庫cd)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("倉庫CD");
            entity.Property(e => e.倉庫名).HasMaxLength(20);
            entity.Property(e => e.商品cd)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("商品CD");
            entity.Property(e => e.在庫区分)
                .IsRequired()
                .HasMaxLength(1);
            entity.Property(e => e.在庫置場cd)
                .HasMaxLength(10)
                .HasColumnName("在庫置場CD");
            entity.Property(e => e.在庫置場名).HasMaxLength(50);
            entity.Property(e => e.棚卸no).HasColumnName("棚卸NO");
            entity.Property(e => e.棚卸年月).HasColumnType("datetime");
            entity.Property(e => e.状況).HasMaxLength(1);
            entity.Property(e => e.良品区分)
                .IsRequired()
                .HasMaxLength(1);
        });

        modelBuilder.Entity<ViewStockRefill>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_STOCK_REFILL");

            entity.Property(e => e.ランク).HasMaxLength(1);
            entity.Property(e => e.仕入先コード).HasMaxLength(8);
            entity.Property(e => e.仕入先名).HasMaxLength(40);
            entity.Property(e => e.倉庫コード)
                .IsRequired()
                .HasMaxLength(3);
            entity.Property(e => e.倉庫名).HasMaxLength(20);
            entity.Property(e => e.商品コード)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.商品分類コード).HasMaxLength(8);
            entity.Property(e => e.商品分類名).HasMaxLength(30);
            entity.Property(e => e.発注日時).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewWarehousingReceiving>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_WAREHOUSING_RECEIVING");

            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入荷予定).HasColumnType("date");
            entity.Property(e => e.入荷日).HasColumnType("datetime");
            entity.Property(e => e.出荷日).HasColumnType("datetime");
            entity.Property(e => e.到着予定日).HasColumnType("datetime");
            entity.Property(e => e.取引先).HasMaxLength(20);
            entity.Property(e => e.納期).HasColumnType("date");
            entity.Property(e => e.配送パターン)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.配送先).HasMaxLength(50);
            entity.Property(e => e.配送先住所).HasMaxLength(90);
        });

        modelBuilder.Entity<ViewWarehousingReceivingDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_WAREHOUSING_RECEIVING_DETAIL");

            entity.Property(e => e.Hat商品コード)
                .HasMaxLength(24)
                .HasColumnName("HAT商品コード");
            entity.Property(e => e.H注番).HasMaxLength(8);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.入出庫日時).HasColumnType("datetime");
            entity.Property(e => e.商品名).HasMaxLength(30);
        });

        modelBuilder.Entity<ViewWarehousingShipping>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_WAREHOUSING_SHIPPING");

            entity.Property(e => e.DenSort)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.SaveKey)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.伝区).HasMaxLength(2);
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.倉庫ステータス).HasMaxLength(1);
            entity.Property(e => e.入荷予定).HasColumnType("date");
            entity.Property(e => e.入荷日).HasColumnType("datetime");
            entity.Property(e => e.出荷指示書印刷)
                .IsRequired()
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.出荷日).HasColumnType("datetime");
            entity.Property(e => e.到着予定日).HasColumnType("datetime");
            entity.Property(e => e.取引先).HasMaxLength(20);
            entity.Property(e => e.納期).HasColumnType("date");
            entity.Property(e => e.配送パターン)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false);
            entity.Property(e => e.配送先).HasMaxLength(50);
            entity.Property(e => e.配送先住所).HasMaxLength(90);
        });

        modelBuilder.Entity<ViewWarehousingShippingDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VIEW_WAREHOUSING_SHIPPING_DETAIL");

            entity.Property(e => e.DenNoLine)
                .IsRequired()
                .HasMaxLength(1)
                .HasColumnName("DEN_NO_LINE");
            entity.Property(e => e.DenSort)
                .IsRequired()
                .HasMaxLength(3)
                .HasColumnName("DEN_SORT");
            entity.Property(e => e.Hat商品コード)
                .HasMaxLength(24)
                .HasColumnName("HAT商品コード");
            entity.Property(e => e.SaveKey)
                .IsRequired()
                .HasMaxLength(24)
                .HasColumnName("SAVE_KEY");
            entity.Property(e => e.伝票番号).HasMaxLength(6);
            entity.Property(e => e.商品名).HasMaxLength(30);
        });

        modelBuilder.Entity<Warehousing>(entity =>
        {
            entity.HasKey(e => e.WarehousingId).HasName("WAREHOUSING_PKC");

            entity.ToTable("WAREHOUSING", tb => tb.HasComment("入出庫★"));

            entity.Property(e => e.WarehousingId)
                .HasComment("入出庫ID,自動採番")
                .HasColumnName("WAREHOUSING_ID");
            entity.Property(e => e.Chuban)
                .HasMaxLength(15)
                .HasComment("受注詳細のCHUBAN,(H注番+連番 マルマ入庫入力用)")
                .HasColumnName("CHUBAN");
            entity.Property(e => e.CreateDate)
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.DenNo)
                .HasMaxLength(6)
                .HasComment("伝票番号")
                .HasColumnName("DEN_NO");
            entity.Property(e => e.ProdCode)
                .HasMaxLength(50)
                .HasComment("商品コード")
                .HasColumnName("PROD_CODE");
            entity.Property(e => e.QualityType)
                .HasMaxLength(1)
                .HasComment("良品区分,G:良品 F:不良品 U:未検品")
                .HasColumnName("QUALITY_TYPE");
            entity.Property(e => e.Quantity)
                .HasComment("数量")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.QuantityBara)
                .HasComment("数量 (バラ)")
                .HasColumnName("QUANTITY_BARA");
            entity.Property(e => e.StockType)
                .HasMaxLength(1)
                .HasComment("在庫区分,1:自社在庫 2:預り在庫 (マルマ)")
                .HasColumnName("STOCK_TYPE");
            entity.Property(e => e.UpdateDate)
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WarehousingCause)
                .HasComment("入出庫理由,0:通常 1:返品 2:検品による数量調整 3:棚卸し数量調整")
                .HasColumnName("WAREHOUSING_CAUSE");
            entity.Property(e => e.WarehousingDatetime)
                .HasComment("入出庫日時")
                .HasColumnType("datetime")
                .HasColumnName("WAREHOUSING_DATETIME");
            entity.Property(e => e.WarehousingDiv)
                .HasMaxLength(1)
                .HasComment("入出庫区分,I:入庫 O:出庫")
                .HasColumnName("WAREHOUSING_DIV");
            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
        });

        modelBuilder.Entity<WhMst>(entity =>
        {
            entity.HasKey(e => e.WhCode);

            entity.ToTable("WH_MST", tb => tb.HasComment("倉庫マスタ"));

            entity.Property(e => e.WhCode)
                .HasMaxLength(3)
                .HasComment("倉庫コード")
                .HasColumnName("WH_CODE");
            entity.Property(e => e.Address1)
                .HasMaxLength(40)
                .HasComment("住所１")
                .HasColumnName("ADDRESS1");
            entity.Property(e => e.Address2)
                .HasMaxLength(40)
                .HasComment("住所２")
                .HasColumnName("ADDRESS2");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("作成日時")
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Creator)
                .HasComment("作成者名")
                .HasColumnName("CREATOR");
            entity.Property(e => e.State)
                .HasMaxLength(4)
                .HasComment("都道府県")
                .HasColumnName("STATE");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("更新日時")
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.Updater)
                .HasComment("更新者名")
                .HasColumnName("UPDATER");
            entity.Property(e => e.WhName)
                .HasMaxLength(20)
                .HasComment("倉庫名")
                .HasColumnName("WH_NAME");
            entity.Property(e => e.WhType)
                .HasMaxLength(1)
                .HasDefaultValueSql("('N')")
                .HasComment("倉庫区分,N:通常倉庫 C:得意先 S:仕入先 D:部門倉庫 P:製品倉庫 M:原材料倉庫")
                .HasColumnName("WH_TYPE");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(8)
                .IsFixedLength()
                .HasComment("郵便番号")
                .HasColumnName("ZIP_CODE");
        });
        modelBuilder.HasSequence<int>("FOS_JYUCHU_H_DEN_NO");
        modelBuilder.HasSequence<int>("FOS_JYUCHU_H_DSEQ");
        modelBuilder.HasSequence<int>("FOS_JYUCHU_H_HAT_ORDER_NO");
        modelBuilder.HasSequence<int>("FOS_JYUCHU_H_ORDER_NO");
        modelBuilder.HasSequence<int>("INVOICE_INVOICE_NO");
        modelBuilder.HasSequence<int>("PU_IMPORT_PU_IMPORT_NO");
        modelBuilder.HasSequence<int>("PU_PU_NO");
        modelBuilder.HasSequence<int>("RETURNING_PRODUCTS_RETURNING_PRODUCTS_ID");
        modelBuilder.HasSequence<int>("SALES_ADJUSTMENT_SALES_ADJUSTMENT_NO");
        modelBuilder.HasSequence<int>("SALES_SALES_NO");
        modelBuilder.HasSequence<int>("STOCK_REFILL_ORDER_STOCK_REFILL_ORDER_NO");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
