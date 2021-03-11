using AX.DataRepository;
using AX.Polygon.Filter;
using AX.Polygon.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace AX.Polygon
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services.AddControllersWithViews(options =>
            {
                //过滤器
                options.Filters.Add<GlobalExceptionFilter>();
                //options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            });

            //序列化
            mvcBuilder.AddNewtonsoftJson(options =>
            {
                //设置时间序列化格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //忽略循环应用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //返回的数据是否使用驼峰　CamelCasePropertyNamesContractResolver　是小写
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //如果字段为null，该字段会依然返回到Json串中。如：“name”：null
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            });
            //数据库链接
            services.AddScoped<IDataRepository>(provider => { return new DataRepository.DataRepository(new MySql.Data.MySqlClient.MySqlConnection("server=localhost;database=test;user=root;password=root;port=3306;pooling=true;charset=utf8mb4;")); });
            //编码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //Encoding.RegisterProvider(CodePa gesEncodingProvider.Instance);
            //内存缓存
            services.AddMemoryCache();

            //services.AddOptions();
            //services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + "DataProtection"));

            //ioc中心绑定
            services.AddHttpContextAccessor();
            services.BindServiceCollection();
            services.InitAXPolygon();

            //运行时应用视图更改
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!string.IsNullOrEmpty(GlobalContext.SystemConfig.VirtualDirectory))
            //{
            //    app.UsePathBase(new PathString(GlobalContext.SystemConfig.VirtualDirectory)); // 让 Pathbase 中间件成为第一个处理请求的中间件， 才能正确的模拟虚拟路径
            //}
            if (env.IsDevelopment())
            { app.UseDeveloperExceptionPage(); }
            else
            { app.UseExceptionHandler("/Home/Error"); }

            //string resource = Path.Combine(env.ContentRootPath, "Resource");
            //FileHelper.CreateDirectory(resource);

            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    //RequestPath = "/Resource",
            //    //FileProvider = new PhysicalFileProvider(resource),
            //    //OnPrepareResponse = GlobalContext.SetCacheControl
            //});

            app.UseRouting();

            //UseAuthentication：帮助我们检查“您是谁？”
            app.UseAuthentication();
            //UseAuthorization：有助于检查“是否允许您访问信息？”
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

            //ioc中心绑定
            app.BindServiceProvider();
        }
    }
}