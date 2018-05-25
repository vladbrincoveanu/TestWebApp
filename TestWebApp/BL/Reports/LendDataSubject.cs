using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public class LendDataSubject : ISubscriber
    {
        private IPublisher _publisherLendData;

        private List<LendViewModel> LendViewModels { get; set; }

        public LendDataSubject(IPublisher publisher)
        {
            LendViewModels = new List<LendViewModel>();
            _publisherLendData = publisher;
            _publisherLendData.RegisterSubscriber(this);
        }

        public void Update(LendViewModel data)
        {
            LendViewModels.Add(data);
        }

        public List<LendViewModel> GetList()
        {
            return LendViewModels;
        }
    }
}
