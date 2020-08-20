using Api.ViewModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<Session> StatrPayment(IList<OrderVm> orderVms, string username);
        public Task Success(string username);
        public Task Cancel(string username);
    }
}
