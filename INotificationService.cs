using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ExperiencePortal.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "INotificationService" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(INotificationServiceCallback))]
    public interface INotificationService
    {
        [OperationContract(IsOneWay = true)]
        void GetNotification(string authenticationToken);
    }

    public interface INotificationServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void SendResult(bool result);
    }
}
