using System;
using System.Collections.Generic;
using System.Text;

namespace CreditMgt.MassTransit.Infrastructure.Messaging.Messages
{
  public  class NewOrderNotification : INewOrderNotification
    {
        public Guid ID { get; set; }
    }
}
