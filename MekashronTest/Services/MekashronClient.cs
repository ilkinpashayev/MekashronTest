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
        public MekashronRequestResult Login(string username, string password)
        {
            LoginResponse loginResponse= _mekashronService.Login(new LoginRequest() { Password= password,UserName=username,IPs="127.0.0.0" });

            string response = loginResponse.@return;
            MekashronRequestResult? mekashronRequestResult =
            JsonSerializer.Deserialize<MekashronRequestResult>(response);
            if (mekashronRequestResult.ResultCode == -1)
            {
                return mekashronRequestResult;
            }
            else
            {
                GetCustomerInfoRequest getCustomerInfoRequest = new GetCustomerInfoRequest()
                {
                    Username = username,
                    Password = password
                };
                GetCustomerInfoResponse getCustomerInfoResponse = GetCustomerData(getCustomerInfoRequest);

                MekashronRequestResult? mekashronInfoRequestResult =
                    JsonSerializer.Deserialize<MekashronRequestResult>(getCustomerInfoResponse.@return);
                return mekashronInfoRequestResult;
            }
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
