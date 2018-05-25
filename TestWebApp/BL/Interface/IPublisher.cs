using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public interface IPublisher
    {
        void RegisterSubscriber(ISubscriber subscriber);
        void RemoveSubscriber(ISubscriber subscriber);
        void NotifySubscribers();
        void AddData(LendViewModel lend);
    }
}
