using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public class LendDataProvider : IPublisher
    {
        List<ISubscriber> Subscribers;

        LendViewModel lendViewModel;

        private static LendDataProvider instance = new LendDataProvider(); //singleton

        public LendDataProvider()
        {
            Subscribers = new List<ISubscriber>();
        
        }

        public void NotifySubscribers()
        {
            foreach (var sub in Subscribers)
            {
                sub.Update(lendViewModel);
            }
        }

        public void RegisterSubscriber(ISubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            Subscribers.Remove(subscriber);
        }

        public void AddData(LendViewModel model)
        {
            lendViewModel = model;
            NotifySubscribers();
        }

        public void RemoveData(LendViewModel model)
        {
            lendViewModel = model;
        }

        public static LendDataProvider getInstance()
        {
            return instance;
        }

    }
}
