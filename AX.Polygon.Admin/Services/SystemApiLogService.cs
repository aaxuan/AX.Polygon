using AX.Polygon.Admin.DataModel;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    public class SystemApiLogService : BaseService<SystemApiLog>
    {
        public async Task DelletAll()
        {
            await DB.DeleteAllAsync<SystemApiLog>();
        }
    }
}