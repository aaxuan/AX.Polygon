using AX.Polygon.Admin.DataModel;
using System.Threading.Tasks;

namespace AX.Polygon.Admin.Services
{
    public class SystemLoginLogService : BaseService<SystemLoginLog>
    {
        public async Task DelletAll()
        {
            await DB.DeleteAllAsync<SystemLoginLog>();
        }
    }
}