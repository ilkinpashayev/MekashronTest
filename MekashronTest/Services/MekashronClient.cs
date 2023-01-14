using MekashronService;
using MekashronTest.Controllers;
using MekashronTest.Models;
using System.Text.Json;

namespace MekashronTest.Services
{
    public class MekashronClient : IMekashronClient
    {
        
        private readonly MekashronService.IICUTech _mekashronService;

        public MekashronClient(MekashronService.IICUTech mekashronService)
        {
            _mekashronService = mekashronService;
        }
        public EntityModel Login(string username, string password)
        {
            LoginResponse loginResponse= _mekashronService.Login(new LoginRequest() { Password= password,UserName=username});

            string response = loginResponse.@return;
            EntityModel? mekashronLoginResult =
            JsonSerializer.Deserialize<EntityModel>(response);

            if (mekashronLoginResult.Email == null)
            {
                MekashronRequestResult? mekashronResult =
                   JsonSerializer.Deserialize<MekashronRequestResult>(response);
                mekashronLoginResult.ResultMessage = mekashronResult.ResultMessage;

            }
            return mekashronLoginResult;
       
        }

        public MekashronRequestResult Register(RegisterNewCustomerRequest newCustomer)
        {
            RegisterNewCustomerResponse registerNewCustomerResponse = _mekashronService.RegisterNewCustomer(newCustomer);   

            string response = registerNewCustomerResponse.@return;
            MekashronRequestResult? mekashronRequestResult =
            JsonSerializer.Deserialize<MekashronRequestResult>(response);
            return mekashronRequestResult;
        }

        public GetCustomerInfoResponse GetCustomerData(GetCustomerInfoRequest customer)
        {
            return _mekashronService.GetCustomerInfo(customer);
        }
    }
}
