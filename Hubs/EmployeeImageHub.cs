using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class EmployeeImageHub : Hub
    {
        public async Task EmployeesFaceUpdate(string employeeNo)
        {
            await Clients.All.SendAsync("ReceiveStaffUpdated", employeeNo);
        }
    }
}