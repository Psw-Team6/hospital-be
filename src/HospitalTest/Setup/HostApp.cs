using HospitalAPI;
using Microsoft.AspNetCore.Mvc.Testing;

namespace HospitalTest.Setup
{
    public class HostApp:WebApplicationFactory<Startup>
    {
    }
}