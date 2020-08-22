﻿using Api.ViewModels.ViewModel;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.Interfaces
{
    public interface IPaymentService
    {
        public Task<Session> StatrPayment(OrderItemsVm itemsVm, string username);
        public Task Success(string username);
        public Task Cancel(string username);
    }
}
