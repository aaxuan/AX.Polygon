using AX.DataRepository;
using AX.Polygon.Admin.DataModel;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    public class SystemLogService : BaseService<SystemLog>
    {
        public static SystemLogService Loger;

        static SystemLogService()
        { Loger = new SystemLogService(); }

        public static async Task<SystemLog> Log(string logintype, string logMessage)
        {
            var loginfo = new SystemLog();
            loginfo.Id = Util.IOCManager.GetService<Util.IDGenerator>().CreateID();
            loginfo.LogType = logintype;
            loginfo.LogMessage = logMessage;
            var db = Util.IOCManager.GetScopeService<IDataRepository>();
            await db.InsertAsync(loginfo);
            return loginfo;
        }

        public async Task DelletAll()
        {
            await DB.DeleteAllAsync<SystemLoginLog>();
        }
    }
}