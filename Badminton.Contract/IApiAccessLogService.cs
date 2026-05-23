using System.Threading.Tasks;

namespace Badminton.Contract
{
    public interface IApiAccessLogService
    {
        public Task<int?> SaveHttpRequest();
        public Task SaveHttpResponse(int id, string responseBody);
    }
}
