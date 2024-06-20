using Microsoft.EntityFrameworkCore;
using NLog;
using HAT_F_api.Models;
using NLog.Web;
using System.Reflection;
using HAT_F_api.Services;
using HAT_F_api;
using Microsoft.Extensions.DependencyInjection;
using HAT_F_api.Services.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using HAT_F_api.Utils;
using HAT_F_api.CustomModels;


// Add services to the container.
var builder = WebApplication.CreateBuilder(args);
InitializeServices(builder.Services);

var app = builder.Build();
InitializeApplication(app);

app.Run();


void InitializeServices(IServiceCollection services)
{
    services.AddControllers();
    services.AddDbContext<HatFContext>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();


    //services.AddSwaggerGen();
    services.AddSwaggerGen(option =>
    {
        // Swaggerのページに接続先DB情報を表示する
        var config = new ConfigurationManager()
            .AddJsonFile("appsettings.json")
#if DEBUG
            .AddJsonFile("appsettings.development.json")
#endif
            .AddUserSecrets<Program>()
            .Build();

        var connectionString = config.GetSection("ConnectionStrings").GetValue<string>("DbConnectString");
        SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connectionString);

        option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = Assembly.GetExecutingAssembly().GetName().Name,
            Version = "1.0",
            Description = $"Connecting DataBase = {sb.InitialCatalog}",
        });
    });

    // services.AddSwaggerGen();
    services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    // SET UP NLOG
    var nlogLogger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();    // appsettings.jsonからNLog設定取得
    services.AddSingleton<NLog.ILogger, Logger>(p => nlogLogger);

    // ASP.NET のテレメトリー収集有効化
    services.AddApplicationInsightsTelemetry();

    // API 認証処理
    // HatFAuthenticationHandler クラスに認証処理の実装があります
    builder.Services.AddAuthentication("HatFApi").AddScheme<AuthenticationSchemeOptions, HatFAuthenticationHandler>("HatFApi", options => { });

    // 検索処理
    services.AddScoped<HatFSearchService>();

    // 更新処理
    services.AddScoped<HatFUpdateService>();

    // 認証処理
    services.AddScoped<HatFAuthenticationService>();

    // シーケンス番号サービス
    builder.Services.AddScoped<SequenceNumberService>();

    // 商品サジェスト
    builder.Services.AddSingleton<ProductSuggestion>();

    // 汎用マスター編集
    builder.Services.AddScoped<MasterEditorService>();

    // API実行コンテキスト
    builder.Services.AddScoped<HatFApiExecutionContext>(context => new HatFApiExecutionContext()
    {
        // アプリケーションでシステム時刻が必要なときは
        // HatFApiExecutionContext.ExecuteDateTimeJstを参照のこと
        // テスト等で任意の日時で動作可能になる
        ExecuteDateTimeUtc = DateTime.Now.ToUniversalTime(),
    });

    // 受注情報補完サービス
    services.AddScoped<CompleteOrderService>();

    // TODO 〇〇サービス
    services.AddScoped<ProcessService>();

    // BLOB SERVICE
    services.AddScoped<BlobService>();

    // 承認サービス
    services.AddScoped<ApprovalService>();

    //メールサービス
    services.AddScoped<MailService>();

    //物件ロックサービス
    services.AddScoped<ConstructionLockService>();

    //仕入金額照合ロックサービス
    services.AddScoped<AmountCheckLockService>();

    //売上額訂正ロックサービス
    services.AddScoped<SalesEditLockService>();

    // 仕入サービス
    services.AddScoped<PurchaseService>();

    // 在庫管理サービス
    services.AddScoped<StockService>();

    // HttpContextを参照するためのIHttpContextAccessorをインジェクション可能にする
    services.AddHttpContextAccessor();

    // エンティティ更新日時、更新者の上書き機能
    services.AddScoped<UpdateInfoSetter>();

    // ログインユーザー識別用
    services.AddScoped<HatFLoginResultAccesser>();

    // 与信額サービス
    services.AddScoped<CreditService>();
}

void InitializeApplication(WebApplication app)
{
    // マイグレーションを手動で適用する方法
    // https://qiita.com/CodeOne/items/6ca2a1070a1fa27fd3bf

#if false
    // Database Migration
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<HatFContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating or initializing the database.");
        }
    }
#endif

    // Configure the HTTP request pipeline.
    // !!!! SWAGGER SHOULD ONLY BE USED IN DEVELOPMENT OPENED JUST FOR INITIAL TESTING !!!!
    // if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();

    // }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}