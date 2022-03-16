using Grpc.Core;
using GrpcServer.Protos;

namespace GrpcServer.Services
{
    public class CustomersService : Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if(request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if(request.UserId == 2)
            {
                output.FirstName = "Jane";
                output.LastName = "Doe";
            }
            else
            {
                output.FirstName = "Greg";
                output.LastName = "Thomes";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Jamie",
                    LastName = "Smith",
                    EmailAddress = "jane@smith.com",
                    Age = 32,
                    IsActive = true,
                },
                new CustomerModel
                {
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "jane@doe.com",
                    Age = 27,
                    IsActive = true,
                },
                new CustomerModel
                {
                    FirstName = "Percy",
                    LastName = "Vere",
                    EmailAddress = "percy@vere.com",
                    Age = 46,
                    IsActive = false,
                }
            };

            foreach (CustomerModel customer in customers)
            {
                await responseStream.WriteAsync(customer);
            }
           
        }
    }
}
