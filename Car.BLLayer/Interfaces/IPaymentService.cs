using Car.DLL.Entities;
using PayStack.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.BLLayer.Interfaces
{
    public interface IPaymentService
    {
        Task<ApiResponse<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId);
    }
}
