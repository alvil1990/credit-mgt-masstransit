﻿

using CreditMgt.MassTransit.Infrastructure.Messaging;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CreditMgt.MassTransit.NotificationService
{
    public class NotificationHostedService : IHostedService
    {
        IBusControl _busControl;
        public NotificationHostedService(IBusControl busControl)
        {
            _busControl = busControl;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _busControl.StartAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _busControl.StopAsync();
            return Task.CompletedTask;
        }


    }
}
