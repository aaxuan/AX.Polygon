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
                //������
                options.Filters.Add<GlobalExceptionFilter>();
                //options.ModelMetadataDetailsProviders.Add(new ModelBindingMetadataProvider());
            });

            //���л�
            mvcBuilder.AddNewtonsoftJson(options =>
            {
                //����ʱ�����л���ʽ
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                //����ѭ��Ӧ��
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //���ص������Ƿ�ʹ���շ塡CamelCasePropertyNamesContractResolver����Сд
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //����ֶ�Ϊnull�����ֶλ���Ȼ���ص�Json���С��磺��name����null
                options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
            });
            //���ݿ�����
            services.AddScoped<IDataRepository>(provider => { return new DataRepository.DataRepository(new MySql.Data.MySqlClient.MySqlConnection("server=localhost;database=test;user=root;password=root;port=3306;pooling=true;charset=utf8mb4;")); });
            //��������
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));
            //Encoding.RegisterProvider(CodePa gesEncodingProvider.Instance);
            //�ڴ滺��
            services.AddMemoryCache();

            //services.AddOptions();
            //services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + "DataProtection"));

            //ioc���İ�
            services.AddHttpContextAccessor();
            services.BindServiceCollection();
            services.InitAXPolygon();

            //����ʱӦ����ͼ����
            services.AddRazorPages().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!string.IsNullOrEmpty(GlobalContext.SystemConfig.VirtualDirectory))
            //{
            //    app.UsePathBase(new PathString(GlobalContext.SystemConfig.VirtualDirectory)); // �� Pathbase �м����Ϊ��һ������������м���� ������ȷ��ģ������·��
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

            //UseAuthentication���������Ǽ�顰����˭����
            app.UseAuthentication();
            //UseAuthorization�������ڼ�顰�Ƿ�������������Ϣ����
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

            //ioc���İ�
            app.BindServiceProvider();
        }
    }
}