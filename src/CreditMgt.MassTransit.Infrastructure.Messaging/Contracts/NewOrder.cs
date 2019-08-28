using System;
using System.Collections.Generic;
using System.Text;

namespace CreditMgt.MassTransit.Infrastructure.Messaging.Messages
{
  public class NewOrder :INewOrder
    {
        public Guid ID { get; set; }
        public string  Name { get; set; }

    }
}
