using DotNetStandard.Vent;

namespace DotNetStandard.Tests.Models
{
    public class Producer
    {
        private EventAggregator _vent = EventAggregator.Instance;
        private string _ventName;

        public Producer(string ventName)
        {
            _ventName = ventName;
        }

        public void TriggerEvent()
        {
            _vent.Trigger(new EventTest(_ventName), "stringParam");
        }
    }
}