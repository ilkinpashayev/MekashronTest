using MekashronService;
using MekashronTest.Models;

namespace MekashronTest.Services
{
    public interface IMekashronClient
    {
        public EntityModel Login(string username, string password);
        public MekashronRequestResult Register(MekashronService.RegisterNewCustomerRequest newCustomer);
        public GetCustomerInfoResponse GetCustomerData(GetCustomerInfoRequest customer);
    }
}
