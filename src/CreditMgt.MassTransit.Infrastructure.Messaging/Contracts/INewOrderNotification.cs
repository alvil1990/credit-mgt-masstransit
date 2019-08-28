using System;
using System.Collections.Generic;
using System.Text;

namespace CreditMgt.MassTransit.Infrastructure.Messaging.Messages
{
  public  interface INewOrderNotification
    {
         Guid ID { get;}
    }
}
