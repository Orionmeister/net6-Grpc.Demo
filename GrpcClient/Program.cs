using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;

//var channel = GrpcChannel.ForAddress("https://localhost:7241");
//var client = new Greeter.GreeterClient(channel);

//HelloRequest request = new HelloRequest { Name = "Can" };
//HelloReply reply = await client.SayHelloAsync(request);

//Console.WriteLine(reply.Message);

var channel = GrpcChannel.ForAddress("https://localhost:7241");
var client = new Customer.CustomerClient(channel);

CustomerLookupModel request = new CustomerLookupModel { UserId = 1};
CustomerModel response = await client.GetCustomerInfoAsync(request);

Console.WriteLine($"Customer is: {response.FirstName} {response.LastName}");

using (var call = client.GetNewCustomers(new NewCustomerRequest()))
{
    while(await call.ResponseStream.MoveNext())
    {
        var customer = call.ResponseStream.Current;

        Console.WriteLine($"Welcome {customer.FirstName} {customer.LastName}: {customer.EmailAddress}");
    }
}

Console.WriteLine("Done!!");
Console.ReadKey();
