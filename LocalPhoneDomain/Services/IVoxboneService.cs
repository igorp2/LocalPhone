using LocalPhoneDomain.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace LocalPhoneDomain.Services
{
    public interface IVoxboneService
    {
        Task<SmsRelatedInformationModel> MakeRequestToVoxboneApiAsync
            (StringContent requestContent, string extraUrl);
    }
}
